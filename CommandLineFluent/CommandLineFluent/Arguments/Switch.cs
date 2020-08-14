namespace CommandLineFluent.Arguments
{
	using System;
	using System.Reflection;

	/// <summary>
	/// A switch that can be toggled on or off
	/// </summary>
	public sealed class Switch<TClass, TProp> : ISwitch<TClass> where TClass : class, new()
	{
		public string? ShortName { get; }
		public string? LongName { get; }
		public string? Name { get; }
		public string HelpText { get; }
		public ArgumentRequired ArgumentRequired { get; }
		public PropertyInfo TargetProperty { get; }
		/// <summary>
		/// The default value to use when nothing is provided.
		/// </summary>
		public TProp DefaultValue { get; }
		/// <summary>
		/// Any dependencies upon other properties, if some have been set up. Otherwise, null.
		/// </summary>
		public Dependencies<TClass, TProp>? Dependencies { get; }
		/// <summary>
		/// Converts from a bool into <typeparamref name="TProp"/>, or returns an error message.
		/// </summary>
		public Func<bool, Maybe<TProp, string>>? Converter { get; }
		internal Switch(string? shortName, string? longName, string? name, string helpText, ArgumentRequired argumentRequired, PropertyInfo targetProperty, TProp defaultValue, Dependencies<TClass, TProp>? dependencies, Func<bool, Maybe<TProp, string>>? converter)
		{
			ShortName = shortName;
			LongName = longName;
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
						converted = Converter.Invoke(true);
					}
					catch (Exception ex)
					{
						return new Error(ErrorCode.SwitchFailedConversion, $"Converter for Switch {Name} threw an exception ({ex.Message})");
					}
					if (converted.Get(out TProp val, out string error))
					{
						TargetProperty.SetValue(target, val);
					}
					else
					{
						return new Error(ErrorCode.SwitchFailedConversion, error);
					}
				}
				else
				{
					TargetProperty.SetValue(target, true);
				}
			}
			else
			{
				if (ArgumentRequired == ArgumentRequired.Required)
				{
					return new Error(ErrorCode.MissingRequiredOption, $"Switch {Name} ({ArgUtils.ShortAndLongName(ShortName, LongName)}) is required and was not present");
				}
				else
				{
					TargetProperty.SetValue(target, DefaultValue);
				}
			}
			return default;
		}
		/// <summary>
		/// The short and long name joined with a |
		/// </summary>
		public string ShortAndLongName()
		{
			return ArgUtils.ShortAndLongName(ShortName, LongName, ArgumentRequired == ArgumentRequired.Optional);
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
			return Dependencies.EvaluateRelationship(obj, gotValue, ArgumentType.Switch);
		}
	}
}
