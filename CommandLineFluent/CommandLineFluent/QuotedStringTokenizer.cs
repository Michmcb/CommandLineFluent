namespace CommandLineFluent
{
	using System.Collections.Generic;
	/// <summary>
	/// Splits a string into tokens. Splits based on spaces, "double quotes", 'single quotes', or `backticks`.
	/// </summary>
	public sealed class QuotedStringTokenizer : ITokenizer
	{
		/// <summary>
		/// Turns the string into tokens. Splits based on spaces, "double quotes", 'single quotes', or `backticks`.
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
		}
		private static char GetDelimiter(char c)
		{
			return c switch
			{
				'"' => '"',
				'\'' => '\'',
				'`' => '`',
				_ => ' ',
			};
		}
	}
}
