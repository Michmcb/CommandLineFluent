namespace CommandLineFluent
{
	public sealed class CliParserConfig
	{
		public string DefaultShortPrefix { get; set; } = "-";
		public string DefaultLongPrefix { get; set; } = "--";
		public string ShortHelpSwitch { get; set; } = "-?";
		public string LongHelpSwitch { get; set; } = "--help";
	}
}
