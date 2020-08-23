namespace CommandLineFluent.Arguments.Config
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
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
		private Func<IReadOnlyCollection<TProp>, IEnumerable<TProp>> createCollection;
		private ArgumentRequired argumentRequired;
		private IEnumerable<TProp>? defaultValues;
		private Dependencies<TClass>? dependencies;
		private string? helpText;
		private string? name;
		//private IReadOnlyCollection<string> ignoredPrefixes;
		private Func<string, Converted<TProp, string>>? converter;
		/// <summary>
		/// Creates a new <see cref="MultiValueConfig{TClass, TProp}"/>. You shouldn't need to create this manually.
		/// </summary>
		/// <param name="converter">The converter to use to convert from string to <typeparamref name="TProp"/>.</param>
		public MultiValueConfig(Func<string, Converted<TProp, string>>? converter)
		{
			//ignoredPrefixes = new List<string>();
			this.converter = converter;
			createCollection = null!;
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public MultiValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, TProp[]>> expression)
		{
			return ForProperty(expression, x => x.ToArray());
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public MultiValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, IList<TProp>>> expression)
		{
			return ForProperty(expression, x => new List<TProp>(x));
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public MultiValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, IReadOnlyList<TProp>>> expression)
		{
			return ForProperty(expression, x => new List<TProp>(x));
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public MultiValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, ICollection<TProp>>> expression)
		{
			return ForProperty(expression, x => new List<TProp>(x));
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public MultiValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, IReadOnlyCollection<TProp>>> expression)
		{
			return ForProperty(expression, x => x);
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public MultiValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, IEnumerable<TProp>>> expression)
		{
			return ForProperty(expression, x => x);
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public MultiValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, List<TProp>>> expression)
		{
			return ForProperty(expression, x => new List<TProp>(x));
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// Any duplicate values are ignored.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public MultiValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, HashSet<TProp>>> expression)
		{
			return ForProperty(expression, x => new HashSet<TProp>(x));
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public MultiValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, Stack<TProp>>> expression)
		{
			return ForProperty(expression, x => new Stack<TProp>(x));
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public MultiValueConfig<TClass, TProp> ForProperty(Expression<Func<TClass, Queue<TProp>>> expression)
		{
			return ForProperty(expression, x => new Queue<TProp>(x));
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// The property must be a collection of <typeparamref name="TProp"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		/// <param name="createCollection">A delegate which accepts a ReadOnlyCollection of <typeparamref name="TProp"/>, and creates a new <typeparamref name="TPropCollection"/>.</param>
		public MultiValueConfig<TClass, TProp> ForProperty<TPropCollection>(Expression<Func<TClass, TPropCollection>> expression, Func<IReadOnlyCollection<TProp>, IEnumerable<TProp>> createCollection) where TPropCollection : IEnumerable<TProp>
		{
			Guard.ThrowIfNull(expression, nameof(expression));
			Guard.ThrowIfNull(createCollection, nameof(createCollection));
			targetProperty = ArgUtils.PropertyInfoFromExpression(expression);
			this.createCollection = createCollection;
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
		public MultiValueConfig<TClass, TProp> IsOptional(IEnumerable<TProp> defaultValues)
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
		public MultiValueConfig<TClass, TProp> WithDependencies(IEnumerable<TProp> defaultValues, Action<Dependencies<TClass>> config)
		{
			Guard.ThrowIfNull(config, nameof(config));
			argumentRequired = ArgumentRequired.HasDependencies;
			this.defaultValues = defaultValues;
			config.Invoke(dependencies = new Dependencies<TClass>());
			return this;
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
		public MultiValueConfig<TClass, TProp> WithConverter(Func<string, Converted<TProp, string>>? converter)
		{
			this.converter = converter;
			return this;
		}
		internal MultiValue<TClass, TProp> Build()
		{
			if (string.IsNullOrEmpty(helpText))
			{
				throw new CliParserBuilderException("You need to provide some help text for Option " + name);
			}
			if (targetProperty == null)
			{
				throw new CliParserBuilderException("You need to set a target property for Option " + name);
			}
			Debug.Assert(createCollection != null, "If targetProperty is not null, createCollection should also not be null");
			if (converter == null && typeof(TProp) != typeof(string))
			{
				throw new CliParserBuilderException(string.Concat("You need to provide a converter from string to ", typeof(TProp).ToString(), " for MultiValue ", name));
			}
			if (dependencies != null)
			{
				dependencies.Validate();
			}

			return new MultiValue<TClass, TProp>(name, helpText, argumentRequired, targetProperty, defaultValues ?? Array.Empty<TProp>(), dependencies, converter, createCollection);
		}
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
