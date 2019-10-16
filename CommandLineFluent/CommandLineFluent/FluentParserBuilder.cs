using System;
using System.Collections.Generic;

namespace CommandLineFluent
{
	/// <summary>
	/// The starting point for setting up a FluentParser to parse command line arguments.
	/// Use the methods Configure, and either WithourVerbs or AddVerb to set this up.
	/// Once finished, use the method Build to validate and create a FluentParser.
	/// </summary>
	public class FluentParserBuilder
	{
		internal Dictionary<string, IFluentVerb> Verbs { get; }
		internal bool? IsUsingVerbs { get; private set; }
		internal FluentParserConfig Config { get; }
		/// <summary>
		/// Creates a new instance of FluentParserBuilder
		/// </summary>
		public FluentParserBuilder()
		{
			Config = new FluentParserConfig();
			Verbs = new Dictionary<string, IFluentVerb>();
		}
		/// <summary>
		/// Configures the parser's options
		/// </summary>
		public FluentParserBuilder Configure(Action<FluentParserConfig> config)
		{
			config?.Invoke(Config);
			return this;
		}
		/// <summary>
		/// Sets up how to parse without using verbs. Technically, this configures a default verb named "default", which
		/// is always used when parsing arguments. Arguments should not start with "default".
		/// </summary>
		/// <typeparam name="T">The type of the class which will be created when arguments are parsed successfully</typeparam>
		public FluentParserBuilder WithoutVerbs<T>(Action<FluentVerb<T>> verblessConfig) where T : class, new()
		{
			if (IsUsingVerbs != null)
			{
				throw new FluentParserBuilderException($@"The FluentParserBuilder has already been configured to use verbs, or has already had WithoutVerbs invoked on it.");
			}
			IsUsingVerbs = false;
			// ssshhh don't tell anybody it's actually a verb in disguise
			FluentVerb<T> v = new FluentVerb<T>(Config, "default");
			verblessConfig?.Invoke(v);
			Verbs.Add("default", v);
			return this;
		}
		/// <summary>
		/// Adds a verb for this parser. The verb name dictates the text that has to be entered on the command line.
		/// e.g. "foo.exe add" invokes the verb with the name "add".
		/// </summary>
		/// <typeparam name="T">The type of the class which will be created when arguments for that verb are parsed successfully</typeparam>
		/// <param name="verbName">The name of the verb</param>
		/// <param name="verbConfig">The action to configure the verb</param>
		public FluentParserBuilder AddVerb<T>(string verbName, Action<FluentVerb<T>> verbConfig) where T : class, new()
		{
			if (IsUsingVerbs == false)
			{
				throw new FluentParserBuilderException($@"The FluentParserBuilder has already been configured to not use verbs.");
			}
			if (Verbs.ContainsKey(verbName))
			{
				throw new FluentParserBuilderException($@"That verb name has already been used, you may only use unique verb names.");
			}
			IsUsingVerbs = true;
			FluentVerb<T> v = new FluentVerb<T>(Config, verbName);
			verbConfig?.Invoke(v);
			Verbs.Add(verbName, v);
			return this;
		}
		/// <summary>
		/// Creates a FluentParser using the configuration of this FluentParserBuilder.
		/// Throws a FluentParserValidationException if anything or any verbs are improperly configured.
		/// </summary>
		public FluentParser Build()
		{
			List<Error> errors = new List<Error>();
			if (Verbs.Values.Count > 0)
			{
				foreach (IFluentVerb verb in Verbs.Values)
				{
					errors.AddRange(verb.Validate());
				}
			}
			else
			{
				errors.Add(new Error(ErrorCode.ProgrammerError, false, @"This parser hasn't been configured with wither AddVerb<T> or WithoutVerbs<T>"));
			}
			if (errors.Count == 0)
			{
				return new FluentParser(this);
			}
			else
			{
				throw new FluentParserValidationException(@"FluentParserBuilder has not been configured correctly", errors);
			}
		}
	}
}
