using CommandLineFluent.Arguments;
using System;
using System.Collections.Generic;

namespace CommandLineFluent
{
	/// <summary>
	/// A single verb, for example, foo.exe add.
	/// Verbs have their own Options, Values, and Switches which they can parse and create an instance of a particular class.
	/// </summary>
	public interface IFluentVerb
	{
		/// <summary>
		/// An indeterminate number of values
		/// </summary>
		IFluentManyValues FluentManyValues { get; }
		/// <summary>
		/// All Values which have been added to this verb
		/// </summary>
		IReadOnlyCollection<IFluentValue> FluentValues { get; }
		/// <summary>
		/// All Switches which have been added to this verb
		/// </summary>
		IReadOnlyCollection<IFluentSwitch> FluentSwitches { get; }
		/// <summary>
		/// All Options which have been added to this verb
		/// </summary>
		IReadOnlyCollection<IFluentOption> FluentOptions { get; }
		/// <summary>
		/// The custom function to use when displaying help information, if HelpText is null.
		/// </summary>
		Func<IFluentVerb, string> HelpTextCreator { get; }
		/// <summary>
		/// The custom function to use when displaying usage information, if UsageText is null.
		/// </summary>s
		Func<IFluentVerb, string> UsageTextCreator { get; }
		/// <summary>
		/// The name used to invoke the verb. This must be unique.
		/// </summary>
		string Name { get; }
		/// <summary>
		/// Human-readable text that provides a description as to what this verb is used for. This is used when displaying Help.
		/// </summary>
		string HelpText { get; }
		/// <summary>
		/// Human-readable text that provides a description as to what this verb is used for. This is used when displaying Help.
		/// </summary>
		[Obsolete("This is no longer used. Instead, use HelpText")]
		string Description { get; }
		/// <summary>
		/// True if the last parsing attempt was successful, false if it was unsuccessful. Null if the verb was not parsed.
		/// This is automatically reset each time you invoke Parse on a FluentParser that owns this verb
		/// </summary>
		bool? Successful { get; }
		/// <summary>
		/// Clears the Verb's parsed instance and sets Successful to null.
		/// </summary>
		void Reset();
		/// <summary>
		/// The type of the object that this verb will provide the parsed the arguments to
		/// </summary>
		Type TargetType { get; }
		/// <summary>
		/// Returns errors if improperly configured, or null if all is well
		/// </summary>
		ICollection<Error> Validate();
		/// <summary>
		/// Parses the provided arguments using this verb's rules. You shouldn't need to use this directly. But if you do,
		/// the first argument should not be this verb's name; if it is, make sure to use .Skip(1) first.
		/// </summary>
		/// <param name="args">The arguments to parse</param>
		IReadOnlyCollection<Error> Parse(IEnumerable<string> args);
	}
}