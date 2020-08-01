namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using static System.Math;

	/// <summary>
	/// Provides default usage and help formatting
	/// </summary>
	//public sealed class ConsoleHelpFormatter
	//{
	//	public ConsoleHelpFormatter(IConsole console)
	//	{
	//		Console = console;
	//	}
	//	public IConsole Console { get; }
	//	/// <summary>
	//	/// Formats the overall usage, given the command which defines how you execute this program.
	//	/// </summary>
	//	/// <param name="verbs">The verbs whose names will be used</param>
	//	/// <param name="execeuteCommand">The command to execute this program, or null to omit it</param>
	//	public string FormatOverallUsage(IEnumerable<IVerb> verbs, string execeuteCommand)
	//	{
	//		if (execeuteCommand != null)
	//		{
	//			execeuteCommand += ' ';
	//		}
	//		return $@"Usage: {execeuteCommand ?? ""}{string.Join("|", verbs.Select(x => x.Name))} options...{Environment.NewLine}{Environment.NewLine}";
	//	}
	//	/// <summary>
	//	/// Formats the overall help.
	//	/// Lists all verbs and their description.
	//	/// </summary>
	//	/// <param name="verbs">The verbs whose descriptions will be used</param>
	//	/// <param name="helpSwitches">A human-readable string showing the short and long help switches</param>
	//	/// <param name="maxLineLength">The maximum line length. If exceeded, an extra line break will be added</param>
	//	public string FormatOverallHelp(IEnumerable<IVerb> verbs, string helpSwitches, int maxLineLength)
	//	{
	//		List<Tuple<string, string>> text = new List<Tuple<string, string>>();
	//		// This is the number of characters we're reserving for the key text
	//		int charsForKey = 0;
	//		foreach (IVerb verb in verbs)
	//		{
	//			charsForKey = Max(verb.Name.Length, charsForKey);
	//			text.Add(new Tuple<string, string>(verb.Name, verb.HelpText));
	//		}
	//		// A few more spaces just so it's easier to read
	//		charsForKey += 3;
	//		StringBuilder allTheHelp = new StringBuilder();
	//		foreach (Tuple<string, string> t in text)
	//		{
	//			allTheHelp.Append(t.Item1.PadRight(charsForKey));
	//			allTheHelp.AppendLine(t.Item2);
	//			// If we've wrapped a line, just shove an extra newline in there so it's still easyish to read
	//			if (maxLineLength < t.Item1.Length + (t.Item2?.Length ?? 0))
	//			{
	//				allTheHelp.AppendLine();
	//			}
	//		}
	//		allTheHelp.AppendLine();
	//		allTheHelp.AppendLine($@"Use verbname {helpSwitches} for detailed help.");
	//		allTheHelp.AppendLine();
	//		return allTheHelp.ToString();
	//	}
	//	/// <summary>
	//	/// Formats the usage text of the Options, Switches, and Values of the verb.
	//	/// Usage is formatted by writing the executeCommand, followed by the verb's Name,
	//	/// </summary>
	//	/// <param name="verb">The verb to write Usage information for</param>
	//	/// <param name="executeCommand">The command used to invoke the program</param>
	//	public string FormatVerbUsage<TClass>(Verb<TClass> verb, string executeCommand) where TClass : class, new()
	//	{
	//		StringBuilder sb = new StringBuilder("Usage: ");
	//		if (!string.IsNullOrEmpty(executeCommand))
	//		{
	//			sb.Append(executeCommand);
	//			sb.Append(' ');
	//		}
	//		if (!string.IsNullOrEmpty(verb.Name))
	//		{
	//			sb.Append(verb.Name);
	//			sb.Append(' ');
	//		}
	//		if (verb.MultiValue == null)
	//		{
	//			int i = 1;
	//			foreach (var val in verb.AllValues)
	//			{
	//				sb.Append(val.Required != false ? "\"" + (string.IsNullOrEmpty(val.Name) ? $"value{i}" : val.Name) + "\"" : $"[{val.Name ?? $"value{i}"}]");
	//				sb.Append(' ');
	//			}
	//		}
	//		else
	//		{
	//			sb.Append(verb.MultiValue.Required != false ? "\"" + (string.IsNullOrEmpty(verb.MultiValue.Name) ? "Values" : verb.MultiValue.Name) + "\"" : $"[{verb.MultiValue.Name ?? "Values"}]");
	//			sb.Append(' ');
	//		}

	//		foreach (var opt in verb.AllOptions)
	//		{
	//			string bothNames = Util.ShortAndLongName(opt, opt.Name);
	//			sb.Append(bothNames);
	//			sb.Append(' ');
	//			/*if (opt.Required == true)
	//			{
	//				sb.Append($@"{bothNames} ""{opt.Name ?? "value"}"" ");
	//			}
	//			else
	//			{
	//				sb.Append($@"[{bothNames} {opt.Name ?? "value"}] ");
	//			}*/
	//		}
	//		foreach (var sw in verb.AllSwitches)
	//		{
	//			// TODO only put them in brackets if they're optional
	//			sb.Append($"{Util.ShortAndLongName(sw)} ");
	//		}
	//		sb.AppendLine();
	//		sb.AppendLine(verb.HelpText);
	//		sb.AppendLine();
	//		return sb.ToString();
	//	}
	//	/// <summary>
	//	/// Formats the help text of the Options, Switches, and Values of the verb. If custom HelpText has been provided, that will be used.
	//	/// If not, then if custom HelpFormatter is set on the verb, that is used to produce the help text. Otherwise, default formatting is used.
	//	/// </summary>
	//	public string FormatVerbHelp<TClass>(Verb<TClass> verb, int maxLineLength) where TClass : class, new()
	//	{
	//		// This is the number of characters we're reserving for the key text
	//		int charsForKey = 0;
	//		string key;
	//		List<(string key, string help)> texts = new List<(string key, string help)>();
	//		// Nothing provided, so default it is
	//		// So, we need to have one line per argument. We want to write the Name/shortName/longName, and then the HelpText associated with it. Easy as pie.
	//		if (verb.MultiValue == null)
	//		{
	//			int i = 1;
	//			foreach (var val in verb.AllValues)
	//			{
	//				key = val.Required != false ? string.IsNullOrEmpty(val.Name) ? $"value{i}" : val.Name : $"[{val.Name ?? $"value{i}"}]";
	//				texts.Add((key, val.HelpText ?? ""));
	//				charsForKey = Max(key.Length, charsForKey);
	//				i++;
	//			}
	//		}
	//		else
	//		{
	//			key = verb.MultiValue.Required != false ? string.IsNullOrEmpty(verb.MultiValue.Name) ? "Values" : verb.MultiValue.Name : $"[{verb.MultiValue.Name ?? "Values"}]";
	//			texts.Add((key, verb.MultiValue.HelpText ?? ""));
	//			charsForKey = key.Length;
	//		}
	//		foreach (var opt in verb.AllOptions)
	//		{
	//			key = Util.ShortAndLongName(opt);
	//			texts.Add((key, opt.HelpText ?? ""));
	//			charsForKey = Max(key.Length, charsForKey);
	//		}
	//		foreach (var sw in verb.AllSwitches)
	//		{
	//			key = sw.ShortAndLongName();
	//			texts.Add((key, sw.HelpText ?? ""));
	//			charsForKey = Max(key.Length, charsForKey);
	//		}
	//		StringBuilder sb = new StringBuilder();

	//		// A few more spaces just so it's easier to read
	//		charsForKey += 3;
	//		foreach ((string key, string help) t in texts)
	//		{
	//			sb.Append(t.key.PadRight(charsForKey));
	//			sb.AppendLine(t.help);
	//			// If we've wrapped a line, just shove an extra newline in there so it's still easyish to read
	//			if (maxLineLength < t.key.Length + t.help.Length)
	//			{
	//				sb.AppendLine();
	//			}
	//		}
	//		sb.AppendLine();
	//		return sb.ToString();
	//	}
	//	/// <summary>
	//	/// Returns the Error.Message, if Error.ShouldBeShownToUser is true. One Message per line.
	//	/// </summary>
	//	/// <param name="errors">The errors</param>
	//	public string FormatErrors(IEnumerable<Error> errors)
	//	{
	//		if (errors.Any())
	//		{
	//			StringBuilder sb = new StringBuilder();
	//			foreach (Error err in errors)
	//			{
	//				if (err.ShouldBeShownToUser)
	//				{
	//					sb.AppendLine(err.Message);
	//				}
	//			}
	//			return sb.ToString();
	//		}
	//		else
	//		{
	//			return "";
	//		}
	//	}
	//}
}
