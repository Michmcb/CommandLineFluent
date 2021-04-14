namespace CommandLineFluent
{
	using System;
	/// <summary>
	/// Configuration options for the <see cref="CliParser"/>.
	/// </summary>
	public sealed class CliParserConfig
	{
		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="stringComparer">If not provided, <see cref="StringComparer.OrdinalIgnoreCase"/>.</param>
		public CliParserConfig(StringComparer? stringComparer = null, string defaultShortPrefix = "-", string defaultLongPrefix = "--", string shortHelpSwitch = "-?", string longHelpSwitch = "--help")
		{
			StringComparer = stringComparer ?? StringComparer.OrdinalIgnoreCase;
			IsCaseSensitive = StringComparer.Compare("a", "A") != 0; // If they compare as the same, that's case insensitive. If they're different, case sensitive.
			DefaultShortPrefix = defaultShortPrefix;
			DefaultLongPrefix = defaultLongPrefix;
			ShortHelpSwitch = shortHelpSwitch;
			LongHelpSwitch = longHelpSwitch;
		}
		/// <summary>
		/// The default prefix to use for any short names. If short names don't start with this, it'll automatically be prepended.
		/// By default: -
		/// </summary>
		public string DefaultShortPrefix { get; set; } = "-";
		/// <summary>
		/// The default prefix to use for any long names. If long names don't start with this, it'll automatically be prepended.
		/// By default: --
		/// </summary>
		public string DefaultLongPrefix { get; set; } = "--";
		/// <summary>
		/// The default short switch to use to request help.
		/// By default: -?
		/// </summary>
		public string ShortHelpSwitch { get; set; } = "-?";
		/// <summary>
		/// The default long switch to use to request help.
		/// By default: --help
		/// </summary>
		public string LongHelpSwitch { get; set; } = "--help";
		/// <summary>
		/// The string comparer to use when parsing argument short/long names and verb names.
		/// Must be passed in the constructor since this is propogated to verbs, which use it in dictionaries.
		/// By default: <see cref="StringComparer.OrdinalIgnoreCase"/>.
		/// </summary>
		public StringComparer StringComparer { get; }
		/// <summary>
		/// Set when you provide a value to <see cref="StringComparer"/>.
		/// If the provided comparer acts as case sensitive, this is set to true. Otherwise this is set to false.
		/// </summary>
		public bool IsCaseSensitive { get; }
	}
}
