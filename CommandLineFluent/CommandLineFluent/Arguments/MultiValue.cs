namespace CommandLineFluent.Arguments
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Reflection;
	/// <summary>
	/// Captures all lone values, such as: Foo.exe Value1 -t someswitch Value2 Value3. All 3 values there would be captured as an array: [Value1, Value2, Value3].
	/// Use this when you want to consume an indeterminate number of values, regardless of their position.
	/// </summary>
	public class MultiValue<TClass, TProp> : IMultiValue<TClass> where TClass : class, new()
	{
		public string? Name { get; }
		public string HelpText { get; }
		public ArgumentRequired ArgumentRequired { get; }
		public PropertyInfo TargetProperty { get; }
		/// <summary>
		/// The default values to use when nothing is provided.
		/// </summary>
		public IReadOnlyCollection<TProp> DefaultValues { get; }
		/// <summary>
		/// Any dependencies upon other properties, if some have been set up. Otherwise, null.
		/// </summary>
		public Dependencies<TClass, TProp>? Dependencies { get; }
		/// <summary>
		/// Converts from a string into <typeparamref name="TProp"/>, or returns an error message.
		/// </summary>
		public Func<string, Maybe<TProp, string>>? Converter { get; }
		//public ICollection<string> IgnoredPrefixes { get; }
		public MultiValue(string? name, string helpText, ArgumentRequired argumentRequired, [DisallowNull]PropertyInfo targetProperty, IReadOnlyCollection<TProp> defaultValues, [AllowNull] Dependencies<TClass, TProp>? dependencies, [AllowNull] Func<string, Maybe<TProp, string>>? converter)//, ICollection<string> ignoredPrefixes)
		{
			Name = name;
			HelpText = helpText;
			ArgumentRequired = argumentRequired;
			TargetProperty = targetProperty;
			DefaultValues = defaultValues;
			Dependencies = dependencies;
			Converter = converter;
			//IgnoredPrefixes = ignoredPrefixes;
		}
		public Error SetValue([DisallowNull] TClass target, IReadOnlyCollection<string> rawValue)
		{
			if (rawValue.Count > 0)
			{
				if (Converter != null)
				{
					List<TProp> convertedValues = new List<TProp>(rawValue.Count);
					try
					{
						foreach (string rv in rawValue)
						{
							if (Converter.Invoke(rv).Success(out TProp val, out string error))
							{
								convertedValues.Add(val);
							}
							else
							{
								return new Error(ErrorCode.MultiValueFailedConversion, error);
							}
						}
					}
					catch (Exception ex)
					{
						return new Error(ErrorCode.MultiValueFailedConversion, $"Converter for MultiValue {Name} threw an exception ({ex.Message})");
					}
					TargetProperty.SetValue(target, convertedValues);
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
		//public bool HasIgnoredPrefix(string str)
		//{
		//	foreach (string prefix in IgnoredPrefixes)
		//	{
		//		if (str.StartsWith(prefix))
		//		{
		//			return true;
		//		}
		//	}
		//	return false;
		//}
		/// <summary>
		/// Checks to make sure that all dependencies are respected. If they are not, returns an Error
		/// describing the first dependency that was violated.
		/// If no dependencies have been set up, returns null.
		/// </summary>
		/// <param name="obj">The object to check</param>
		public Error EvaluateDependencies([DisallowNull] TClass obj, bool gotValue)
		{
			if (Dependencies == null)
			{
				return default;
			}
			return Dependencies.EvaluateRelationship(obj, gotValue, ArgumentType.MultiValue);
		}
	}
}
