namespace CommandLineFluent
{
	public sealed class CliParserConfig
	{
		public string DefaultShortPrefix { get; internal set; }
		public string DefaultLongPrefix { get; internal set; }
		public string ShortHelpSwitch { get; internal set; }
		public string LongHelpSwitch { get; internal set; }
	}
}
