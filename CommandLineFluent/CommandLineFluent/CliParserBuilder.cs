namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;

	public sealed class CliParserBuilder
	{
		private readonly Dictionary<string, IVerb> verbs;
		private readonly CliParserConfig config;
		private readonly IConsole console;
		public CliParserBuilder()
		{
			config = new CliParserConfig();
		}
		public CliParserBuilder(CliParserConfig config)
		{
			this.config = config;
			console = new StandardConsole();
		}
		public CliParserBuilder(CliParserConfig config, IConsole console)
		{
			this.config = config;
			this.console = console;
		}
		/// <summary>
		/// Adds a verb for this parser. To invoke it, the user has to enter <paramref name="name"/> on the command line.
		/// e.g. "foo.exe add" invokes the verb with the name "add".
		/// </summary>
		/// <typeparam name="TOpt">The type of the class which will be created when arguments for that verb are parsed successfully</typeparam>
		/// <param name="name">The name of the verb</param>
		/// <param name="config">The action to configure the verb</param>
		public CliParserBuilder AddVerb<TOpt>(string name, Action<Verb<TOpt>> config) where TOpt : class, new()
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(nameof(name), "Verb Name cannot be null or an empty string");
			}
			if (verbs.ContainsKey(name))
			{
				throw new CliParserBuilderException("That verb name has already been used, you may only use unique verb names.");
			}
			Verb<TOpt> v = new Verb<TOpt>(name);
			verbs.Add(name, v);
			config?.Invoke(v);
			return this;
		}
		/// <summary>
		/// Creates a <see cref="CliParser"/>.
		/// Throws a <see cref="CliParserBuilderException"/> if anything or any verbs are improperly configured.
		/// </summary>
		public CliParser Build()
		{
			List<Error> errors = new List<Error>();
			if (verbs.Values.Count > 0)
			{
				foreach (IVerb verb in verbs.Values)
				{
					errors.AddRange(verb.Validate());
				}
			}
			else
			{
				errors.Add(new Error(ErrorCode.ProgrammerError, false, @"This parser hasn't been configured with AddVerb<T>"));
			}
			if (errors.Count == 0)
			{
				return new CliParser(console, verbs, config);
			}
			else
			{
				throw new CliParserBuilderException("CliParserBuilder has not been configured correctly", errors);
			}
		}
	}
}
