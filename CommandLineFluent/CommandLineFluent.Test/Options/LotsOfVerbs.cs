namespace CommandLineFluent.Test.Options
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
	public abstract class Verb
	{
		public string Value { get; set; }
		public string Option { get; set; }
		public bool Switch { get; set; }
	}
	public class VerbVariety
	{
		public string Value { get; set; }
		public int Option { get; set; }
		public bool Switch { get; set; }
		public int? OptionNullable { get; set; }
	}
	public class Verb1 : Verb { }
	public class Verb2 : Verb { }
	public class Verb3 : Verb { }
	public class Verb4 : Verb { }
	public class Verb5 : Verb { }
	public class Verb6 : Verb { }
	public class Verb7 : Verb { }
	public class Verb8 : Verb { }
	public class Verb9 : Verb { }
	public class Verb10 : Verb { }
	public class Verb11 : Verb { }
	public class Verb12 : Verb { }
	public class Verb13 : Verb { }
	public class Verb14 : Verb { }
	public class Verb15 : Verb { }
	public class Verb16 : Verb { }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
}
