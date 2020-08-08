namespace CommandLineFluent.Arguments
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Reflection;

	/// <summary>
	/// An argument with a single value
	/// </summary>
	public sealed class Option<TClass, TProp> : IOption<TClass> where TClass : class, new()
	{
		public string? ShortName { get; }
		public string? LongName { get; }
		public string? Name { get; private set; }
		public string HelpText { get; private set; }
		public ArgumentRequired ArgumentRequired { get; private set; }
		public PropertyInfo? TargetProperty { get; private set; }
		public TProp DefaultValue { get; private set; }
		public Dependencies<TClass, TProp>? Dependencies { get; private set; }
		public Func<string, Converted<TProp>>? Converter { get; private set; }
		internal Option(string? shortName, string? longName)
		{
			ShortName = shortName;
			LongName = longName;
			HelpText = string.Empty;
			DefaultValue = default;
		}
		public Error SetValue(TClass target, string? value)
		{
			if (value != null)
			{
				if (Converter != null)
				{
					Converted<TProp> converted;
					try
					{
						converted = Converter.Invoke(value);
					}
					catch (Exception ex)
					{
						return new Error(ErrorCode.OptionFailedConversion, $"Converter for Option {Name} threw an exception ({ex.Message})");
					}
					if (converted.Get(out TProp val, out string error))
					{
						TargetProperty.SetValue(target, val);
					}
					else
					{
						return new Error(ErrorCode.OptionFailedConversion, error);
					}
				}
				else
				{
					TargetProperty.SetValue(target, value);
				}
			}
			else
			{
				if(ArgumentRequired == ArgumentRequired.Required)
				{
					return new Error(ErrorCode.MissingRequiredOption, $"Option {Name} ({ArgUtils.ShortAndLongName(ShortName, LongName)}) is required and did not have a value provided");
				}
				else
				{
					TargetProperty.SetValue(target, DefaultValue);
				}
			}
			return default;
		}
		/// <summary>
		/// Configures this to set the provided property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set</param>
		public Option<TClass, TProp> ForProperty(Expression<Func<TClass, TProp>> expression)
		{
			TargetProperty = ArgUtils.PropertyInfoFromExpression(expression);
			return this;
		}
		/// <summary>
		/// Configures this to be required. By default, Options are required.
		/// </summary>
		public Option<TClass, TProp> IsRequired()
		{
			ArgumentRequired = ArgumentRequired.Required;
			DefaultValue = default;
			Dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this as optional, with a default value when not provided.
		/// </summary>
		/// <param name="defaultValue">The value to use as a default value when this Option is not provided. If not provided, this is the default value for <typeparamref name="TProp"/></param>
		public Option<TClass, TProp> IsOptional(TProp defaultValue = default)
		{
			ArgumentRequired = ArgumentRequired.Optional;
			DefaultValue = defaultValue;
			Dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this to only be required or must not appear under certain circumstances.
		/// If any rule is violated, parsing is considered to have failed. If all rules pass, then parsing is considered to have succeeded.
		/// You can specify that the user has to provide this Value depending upon the value of other properties (after parsing and conversion)
		/// </summary>
		public Option<TClass, TProp> WithDependencies(TProp defaultValue, Action<Dependencies<TClass, TProp>> config)
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
				throw new ArgumentNullException(nameof(config), "config cannot be null");
			}
		}
		/// <summary>
		/// Configures this to show the provided Help Text.
		/// </summary>
		/// <param name="helpText">The help text for this Option</param>
		public Option<TClass, TProp> WithHelpText(string helpText)
		{
			HelpText = helpText;
			return this;
		}
		/// <summary>
		/// Configures this to have the provided human-readable name.
		/// By default this is used to produce Usage Text
		/// </summary>
		/// <param name="name">The human-readable name</param>
		public Option<TClass, TProp> WithName(string name)
		{
			Name = name;
			return this;
		}
		/// <summary>
		/// The converter to invoke on the provided value before assigning it to the property of <typeparamref name="TClass"/>.
		/// If not provided, no converter will be used. If no value is provided, the converter will not be invoked.
		/// </summary>
		/// <param name="converter">A convert that converts a string to <typeparamref name="TProp"/>.</param>
		public Option<TClass, TProp> WithConverter(Func<string, Converted<TProp>> converter)
		{
			Converter = converter;
			return this;
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
			return Dependencies.EvaluateRelationship(obj, gotValue, ArgumentType.Option);
		}
		/// <summary>
		/// The short and long name joined with a |
		/// </summary>
		public string ShortAndLongName()
		{
			return ArgUtils.ShortAndLongName(ShortName, LongName, ArgumentRequired == ArgumentRequired.Optional);
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