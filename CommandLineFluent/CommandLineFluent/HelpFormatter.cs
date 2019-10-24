using CommandLineFluent.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Math;

namespace CommandLineFluent
{
	/// <summary>
	/// Provides default usage and help formatting
	/// </summary>
	public static class HelpFormatter
	{
		/// <summary>
		/// Formats the overall usage, given the command which defines how you execute this program.
		/// </summary>
		/// <param name="verbs">The verbs whose names will be used</param>
		/// <param name="execeuteCommand">The command to execute this program, or null to omit it</param>
		public static string FormatOverallUsage(IEnumerable<IFluentVerb> verbs, string execeuteCommand)
		{
			if (execeuteCommand != null)
			{
				execeuteCommand += ' ';
			}
			return $@"Usage: {execeuteCommand ?? ""}{string.Join("|", verbs.Select(x => x.Name))} options...{Environment.NewLine}{Environment.NewLine}";
		}
		/// <summary>
		/// Formats the overall help.
		/// Lists all verbs and their description.
		/// </summary>
		/// <param name="verbs">The verbs whose descriptions will be used</param>
		/// <param name="helpSwitches">A human-readable string showing the short and long help switches</param>
		/// <param name="maxLineLength">The maximum line length. If exceeded, an extra line break will be added</param>
		public static string FormatOverallHelp(IEnumerable<IFluentVerb> verbs, string helpSwitches, int maxLineLength)
		{
			List<Tuple<string, string>> text = new List<Tuple<string, string>>();
			// This is the number of characters we're reserving for the key text
			int charsForKey = 0;
			foreach (IFluentVerb verb in verbs)
			{
				charsForKey = Max(verb.Name.Length, charsForKey);
				text.Add(new Tuple<string, string>(verb.Name, verb.HelpText));
			}
			// A few more spaces just so it's easier to read
			charsForKey += 3;
			StringBuilder allTheHelp = new StringBuilder();
			foreach (Tuple<string, string> t in text)
			{
				allTheHelp.Append(t.Item1.PadRight(charsForKey));
				allTheHelp.AppendLine(t.Item2);
				// If we've wrapped a line, just shove an extra newline in there so it's still easyish to read
				if (maxLineLength < t.Item1.Length + (t.Item2?.Length ?? 0))
				{
					allTheHelp.AppendLine();
				}
			}
			allTheHelp.AppendLine();
			allTheHelp.AppendLine($@"Use verbname {helpSwitches} for detailed help.");
			allTheHelp.AppendLine();
			return allTheHelp.ToString();
		}
		/// <summary>
		/// Formats the help text of the Options, Switches, and Values of the verb. If custom HelpText has been provided, that will be used.
		/// If not, then if custom HelpFormatter is set on the verb, that is used to produce the help text. Otherwise, default formatting is used.
		/// </summary>
		public static string FormatVerbHelp(IFluentVerb verb, int maxLineLength)
		{
			// This is the number of characters we're reserving for the key text
			int charsForKey = 0;
			int i = 0;
			string[] keyText = new string[(verb.FluentValues?.Count ?? 1) + verb.FluentOptions.Count + verb.FluentSwitches.Count];
			string[] helpText = new string[keyText.Length];
			// Nothing provided, so default it is
			// So, we need to have one line per argument. We want to write the Name/shortName/longName, and then the HelpText associated with it. Easy as pie.
			if (verb.FluentManyValues == null)
			{
				foreach (IFluentValue val in verb.FluentValues)
				{
					keyText[i] = val.Required ? val.Name ?? $"value{i}" : $"[{val.Name ?? $"value{i}"}]";
					helpText[i] = val.HelpText;
					charsForKey = Max(keyText[i++].Length, charsForKey);
				}
			}
			else
			{
				keyText[i] = verb.FluentManyValues.Required ? verb.FluentManyValues.Name ?? "Values" : $"[{verb.FluentManyValues.Name ?? "Values"}]";
				helpText[i] = verb.FluentManyValues.HelpText;
				// This is the very first one we do, so just set the widest key to whatever its length was
				charsForKey = helpText[i++].Length;
			}
			foreach (IFluentOption opt in verb.FluentOptions)
			{
				string name = Util.ShortAndLongName(opt);
				keyText[i] = $"[{name}]";
				helpText[i++] = opt.HelpText;
				charsForKey = Max(name.Length, charsForKey);
			}
			foreach (IFluentSwitch sw in verb.FluentSwitches)
			{
				string name = sw.ShortAndLongName();
				keyText[i] = name;
				helpText[i++] = sw.HelpText;
				charsForKey = Max(name.Length, charsForKey);
			}
			StringBuilder sb = new StringBuilder();

			// A few more spaces just so it's easier to read
			charsForKey += 3;
			for (i = 0; i < keyText.Length; i++)
			{
				sb.Append(keyText[i].PadRight(charsForKey));
				sb.AppendLine(helpText[i]);
				// If we've wrapped a line, just shove an extra newline in there so it's still easyish to read
				if (maxLineLength < keyText[i].Length + helpText[i].Length)
				{
					sb.AppendLine();
				}
			}
			sb.AppendLine();
			return sb.ToString();
		}
		/// <summary>
		/// Formats the usage text of the Options, Switches, and Values of the verb.
		/// Usage is formatted by writing the executeCommand, followed by the verb's Name,
		/// </summary>
		/// <param name="verb">The verb to write Usage information for</param>
		/// <param name="executeCommand">The command used to invoke the program</param>
		public static string FormatVerbUsage(IFluentVerb verb, string executeCommand)
		{
			StringBuilder sb = new StringBuilder("Usage: ");
			sb.Append(executeCommand + ' ' + verb.Name + ' ');
			if (verb.FluentManyValues == null)
			{
				int i = 1;
				foreach (IFluentValue val in verb.FluentValues)
				{
					sb.Append(val.Required ? val.Name ?? $"value{i}" : $"[{val.Name ?? $"value{i}"}]");
					sb.Append(' ');
				}
			}
			else
			{
				sb.Append(verb.FluentManyValues.Required ? verb.FluentManyValues.Name : $"[{verb.FluentManyValues.Name}]");
				sb.Append(' ');
			}

			foreach (IFluentOption opt in verb.FluentOptions)
			{
				string bothNames = Util.ShortAndLongName(opt);
				if (opt.Required)
				{
					sb.Append($"{bothNames} {opt.Name ?? "value"} ");
				}
				else
				{
					sb.Append($"[{bothNames} {opt.Name ?? "value"}] ");
				}
			}
			foreach (IFluentSwitch sw in verb.FluentSwitches)
			{
				// Switches are always optional
				sb.Append($"[{Util.ShortAndLongName(sw)}] ");
			}
			sb.AppendLine();
			sb.AppendLine(verb.HelpText);
			sb.AppendLine();
			return sb.ToString();
		}
		/// <summary>
		/// Returns the Error.Message, if Error.ShouldBeShownToUser is true. One Message per line.
		/// </summary>
		/// <param name="errors">The errors</param>
		public static string FormatErrors(IEnumerable<Error> errors)
		{
			if (errors.Any())
			{
				StringBuilder sb = new StringBuilder();
				foreach (Error err in errors)
				{
					if (err.ShouldBeShownToUser)
					{
						sb.AppendLine(err.Message);
					}
				}
				return sb.ToString();
			}
			else
			{
				return "";
			}
		}
	}
}
