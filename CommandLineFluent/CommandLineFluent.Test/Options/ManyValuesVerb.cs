namespace CommandLineFluent.Test.Options
{
	using System.Collections.Generic;
	public class ManyValuesVerb
	{
		public ICollection<string> ManyValues { get; set; }
		public bool Switch { get; set; }
		public string Option { get; set; }
	}
}
