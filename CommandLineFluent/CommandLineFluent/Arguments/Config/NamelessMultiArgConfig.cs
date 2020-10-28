namespace CommandLineFluent.Arguments.Config
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	/// <summary>
	/// A class to configure a multi-argument which is set without a short/long name.
	/// </summary>
	/// <typeparam name="TClass">The type of the class to configure.</typeparam>
	/// <typeparam name="TProp">The type of the property of <typeparamref name="TClass"/> to configure.</typeparam>
	/// <typeparam name="TPropCollection">The type of the collection.</typeparam>
	public sealed class NamelessMultiArgConfig<TClass, TProp, TPropCollection> where TClass : class, new()
	{
		private TPropCollection defaultValue;
		internal Dependencies<TClass>? configuredDependencies;
		/// <summary>
		/// Creates a new instance with nothing configured.
		/// </summary>
		public NamelessMultiArgConfig() { }
		/// <summary>
		/// Creates a new instance with a few preconfigured properties.
		/// This is useful when creating an extension method for a certain type; the converter and collection creator can be set to a default,
		/// and it can be required or not based on its nullability.
		/// </summary>
		public NamelessMultiArgConfig(bool isRequired, Func<string, Converted<TProp, string>> converter, Func<IEnumerable<TProp>, TPropCollection> createCollection)
		{
			IsRequired = isRequired;
			Converter = converter;
			Accumulator = createCollection;
			HelpText = string.Empty;
		}
		/// <summary>
		/// Whether or not this argument is required.
		/// Note, setting <see cref="DefaultValue"/> will set this to false automatically.
		/// </summary>
		public bool IsRequired { get; set; }
		/// <summary>
		/// The default value; by setting this, <see cref="IsRequired"/> is also set to false.
		/// </summary>
		public TPropCollection DefaultValue { get => defaultValue; set { IsRequired = false; defaultValue = value; } }
		/// <summary>
		/// Can be used to set dependencies. Each call made on this sets a new rule.
		/// </summary>
		public Dependencies<TClass> HasDependency
		{
			get => configuredDependencies ??= new Dependencies<TClass>();
			private set => configuredDependencies = value;
		}
		/// <summary>
		/// Help text displayed to the user.
		/// </summary>
		public string? HelpText { get; set; }
		/// <summary>
		/// A descriptive name, only used to display to the user.
		/// </summary>
		public string? DescriptiveName { get; set; }
		/// <summary>
		/// A function to use to convert from string into <typeparamref name="TProp"/>.
		/// </summary>
		public Func<string, Converted<TProp, string>> Converter { get; set; }
		/// <summary>
		/// A function to use to create a collection of <typeparamref name="TPropCollection"/> from an enumerable of <typeparamref name="TProp"/>.
		/// </summary>
		public Func<IEnumerable<TProp>, TPropCollection> Accumulator { get; set; }
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
