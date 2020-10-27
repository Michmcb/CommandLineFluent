namespace CommandLineFluent.Arguments.Config
{
	using System;
	using System.ComponentModel;

	public sealed class NamedArgConfig<TClass, TProp, TRaw> where TClass : class, new()
	{
		private TProp defaultValue;
		public NamedArgConfig() { }
		public NamedArgConfig(bool isRequired, Func<TRaw, Converted<TProp, string>>? converter)
		{
			IsRequired = isRequired;
			Converter = converter;
		}
		public string? ShortName { get; set; }
		public string? LongName { get; set; }
		public bool IsRequired { get; set; }
		public TProp DefaultValue { get => defaultValue; set { IsRequired = false; defaultValue = value; } }
		public Dependencies<TClass>? Dependencies { get; set; }
		public string HelpText { get; set; } = string.Empty;
		public string? DescriptiveName { get; set; }
		public Func<TRaw, Converted<TProp, string>>? Converter { get; set; }
		// This stuff is useless and just adds clutter, so hide it
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}
	}
}
