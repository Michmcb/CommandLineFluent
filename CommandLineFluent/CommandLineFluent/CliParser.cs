namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	/// <summary>
	/// Parses arguments into classes.
	/// Create this class using a CliParserBuilder.
	/// </summary>
	public sealed class CliParser
	{
		private readonly Dictionary<string, IVerb> verbs;
		private readonly CliParserConfig config;
		/// <summary>
		/// Create this class using a CliParserBuilder.
		/// </summary>
		/// <param name="console"></param>
		/// <param name="tokenizer"></param>
		/// <param name="msgFormatter"></param>
		/// <param name="verbs"></param>
		/// <param name="config"></param>
		internal CliParser(IConsole console, ITokenizer tokenizer, IMessageFormatter msgFormatter, Dictionary<string, IVerb> verbs, CliParserConfig config)
		{
			Console = console;
			Tokenizer = tokenizer;
			MsgFormatter = msgFormatter;
			this.verbs = verbs;
			this.config = config;
		}
		/// <summary>
		/// Used to write to the console. Mostly just exists to simplify unit testing.
		/// </summary>
		public IConsole Console { get; }
		/// <summary>
		/// Only used if you call <see cref="Parse(string)"/>. It splits up the string into arguments.
		/// </summary>
		public ITokenizer Tokenizer { get; }
		/// <summary>
		/// Used to create messages for the user.
		/// </summary>
		public IMessageFormatter MsgFormatter { get; }
		/// <summary>
		/// Parses the provided args, after splitting them into tokens using <see cref="Tokenizer"/>, and turns them into a class.
		/// </summary>
		/// <param name="args">The args to split into tokens and parse.</param>
		/// <returns>An IParseResult when successful, or a collection of Errors on failure.</returns>
		public Maybe<IParseResult, IReadOnlyCollection<Error>> Parse(string args)
		{
			if (args == null)
			{
				throw new ArgumentNullException(nameof(args));
			}
			return Parse(Tokenizer.Tokenize(args));
		}
		/// <summary>
		/// Parses the provided args and turns them into a class.
		/// </summary>
		/// <param name="args">The args to parse.</param>
		/// <returns>An IParseResult when successful, or a collection of Errors on failure.</returns>
		public Maybe<IParseResult, IReadOnlyCollection<Error>> Parse(IEnumerable<string> args)
		{
			if (args == null)
			{
				throw new ArgumentNullException(nameof(args));
			}
			string? firstArg = args.FirstOrDefault();
			if (firstArg != null)
			{
				if (verbs.TryGetValue(firstArg, out IVerb? verb))
				{
					return verb.Parse(args.Skip(1));
				}
				// If the verb isn't found, it might be just the help switch
				else if (firstArg.Equals(config.ShortHelpSwitch, StringComparison.OrdinalIgnoreCase) || firstArg.Equals(config.LongHelpSwitch, StringComparison.OrdinalIgnoreCase))
				{
					return new Error[] { new Error(ErrorCode.HelpRequested, "") };
				}
				return new Error[] { new Error(ErrorCode.InvalidVerb, "The verb provided was not valid: " + firstArg) };
			}
			return new Error[] { new Error(ErrorCode.NoVerbFound, "No input") };
		}
		/// <summary>
		/// Writes the provided Errors using <see cref="IMessageFormatter.WriteErrors(IConsole, IEnumerable{Error})"/>.
		/// </summary>
		/// <param name="errors">The errors to write.</param>
		public void WriteErrors(IEnumerable<Error> errors)
		{
			MsgFormatter.WriteErrors(Console, errors);
		}
		/// <summary>
		/// Writes overall help, i.e. using <see cref="IMessageFormatter.WriteOverallHelp(IConsole, IReadOnlyCollection{IVerb}, CliParserConfig)"/>.
		/// </summary>
		public void WriteOverallHelp()
		{
			MsgFormatter.WriteOverallHelp(Console, verbs.Values, config);
		}
		/// <summary>
		/// Writes help for a specific verb; i.e. using <see cref="IMessageFormatter.WriteSpecificHelp{TClass}(IConsole, Verb{TClass}, CliParserConfig)"/>.
		/// </summary>
		/// <param name="verb">The verb to write detailed usage/help for.</param>
		public void WriteHelp(IVerb verb)
		{
			// TODO When the user screws up invoking a verb we'd still want to know what verb they TRIED to invoke so we can write specific help
			verb.WriteHelpTo(MsgFormatter, Console);
		}
	}
}
