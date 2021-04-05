namespace CommandLineFluent
{
	using System;
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
		/// A human-readable name which describes this verb.
		/// </summary>
		string DescriptiveName { get; set; }
		/// <summary>
		/// Human-readable help for this verb.
		/// </summary>
		string HelpText { get; set; }
		/// <summary>
		/// Any sub-verbs that this verb has
		/// </summary>
		IReadOnlyList<IVerb> AllVerbs { get; }
		/// <summary>
		/// Adds a verb for this parser. To invoke it, the user has to enter <paramref name="longName"/> on the command line.
		/// e.g. "foo.exe add" invokes the verb with the name "add".
		/// </summary>
		/// <param name="longName">The long name of the verb</param>
		/// <param name="config">The action to configure the verb</param>
		public void AddVerb(string longName, Action<Verb> config);
		/// <summary>
		/// Adds a verb for this parser. To invoke it, the user has to enter <paramref name="longName"/> or <paramref name="shortName"/> on the command line.
		/// e.g. "foo.exe add" invokes the verb with the name "add".
		/// </summary>
		/// <param name="longName">The long name of the verb</param>
		/// <param name="shortName">The short name of the verb</param>
		/// <param name="config">The action to configure the verb</param>
		public void AddVerb(string longName, string shortName, Action<Verb> config);
		/// <summary>
		/// Adds a verb for this parser. To invoke it, the user has to enter <paramref name="longName"/> on the command line.
		/// e.g. "foo.exe add" invokes the verb with the name "add".
		/// </summary>
		/// <typeparam name="TClass">The type of the class which will be created when arguments for that verb are parsed successfully</typeparam>
		/// <param name="longName">The long name of the verb</param>
		/// <param name="config">The action to configure the verb</param>
		public void AddVerb<TClass>(string longName, Action<Verb<TClass>> config) where TClass : class, new();
		/// <summary>
		/// Adds a verb for this parser. To invoke it, the user has to enter <paramref name="longName"/> or <paramref name="shortName"/> on the command line.
		/// e.g. "foo.exe add" invokes the verb with the name "add".
		/// </summary>
		/// <typeparam name="TClass">The type of the class which will be created when arguments for that verb are parsed successfully</typeparam>
		/// <param name="longName">The long name of the verb</param>
		/// <param name="shortName">The short name of the verb</param>
		/// <param name="config">The action to configure the verb</param>
		public void AddVerb<TClass>(string longName, string shortName, Action<Verb<TClass>> config) where TClass : class, new();
		/// <summary>
		/// Parses the provided arguments using this verb's rules. You shouldn't need to use this directly. But if you do,
		/// the first argument should not be this verb's name; if it is, make sure to skip the first argument.
		/// </summary>
		/// <param name="args">The arguments to parse</param>
		IParseResult Parse(IEnumerator<string> args);
		/// <summary>
		/// Calls <see cref="IMessageFormatter.WriteSpecificHelp{TClass}(IConsole, Verb{TClass})"/>, passing this verb as a parameter.
		/// </summary>
		void WriteSpecificHelp(IConsole console, IMessageFormatter msgFormatter);
	}
}