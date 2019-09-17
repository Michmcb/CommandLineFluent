using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandLineFluent
{
	/// <summary>
	/// The starting point for parsing command line arguments (or other stuff, if suitable).
	/// Use the methods Configure, and either WithourVerbs or AddVerb to set up the FluentParser so it
	/// can parse stuff. I'd recommend keeping an instance of this if it's needed multiple times.
	/// </summary>
	public class FluentParser
	{
		private readonly Dictionary<string, IFluentVerb> _verbs;
		public IReadOnlyDictionary<string, IFluentVerb> Verbs => _verbs;
		/// <summary>
		/// If null, the FluentParser hasn't been configured either way.
		/// If true, the FluentParser has been set up to parse at least one verb.
		/// If false, the FluentParser has been set up to not parse any verbs, so it just parses arguments as-is.
		/// </summary>
		public bool? IsUsingVerbs { get; private set; }
		/// <summary>
		/// The current configuration. Use Configure to set this in a fluent way.
		/// </summary>
		public FluentParserConfig Config { get; private set; }
		/// <summary>
		/// Creates a new instance of FluentParser
		/// </summary>
		public FluentParser()
		{
			IsUsingVerbs = null;
			_verbs = new Dictionary<string, IFluentVerb>();
			Config = new FluentParserConfig();
		}
		/// <summary>
		/// Configures the parser's options
		/// </summary>
		public FluentParser Configure(Action<FluentParserConfig> config)
		{
			config?.Invoke(Config);
			return this;
		}
		/// <summary>
		/// Sets up how to parse without using verbs. Technically, this configures a default verb named "default", which
		/// is always used when parsing arguments. Arguments should not start with "default".
		/// </summary>
		/// <typeparam name="T">The type of the class which will be created when arguments are parsed successfully</typeparam>
		public FluentParser WithoutVerbs<T>(Action<FluentVerb<T>> verblessConfig) where T : class, new()
		{
			if (IsUsingVerbs != null)
			{
				throw new FluentParserException($@"The FluentParser has already been configured to use verbs, or has already had WithoutVerbs invoked on it.", ErrorCode.ProgrammerError);
			}
			IsUsingVerbs = false;
			// ssshhh don't tell anybody it's actually a verb in disguise
			FluentVerb<T> v = new FluentVerb<T>(Config, "default");
			verblessConfig?.Invoke(v);
			_verbs.Add("default", v);
			return this;
		}
		/// <summary>
		/// Adds a verb for this parser. The verb name dictates the text that has to be entered on the command line.
		/// e.g. "foo.exe add" invokes the verb with the name "add".
		/// </summary>
		/// <typeparam name="T">The type of the class which will be created when arguments for that verb are parsed successfully</typeparam>
		/// <param name="verbName">The name of the verb</param>
		public FluentParser AddVerb<T>(string verbName, Action<FluentVerb<T>> verbConfig) where T : class, new()
		{
			if (IsUsingVerbs == false)
			{
				throw new FluentParserException($@"The FluentParser has already been configured to not use verbs.", ErrorCode.ProgrammerError);
			}
			if (_verbs.ContainsKey(verbName))
			{
				throw new FluentParserException($@"That verb name has already been used, you may only use unique verb names.", ErrorCode.ProgrammerError);
			}
			IsUsingVerbs = true;
			FluentVerb<T> v = new FluentVerb<T>(Config, verbName);
			verbConfig?.Invoke(v);
			_verbs.Add(verbName, v);
			return this;
		}
		/// <summary>
		/// Returns errors if improperly configured, or an empty collection if all is well
		/// </summary>
		public ICollection<Error> Validate()
		{
			List<Error> errors = new List<Error>();
			if (_verbs.Values.Any())
			{
				foreach (IFluentVerb verb in _verbs.Values)
				{
					errors.AddRange(verb.Validate());
				}
			}
			else
			{
				errors.Add(new Error(ErrorCode.ProgrammerError, false, @"This parser hasn't been configured with wither AddVerb<T> or WithoutVerbs<T>"));
			}
			return errors;
		}
		/// <summary>
		/// Throws a FluentParserValidationException the Parser is improperly configured.
		/// Equivalent to throwing an Exception if Validate() does not return null or an empty collection.
		/// </summary>
		public void ThrowIfImproperlyConfigured()
		{
			ICollection<Error> errors = Validate();
			if ((errors?.Count ?? 0) > 0)
			{
				throw new FluentParserValidationException(@"FluentParser has not been configured correctly", errors);
			}
		}
		/// <summary>
		/// Parses the provided arguments. If AddVerb was used, the first argument is interpreted as the verb name, and the rest of them are parsed normally.
		/// If WithoutVerbs was used, all arguments are parsed normally.
		/// If an error is encountered, help/usage will be written automatically, if that has been configured. Usage and Description is written for all verbs if no
		/// verb was parsed, or if a specific verb was parsed, detailed help/usage is written for that verb.
		/// </summary>
		/// <param name="args">The arguments to parse</param>
		public FluentParserResult Parse(IEnumerable<string> args)
		{
			IFluentVerb verb = null;
			FluentParserResult result;
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (IsUsingVerbs == true)
			{
				// We need at least 1 element if we're using verbs
				if (args.Any())
				{
					foreach (IFluentVerb v in _verbs.Values)
					{
						v.Reset();
					}
					string verbText = args.FirstOrDefault();
					if (verbText != Config.ShortHelpSwitch && verbText != Config.LongHelpSwitch)
					{
						if (_verbs.TryGetValue(verbText, out verb))
						{
							result = new FluentParserResult(verb.Parse(args.Skip(1)), verb);
						}
						else
						{
							return new FluentParserResult(new Error[] { new Error(ErrorCode.InvalidVerb, true, $@"The verb {verbText} is not a recognized verb") }, null);
						}
					}
					else
					{
						result = new FluentParserResult(new Error[] { new Error(ErrorCode.HelpRequested, false) }, null);
					}
				}
				else
				{
					result = new FluentParserResult(new Error[] { new Error(ErrorCode.NoVerbFound, true, $@"A verb is required") }, null);
				}
			}
			else if (IsUsingVerbs == false)
			{
				IFluentVerb v = _verbs.First().Value;
				v.Reset();
				result = new FluentParserResult(v.Parse(args), v);
			}
			else
			{
				throw new FluentParserException($@"The FluentParser has not been configured with AddVerb or WithoutVerbs", ErrorCode.ProgrammerError);
			}

			if (!result.Success)
			{
				if (verb != null)
				{
					WriteUsageAndHelp(verb);
				}
				else
				{
					WriteOverallUsageAndHelp();
				}
			}
			return result;
		}
		/// <summary>
		/// Writes a summary of how you invoke the program and how you can obtain further help, by specifying a verb.
		/// Order of priority is: this.Config.UsageText, then this.Config.UsageTextCreator, then the default HelpFormatter.FormatOverallUsage.
		/// Same thing for Help Text.
		/// Does nothing if Config.WriteUsageAndHelp is null.
		/// </summary>
		public void WriteOverallUsageAndHelp()
		{
			if (Config.WriteUsageAndHelp == null)
			{
				return;
			}

			if (Config.UsageText == null)
			{
				if (Config.UsageTextCreator == null)
				{
					Config.WriteUsageAndHelp(HelpFormatter.FormatOverallUsage(Verbs.Values, Config.ExeceuteCommand));
				}
				else
				{
					Config.WriteUsageAndHelp(Config.UsageTextCreator.Invoke(this, Config.ExeceuteCommand));
				}
			}
			else
			{
				Config.WriteUsageAndHelp(Config.UsageText);
			}
			if (Config.HelpText == null)
			{
				if (Config.HelpTextCreator == null)
				{
					Config.WriteUsageAndHelp(HelpFormatter.FormatOverallHelp(Verbs.Values, Util.ShortAndLongName(Config.ShortHelpSwitch, Config.LongHelpSwitch), Config.MaxLineLength));
				}
				else
				{
					Config.WriteUsageAndHelp(Config.HelpTextCreator.Invoke(this));
				}
			}
			else
			{
				Config.WriteUsageAndHelp(Config.HelpText);
			}
		}
		/// <summary>
		/// Writes the Usage Text, and then the Help Text for the specified verb. Order of priority is:
		/// verb.UsageText, then verb.UsageTextCreator, then the default HelpFormatter.FormatVerbUsage.
		/// Same thing for Help Text.
		/// Does nothing if Config.WriteUsageAndHelp is null.
		/// </summary>
		/// <param name="verb">The verb to write detailed usage/help for</param>
		public void WriteUsageAndHelp(IFluentVerb verb)
		{
			if (Config.WriteUsageAndHelp == null)
			{
				return;
			}

			if (verb.UsageText == null)
			{
				if (verb.UsageTextCreator == null)
				{
					Config.WriteUsageAndHelp(HelpFormatter.FormatVerbUsage(verb, Config.ExeceuteCommand));
				}
				else
				{
					Config.WriteUsageAndHelp(verb.UsageTextCreator.Invoke(verb, Config.ExeceuteCommand));
				}
			}
			else
			{
				Config.WriteUsageAndHelp(verb.UsageText);
			}
			if (verb.HelpText == null)
			{
				if (verb.HelpTextCreator == null)
				{
					Config.WriteUsageAndHelp(HelpFormatter.FormatVerbHelp(verb, Config.MaxLineLength));
				}
				else
				{
					Config.WriteUsageAndHelp(verb.HelpTextCreator.Invoke(verb));
				}
			}
			else
			{
				Config.WriteUsageAndHelp(verb.HelpText);
			}
		}
	}
}
