namespace CommandLineFluent
{
	using System;
	public sealed class CliParserConfig
	{
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
		/// By default: StringComparer.OrdinalIgnoreCase.
		/// </summary>
		public StringComparer StringComparer { get; set; } = StringComparer.OrdinalIgnoreCase;
	}
}
