namespace CommandLineFluent.Arguments.Config
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	public sealed class NamelessMultiArgConfig<TClass, TProp, TPropCollection> where TClass : class, new()
	{
		private TPropCollection defaultValue;
		public NamelessMultiArgConfig(bool isRequired, Func<string, Converted<TProp, string>> converter, Func<IEnumerable<TProp>, TPropCollection> createCollection)
		{
			IsRequired = isRequired;
			Converter = converter;
			CreateCollection = createCollection;
			HelpText = string.Empty;
		}
		public bool IsRequired { get; set; }
		public TPropCollection DefaultValue { get => defaultValue; set { IsRequired = false; defaultValue = value; } }
		public Dependencies<TClass>? Dependencies { get; set; }
		public string HelpText { get; set; }
		public string? DescriptiveName { get; set; }
		public Func<string, Converted<TProp, string>> Converter { get; set; }
		public Func<IEnumerable<TProp>, TPropCollection> CreateCollection { get; set; }
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
