namespace CommandLineFluent
{
	using System.Collections.Generic;
	/// <summary>
	/// Splits a string into tokens. Splits based on spaces, or "double quotes" or 'single quotes'.
	/// </summary>
	public sealed class QuotedStringTokenizer : ITokenizer
	{
		/// <summary>
		/// Turns the string into tokens. Delimited based on spaces, "double quotes", or 'single quotes'.
		/// If you don't terminate the last pair of quotes ("like this), then the last token will be to the end of the string, including any whitespace or newlines
		/// </summary>
		/// <param name="str">The string to split into tokens</param>
		public IEnumerable<string> Tokenize(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				yield break;
			}

			int i = 0;
			while (true)
			{
				// First, find a non-whitespace character
				for (; i < str.Length && char.IsWhiteSpace(str[i]); i++) ;

				// Hit the end of the string before finding something interesting, just stop
				if (i == str.Length) break;

				// Now we get the ending delimiter to find
				char delim = GetDelimiter(str[i]);

				// If a space, we just need to find the next space
				if (delim == ' ')
				{
					int spaceIndex = str.IndexOf(' ', i);
					if (spaceIndex == -1)
					{
						yield return str.Substring(i);
						break;
					}
					yield return str.Substring(i, spaceIndex - i);
					i = spaceIndex;
				}
				else
				{
					// If the line ends with a quote, then we stop
					if (++i == str.Length) break;

					// Otherwise find the next delimiter and return that as the substring
					int quoteIndex = str.IndexOf(delim, i);
					if (quoteIndex == -1)
					{
						yield return str.Substring(i);
						break;
					}
					yield return str.Substring(i, quoteIndex - i);
					if ((i = quoteIndex + 1) == str.Length) break;
				}
			}

			// Find the first character that isn't a whitespace character, and have i set to that char

			//char delimitingChar = GetDelimiter(str[i]);
			//int from = (delimitingChar == ' ') ? i : i + 1;
			//for (i = from; i < str.Length; i++)
			//{
			//	// We have to keep going until we find the delimiting character.
			//	if (str[i] == delimitingChar)
			//	{
			//		yield return str.Substring(from, i - from);
			//		// And now, we need to find the next delimiting char. To do that, just skip whitespace.
			//		// Plus though, if we ended on a quote, jump 1 character ahead. Otherwise, we won't move forwards!
			//		if (delimitingChar != ' ')
			//		{
			//			i++;
			//		}
			//
			//		for (; i < str.Length && char.IsWhiteSpace(str[i]); i++)
			//		{
			//		}
			//		// By skipping 1 char above if we ended on a quote, we might have gone past the end of the string
			//		if (i < str.Length)
			//		{
			//			// If the delimiting char is a quote, then we have to make sure to NOT include it in the tokenized string.
			//			delimitingChar = GetDelimiter(str[i]);
			//			from = (delimitingChar == ' ') ? i : i + 1;
			//		}
			//		// If we did go past the end of a string, then make sure we don't try and capture another substring at the end of this
			//		else
			//		{
			//			from = i;
			//		}
			//	}
			//}
			//// If we hit the end of the line with no delimiting char, then consider the token to be closed
			//// Yes this means that the user doesn't HAVE to close their quotes
			//if (from < str.Length)
			//{
			//	yield return str.Substring(from);
			//}
		}
		private char GetDelimiter(char c)
		{
			switch (c)
			{
				case '"':
					return '"';
				case '\'':
					return '\'';
				case '`':
					return '`';
				default:
					return ' ';
			}
		}
	}
}
