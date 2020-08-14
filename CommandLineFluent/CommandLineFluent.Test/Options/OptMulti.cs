using System.Collections.Generic;

namespace CommandLineFluent.Test.Options
{
	public sealed class OptMulti
	{
		public string Option2 { get; internal set; }
		public string Option1 { get; internal set; }
		public IReadOnlyCollection<string> Values { get; internal set; }
	}
}
