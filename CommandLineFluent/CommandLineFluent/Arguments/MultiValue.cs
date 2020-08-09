namespace CommandLineFluent.Arguments
{
	using System;
	using System.Collections.Generic;
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
		public PropertyInfo TargetProperty { get; private set; }
		public IReadOnlyCollection<TProp> DefaultValues { get; private set; }
		public Dependencies<TClass, TProp>? Dependencies { get; private set; }
		public Func<IReadOnlyCollection<string>, Maybe<TProp, string>>? Converter { get; private set; }
		public ICollection<string> IgnoredPrefixes { get; }
		public MultiValue(string? name, string helpText, ArgumentRequired argumentRequired, PropertyInfo targetProperty, IReadOnlyCollection<TProp> defaultValues, Dependencies<TClass, TProp>? dependencies, Func<IReadOnlyCollection<string>, Maybe<TProp, string>>? converter, ICollection<string> ignoredPrefixes)
		{
			Name = name;
			HelpText = helpText;
			ArgumentRequired = argumentRequired;
			TargetProperty = targetProperty;
			DefaultValues = defaultValues;
			Dependencies = dependencies;
			Converter = converter;
			IgnoredPrefixes = ignoredPrefixes;
		}
		public Error SetValue(TClass target, IReadOnlyCollection<string> rawValue)
		{
			if (rawValue.Count > 0)
			{
				if (Converter != null)
				{
					Maybe<TProp, string> converted;
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
	}
}
