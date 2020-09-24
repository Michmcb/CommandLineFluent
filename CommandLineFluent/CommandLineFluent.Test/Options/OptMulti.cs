using System.Collections.Generic;

namespace CommandLineFluent.Test.Options
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
	public sealed class OptMulti
	{
		public string Option2 { get; internal set; }
		public string Option1 { get; internal set; }
		public IReadOnlyCollection<int> Values { get; internal set; }
	}
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
}
