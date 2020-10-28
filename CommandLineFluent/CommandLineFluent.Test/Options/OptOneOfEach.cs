namespace CommandLineFluent.Test.Options
{
	using System.Collections.Generic;
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
	public sealed class OptOneOfEach
	{
		public int Option { get; set; }
		public bool Switch { get; set; }
		public string Value { get; set; }
		public IReadOnlyCollection<string> ManyValues { get; set; }
	}
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
}