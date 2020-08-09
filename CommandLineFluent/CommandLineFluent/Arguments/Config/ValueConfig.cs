namespace CommandLineFluent.Arguments.Config
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Collections.Generic;

	public sealed class ValueConfig<TClass, TProp> where TClass : class, new()
	{
		private PropertyInfo? targetProperty;
		private ArgumentRequired argumentRequired;
		private TProp defaultValue;
		private Dependencies<TClass, TProp>? dependencies;
		private string? helpText;
		private string? name;
		private Func<string, Maybe<TProp, string>>? converter;
		public ValueConfig()
		{
			defaultValue = default!;
		}
		internal bool HasConverter => converter != null;
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set</param>
		public ValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, TProp>> expression)
		{
			targetProperty = ArgUtils.PropertyInfoFromExpression(expression);
			return this;
		}
		/// <summary>
		/// Configures this to be required. By default, Options are required.
		/// </summary>
		public ValueConfig<TClass, TProp> IsRequired()
		{
			argumentRequired = ArgumentRequired.Required;
			defaultValue = default!;
			dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this as optional, with a default value when not provided.
		/// </summary>
		/// <param name="defaultValue">The value to use as a default value when this Option is not provided. If not provided, this is the default value for <typeparamref name="TProp"/></param>
		public ValueConfig<TClass, TProp> IsOptional(TProp defaultValue = default)
		{
			argumentRequired = ArgumentRequired.Optional;
			this.defaultValue = defaultValue;
			dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this to only be required or must not appear under certain circumstances.
		/// If any rule is violated, parsing is considered to have failed. If all rules pass, then parsing is considered to have succeeded.
		/// You can specify that the user has to provide this Value depending upon the value of other properties (after parsing and conversion)
		/// </summary>
		public ValueConfig<TClass, TProp> WithDependencies(TProp defaultValue, Action<Dependencies<TClass, TProp>> config)
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
		/// <param name="helpText">The help text for this Option</param>
		public ValueConfig<TClass, TProp> WithHelpText(string helpText)
		{
			this.helpText = helpText;
			return this;
		}
		/// <summary>
		/// Configures this to have the provided human-readable name.
		/// By default this is used to produce Usage Text
		/// </summary>
		/// <param name="name">The human-readable name</param>
		public ValueConfig<TClass, TProp> WithName(string name)
		{
			this.name = name;
			return this;
		}
		/// <summary>
		/// The converter to invoke on the provided value before assigning it to the property of <typeparamref name="TClass"/>.
		/// If not provided, no converter will be used. If no value is provided, the converter will not be invoked.
		/// </summary>
		/// <param name="converter">A convert that converts a string to <typeparamref name="TProp"/>.</param>
		public ValueConfig<TClass, TProp> WithConverter(Func<string, Maybe<TProp, string>> converter)
		{
			this.converter = converter;
			return this;
		}
		internal Value<TClass, TProp> Build()
		{
			if (string.IsNullOrWhiteSpace(helpText))
			{
				throw new CliParserBuilderException("You need to provide some help text for Option " + name);
			}
			if (targetProperty == null)
			{
				throw new CliParserBuilderException("You need to set a target property for Option " + name);
			}
			if (dependencies != null)
			{
				IEnumerable<Error> errors = dependencies.Validate();
				if (errors.Any())
				{
					throw new CliParserBuilderException("Dependencies are not valid: " + string.Join(", ", errors));
				}
			}

			return new Value<TClass, TProp>(name, helpText, argumentRequired, targetProperty, defaultValue, dependencies, converter);
		}
	}
}
