﻿namespace CommandLineFluent
{
	using System.Collections.Generic;

	/// <summary>
	/// A single verb, for example, foo.exe add.
	/// Verbs have their own Options, Values, and Switches which they can parse and create an instance of a particular class.
	/// </summary>
	public interface IVerb
	{
		/// <summary>
		/// The long name of this verb. This is what the user must enter to invoke this verb. This must be unique.
		/// </summary>
		string LongName { get; }
		/// <summary>
		/// The short name of this verb. This is what the user must enter to invoke this verb. This must be unique.
		/// </summary>
		string? ShortName { get; }
		/// <summary>
		/// Human-readable help for this verb.
		/// </summary>
		string HelpText { get; set; }
		/// <summary>
		/// Parses the provided arguments using this verb's rules. You shouldn't need to use this directly. But if you do,
		/// the first argument should not be this verb's name; if it is, make sure to use .Skip(1) first.
		/// </summary>
		/// <param name="args">The arguments to parse</param>
		IParseResult Parse(IEnumerable<string> args);
		/// <summary>
		/// Parses the provided arguments using this verb's rules. You shouldn't need to use this directly. But if you do,
		/// the first argument should not be this verb's name; if it is, make sure to use .Skip(1) first.
		/// </summary>
		/// <param name="args">The arguments to parse</param>
		IParseResult Parse(IEnumerator<string> args);
		/// <summary>
		/// Calls <see cref="IMessageFormatter.WriteSpecificHelp{TClass}(IConsole, Verb{TClass})"/>, passing this verb as a parameter.
		/// </summary>
		void WriteSpecificHelp(IConsole console, IMessageFormatter msgFormatter);
		/// <summary>
		/// The short and long name joined with a |
		/// </summary>
		string ShortAndLongName();
	}
}