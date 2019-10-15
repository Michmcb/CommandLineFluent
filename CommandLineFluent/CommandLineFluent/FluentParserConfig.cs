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
		/// The Action to invoke to write Errors, Help, and Usage messages
		/// </summary>
		public Action<string> WriteMessages { get; private set; }
		/// <summary>
		/// The Action to invoke to write Errors
		/// </summary>
		public Action<IEnumerable<Error>, Action<string>> WriteErrors { get; private set; }
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
		/// The custom function to use when displaying help information, if HelpText is null.
		/// </summary>
		public Func<FluentParser, string> HelpTextCreator { get; private set; }
		/// <summary>
		/// The custom function to use when displaying usage information, if UsageText is null.
		/// </summary>s
		public Func<FluentParser, string, string> UsageTextCreator { get; private set; }

		/// <summary>
		/// Configures to use a DefaultShortPrefix of -, a DefaultLongPrefix of --, Help switches of -? and --help,
		/// and Errors/Help/Usage is automatically written to Console.WriteLine on any error, with a MaxLineLength of Console.WindowWidth.
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
				WriteMessages = Console.WriteLine;
				ShowHelpAndUsageOnFailure();
				ShowErrorsOnFailure();
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
		public void UseHelpSwitch(string shortName = null, string longName = null)
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
		/// <summary>
		/// Defines the delimiters that may be used to break up Options when parsing. e.g. If you define = as a delimiter,
		/// then the string videoSource=C:\video.mp4 will be interpreted as an Option with name "videoSource", and its value will be "C:\video.mp4".
		/// By default, this is nothing.
		/// </summary>
		/// <param name="delimiters">The delimiters that are allowed</param>
		//public void UseDelimiters(params char[] delimiters)
		//{
		//	Delimiters = delimiters;
		//}
		/// <summary>
		/// The command used to execute the program. Used when writing help text.
		/// If null, then the file name returned by System.Reflection.Assembly.GetEntryAssembly().Location will be used
		/// </summary>
		public void WithExecuteCommand(string execeuteCommand = null)
		{
			ExeceuteCommand = execeuteCommand ?? System.IO.Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location);
		}
		/// <summary>
		/// Defines the Action to invoke with the usage message on failure. By default, it will be Console.WriteLine.
		/// This Action will automatically be invoked upon an Error of any kind (including encountering the help switch).
		/// Usage is shown before Help.
		/// </summary>
		/// <param name="writeHelpAndUsage">What is invoked to write usage. If null, Console.WriteLine is used.</param>
		public void ShowHelpAndUsageOnFailure(Action<string> writeHelpAndUsage = null)
		{
			WriteMessages = writeHelpAndUsage ?? Console.WriteLine;
		}
		/// <summary>
		/// Defines the action to invoke when any errors are encountered. By default, it will write Errors that should be shown to the user to Console.WriteLine (Message property).
		/// This Action will automatically be invoked upon an Error of any kind (including encountering the help switch).
		/// </summary>
		/// <param name="writeErrors">What is invoked to write Errors. If null, Console.WriteLine</param>
		public void ShowErrorsOnFailure(Action<IEnumerable<Error>, Action<string>> writeErrors = null)
		{
			WriteErrors = writeErrors ?? Util.WriteErrors;
		}
		/// <summary>
		/// Configures custom help text
		/// </summary>
		/// <param name="helpText">The help text</param>
		public void WithHelpText(string helpText)
		{
			HelpText = helpText;
		}
		/// <summary>
		/// Configures custom usage text
		/// </summary>
		/// <param name="usageText">The usage text</param>
		public void WithUsageText(string usageText)
		{
			UsageText = usageText;
		}
		/// <summary>
		/// Configures a custom help text creator
		/// </summary>
		/// <param name="helpTextCreator">This takes an instance of this verb, and should return a string which is the help text</param>
		public void WithHelpFormatter(Func<FluentParser, string> helpTextCreator)
		{
			HelpTextCreator = helpTextCreator;
		}
		/// <summary>
		/// Configures a custom usage text creator. The string parameter is the ExecuteCommand property
		/// </summary>
		/// <param name="usageTextCreator">This takes an instance of this verb, and should return a string which is the help text</param>
		public void WithUsageFormatter(Func<FluentParser, string, string> usageTextCreator)
		{
			UsageTextCreator = usageTextCreator;
		}
	}
}