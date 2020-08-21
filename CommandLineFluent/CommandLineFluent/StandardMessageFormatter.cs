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
		// TODO Write help for arguments in the same order as the user added them to the Verb. To do this, make a new List<IArgument<TClass>> in the Verb classes and stuff it full of all the Arguments
		// TODO wrap on >80 chars
		public StandardMessageFormatter()
		{
			KeywordColor = ConsoleColor.Cyan;
		}
		/// <summary>
		/// The color that keywords are written in, such as verb names and short/long names for options/switches
		/// </summary>
		public ConsoleColor KeywordColor { get; set; }
		/// <summary>
		/// Writes the overall help.
		/// Lists all verbs and their help text.
		/// </summary>
		/// <param name="console">The console help is written to.</param>
		public void WriteOverallHelp(IConsole console, IReadOnlyCollection<IVerb> verbs, CliParserConfig config)
		{
			ConsoleColor original = console.ForegroundColor;
			console.WriteLine($@"Usage: {string.Join("|", verbs.Select(x => x.Name))} options...{Environment.NewLine}{Environment.NewLine}");

			// This is the number of characters we're reserving for the key text
			// A few more spaces just so it's easier to read
			foreach (IVerb verb in verbs)
			{
				console.WriteLine(string.Concat(verb.Name, ThreeSpaces, verb.HelpText));
				console.WriteLine();
			}
			console.Write("For detailed help, use: ");
			console.ForegroundColor = KeywordColor;
			console.WriteLine($"verbname {config.ShortHelpSwitch}|{config.LongHelpSwitch}");
			console.WriteLine();
			console.ForegroundColor = original;
		}
		/// <summary>
		/// Writes specific help for <paramref name="verb"/> to <paramref name="console"/>. Shows all of the possible arguments and their help text.
		/// Any arguments that aren't required are shown in [brackets].
		/// </summary>
		/// <typeparam name="TClass">The class that this verb parses input into.</typeparam>
		/// <param name="console">The console help is written to.</param>
		/// <param name="verb">The verb to write help for.</param>
		public void WriteSpecificHelp<TClass>(IConsole console, Verb<TClass> verb) where TClass : class, new()
		{
			ConsoleColor original = console.ForegroundColor;
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
				console.ForegroundColor = KeywordColor;
				console.Write(opt.ShortAndLongName() + ThreeSpaces);
				console.ForegroundColor = original;
				WriteRequiredness(console, opt.ArgumentRequired);
				console.WriteLine(opt.HelpText);
				console.WriteLine();
			}
			foreach (ISwitch<TClass> sw in verb.AllSwitches)
			{
				console.ForegroundColor = KeywordColor;
				console.Write(sw.ShortAndLongName() + ThreeSpaces);
				console.ForegroundColor = original;
				WriteRequiredness(console, sw.ArgumentRequired);
				console.WriteLine(sw.HelpText);
				console.WriteLine();
			}
			foreach (IValue<TClass> val in verb.AllValues)
			{
				console.ForegroundColor = KeywordColor;
				console.Write(val.Name + ThreeSpaces);
				console.ForegroundColor = original;
				WriteRequiredness(console, val.ArgumentRequired);
				console.WriteLine(val.HelpText);
				console.WriteLine();
			}
			if (verb.MultiValue != null)
			{
				console.ForegroundColor = KeywordColor;
				console.Write(verb.MultiValue.Name + ThreeSpaces);
				console.ForegroundColor = original;
				WriteRequiredness(console, verb.MultiValue.ArgumentRequired);
				console.WriteLine(verb.MultiValue.HelpText);
				console.WriteLine();
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
			console.ForegroundColor = original;
		}
		/// <summary>
		/// Writes each error's Message property to the console
		/// </summary>
		/// <param name="console">The console errors are written to.</param>
		/// <param name="errors">The errors to write.</param>
		public void WriteErrors(IConsole console, IEnumerable<Error> errors)
		{
			ConsoleColor original = console.ForegroundColor;
			console.ForegroundColor = ConsoleColor.Red;

			foreach (Error e in errors)
			{
				console.WriteLine(e.Message);
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
