namespace CommandLineFluent.Test.Options
{
	using System;
	public sealed class EveryPrimitiveTypeNullable
	{
		public string? Str { get; set; }
		public short? Short { get; set; }
		public ushort? UShort { get; set; }
		public int? Int { get; set; }
		public uint? UInt { get; set; }
		public long? Long { get; set; }
		public ulong? ULong { get; set; }
		public float? Float { get; set; }
		public double? Double { get; set; }
		public decimal? Decimal { get; set; }
		public MyEnum? MyEnum { get; set; }
		public DateTime? DateTime { get; set; }
		public TimeSpan? TimeSpan { get; set; }
		public Guid? Guid { get; set; }
	}
}
