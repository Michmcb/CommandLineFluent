﻿namespace CommandLineFluent.Arguments.Config
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Configures a Switch.
	/// </summary>
	/// <typeparam name="TClass">The type of the target class.</typeparam>
	/// <typeparam name="TProp">The type of the target property.</typeparam>
	public sealed class SwitchConfig<TClass, TProp> where TClass : class, new()
	{
		private readonly string? shortName;
		private readonly string? longName;
		private PropertyInfo? targetProperty;
		private ArgumentRequired argumentRequired;
		private TProp defaultValue;
		private Dependencies<TClass, TProp>? dependencies;
		private string? helpText;
		private string? name;
		private Func<bool, Maybe<TProp, string>>? converter;
		/// <summary>
		/// Creates a new <see cref="SwitchConfig{TClass, TProp}"/>. You shouldn't need to create this manually.
		/// </summary>
		/// <param name="shortName">The short name the user can use to provide this.</param>
		/// <param name="shortName">The long name the user can use to provide this.</param>
		/// <param name="converter">The converter to use to convert from bool to <typeparamref name="TProp"/>.</param>
		public SwitchConfig([AllowNull] string? shortName, [AllowNull] string? longName, [AllowNull] Func<bool, Maybe<TProp, string>>? converter)
		{
			this.shortName = shortName;
			this.longName = longName;
			this.converter = converter;
			defaultValue = default!;
			argumentRequired = ArgumentRequired.Optional;
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// The property must be a <typeparamref name="TProp"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public SwitchConfig<TClass, TProp> ForProperty(Expression<Func<TClass, TProp>> expression)
		{
			targetProperty = ArgUtils.PropertyInfoFromExpression(expression);
			return this;
		}
		/// <summary>
		/// Configures this to be required.
		/// </summary>
		public SwitchConfig<TClass, TProp> IsRequired()
		{
			argumentRequired = ArgumentRequired.Required;
			defaultValue = default!;
			dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this as optional, with a default value when not provided. By default, switches are optional, with the default value of <typeparamref name="TProp"/>.
		/// </summary>
		/// <param name="defaultValue">The values to use as a default value when this is not provided.</param>
		public SwitchConfig<TClass, TProp> IsOptional(TProp defaultValue = default)
		{
			argumentRequired = ArgumentRequired.Optional;
			this.defaultValue = defaultValue;
			dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this to only be required or must not appear under certain circumstances.
		/// If any rule is violated, parsing is considered to have failed. If all rules pass, then parsing is considered to have succeeded.
		/// You can specify that the user has to provide this depending upon the value of other properties (after parsing and conversion)
		/// </summary>
		/// <param name="defaultValue">The default value to use when the rules allow this to not be provided.</param>
		/// <param name="config">An action to configure the dependencies.</param>
		public SwitchConfig<TClass, TProp> WithDependencies(TProp defaultValue, Action<Dependencies<TClass, TProp>> config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), "config cannot be null");
			}
			argumentRequired = ArgumentRequired.HasDependencies;
			this.defaultValue = defaultValue;
			config.Invoke(dependencies = new Dependencies<TClass, TProp>());
			return this;
		}
		/// <summary>
		/// Configures this to show the provided Help Text.
		/// </summary>
		/// <param name="helpText">The help text.</param>
		public SwitchConfig<TClass, TProp> WithHelpText(string helpText)
		{
			this.helpText = helpText;
			return this;
		}
		/// <summary>
		/// Configures this to have the provided human-readable name.
		/// </summary>
		/// <param name="name">The human-readable name</param>
		public SwitchConfig<TClass, TProp> WithName(string name)
		{
			this.name = name;
			return this;
		}
		/// <summary>
		/// The converter to invoke on the provided string before assigning it to the property of <typeparamref name="TClass"/>.
		/// If not provided, no converter will be used; this is only valid to do if <typeparamref name="TProp"/> is string.
		/// If the user doesn't provide any value, the converter isn't invoked, instead the default value provided is used.
		/// </summary>
		/// <param name="converter">A convert that converts a string to <typeparamref name="TProp"/>.</param>
		public SwitchConfig<TClass, TProp> WithConverter(Func<bool, Maybe<TProp, string>> converter)
		{
			this.converter = converter;
			return this;
		}
		internal Switch<TClass, TProp> Build()
		{
			if (string.IsNullOrWhiteSpace(helpText))
			{
				throw new CliParserBuilderException("You need to provide some help text for Switch " + name);
			}
			if (targetProperty == null)
			{
				throw new CliParserBuilderException("You need to set a target property for Switch " + name);
			}
			if (converter == null && typeof(TProp) != typeof(bool))
			{
				throw new CliParserBuilderException(string.Concat("You need to provide a converter from bool to ", typeof(TProp).FullName, " for Switch ", name));
			}
			if (dependencies != null)
			{
				dependencies.Validate();
			}

			return new Switch<TClass, TProp>(shortName, longName, name, helpText, argumentRequired, targetProperty, defaultValue, dependencies, converter);
		}
	}
}
