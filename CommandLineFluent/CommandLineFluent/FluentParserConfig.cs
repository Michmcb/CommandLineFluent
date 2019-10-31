using System;
using System.Collections.Generic;

namespace CommandLineFluent
{
	/// <summary>
	/// Configuration options affecting the behaviour of the FluentParser
	/// </summary>
	public class FluentParserConfig
	{
		internal FluentParserConfig() { }
		//public char[] Delimiters { get; private set; }
		/// <summary>
		/// A special switch which will cause parsing to immediately stop and return a single Error: HelpRequested
		/// </summary>
		public string ShortHelpSwitch { get; private set; }
		/// <summary>
		/// A special switch which will cause parsing to immediately stop and return a single Error: HelpRequested
		/// </summary>
		public string LongHelpSwitch { get; private set; }
		/// <summary>
		/// Default prefix for all short names
		/// </summary>
		public string DefaultShortPrefix { get; private set; }
		/// <summary>
		/// Default prefix for all long names
		/// </summary>
		public string DefaultLongPrefix { get; private set; }
		/// <summary>
		/// The Action that will be invoked when writing Error, Help, or Usage text, with a newline
		/// </summary>
		public Action<string> WriteText { get; private set; }
		/// <summary>
		/// The Action that will be invoked to get the Help text
		/// </summary>
		public Func<FluentParser, string> GetHelpText { get; private set; }
		/// <summary>
		/// The Action that will be invoked to get the Usage text
		/// </summary>
		public Func<FluentParser, string> GetUsageText { get; private set; }
		/// <summary>
		/// The Action that will be invoked to create a human-readable string to display Errors to the user
		/// </summary>
		public Func<IEnumerable<Error>, string> GetErrorsText { get; private set; }
		/// <summary>
		/// Defines the maximum number of characters that can fit on one line when writing help/usage text
		/// </summary>
		public int MaxLineLength { get; private set; }
		/// <summary>
		/// The command used to execute the program. Used when writing help text.
		/// </summary>
		public string ExeceuteCommand { get; private set; }
		/// <summary>
		/// Custom help text to be used when dispalying help information. This takes precedence over HelpTextCreator
		/// </summary>
		public string HelpText { get; private set; }
		/// <summary>
		/// Custom help text to be used when dispalying help information. This takes precedence over UsageTextCreator
		/// </summary>
		public string UsageText { get; private set; }
		/// <summary>
		/// Configures to use a DefaultShortPrefix of -, a DefaultLongPrefix of --, Help switches of -? and --help,
		/// and Errors/Help/Usage is automatically written to Console.Write on any error, with a MaxLineLength of Console.WindowWidth.
		/// If the Console cannot be used, then WriteHelp and WriteUsage will be set to null and MaxLineLength will be set to 10,000
		/// </summary>
		public void ConfigureWithDefaults()
		{
			DefaultShortPrefix = "-";
			DefaultLongPrefix = "--";
			ShortHelpSwitch = "-?";
			LongHelpSwitch = "--help";
			// If they're not using a console then we'll get an exception on trying to read WindowWidth, so catch that
			try
			{
				MaxLineLength = Console.WindowWidth;
				WriteText = Console.Write;
				GetHelpText = GetHelpTextDefault;
				GetUsageText = GetUsageTextDefault;
				GetErrorsText = HelpFormatter.FormatErrors;
			}
			catch (System.IO.IOException)
			{
				MaxLineLength = 10000;
			}
		}
		/// <summary>
		/// Configures a special switch which, if encountered, will cause parsing to immediately stop and return a single Error, with the ErrorCode HelpRequested.
		/// Use this in conjunction with ShowUsageOnFailure/ShowHelpOnFailure to automatically show help.
		/// </summary>
		/// <param name="shortName">The short name. By default, -?</param>
		/// <param name="longName">The long name. By default, --help</param>
		public void UseHelpSwitch(string shortName = "-?", string longName = "--help")
		{
			ShortHelpSwitch = shortName;
			LongHelpSwitch = longName;
		}
		/// <summary>
		/// Defines a short and long name prefix that will automatically be prepended to any non-null short and long names for any Options/Switches.
		/// By default, this is nothing.
		/// </summary>
		/// <param name="defaultShortPrefix">The prefix to apply to short names</param>
		/// <param name="defaultLongPrefix">The prefix to apply to long names</param>
		/// <returns></returns>
		public void UseDefaultPrefixes(string defaultShortPrefix, string defaultLongPrefix)
		{
			DefaultShortPrefix = defaultShortPrefix;
			DefaultLongPrefix = defaultLongPrefix;
		}
		// <summary>
		// Defines the delimiters that may be used to break up Options when parsing. e.g. If you define = as a delimiter,
		// then the string videoSource=C:\video.mp4 will be interpreted as an Option with name "videoSource", and its value will be "C:\video.mp4".
		// By default, this is nothing.
		// </summary>
		// <param name="delimiters">The delimiters that are allowed</param>
		//public void UseDelimiters(params char[] delimiters)
		//{
		//	Delimiters = delimiters;
		//}
		/// <summary>
		/// Defines the Action to invoke to write help, usage, and errors text. If null, this will be
		/// Console.Write
		/// </summary>
		/// <param name="textWriter">The Action</param>
		public void WithTextWriter(Action<string> textWriter = null)
		{
			WriteText = textWriter ?? Console.Write;
		}
		/// <summary>
		/// The command used to execute the program. Used when writing help text.
		/// If null, then the file name returned by System.Reflection.Assembly.GetEntryAssembly().Location will be used
		/// </summary>
		public void WithExecuteCommand(string execeuteCommand = null)
		{
			ExeceuteCommand = execeuteCommand ?? System.IO.Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location);
		}
		/// <summary>
		/// Configures the FluentParser to write default help and usage text on failure.
		/// It will be written will automatically upon an Error of any kind (including encountering the help switch).
		/// Usage is shown before Help.
		/// </summary>
		public void ShowHelpAndUsageOnFailure()
		{
			GetHelpText = GetHelpTextDefault;
			GetUsageText = GetUsageTextDefault;
		}
		/// <summary>
		/// Defines the action to invoke when any errors are encountered. By default, it will write Errors that should be shown to the user to Console.WriteLine (Message property).
		/// This Action will automatically be invoked upon an Error of any kind (including encountering the help switch).
		/// </summary>
		/// <param name="errorTextCreator">What is invoked to write Errors. If null, default formatting is used</param>
		public void ShowErrorsOnFailure(Func<IEnumerable<Error>, string> errorTextCreator = null)
		{
			GetErrorsText = errorTextCreator ?? HelpFormatter.FormatErrors;
		}
		/// <summary>
		/// Defines a custom errors text formatter, which returns the errors text to show to the user.
		/// </summary>
		/// <param name="errorTextCreator">What is invoked to write Errors. If null, Console.WriteLine</param>
		public void WithErrorFormatter(Func<IEnumerable<Error>, string> errorTextCreator)
		{
			GetErrorsText = errorTextCreator;
		}
		/// <summary>
		/// Defines a custom help text formatter, which returns the help text to show to the user.
		/// </summary>
		/// <param name="helpTextCreator">This takes an instance of this verb, and should return a string which is the help text</param>
		public void WithHelpFormatter(Func<FluentParser, string> helpTextCreator)
		{
			GetHelpText = helpTextCreator;
		}
		/// <summary>
		/// Defines a custom usage text creator, which returns the usage text to show to the user.
		/// </summary>
		/// <param name="usageTextCreator">This takes an instance of this verb, and should return a string which is the help text</param>
		public void WithUsageFormatter(Func<FluentParser, string> usageTextCreator)
		{
			GetUsageText = usageTextCreator;
		}
		internal string GetHelpTextDefault(FluentParser fp)
		{
			return HelpFormatter.FormatOverallHelp(fp.Verbs.Values, Util.ShortAndLongName(ShortHelpSwitch, LongHelpSwitch), MaxLineLength);
		}
		internal string GetUsageTextDefault(FluentParser fp)
		{
			return HelpFormatter.FormatOverallUsage(fp.Verbs.Values, ExeceuteCommand);
		}
	}
}