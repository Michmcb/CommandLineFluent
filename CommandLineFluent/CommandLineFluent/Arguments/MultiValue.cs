
namespace CommandLineFluent.Arguments
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Reflection;

	/// <summary>
	/// Captures all lone values.
	/// Use this when you want to consume an indeterminate number of values, regardless of their position.
	/// </summary>
	public class MultiValue<TClass, TProp> : IMultiValue<TClass> where TClass : class, new()
	{
		public string? Name { get; private set; }
		public string HelpText { get; private set; }
		public ArgumentRequired ArgumentRequired { get; private set; }
		public PropertyInfo? TargetProperty { get; private set; }
		public IReadOnlyCollection<TProp> DefaultValues { get; private set; }
		public Dependencies<TClass, TProp>? Dependencies { get; private set; }
		public Func<IReadOnlyCollection<string>, Converted<TProp>>? Converter { get; private set; }
		public ICollection<string> IgnoredPrefixes { get; }
		internal MultiValue()
		{
			IgnoredPrefixes = new List<string>();
			HelpText = string.Empty;
			ArgumentRequired = ArgumentRequired.Required;
			DefaultValues = Array.Empty<TProp>();
		}
		public Error SetValue(TClass target, IReadOnlyCollection<string> rawValue)
		{
			if (rawValue.Count > 0)
			{
				if (Converter != null)
				{
					Converted<TProp> converted;
					try
					{
						converted = Converter.Invoke(rawValue);
					}
					catch (Exception ex)
					{
						return new Error(ErrorCode.MultiValueFailedConversion, $"Converter for MultiValue {Name} threw an exception ({ex.Message})");
					}
					if (converted.Get(out TProp val, out string error))
					{
						TargetProperty.SetValue(target, val);
					}
					else
					{
						return new Error(ErrorCode.MultiValueFailedConversion, error);
					}
				}
				else
				{
					TargetProperty.SetValue(target, rawValue);
				}
			}
			else
			{
				if (ArgumentRequired == ArgumentRequired.Required)
				{
					return new Error(ErrorCode.MissingRequiredMultiValue, $"MultiValue {Name} is required and did not have any values provided");
				}
				else
				{
					TargetProperty.SetValue(target, DefaultValues);
				}
			}
			return default;
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set</param>
		public MultiValue<TClass, TProp> ForProperty(Expression<Func<TClass, TProp>> expression)
		{
			TargetProperty = ArgUtils.PropertyInfoFromExpression(expression);
			return this;
		}
		/// <summary>
		/// Configures this to be required. By default, Options are required.
		/// </summary>
		public MultiValue<TClass, TProp> IsRequired()
		{
			ArgumentRequired = ArgumentRequired.Required;
			DefaultValues = null;
			Dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this as optional, with a default value when not provided.
		/// </summary>
		/// <param name="defaultValues">The values to use as a default value when this is not provided. If not provided, this is an empty collection.</param>
		public MultiValue<TClass, TProp> IsOptional(params TProp[] defaultValues)
		{
			ArgumentRequired = ArgumentRequired.Optional;
			DefaultValues = defaultValues;
			Dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this to only be required or must not appear under certain circumstances.
		/// If any rule is violated, parsing is considered to have failed. If all rules pass, then parsing is considered to have succeeded.
		/// You can specify that the user has to provide this Value depending upon the value of other properties (after parsing and conversion)
		/// </summary>
		public MultiValue<TClass, TProp> WithDependencies(IReadOnlyCollection<TProp> defaultValues, Action<Dependencies<TClass, TProp>> config)
		{
			if (config != null)
			{
				ArgumentRequired = ArgumentRequired.HasDependencies;
				DefaultValues = defaultValues;
				config.Invoke(Dependencies = new Dependencies<TClass, TProp>());
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
		public MultiValue<TClass, TProp> WithHelpText(string helpText)
		{
			HelpText = helpText;
			return this;
		}
		/// <summary>
		/// Configures this to have the provided human-readable name.
		/// By default this is used to produce Usage Text
		/// </summary>
		/// <param name="name">The human-readable name</param>
		public MultiValue<TClass, TProp> WithName(string name)
		{
			Name = name;
			return this;
		}
		public MultiValue<TClass, TProp> IgnorePrefixes(params string[] prefixes)
		{
			foreach (string? p in prefixes)
			{
				IgnoredPrefixes.Add(p);
			}
			return this;
		}
		public MultiValue<TClass, TProp> IgnorePrefixes(IEnumerable<string> prefixes)
		{
			foreach (string? p in prefixes)
			{
				IgnoredPrefixes.Add(p);
			}
			return this;
		}
		/// <summary>
		/// The converter to invoke on the provided value before assigning it to the property of <typeparamref name="TClass"/>.
		/// If not provided, no converter will be used. If no value is provided, the converter will not be invoked.
		/// </summary>
		/// <param name="converter">A convert that converts a string to <typeparamref name="TProp"/>.</param>
		public MultiValue<TClass, TProp> WithConverter(Func<IReadOnlyCollection<string>, Converted<TProp>> converter)
		{
			Converter = converter;
			return this;
		}
		public bool HasIgnoredPrefix(string str)
		{
			foreach (string prefix in IgnoredPrefixes)
			{
				if (str.StartsWith(prefix))
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Checks to make sure that all dependencies are respected. If they are not, returns an Error
		/// describing the first dependency that was violated.
		/// If no dependencies have been set up, returns null.
		/// </summary>
		/// <param name="obj">The object to check</param>
		public Error EvaluateDependencies(TClass obj, bool gotValue)
		{
			if (Dependencies == null)
			{
				return default;
			}
			return Dependencies.EvaluateRelationship(obj, gotValue, ArgumentType.MultiValue);
		}
		public IEnumerable<Error> Validate()
		{
			if (TargetProperty == null)
			{
				yield return new Error(ErrorCode.ProgrammerError, $"{Name} does not have a target property set");
			}
			if (Dependencies != null)
			{
				foreach (Error error in Dependencies.Validate())
				{
					yield return error;
				}
			}
		}
	}
}
