namespace CommandLineFluent.Arguments
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Reflection;

	/// <summary>
	/// A switch toggled on or off, supplied like -s
	/// </summary>
	public class Switch<TClass, TProp> : ISwitch<TClass> where TClass : class, new()
	{
		/// <summary>
		/// Short name for this Option
		/// </summary>
		public string? ShortName { get; }
		/// <summary>
		/// Long name for this Option
		/// </summary>
		public string? LongName { get; }
		public string HelpText { get; private set; }
		/// <summary>
		/// If not required, this is the default value used when the Switch is not provided
		/// </summary>
		public TProp DefaultValue { get; private set; }
		/// <summary>
		/// Dependencies on other properties which dictate whether or not this is required.
		/// </summary>
		public Dependencies<TClass, TProp>? Dependencies { get; private set; }
		public string? Name { get; private set; }
		public ArgumentRequired ArgumentRequired { get; private set; }
		public PropertyInfo? TargetProperty { get; private set; }
		public Func<bool, Maybe<TProp, string>> Converter { get; private set; }
		internal Switch(string shortName, string longName)
		{
			ShortName = shortName;
			LongName = longName;
			ArgumentRequired = ArgumentRequired.Optional;
		}
		/// <summary>
		/// Attempts to set the value of the target property of the target object to the provided value. Uses any converter/validator provided.
		/// You shouldn't call this, Parse will automatically invoke this with appropriate values.
		/// </summary>
		/// <param name="target">The instance of <typeparamref name="TClass"/> on which to set the property</param>
		/// <param name="value">The raw value of the argument. If necessary, will be converted to the <typeparamref name="TProp"/> using the Converter set</param>
		public Error SetValue(TClass target, string? value)
		{
			if (value != null)
			{
				if (Converter != null)
				{
					Maybe<TProp, string> converted;
					try
					{
						converted = Converter.Invoke(true);

						if (!converted.Ok)
						{
						}
					}
					catch (Exception ex)
					{
						return new Error(ErrorCode.SwitchFailedConversion, false, $"Converter for Option {Name} threw an exception ({ex.Message})", ex);
					}
					if (converted.Get(out TProp val, out string error))
					{
						TargetProperty.SetValue(target, val);
					}
					else
					{
						return new Error(ErrorCode.SwitchFailedConversion, true, error);
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
					return new Error(ErrorCode.MissingRequiredOption, true, $"Switch {ArgUtils.ShortAndLongName(ShortName, LongName)} is required and was not present");
				}
				else
				{
					TargetProperty.SetValue(target, DefaultValue);
				}
			}
			return null;
		}
		/// <summary>
		/// Configures this Option to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set</param>
		public Switch<TClass, TProp> ForProperty(Expression<Func<TClass, TProp>> expression)
		{
			TargetProperty = ArgUtils.PropertyInfoFromExpression(expression);
			return this;
		}
		/// <summary>
		/// Configures this Option to be required. By default, Options are required unless you use IsOptional.
		/// </summary>
		public Switch<TClass, TProp> IsRequired()
		{
			ArgumentRequired = ArgumentRequired.Required;
			return this;
		}
		/// <summary>
		/// Configures this Option as optional, with a default value when not provided.
		/// If you are setting up Conditional requirements, use WithDefaultValue instead to specify a fallback value.
		/// </summary>
		/// <param name="defaultValue">The value to use as a default value when this Option is not provided. If not provided, this is the default value for <typeparamref name="TProp"/></param>
		public Switch<TClass, TProp> IsOptional(TProp defaultValue = default)
		{
			ArgumentRequired = ArgumentRequired.Optional;
			DefaultValue = defaultValue;
			return this;
		}
		/// <summary>
		/// Configures this Option to show the provided Help Text.
		/// </summary>
		/// <param name="helpText">The help text for this Option</param>
		public Switch<TClass, TProp> WithHelpText(string helpText)
		{
			HelpText = helpText;
			return this;
		}
		/// <summary>
		/// The converter to invoke on the provided value before assigning it to the property of <typeparamref name="TClass"/>.
		/// If not provided, no converter will be used. If this Option is not provided, the converter will not be invoked.
		/// The converter is considered to have failed if it throws an exception, or if the error message is not null.
		/// In all other cases, it is considered successful.
		/// </summary>
		/// <param name="converter">A convert that converts a string to <typeparamref name="TProp"/></param>
		public Switch<TClass, TProp> WithConverter(Func<bool, Maybe<TProp, string>> converter)
		{
			Converter = converter;
			return this;
		}
		/// <summary>
		/// The short and long name joined with a |
		/// </summary>
		public string ShortAndLongName()
		{
			return ArgUtils.ShortAndLongName(ShortName, LongName, ArgumentRequired == ArgumentRequired.Optional);
		}
		/// <summary>
		/// Configures this Option to only be required or must not appear under certain circumstances.
		/// If any rule is violated, parsing is considered to have failed. If all rules pass, then parsing is considered to have succeeded.
		/// You can specify that the user has to provide this Value depending upon the value of other properties (after parsing, validation, and conversion)
		/// </summary>
		public Switch<TClass, TProp> WithDependencies(TProp defaultValue, Action<Dependencies<TClass, TProp>> config)
		{
			if (config != null)
			{
				ArgumentRequired = ArgumentRequired.HasDependencies;
				DefaultValue = defaultValue;
				config.Invoke(Dependencies = new Dependencies<TClass, TProp>());
				return this;
			}
			else
			{
				throw new ArgumentNullException(nameof(config), @"config cannot be null");
			}
		}
		/// <summary>
		/// Checks to make sure that all dependencies are respected. If they are not, returns an Error
		/// describing the first dependency that was violated.
		/// If no dependencies have been set up, returns null.
		/// </summary>
		/// <param name="obj">The object to check</param>
		public Error EvaluateDependencies(TClass obj, bool gotValue)
		{
			if (Dependencies == null && ArgumentRequired == ArgumentRequired.HasDependencies)
			{
				return null;
			}
			return Dependencies.EvaluateRelationship(obj, gotValue, ArgumentType.Option);
		}
		/// <summary>
		/// Validates this FluentArgument, returning an Error object if something is invalid.
		/// </summary>
		public IEnumerable<Error> Validate()
		{
			if (TargetProperty == null)
			{
				yield return new Error(ErrorCode.ProgrammerError, false, $"{Name} does not have a target property set");
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
