namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;

	public sealed class CliParser
	{
		private readonly IConsole console;
		private Dictionary<string, IVerb> verbs;
		private CliParserConfig config;
		internal CliParser(IConsole console, Dictionary<string, IVerb> verbs, CliParserConfig config)
		{
			this.console = console;
			this.verbs = verbs;
			this.config = config;
		}
		// TODO implement the parser
		public IParseResult Parse(string args)
		{
			return default;
		}
		public IParseResult Parse(IEnumerable<string> args)
		{
			return default;
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
		public void WriteOverallUsageAndHelp()
		{
			throw new NotImplementedException("");
		}
		/// <summary>
		/// Writes the Usage Text, and then the Help Text for the specified verb. Order of priority is:
		/// verb.UsageText, then verb.UsageTextCreator, then the default HelpFormatter.FormatVerbUsage.
		/// Same thing for Help Text.
		/// Does nothing if Config.WriteText is null.
		/// </summary>
		/// <param name="verb">The verb to write detailed usage/help for</param>
		public void WriteUsageAndHelp(IVerb verb)
		{
			throw new NotImplementedException("");
		}
	}
}
