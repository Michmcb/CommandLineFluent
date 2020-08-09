namespace CommandLineFluent.Arguments.Config
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;

	public sealed class MultiValueConfig<TClass, TProp> where TClass : class, new()
	{
		private PropertyInfo? targetProperty;
		private ArgumentRequired argumentRequired;
		private IReadOnlyCollection<TProp>? defaultValues;
		private Dependencies<TClass, TProp>? dependencies;
		private string? helpText;
		private string? name;
		private ICollection<string> ignoredPrefixes;
		private Func<IReadOnlyCollection<string>, Maybe<TProp, string>>? converter;
		public MultiValueConfig()
		{
			ignoredPrefixes = new List<string>();
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set</param>
		public MultiValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, TProp>> expression)
		{
			targetProperty = ArgUtils.PropertyInfoFromExpression(expression);
			return this;
		}
		/// <summary>
		/// Configures this to be required. By default, Options are required.
		/// </summary>
		public MultiValueConfig<TClass, TProp> IsRequired()
		{
			argumentRequired = ArgumentRequired.Required;
			defaultValues = null;
			dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this as optional, with a default value when not provided.
		/// </summary>
		/// <param name="defaultValues">The values to use as a default value when this is not provided. If not provided, this is an empty collection.</param>
		public MultiValueConfig<TClass, TProp> IsOptional(params TProp[] defaultValues)
		{
			argumentRequired = ArgumentRequired.Optional;
			this.defaultValues = defaultValues;
			dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this to only be required or must not appear under certain circumstances.
		/// If any rule is violated, parsing is considered to have failed. If all rules pass, then parsing is considered to have succeeded.
		/// You can specify that the user has to provide this Value depending upon the value of other properties (after parsing and conversion)
		/// </summary>
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
		/// <param name="helpText">The help text for this Option</param>
		public MultiValueConfig<TClass, TProp> WithHelpText(string helpText)
		{
			this.helpText = helpText;
			return this;
		}
		/// <summary>
		/// Configures this to have the provided human-readable name.
		/// By default this is used to produce Usage Text
		/// </summary>
		/// <param name="name">The human-readable name</param>
		public MultiValueConfig<TClass, TProp> WithName(string name)
		{
			this.name = name;
			return this;
		}
		public MultiValueConfig<TClass, TProp> IgnorePrefixes(params string[] prefixes)
		{
			foreach (string? p in prefixes)
			{
				ignoredPrefixes.Add(p);
			}
			return this;
		}
		public MultiValueConfig<TClass, TProp> IgnorePrefixes(IEnumerable<string> prefixes)
		{
			foreach (string? p in prefixes)
			{
				ignoredPrefixes.Add(p);
			}
			return this;
		}
		/// <summary>
		/// The converter to invoke on the provided value before assigning it to the property of <typeparamref name="TClass"/>.
		/// If not provided, no converter will be used. If no value is provided, the converter will not be invoked.
		/// </summary>
		/// <param name="converter">A convert that converts a string to <typeparamref name="TProp"/>.</param>
		public MultiValueConfig<TClass, TProp> WithConverter(Func<IReadOnlyCollection<string>, Maybe<TProp, string>> converter)
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
			if (dependencies != null)
			{
				IEnumerable<Error> errors = dependencies.Validate();
				if (errors.Any())
				{
					throw new CliParserBuilderException("Dependencies are not valid: " + string.Join(", ", errors));
				}
			}

			return new MultiValue<TClass, TProp>(name, helpText, argumentRequired, targetProperty, defaultValues ?? Array.Empty<TProp>(), dependencies, converter, ignoredPrefixes);
		}
	}
}
