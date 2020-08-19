namespace CommandLineFluent.Test.Options
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
	public class ComplexVerb1
	{
		public string RequiredValue { get; set; }
		public int ConvertedValue { get; set; }
		public string? OptionalValue { get; set; }
		public bool Switch1 { get; set; }
		public bool DefaultValueSwitch { get; set; }
		public string ConvertedSwitch { get; set; }
		public string DefaultValueConvertedSwitch { get; set; }
		public string RequiredOption { get; set; }
		public string OptionalOption { get; set; }
		public int ConvertedOption { get; set; }
	}
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
}
