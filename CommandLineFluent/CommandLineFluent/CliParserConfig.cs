namespace CommandLineFluent
{
	using System;
	public sealed class CliParserConfig
	{
		private StringComparer sc = StringComparer.OrdinalIgnoreCase;
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
		/// Setting this will also set <see cref="IsCaseSensitive"/>.
		/// By default: <see cref="StringComparer.OrdinalIgnoreCase"/>.
		/// </summary>
		public StringComparer StringComparer
		{
			get => sc;
			set
			{
				sc = value ?? throw new ArgumentNullException(nameof(value), "The value of StringComparer cannot be set to null");
				IsCaseSensitive = value.Compare("a", "A") != 0; // If they compare as the same, that's case insensitive. If they're different, case sensitive.
			}
		}
		/// <summary>
		/// Set when you provide a value to <see cref="StringComparer"/>.
		/// If the provided comparer acts as case sensitive, this is set to true. Otherwise this is set to false.
		/// </summary>
		public bool IsCaseSensitive { get; private set; } = false;
	}
}
