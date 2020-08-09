namespace CommandLineFluent
{
	using System.Collections.Generic;

	/// <summary>
	/// A single verb, for example, foo.exe add.
	/// Verbs have their own Options, Values, and Switches which they can parse and create an instance of a particular class.
	/// </summary>
	public interface IVerb
	{
		/// <summary>
		/// The name used to invoke the verb. This must be unique.
		/// </summary>
		string Name { get; }
		string HelpText { get; set; }
		/// <summary>
		/// Parses the provided arguments using this verb's rules. You shouldn't need to use this directly. But if you do,
		/// the first argument should not be this verb's name; if it is, make sure to use .Skip(1) first.
		/// </summary>
		/// <param name="args">The arguments to parse</param>
		Maybe<IParseResult, IReadOnlyCollection<Error>> Parse(IEnumerable<string> args);
		void WriteHelpTo(IMessageFormatter helpFormatter, IConsole console);
	}
}