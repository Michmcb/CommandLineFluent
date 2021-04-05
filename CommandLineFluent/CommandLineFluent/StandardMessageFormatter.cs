namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Provides default usage and help formatting.
	/// It will write the keywords on the left, and descriptions on the right.
	/// Descriptions are padded with the length of the longest keyword, plus 3 spaces.
	/// If the descriptions are too long to fit on one line, they will wrap to the next line. Wrapped lines are all lined up.
	/// If help text defined has explicit newlines, it will make sure that descriptions are still aligned.
	/// </summary>
	public sealed class StandardMessageFormatter : IMessageFormatter
	{
		private static readonly char[] LineBreakChars = new char[] { '\n', '\r' };
		public const string ThreeSpaces = "   ";
		// TODO Write help for arguments in the same order the user added them to the Verb. To do this, make a new List<IArgument<TClass>> in the Verb classes and stuff it full of all the Arguments
		public StandardMessageFormatter(ConsoleColor keywordColor)
		{
			KeywordColor = keywordColor;
		}
		/// <summary>
		/// The color that keywords are written in, such as verb names and short/long names for options/switches
		/// </summary>
		public ConsoleColor KeywordColor { get; set; }
		/// <summary>
		/// Writes the overall help.
		/// Lists all verbs, their aliases, and their help text.
		/// </summary>
		/// <param name="console">The console help is written to.</param>
		public void WriteOverallHelp(IConsole console, IReadOnlyCollection<IVerb> verbs, CliParserConfig config)
		{
			ConsoleColor original = console.ForegroundColor;
			int width = console.CurrentWidth;
			console.WriteLine("Verbs...");

			// This is the number of characters we're reserving for the key text
			// A few more spaces just so it's easier to read

			WritePaddedKeywordDescriptions(console,
				KeywordColor,
				verbs.Select(verb => new KeywordAndDescription(verb.DescriptiveName, verb.HelpText)));

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
			console.Write(verb.DescriptiveName + ' ');

			foreach (IOption opt in verb.AllOptions)
			{
				console.Write(ArgUtils.ShortAndLongName(opt.ShortName, opt.LongName, opt.DescriptiveName, opt.ArgumentRequired != ArgumentRequired.Required));
				console.Write(' ');
			}
			foreach (ISwitch sw in verb.AllSwitches)
			{
				console.Write(ArgUtils.ShortAndLongName(sw.ShortName, sw.LongName, sw.ArgumentRequired != ArgumentRequired.Required));
				console.Write(' ');
			}
			int i = 1;
			foreach (IValue val in verb.AllValues)
			{
				console.Write(val.ArgumentRequired == ArgumentRequired.Required ? val.DescriptiveName : string.Concat('[', val.DescriptiveName, ']'));
				console.Write(' ');
				i++;
			}
			if (verb.MultiValue != null)
			{
				console.Write(verb.MultiValue.ArgumentRequired == ArgumentRequired.Required
					? verb.MultiValue.DescriptiveName
					: string.Concat('[', verb.MultiValue.DescriptiveName, ']'));
			}
			console.WriteLine();
			console.WriteLine();

			List<KeywordAndDescription> stuffToWrite = new();
			if (verb.AllVerbs.Count != 0)
			{
				console.WriteLine("Verbs: ");
				foreach (IVerb subVerb in verb.AllVerbs)
				{
					stuffToWrite.Add(new(subVerb.DescriptiveName, subVerb.HelpText));
				}
				WritePaddedKeywordDescriptions(console, KeywordColor, stuffToWrite);
				stuffToWrite.Clear();
			}
			if (verb.AllOptions.Count != 0)
			{
				console.WriteLine("Options: ");
				foreach (IOption opt in verb.AllOptions)
				{
					stuffToWrite.Add(new KeywordAndDescription(opt.ShortAndLongName(), GetRequiredness(opt.ArgumentRequired) + opt.HelpText));
				}
				WritePaddedKeywordDescriptions(console, KeywordColor, stuffToWrite);
				stuffToWrite.Clear();
			}
			if (verb.AllSwitches.Count != 0)
			{
				console.WriteLine("Switches: ");
				foreach (ISwitch sw in verb.AllSwitches)
				{
					stuffToWrite.Add(new KeywordAndDescription(sw.ShortAndLongName(), GetRequiredness(sw.ArgumentRequired) + sw.HelpText));
				}
				WritePaddedKeywordDescriptions(console, KeywordColor, stuffToWrite);
				stuffToWrite.Clear();
			}
			if (verb.AllValues.Count != 0)
			{
				console.WriteLine("Values: ");
				foreach (IValue val in verb.AllValues)
				{
					stuffToWrite.Add(new KeywordAndDescription(val.DescriptiveName, GetRequiredness(val.ArgumentRequired) + val.HelpText));
				}
				WritePaddedKeywordDescriptions(console, KeywordColor, stuffToWrite);
				stuffToWrite.Clear();
			}
			if (verb.MultiValue != null)
			{
				console.WriteLine("Multi-Value: ");
				stuffToWrite.Add(new KeywordAndDescription(verb.MultiValue.DescriptiveName, GetRequiredness(verb.MultiValue.ArgumentRequired) + verb.MultiValue.HelpText));
				WritePaddedKeywordDescriptions(console, KeywordColor, stuffToWrite);
				stuffToWrite.Clear();
			}
			console.ForegroundColor = original;
		}
		/// <summary>
		/// Writes specific help for <paramref name="verb"/> to <paramref name="console"/>. Shows all of the possible arguments and their help text.
		/// Any arguments that aren't required are shown in [brackets].
		/// </summary>
		/// <param name="console">The console help is written to.</param>
		/// <param name="verb">The verb to write help for.</param>
		public void WriteSpecificHelp(IConsole console, Verb verb)
		{
			ConsoleColor original = console.ForegroundColor;
			console.WriteLine(verb.HelpText);
			console.WriteLine();

			List<KeywordAndDescription> stuffToWrite = new();
			if (verb.AllVerbs.Count != 0)
			{
				console.WriteLine("Verbs: ");
				foreach (IVerb subVerb in verb.AllVerbs)
				{
					stuffToWrite.Add(new(subVerb.DescriptiveName, subVerb.HelpText));
				}
				WritePaddedKeywordDescriptions(console, KeywordColor, stuffToWrite);
				stuffToWrite.Clear();
			}
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
		/// <summary>
		/// Writes keywords and descriptions to <paramref name="console"/>. All keywords written with <paramref name="keywordColor"/>, are padded on the right with 3 spaces,
		/// and descriptions are broken into lines so they don't exceed the width of <paramref name="console"/>. Each line is left-padded so the descriptions all line up.
		/// If there are any newlines included, those are also taken into account.
		/// If the length of the longest keyword (plus 3 spaces) leaves at least 20 chars for the description, the descriptions are written so the lines are all left-aligned.
		/// </summary>
		/// <param name="console">The console to write the keywords and descriptions to.</param>
		/// <param name="keywordColor">The color to use for the keywords.</param>
		/// <param name="keywordsAndDescriptions">A sequence of keywords, and their descriptions</param>
		public static void WritePaddedKeywordDescriptions(IConsole console, ConsoleColor keywordColor, IEnumerable<KeywordAndDescription> keywordsAndDescriptions)
		{
			// Just bail out if there's nothing to do
			if (!keywordsAndDescriptions.Any()) return;
			ConsoleColor original = console.ForegroundColor;
			int width = console.CurrentWidth;
			// 3 extra spaces, so it's legible
			// Max won't throw an exception since we exit if the sequence is empty
			int longestKeyword = keywordsAndDescriptions.Max(x => x.Keyword.Length) + 3;
			int lengthForDescription = width - longestKeyword;

			// 20 characters is way too thin to be legible, so if the keywords are too long, just don't bother padding
			if (lengthForDescription >= 20)
			{
				string padding = new(' ', longestKeyword);
				foreach (KeywordAndDescription kd in keywordsAndDescriptions)
				{
					console.ForegroundColor = keywordColor;
					console.Write(kd.Keyword.PadRight(longestKeyword));
					console.ForegroundColor = original;
					if (kd.Description.Length <= lengthForDescription)
					{
						console.WriteLine(kd.Description);
					}
					else
					{
						IList<(int from, int length)> breaks = GetLineBreaks(kd.Description, lengthForDescription);
						(int from, int length) b = breaks[0];
						string line = kd.Description.Substring(b.from, b.length);
						console.WriteLine(line);
						for (int i = 1; i < breaks.Count; i++)
						{
							b = breaks[i];
							line = kd.Description.Substring(b.from, b.length);
							console.Write(padding);
							console.WriteLine(line);
						}
					}
					console.WriteLine();
				}
			}
			else
			{
				foreach (KeywordAndDescription kd in keywordsAndDescriptions)
				{
					console.ForegroundColor = keywordColor;
					console.Write(kd.Keyword + ThreeSpaces);
					console.ForegroundColor = original;
					console.WriteLine(kd.Description);
					console.WriteLine();
				}
			}
		}
		/// <summary>
		/// Gets a list of from/length pairs. You can iterate through this list and take substrings of <paramref name="str"/> to produce lines,
		/// each of which is no longer than <paramref name="maxLineLength"/>.
		/// Each line is as long as possible, preferring to break on a whitespace character. If there's no whitespace character to break on, it simply breaks in the middle of the word.
		/// If there are any newlines in <paramref name="str"/>, it will consider those as breaks, as well.
		/// </summary>
		/// <param name="str">The line to find breaks.</param>
		/// <param name="maxLineLength">The maximum length each line should be.</param>
		/// <returns></returns>
		public static IList<(int from, int length)> GetLineBreaks(string str, int maxLineLength)
		{
			List<(int from, int length)> lineBreaks = new();
			int currentIndex;
			int prevIndex = 0;
			while (str.Length >= prevIndex)
			{
				currentIndex = prevIndex + maxLineLength;
				int newlineIndex = str.IndexOfAny(LineBreakChars, prevIndex, Math.Min(maxLineLength, str.Length - prevIndex));
				if (newlineIndex != -1)
				{
					lineBreaks.Add((prevIndex, newlineIndex - prevIndex));
					// There's a new line here, so find the first non-newline char, and consider that the start of the next line.
					for (prevIndex = newlineIndex; prevIndex < str.Length; prevIndex++)
					{
						char c = str[prevIndex];
						if (c != '\n' && c != '\r')
						{
							break;
						}
					}
				}
				else if (currentIndex < str.Length)
				{
					char c = str[currentIndex];
					if (char.IsWhiteSpace(c))
					{
						// We can break at this char
						lineBreaks.Add((prevIndex, currentIndex - prevIndex));
						prevIndex = currentIndex + 1;
					}
					else
					{
						// If not, then we need to search backwards for a whitespace character.
						// If we don't find any, then just split up this word.
						currentIndex = LastIndexOfWhitespace(str, currentIndex, prevIndex);
						if (currentIndex != -1)
						{
							lineBreaks.Add((prevIndex, currentIndex - prevIndex));
							prevIndex = currentIndex + 1;
						}
						else
						{
							lineBreaks.Add((prevIndex, maxLineLength));
							prevIndex += maxLineLength;
						}
					}
				}
				else
				{
					lineBreaks.Add((prevIndex, str.Length - prevIndex));
					prevIndex = currentIndex;
				}
			}
			return lineBreaks;
		}
		public static int LastIndexOfWhitespace(string str, int start, int finish)
		{
			for (int i = start; i >= finish; i--)
			{
				if (char.IsWhiteSpace(str[i]))
				{
					return i;
				}
			}
			return -1;
		}
		public static string GetRequiredness(ArgumentRequired r)
		{
			return r switch
			{
				ArgumentRequired.Required => "Required. ",
				ArgumentRequired.Optional => "Optional. ",
				ArgumentRequired.HasDependencies => "Sometimes required. ",
				_ => "",
			};
		}
	}
}
