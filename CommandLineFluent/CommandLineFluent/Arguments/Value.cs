namespace CommandLineFluent.Arguments
{
	using System;
	using System.Reflection;

	/// <summary>
	/// An argument with a single value
	/// </summary>
	public sealed class Value<TClass, TProp> : IValue<TClass> where TClass : class, new()
	{
		public string? Name { get; private set; }
		public string HelpText { get; private set; }
		public ArgumentRequired ArgumentRequired { get; private set; }
		public PropertyInfo TargetProperty { get; private set; }
		public TProp DefaultValue { get; private set; }
		public Dependencies<TClass, TProp>? Dependencies { get; private set; }
		public Func<string, Maybe<TProp, string>>? Converter { get; private set; }
		internal Value(string? name, string helpText, ArgumentRequired argumentRequired, PropertyInfo targetProperty, TProp defaultValue, Dependencies<TClass, TProp>? dependencies, Func<string, Maybe<TProp, string>>? converter)
		{
			Name = name;
			HelpText = helpText;
			ArgumentRequired = argumentRequired;
			TargetProperty = targetProperty;
			DefaultValue = defaultValue;
			Dependencies = dependencies;
			Converter = converter;
		}
		public Error SetValue(TClass target, string? rawValue)
		{
			if (rawValue != null)
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
						return new Error(ErrorCode.ValueFailedConversion, $"Converter for Value {Name} threw an exception ({ex.Message})");
					}
					if (converted.Get(out TProp val, out string error))
					{
						TargetProperty.SetValue(target, val);
					}
					else
					{
						return new Error(ErrorCode.ValueFailedConversion, error);
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
					return new Error(ErrorCode.MissingRequiredValue, $"Value {Name} is required and did not have a value provided");
				}
				else
				{
					TargetProperty.SetValue(target, DefaultValue);
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
