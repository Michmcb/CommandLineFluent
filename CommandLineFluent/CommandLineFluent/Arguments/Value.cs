namespace CommandLineFluent.Arguments
{
	using System;

	/// <summary>
	/// An argument with a single value
	/// </summary>
	public sealed class Value<TClass, TProp> : IValue<TClass> where TClass : class, new()
	{
		public string? DescriptiveName { get; }
		public string HelpText { get; }
		public ArgumentRequired ArgumentRequired { get; }
		public Action<TClass, TProp> PropertySetter { get; }
		/// <summary>
		/// The default value to use when nothing is provided.
		/// </summary>
		public TProp DefaultValue { get; }
		/// <summary>
		/// Any dependencies upon other properties, if some have been set up. Otherwise, null.
		/// </summary>
		public Dependencies<TClass>? Dependencies { get; }
		/// <summary>
		/// Converts from a string into <typeparamref name="TProp"/>, or returns an error message.
		/// </summary>
		public Func<string, Converted<TProp, string>> Converter { get; }
		internal Value(string? name, string helpText, ArgumentRequired argumentRequired, Action<TClass, TProp> propertySetter,
			TProp defaultValue, Dependencies<TClass>? dependencies, Func<string, Converted<TProp, string>> converter)
		{
			DescriptiveName = name;
			HelpText = helpText;
			ArgumentRequired = argumentRequired;
			PropertySetter = propertySetter;
			DefaultValue = defaultValue;
			Dependencies = dependencies;
			Converter = converter;
		}
		public Error SetValue(TClass target, string? rawValue)
		{
			if (rawValue != null)
			{
				Converted<TProp, string> converted;
				try
				{
					converted = Converter.Invoke(rawValue);
				}
				catch (Exception ex)
				{
					return new Error(ErrorCode.ValueFailedConversion, $"Converter for Value {DescriptiveName} threw an exception ({ex.ToString()})");
				}
				if (converted.Success(out TProp val, out string error))
				{
					PropertySetter(target, val);
				}
				else
				{
					return new Error(ErrorCode.ValueFailedConversion, error);
				}
			}
			else
			{
				if (ArgumentRequired == ArgumentRequired.Required)
				{
					return new Error(ErrorCode.MissingRequiredValue, $"Value {DescriptiveName} is required and did not have a value provided");
				}
				else
				{
					PropertySetter(target, DefaultValue);
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
			return Dependencies.EvaluateRelationship(obj, gotValue, ArgumentType.Value);
		}
	}
}
