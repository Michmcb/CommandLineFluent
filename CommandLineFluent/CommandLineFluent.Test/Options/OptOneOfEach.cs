namespace CommandLineFluent.Test.Options
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
	public sealed class OptOneOfEach
	{
		public string Option { get; internal set; }
		public bool Switch { get; internal set; }
		public string Value { get; internal set; }
	}
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
}