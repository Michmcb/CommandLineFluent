namespace CommandLineFluent.Arguments.Config
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;

	/// <summary>
	/// Configures a MultiValue.
	/// </summary>
	/// <typeparam name="TClass">The type of the target class.</typeparam>
	/// <typeparam name="TProp">The type of the target property.</typeparam>
	public sealed class MultiValueConfig<TClass, TProp> where TClass : class, new()
	{
		private PropertyInfo? targetProperty;
		private ArgumentRequired argumentRequired;
		private IReadOnlyCollection<TProp>? defaultValues;
		private Dependencies<TClass, TProp>? dependencies;
		private string? helpText;
		private string? name;
		//private IReadOnlyCollection<string> ignoredPrefixes;
		private Func<string, Maybe<TProp, string>>? converter;
		/// <summary>
		/// Creates a new <see cref="MultiValueConfig{TClass, TProp}"/>. You shouldn't need to create this manually.
		/// </summary>
		/// <param name="converter">The converter to use to convert from string to <typeparamref name="TProp"/>.</param>
		public MultiValueConfig([AllowNull]Func<string, Maybe<TProp, string>>? converter)
		{
			//ignoredPrefixes = new List<string>();
			this.converter = converter;
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// The property must be a collection of <typeparamref name="TProp"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public MultiValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, TProp>> expression)
		{
			targetProperty = ArgUtils.PropertyInfoFromExpression(expression);
			return this;
		}
		/// <summary>
		/// Configures this to be required. By default, MultiValues are required.
		/// </summary>
		public MultiValueConfig<TClass, TProp> IsRequired()
		{
			argumentRequired = ArgumentRequired.Required;
			defaultValues = null;
			dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this as optional, with default values when not provided.
		/// </summary>
		/// <param name="defaultValues">The values to use as a default value when this is not provided.</param>
		public MultiValueConfig<TClass, TProp> IsOptional(params TProp[] defaultValues)
		{
			argumentRequired = ArgumentRequired.Optional;
			this.defaultValues = defaultValues;
			dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this as optional, with default values when not provided.
		/// </summary>
		/// <param name="defaultValues">The values to use as a default value when this is not provided.</param>
		public MultiValueConfig<TClass, TProp> IsOptional(IReadOnlyCollection<TProp> defaultValues)
		{
			argumentRequired = ArgumentRequired.Optional;
			this.defaultValues = defaultValues;
			dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this to only be required or must not appear under certain circumstances.
		/// If any rule is violated, parsing is considered to have failed. If all rules pass, then parsing is considered to have succeeded.
		/// You can specify that the user has to provide this depending upon the value of other properties (after parsing and conversion)
		/// </summary>
		/// <param name="defaultValues">The default values to use when the rules allow this to not be provided.</param>
		/// <param name="config">An action to configure the dependencies.</param>
		public MultiValueConfig<TClass, TProp> WithDependencies(IReadOnlyCollection<TProp> defaultValues, Action<Dependencies<TClass, TProp>> config)
		{
			if (config != null)
			{
				argumentRequired = ArgumentRequired.HasDependencies;
				this.defaultValues = defaultValues;
				config.Invoke(dependencies = new Dependencies<TClass, TProp>());
				return this;
			}
			else
			{
				throw new ArgumentNullException(nameof(config), "config cannot be null");
			}
		}
		/// <summary>
		/// Configures this to show the provided Help Text.
		/// </summary>
		/// <param name="helpText">The help text.</param>
		public MultiValueConfig<TClass, TProp> WithHelpText(string helpText)
		{
			this.helpText = helpText;
			return this;
		}
		/// <summary>
		/// Configures this to have the provided human-readable name.
		/// </summary>
		/// <param name="name">The human-readable name.</param>
		public MultiValueConfig<TClass, TProp> WithName(string name)
		{
			this.name = name;	
			return this;
		}
		// /// <summary>
		// /// If any of the provided values start with these prefixes, parsing will fail.
		// /// This is useful to prevent typos such as --opotin (instead of --option) being interpreted as a value.
		// /// </summary>
		// /// <param name="prefixes"></param>
		// /// <returns></returns>
		// public MultiValueConfig<TClass, TProp> IgnorePrefixes(params string[] prefixes)
		// {
		// 	ignoredPrefixes = prefixes;
		// 	return this;
		// }
		// public MultiValueConfig<TClass, TProp> IgnorePrefixes(IReadOnlyCollection<string> prefixes)
		// {
		// 	ignoredPrefixes = prefixes;
		// 	return this;
		// }
		/// <summary>
		/// The converter to invoke on the provided strings before assigning them to the property of <typeparamref name="TClass"/>.
		/// If not provided, no converter will be used; this is only valid to do if <typeparamref name="TProp"/> is string.
		/// If the user doesn't provide any values, the converter isn't invoked, instead the default values provided are used.
		/// </summary>
		/// <param name="converter">A convert that converts a string to <typeparamref name="TProp"/>.</param>
		public MultiValueConfig<TClass, TProp> WithConverter([AllowNull]Func<string, Maybe<TProp, string>>? converter)
		{
			this.converter = converter;
			return this;
		}
		internal MultiValue<TClass, TProp> Build()
		{
			if (string.IsNullOrWhiteSpace(helpText))
			{
				throw new CliParserBuilderException("You need to provide some help text for Option " + name);
			}
			if (targetProperty == null)
			{
				throw new CliParserBuilderException("You need to set a target property for Option " + name);
			}
			if (converter == null && typeof(TProp) != typeof(string))
			{
				throw new CliParserBuilderException(string.Concat("You need to provide a converter from string to ", typeof(TProp).FullName, " for MultiValue ", name));
			}
			if (dependencies != null)
			{
				IEnumerable<Error> errors = dependencies.Validate();
				if (errors.Any())
				{
					throw new CliParserBuilderException("Dependencies are not valid: " + string.Join(", ", errors));
				}
			}

			return new MultiValue<TClass, TProp>(name, helpText, argumentRequired, targetProperty, defaultValues ?? Array.Empty<TProp>(), dependencies, converter);
		}
	}
}
