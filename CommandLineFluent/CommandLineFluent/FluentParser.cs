using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandLineFluent
{
	/// <summary>
	/// Able to parse command line arguments (or other stuff, if suitable).
	/// To create one, you need to use FluentParserBuilder.
	/// I'd recommend keeping an instance of this if it's needed multiple times.
	/// </summary>
	public class FluentParser
	{
		private readonly Dictionary<string, IFluentVerb> _verbs;
		/// <summary>
		/// Verb names to the Verbs themselves.
		/// </summary>
		public IReadOnlyDictionary<string, IFluentVerb> Verbs => _verbs;
		/// <summary>
		/// If true, the FluentParser has been set up to parse at least one verb.
		/// If false, the FluentParser has been set up to not parse any verbs, so it just parses arguments as-is.
		/// </summary>
		public bool IsUsingVerbs { get; }
		/// <summary>
		/// The current configuration.
		/// </summary>
		public FluentParserConfig Config { get; }
		/// <summary>
		/// Creates a new instance of FluentParser
		/// </summary>
		internal FluentParser(FluentParserBuilder builder)
		{
			IsUsingVerbs = builder.IsUsingVerbs.Value;
			_verbs = builder.Verbs;
			Config = builder.Config;
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
							result = new FluentParserResult(new Error[] { new Error(ErrorCode.InvalidVerb, true, $@"The verb {verbText} is not a recognized verb") }, null);
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
				WriteErrors(result.Errors);
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
		/// Writes the provided Errors.
		/// Does nothing if Config.WriteErrors is null.
		/// </summary>
		/// <param name="errors">The errors to write</param>
		public void WriteErrors(IEnumerable<Error> errors)
		{
			Config.WriteErrors?.Invoke(errors, Config.WriteMessages);
		}
		/// <summary>
		/// Writes a summary of how you invoke the program and how you can obtain further help, by specifying a verb.
		/// Order of priority is: this.Config.UsageText, then this.Config.UsageTextCreator, then the default HelpFormatter.FormatOverallUsage.
		/// Same thing for Help Text.
		/// Does nothing if Config.WriteUsageAndHelp is null.
		/// </summary>
		public void WriteOverallUsageAndHelp()
		{
			if (Config.WriteMessages == null)
			{
				return;
			}

			if (Config.UsageText == null)
			{
				if (Config.UsageTextCreator == null)
				{
					Config.WriteMessages(HelpFormatter.FormatOverallUsage(Verbs.Values, Config.ExeceuteCommand));
				}
				else
				{
					Config.WriteMessages(Config.UsageTextCreator.Invoke(this, Config.ExeceuteCommand));
				}
			}
			else
			{
				Config.WriteMessages(Config.UsageText);
			}
			if (Config.HelpText == null)
			{
				if (Config.HelpTextCreator == null)
				{
					Config.WriteMessages(HelpFormatter.FormatOverallHelp(Verbs.Values, Util.ShortAndLongName(Config.ShortHelpSwitch, Config.LongHelpSwitch), Config.MaxLineLength));
				}
				else
				{
					Config.WriteMessages(Config.HelpTextCreator.Invoke(this));
				}
			}
			else
			{
				Config.WriteMessages(Config.HelpText);
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
			if (Config.WriteMessages == null)
			{
				return;
			}

			if (verb.UsageText == null)
			{
				if (verb.UsageTextCreator == null)
				{
					Config.WriteMessages(HelpFormatter.FormatVerbUsage(verb, Config.ExeceuteCommand));
				}
				else
				{
					Config.WriteMessages(verb.UsageTextCreator.Invoke(verb, Config.ExeceuteCommand));
				}
			}
			else
			{
				Config.WriteMessages(verb.UsageText);
			}
			if (verb.HelpText == null)
			{
				if (verb.HelpTextCreator == null)
				{
					Config.WriteMessages(HelpFormatter.FormatVerbHelp(verb, Config.MaxLineLength));
				}
				else
				{
					Config.WriteMessages(verb.HelpTextCreator.Invoke(verb));
				}
			}
			else
			{
				Config.WriteMessages(verb.HelpText);
			}
		}
	}
}
