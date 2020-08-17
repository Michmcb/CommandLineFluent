namespace CommandLineFluent
{
	using System.Collections.Generic;
	/// <summary>
	/// Provides an interface to split a string into multiple tokens. Useful when making a shell-like interface,
	/// and you need to split the user-provided string into arguments.
	/// </summary>
	public interface ITokenizer
	{
		/// <summary>
		/// Splits a string into a collection of tokens.
		/// </summary>
		/// <param name="args">The string to split.</param>
		/// <returns>The tokens.</returns>
		IEnumerable<string> Tokenize(string args);
	}
}