namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;

	public sealed class CliParserBuilder
	{
		private readonly Dictionary<string, IVerb> verbs;
		private readonly CliParserConfig config;
		private IConsole? console;
		private ITokenizer? tokenizer;
		private IMessageFormatter? msgFormatter;
		public CliParserBuilder()
		{
			verbs = new Dictionary<string, IVerb>();
			config = new CliParserConfig();
		}
		public CliParserBuilder(CliParserConfig config)
		{
			verbs = new Dictionary<string, IVerb>();
			this.config = config;
		}
		/// <summary>
		/// Specifies the IConsole to use.
		/// By default, this is <see cref="StandardConsole"/>, which just uses the static class <see cref="Console"/>.
		/// </summary>
		/// <param name="console">The console to use.</param>
		public CliParserBuilder UseConsole(IConsole console)
		{
			this.console = console;
			return this;
		}
		/// <summary>
		/// Specifies the ITokenizer to use.
		/// By default, this is <see cref="QuotedStringTokenizer"/>, which splits strings into tokens based on single or double quotes, or spaces.
		/// </summary>
		/// <param name="tokenizer">The tokenizer to use.</param>
		public CliParserBuilder UseTokenizer(ITokenizer tokenizer)
		{
			this.tokenizer = tokenizer;
			return this;
		}
		/// <summary>
		/// Specifies the IMessageFormatter to use.
		/// By default, this is <see cref="StandardMessageFormatter"/>.
		/// </summary>
		/// <param name="msgFormatter">The tokenizer to use.</param>
		public CliParserBuilder UseHelpFormatter(IMessageFormatter msgFormatter)
		{
			this.msgFormatter = msgFormatter;
			return this;
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
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), "You need to configure the verb");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(nameof(name), "Verb Name cannot be null or an empty string");
			}
			if (verbs.ContainsKey(name))
			{
				throw new CliParserBuilderException("That verb name has already been used, you may only use unique verb names");
			}
			Verb<TOpt> v = new Verb<TOpt>(name, this.config);
			verbs.Add(name, v);
			config.Invoke(v);
			return this;
		}
		/// <summary>
		/// Creates a <see cref="CliParser"/>.
		/// Throws a <see cref="CliParserBuilderException"/> if anything is improperly configured.
		/// </summary>
		public CliParser Build()
		{
			List<Error> errors = new List<Error>();
			if (verbs.Values.Count <= 0)
			{
				throw new CliParserBuilderException("The parser has no verbs, use AddVerb<T> to add some verbs");
			}
			if (errors.Count == 0)
			{
				return new CliParser(console ?? new StandardConsole(), tokenizer ?? new QuotedStringTokenizer(), msgFormatter ?? new StandardMessageFormatter(), verbs, config);
			}
			else
			{
				throw new CliParserBuilderException("CliParserBuilder has not been configured correctly", errors);
			}
		}
	}
}