namespace CommandLineFluent
{
	using System;

	public readonly struct KeywordAndDescription : IEquatable<KeywordAndDescription>
	{
		public KeywordAndDescription(string keyword, string description)
		{
			Keyword = keyword;
			Description = description;
		}
		public string Keyword { get; }
		public string Description { get; }
		public override bool Equals(object? obj)
		{
			return obj is KeywordAndDescription description && Equals(description);
		}
		public bool Equals(KeywordAndDescription other)
		{
			return Keyword == other.Keyword &&
					 Description == other.Description;
		}
		public override int GetHashCode()
		{
#if NETSTANDARD2_0
			int hashCode = 1422190413;
			hashCode = hashCode * -1521134295 + System.Collections.Generic.EqualityComparer<string>.Default.GetHashCode(Keyword);
			hashCode = hashCode * -1521134295 + System.Collections.Generic.EqualityComparer<string>.Default.GetHashCode(Description);
			return hashCode;
#else
			return HashCode.Combine(Keyword, Description);
#endif
		}
		public static bool operator ==(KeywordAndDescription left, KeywordAndDescription right) => left.Equals(right);
		public static bool operator !=(KeywordAndDescription left, KeywordAndDescription right) => !(left == right);
	}
}
