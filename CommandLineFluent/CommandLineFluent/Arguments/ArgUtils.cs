namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// Some utilities for arguments
	/// </summary>
	public static class ArgUtils
	{
		/// <summary>
		/// Returns a string which has the <paramref name="shortName"/> and <paramref name="longName"/> separated by a pipe, like this: shortName|longName.
		/// If <paramref name="encloseInBrackets"/> is true, it'll be like this: [shortName|longName].
		/// If shortName or longName is null, it will just show only the non-null name.
		/// </summary>
		/// <param name="shortName">The short name.</param>
		/// <param name="longName">The long name.</param>
		/// <param name="encloseInBrackets">If true, the string will be enclosed in [].</param>
		/// <returns>A string with short and long name, separated by a pipe. Or if one is null, only the non-null name.</returns>
		public static string ShortAndLongName(string? shortName, string? longName, bool encloseInBrackets = false)
		{
			return shortName != null && longName != null
				? encloseInBrackets ? string.Concat('[', shortName, '|', longName, ']') : string.Concat(shortName, '|', longName)
				: encloseInBrackets ? string.Concat('[', shortName ?? longName, ']') : shortName ?? longName ?? string.Empty;
		}
		/// <summary>
		/// Returns a string which has the <paramref name="shortName"/> and <paramref name="longName"/> separated by a pipe, followed by the <paramref name="valueName"/>, like this: shortName|longName valueName.
		/// If <paramref name="encloseInBrackets"/> is true, it'll be like this: [shortName|longName valueName].
		/// If shortName or longName is null, it will just show only the non-null name.
		/// </summary>
		/// <param name="shortName">The short name.</param>
		/// <param name="longName">The long name.</param>
		/// <param name="valueName">The name of the value.</param>
		/// <param name="encloseInBrackets">If true, the string will be enclosed in [].</param>
		/// <returns>A string with short and long name, separated by a pipe. Or if one is null, only the non-null name.</returns>
		public static string ShortAndLongName(string? shortName, string? longName, string valueName, bool encloseInBrackets = false)
		{
			return shortName != null && longName != null
				? encloseInBrackets ? string.Concat('[', shortName, '|', longName, ' ', valueName, ']') : string.Concat(shortName, '|', longName, ' ', valueName)
				: encloseInBrackets ? string.Concat('[', shortName ?? longName, ' ', valueName, ']') : string.Concat(shortName ?? longName, ' ', valueName);
		}
	}
}