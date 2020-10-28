namespace CommandLineFluent.Arguments.Config
{
	using System;
	using System.ComponentModel;

	/// <summary>
	/// A class to configure a argument which is set with a short/long name.
	/// </summary>
	/// <typeparam name="TClass">The type of the class to configure.</typeparam>
	/// <typeparam name="TProp">The type of the property of <typeparamref name="TClass"/> to configure.</typeparam>
	/// <typeparam name="TRaw">The raw type that is retrieved from the command line.</typeparam>
	public sealed class NamedArgConfig<TClass, TProp, TRaw> where TClass : class, new()
	{
		private TProp defaultValue;
		internal Dependencies<TClass>? configuredDependencies;
		/// <summary>
		/// Creates a new instance with nothing configured.
		/// </summary>
		public NamedArgConfig() { }
		/// <summary>
		/// Creates a new instance with a few preconfigured properties.
		/// This is useful when creating an extension method for a certain type; the converter can be set to a default,
		/// and it can be required or not based on its nullability.
		/// </summary>
		/// <param name="isRequired"></param>
		/// <param name="converter"></param>
		public NamedArgConfig(bool isRequired, Func<TRaw, Converted<TProp, string>>? converter)
		{
			IsRequired = isRequired;
			Converter = converter;
		}
		/// <summary>
		/// The short name used to provide this argument.
		/// </summary>
		public string? ShortName { get; set; }
		/// <summary>
		/// The long name used to provide this argument.
		/// </summary>
		public string? LongName { get; set; }
		/// <summary>
		/// Whether or not this argument is required.
		/// </summary>
		public bool IsRequired { get; set; }
		/// <summary>
		/// The default value; by setting this, <see cref="IsRequired"/> is also set to false.
		/// </summary>
		public TProp DefaultValue { get => defaultValue; set { IsRequired = false; defaultValue = value; } }
		/// <summary>
		/// Can be used to set dependencies. Each call made on this sets a new rule.
		/// </summary>
		public Dependencies<TClass> Dependencies
		{
			get =>  configuredDependencies ??= new Dependencies<TClass>();
			private set => configuredDependencies = value;
		}
		/// <summary>
		/// Help text displayed to the user.
		/// </summary>
		public string HelpText { get; set; } = string.Empty;
		/// <summary>
		/// A descriptive name, only used to display to the user.
		/// </summary>
		public string? DescriptiveName { get; set; }
		/// <summary>
		/// A function to use to convert from <typeparamref name="TRaw"/> into <typeparamref name="TProp"/>.
		/// </summary>
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
