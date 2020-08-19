namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	/// <summary>
	/// Splits a string into tokens. Splits based on spaces, or "double quotes" or 'single quotes'.
	/// </summary>
	public sealed class QuotedStringTokenizer : ITokenizer
	{
		public QuotedStringTokenizer() { }
		/// <summary>
		/// Turns the string into tokens. Delimited based on spaces, "double quotes", or 'single quotes'.
		/// If you don't terminate the last pair of quotes ("like this), then the last token will be to the end of the string, including any whitespace or newlines
		/// </summary>
		/// <param name="line">The string to split into tokens</param>
		public IEnumerable<string> Tokenize(string line)
		{
			// Empty strings, no tokens at all
			if (string.IsNullOrWhiteSpace(line))
			{
				yield break;
			}

			int i = 0;
			// Find the first character that isn't a whitespace character, and have i set to that char
			for (; i < line.Length && char.IsWhiteSpace(line[i]); i++)
			{
			}
			char delimitingChar = GetDelimiter(line[i]);
			int from = (delimitingChar == ' ') ? i : i + 1;
			for (i = from; i < line.Length; i++)
			{
				// We have to keep going until we find the delimiting character.
				if (line[i] == delimitingChar)
				{
					yield return line.Substring(from, i - from);
					// And now, we need to find the next delimiting char. To do that, just skip whitespace.
					// Plus though, if we ended on a quote, jump 1 character ahead. Otherwise, we won't move forwards!
					if (delimitingChar != ' ')
					{
						i++;
					}

					for (; i < line.Length && char.IsWhiteSpace(line[i]); i++)
					{
					}
					// By skipping 1 char above if we ended on a quote, we might have gone past the end of the string
					if (i < line.Length)
					{
						// If the delimiting char is a quote, then we have to make sure to NOT include it in the tokenized string.
						delimitingChar = GetDelimiter(line[i]);
						from = (delimitingChar == ' ') ? i : i + 1;
					}
					// If we did go past the end of a string, then make sure we don't try and capture another substring at the end of this
					else
					{
						from = i;
					}
				}
			}
			// If we hit the end of the line with no delimiting char, then consider the token to be closed
			// Yes this means that the user doesn't HAVE to close their quotes
			if (from < line.Length)
			{
				yield return line.Substring(from);
			}
		}
		private char GetDelimiter(char c)
		{
			switch (c)
			{
				case '"':
					return '"';
				case '\'':
					return '\'';
				default:
					return ' ';
			}
		}
	}
}
