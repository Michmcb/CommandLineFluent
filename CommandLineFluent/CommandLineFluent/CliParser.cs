namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public sealed class CliParser
	{
		private readonly IConsole console;
		private readonly ITokenizer tokenizer;
		private readonly IMessageFormatter helpFormatter;
		private Dictionary<string, IVerb> verbs;
		private CliParserConfig config;
		internal CliParser(IConsole console, ITokenizer tokenizer, IMessageFormatter helpFormatter, Dictionary<string, IVerb> verbs, CliParserConfig config)
		{
			this.console = console;
			this.tokenizer = tokenizer;
			this.helpFormatter = helpFormatter;
			this.verbs = verbs;
			this.config = config;
		}
		public Maybe<IParseResult, IReadOnlyCollection<Error>> Parse(string args)
		{
			if (args == null)
			{
				throw new ArgumentNullException(nameof(args));
			}
			return Parse(tokenizer.Tokenize(args));
		}
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
		/// Writes the provided Errors.
		/// Does nothing if Config.WriteErrors is null.
		/// </summary>
		/// <param name="errors">The errors to write</param>
		public void WriteErrors(IEnumerable<Error> errors)
		{
			throw new NotImplementedException("");
		}
		/// <summary>
		/// Writes a summary of how you invoke the program and how you can obtain further help, by specifying a verb.
		/// It uses Config.GetUsageText and Config.GetHelpText.
		/// Same thing for Help Text.
		/// Does nothing if Config.WriteText is null.
		/// </summary>
		public void WriteOverallHelp()
		{
			helpFormatter.WriteOverallHelp(console, verbs.Values, config);
		}
		/// <summary>
		/// Writes the Usage Text, and then the Help Text for the specified verb. Order of priority is:
		/// verb.UsageText, then verb.UsageTextCreator, then the default HelpFormatter.FormatVerbUsage.
		/// Same thing for Help Text.
		/// Does nothing if Config.WriteText is null.
		/// </summary>
		/// <param name="verb">The verb to write detailed usage/help for</param>
		public void WriteHelp(IVerb verb)
		{
			verb.WriteHelpTo(helpFormatter, console);
		}
	}
}
