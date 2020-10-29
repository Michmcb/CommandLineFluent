namespace CommandLineFluent.Test.Options
{
using System.Collections.Generic;
	public sealed class OptArray { public string[] Collection { get; set; } }
	public sealed class OptList { public List<string> Collection { get; set; } }
	public sealed class OptIList { public IList<string> Collection { get; set; } }
	public sealed class OptIReadOnlyList { public IReadOnlyList<string> Collection { get; set; } }
	public sealed class OptICollection { public ICollection<string> Collection { get; set; } }
	public sealed class OptIReadOnlyCollection { public IReadOnlyCollection<string> Collection { get; set; } }
	public sealed class OptIEnumerable { public IEnumerable<string> Collection { get; set; } }
	public sealed class OptHashSet { public HashSet<string> Collection { get; set; } }
	public sealed class OptStack { public Stack<string> Collection { get; set; } }
	public sealed class OptQueue { public Queue<string> Collection { get; set; } }
}
