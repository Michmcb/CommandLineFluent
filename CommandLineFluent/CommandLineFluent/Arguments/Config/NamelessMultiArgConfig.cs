namespace CommandLineFluent.Arguments.Config
{
	using System;
	using System.Collections.Generic;

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
		public NamelessMultiArgConfig()
		{
			defaultValue = default!;
			Converter = null!;
			Accumulator = null!;
		}
		/// <summary>
		/// Creates a new instance with a few preconfigured properties.
		/// This is useful when creating an extension method for a certain type; the converter and collection creator can be set to a default,
		/// and it can be required or not based on its nullability.
		/// </summary>
		public NamelessMultiArgConfig(bool required, Func<string, Converted<TProp, string>> converter, Func<IEnumerable<TProp>, TPropCollection> accumulator)
		{
			defaultValue = default!;
			Required = required;
			Converter = converter;
			Accumulator = accumulator;
			HelpText = string.Empty;
		}
		/// <summary>
		/// Whether or not this argument is required.
		/// Note, setting <see cref="DefaultValue"/> will set this to false automatically.
		/// </summary>
		public bool Required { get; set; }
		/// <summary>
		/// The default value; by setting this, <see cref="Required"/> is also set to false.
		/// </summary>
		public TPropCollection DefaultValue { get => defaultValue; set { Required = false; defaultValue = value; } }
		/// <summary>
		/// Help text displayed to the user.
		/// </summary>
		public string? HelpText { get; set; }
		/// <summary>
		/// A descriptive name, only used to display to the user.
		/// If this is null, the name of the property to which this is assigned will be used.
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
		/// <summary>
		/// Can be used to set dependencies. Each call made on this sets a new rule.
		/// </summary>
		public Dependencies<TClass> HasDependency => configuredDependencies ??= new Dependencies<TClass>();
		/// <summary>
		/// This argument is required
		/// </summary>
		public NamelessMultiArgConfig<TClass, TProp, TPropCollection> IsRequired() { Required = true; return this; }
		/// <summary>
		/// This argument is not required, and has a default value when it is not provided.
		/// </summary>
		public NamelessMultiArgConfig<TClass, TProp, TPropCollection> IsOptional(TPropCollection defaultValue) { DefaultValue = defaultValue; return this; }
		/// <summary>
		/// Help text displayed to the user.
		/// </summary>
		public NamelessMultiArgConfig<TClass, TProp, TPropCollection> WithHelpText(string? helpText) { HelpText = helpText; return this; }
		/// <summary>
		/// A descriptive name, only used to display to the user.
		/// </summary>
		public NamelessMultiArgConfig<TClass, TProp, TPropCollection> WithDescriptiveName(string? descriptiveName) { DescriptiveName = descriptiveName; return this; }
		/// <summary>
		/// A function to use to convert from string into <typeparamref name="TProp"/>.
		/// </summary>
		public NamelessMultiArgConfig<TClass, TProp, TPropCollection> WithConverter(Func<string, Converted<TProp, string>> converter) { Converter = converter; return this; }
		/// <summary>
		/// A function to use to create a collection of <typeparamref name="TPropCollection"/> from an enumerable of <typeparamref name="TProp"/>.
		/// </summary>
		public NamelessMultiArgConfig<TClass, TProp, TPropCollection> WithAccumulator(Func<IEnumerable<TProp>, TPropCollection> accumulator) { Accumulator = accumulator; return this; }
		/// <summary>
		/// Configures dependencies, with a default value when this argument is not required.
		/// </summary>
		public NamelessMultiArgConfig<TClass, TProp, TPropCollection> WithDependencies(TPropCollection defaultValue, Action<Dependencies<TClass>> config) { DefaultValue = defaultValue; config(HasDependency); return this; }
	}
}
