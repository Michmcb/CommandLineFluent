namespace CommandLineFluent
{
	using System.Collections.Generic;
	/// <summary>
	/// Provides ways for certain messages to be formatted.
	/// </summary>
	public interface IMessageFormatter
	{
		/// <summary>
		/// Writes the provided <paramref name="errors"/> to <paramref name="console"/>.
		/// </summary>
		/// <param name="console">The console to write to.</param>
		/// <param name="errors">The errors to write.</param>
		void WriteErrors(IConsole console, IEnumerable<Error> errors);
		/// <summary>
		/// Should be used when the user enters a help switch with no verb.
		/// Writes a summary of help to <paramref name="console"/> on the provided <paramref name="verbs"/>.
		/// The <paramref name="config"/> is provided so it can also write the short/long help switches,
		/// so the user can be told they can do: "verb -?" for specific help.
		/// </summary>
		/// <param name="console">The console to write to.</param>
		/// <param name="verbs">The verbs to write help for.</param>
		/// <param name="config">The current config, in particular, it has short/long help switches.</param>
		void WriteOverallHelp(IConsole console, IReadOnlyCollection<IVerb> verbs, CliParserConfig config);
		/// <summary>
		/// Should be used when the user enters a help switch and a verb: "verb -?".
		/// Writes specific help to <paramref name="console"/> on the provided <paramref name="verb"/>.
		/// </summary>
		/// <typeparam name="TClass">The class that this Verb parses arguments into.</typeparam>
		/// <param name="console">The console to write to.</param>
		/// <param name="verb">The verb to write help for.</param>
		void WriteSpecificHelp<TClass>(IConsole console, Verb<TClass> verb) where TClass : class, new();
	}
}