namespace CommandLineFluent.Arguments
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	/// <summary>
	/// Captures all lone values, such as: Foo.exe Value1 -t someswitch Value2 Value3. All 3 values there would be captured as a list: [Value1, Value2, Value3].
	/// Use this when you want to consume an indeterminate number of values, regardless of their position.
	/// </summary>
	public class MultiValue<TClass, TProp, TPropCollection> : IMultiValue<TClass> where TClass : class, new()
	{
		public string? DescriptiveName { get; }
		public string HelpText { get; }
		public ArgumentRequired ArgumentRequired { get; }
		public PropertyInfo TargetProperty { get; }
		/// <summary>
		/// The default values to use when nothing is provided.
		/// </summary>
		public TPropCollection DefaultValues { get; }
		/// <summary>
		/// Any dependencies upon other properties, if some have been set up. Otherwise, null.
		/// </summary>
		public Dependencies<TClass>? Dependencies { get; }
		/// <summary>
		/// Converts from a string into <typeparamref name="TProp"/>, or returns an error message.
		/// </summary>
		public Func<string, Converted<TProp, string>> Converter { get; }
		/// <summary>
		/// Creates a new collection, filled with the values provided.
		/// If you don't provide one, the specific type isn't guaranteed
		/// </summary>
		public Func<IEnumerable<TProp>, TPropCollection> CreateCollection { get; }
		public MultiValue(string? name, string helpText, ArgumentRequired argumentRequired, PropertyInfo targetProperty, TPropCollection defaultValues,
			Dependencies<TClass>? dependencies, Func<string, Converted<TProp, string>> converter, Func<IEnumerable<TProp>, TPropCollection> createCollection)
		{
			DescriptiveName = name;
			HelpText = helpText;
			ArgumentRequired = argumentRequired;
			TargetProperty = targetProperty;
			DefaultValues = defaultValues;
			Dependencies = dependencies;
			Converter = converter;
			CreateCollection = createCollection;
		}
		public Error SetValue(TClass target, List<string> rawValue)
		{
			if (rawValue.Count > 0)
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
					return new Error(ErrorCode.MultiValueFailedConversion, $"Converter for MultiValue {DescriptiveName} threw an exception ({ex.ToString()})");
				}
				TargetProperty.SetValue(target, CreateCollection(convertedValues));
			}
			else
			{
				if (ArgumentRequired == ArgumentRequired.Required)
				{
					return new Error(ErrorCode.MissingRequiredMultiValue, $"MultiValue {DescriptiveName} is required and did not have any values provided");
				}
				else
				{
					TargetProperty.SetValue(target, DefaultValues);
				}
			}
			return default;
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
