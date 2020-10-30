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
		public NamedArgConfig(bool required, Func<TRaw, Converted<TProp, string>> converter)
		{
			Required = required;
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
		/// Note, setting <see cref="DefaultValue"/> will set this to false automatically.
		/// </summary>
		public bool Required { get; set; }
		/// <summary>
		/// The default value; by setting this, <see cref="Required"/> is also set to false.
		/// </summary>
		public TProp DefaultValue { get => defaultValue; set { Required = false; defaultValue = value; } }
		/// <summary>
		/// Help text displayed to the user.
		/// </summary>
		public string? HelpText { get; set; }
		/// <summary>
		/// A descriptive name, only used to display to the user.
		/// </summary>
		public string? DescriptiveName { get; set; }
		/// <summary>
		/// A function to use to convert from <typeparamref name="TRaw"/> into <typeparamref name="TProp"/>.
		/// </summary>
		public Func<TRaw, Converted<TProp, string>> Converter { get; set; }
		/// <summary>
		/// Can be used to set dependencies. Each call made on this sets a new rule.
		/// You should set a default value when using dependencies.
		/// </summary>
		public Dependencies<TClass> HasDependency
		{
			get => configuredDependencies ??= new Dependencies<TClass>();
		}

		/// <summary>
		/// The short name used to provide this argument.
		/// </summary>
		public NamedArgConfig<TClass, TProp, TRaw> WithShortName(string? shortName) { ShortName = shortName; return this; }
		/// <summary>
		/// The long name used to provide this argument.
		/// </summary>
		public NamedArgConfig<TClass, TProp, TRaw> WithLongName(string? longName) { LongName = longName; return this; }
		/// <summary>
		/// The short and long name used to provide this argument
		/// </summary>
		public NamedArgConfig<TClass, TProp, TRaw> WithShortAndLongName(string? shortName, string? longName) { ShortName = shortName; LongName = longName; return this; }
		/// <summary>
		/// This argument is required
		/// </summary>
		public NamedArgConfig<TClass, TProp, TRaw> IsRequired() { Required = true; return this; }
		/// <summary>
		/// This argument is not required, and has a default value when it is not provided.
		/// </summary>
		public NamedArgConfig<TClass, TProp, TRaw> IsOptional(TProp defaultValue) { DefaultValue = defaultValue; return this; }
		/// <summary>
		/// Help text displayed to the user.
		/// </summary>
		public NamedArgConfig<TClass, TProp, TRaw> WithHelpText(string? helpText) { HelpText = helpText; return this; }
		/// <summary>
		/// A descriptive name, only used to display to the user.
		/// </summary>
		public NamedArgConfig<TClass, TProp, TRaw> WithDescriptiveName(string? descriptiveName) { DescriptiveName = descriptiveName; return this; }
		/// <summary>
		/// A function to use to convert from <typeparamref name="TRaw"/> into <typeparamref name="TProp"/>.
		/// </summary>
		public NamedArgConfig<TClass, TProp, TRaw> WithConverter(Func<TRaw, Converted<TProp, string>> converter) { Converter = converter; return this; }
		/// <summary>
		/// Configures dependencies, with a default value when this argument is not required.
		/// </summary>
		public NamedArgConfig<TClass, TProp, TRaw> WithDependencies(TProp defaultValue, Action<Dependencies<TClass>> config) { DefaultValue = defaultValue; config(HasDependency); return this; }
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
