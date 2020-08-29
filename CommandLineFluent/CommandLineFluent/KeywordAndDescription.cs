namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;

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
			int hashCode = 1422190413;
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Keyword);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
			return hashCode;
		}
		public static bool operator ==(KeywordAndDescription left, KeywordAndDescription right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(KeywordAndDescription left, KeywordAndDescription right)
		{
			return !(left == right);
		}
	}
}
