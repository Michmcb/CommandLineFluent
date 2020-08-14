namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using CommandLineFluent.Arguments;

	/// <summary>
	/// Provides default usage and help formatting
	/// </summary>
	public sealed class StandardMessageFormatter : IMessageFormatter
	{
		public const string ThreeSpaces = "   ";
		// TODO allow telling the StandardMessageFormatter how to order values/switches/options
		// TODO wrap on >80 chars
		// TODO maybe make the verb name cyan?
		public StandardMessageFormatter()
		{

		}
		/// <summary>
		/// Formats the overall help.
		/// Lists all verbs and their description.
		/// </summary>
		public void WriteOverallHelp(IConsole console, IReadOnlyCollection<IVerb> verbs, CliParserConfig config)
		{
			console.WriteLine($@"Usage: {string.Join("|", verbs.Select(x => x.Name))} options...{Environment.NewLine}{Environment.NewLine}");

			// This is the number of characters we're reserving for the key text
			// A few more spaces just so it's easier to read
			foreach (IVerb verb in verbs)
			{
				console.WriteLine(string.Concat(verb.Name, ThreeSpaces, verb.HelpText));
				console.WriteLine();
			}
			console.WriteLine($@"Use verbname {config.ShortHelpSwitch}|{config.LongHelpSwitch} for detailed help.");
			console.WriteLine();
		}
		public void WriteSpecificHelp<TClass>(IConsole console, Verb<TClass> verb, CliParserConfig config) where TClass : class, new()
		{
			console.WriteLine(verb.HelpText);
			console.Write(verb.Name + " ");

			int i = 1;
			foreach (IOption<TClass> opt in verb.AllOptions)
			{
				console.Write(ArgUtils.ShortAndLongName(opt.ShortName, opt.LongName, opt.Name ?? "?", opt.ArgumentRequired != ArgumentRequired.Required));
				console.Write(" ");
			}
			foreach (ISwitch<TClass> sw in verb.AllSwitches)
			{
				console.Write(ArgUtils.ShortAndLongName(sw.ShortName, sw.LongName, sw.ArgumentRequired != ArgumentRequired.Required));
				console.Write(" ");
			}
			foreach (IValue<TClass> val in verb.AllValues)
			{
				console.Write(val.ArgumentRequired == ArgumentRequired.Required ? "\"" + (string.IsNullOrEmpty(val.Name) ? $"value{i}" : val.Name) + "\" " : $"[{val.Name ?? $"value{i}"}] ");
				i++;
			}
			if (verb.MultiValue != null)
			{
				console.Write(verb.MultiValue.ArgumentRequired == ArgumentRequired.Required ? "\"" + (string.IsNullOrEmpty(verb.MultiValue.Name) ? "Values..." : verb.MultiValue.Name) + "\"" : $"[{verb.MultiValue.Name ?? "Values..."}]");
			}
			console.WriteLine();
			console.WriteLine();

			foreach (IOption<TClass> opt in verb.AllOptions)
			{
				console.Write(opt.ShortAndLongName() + ThreeSpaces);
				WriteRequiredness(console, opt.ArgumentRequired);
				console.WriteLine(opt.HelpText);
			}
			foreach (ISwitch<TClass> sw in verb.AllSwitches)
			{
				console.Write(sw.ShortAndLongName() + ThreeSpaces);
				WriteRequiredness(console, sw.ArgumentRequired);
				console.WriteLine(sw.HelpText);
			}
			foreach (IValue<TClass> val in verb.AllValues)
			{
				console.Write(val.Name + ThreeSpaces);
				WriteRequiredness(console, val.ArgumentRequired);
				console.WriteLine(val.HelpText);
			}
			if (verb.MultiValue != null)
			{
				console.Write(verb.MultiValue.Name + ThreeSpaces);
				WriteRequiredness(console, verb.MultiValue.ArgumentRequired);
				console.WriteLine(verb.MultiValue.HelpText);
			}

			// A few more spaces just so it's easier to read
			//foreach ((string key, string help) t in texts)
			//{
			//	sb.Append(t.key.PadRight(charsForKey));
			//	sb.AppendLine(t.help);
			//	// If we've wrapped a line, just shove an extra newline in there so it's still easyish to read
			//	if (maxLineLength < t.key.Length + t.help.Length)
			//	{
			//		sb.AppendLine();
			//	}
			//}
			//return sb.ToString();
		}
		public void WriteErrors(IConsole console, IEnumerable<Error> errors)
		{
			ConsoleColor original = console.ForegroundColor;
			console.ForegroundColor = ConsoleColor.Red;

			foreach (Error e in errors)
			{
				console.WriteLine(e.ToString());
			}

			console.ForegroundColor = original;
		}
		private static void WriteRequiredness(IConsole console, ArgumentRequired r)
		{
			switch (r)
			{
				case ArgumentRequired.Required:
					console.Write("Required. ");
					break;
				case ArgumentRequired.Optional:
					console.Write("Optional. ");
					break;
			}
		}
	}
}
