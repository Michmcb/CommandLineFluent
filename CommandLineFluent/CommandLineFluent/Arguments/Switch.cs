namespace CommandLineFluent.Arguments
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Reflection;

	/// <summary>
	/// A switch that can be toggled on or off
	/// </summary>
	public sealed class Switch<TClass, TProp> : ISwitch<TClass> where TClass : class, new()
	{
		public string? ShortName { get; }
		public string? LongName { get; }
		public string? Name { get; private set; }
		public string HelpText { get; private set; }
		public ArgumentRequired ArgumentRequired { get; private set; }
		public PropertyInfo? TargetProperty { get; private set; }
		public TProp DefaultValue { get; private set; }
		public Dependencies<TClass, TProp>? Dependencies { get; private set; }
		public Func<bool, Converted<TProp>>? Converter { get; private set; }
		internal Switch(string? shortName, string? longName)
		{
			ShortName = shortName;
			LongName = longName;
			HelpText = string.Empty;
			ArgumentRequired = ArgumentRequired.Optional;
			DefaultValue = default;
		}
		public Error SetValue(TClass target, string? rawValue)
		{
			if (rawValue != null)
			{
				if (Converter != null)
				{
					Converted<TProp> converted;
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
		/// Configures this to set the property of <typeparamref name="TClass"/>.
		/// </summary>
		/// <param name="expression">The property to set.</param>
		public Switch<TClass, TProp> ForProperty(Expression<Func<TClass, TProp>> expression)
		{
			TargetProperty = ArgUtils.PropertyInfoFromExpression(expression);
			return this;
		}
		/// <summary>
		/// Configures this to be required. By default, Switches are optional.
		/// </summary>
		public Switch<TClass, TProp> IsRequired()
		{
			ArgumentRequired = ArgumentRequired.Required;
			DefaultValue = default;
			Dependencies = null;
			return this;
		}
		/// <summary>
		/// Configures this as optional, with a default value when not provided.
		/// </summary>
		/// <param name="defaultValue">The value to use as a default value when this Switch is not provided. If not provided, this is the default value for <typeparamref name="TProp"/></param>
		public Switch<TClass, TProp> IsOptional(TProp defaultValue = default)
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
				throw new ArgumentNullException(nameof(config), "config cannot be null");
			}
		}
		/// <summary>
		/// Configures this with the provided <paramref name="helpText"/>
		/// </summary>
		/// <param name="helpText">The help text for this Option.</param>
		public Switch<TClass, TProp> WithHelpText(string helpText)
		{
			HelpText = helpText;
			return this;
		}
		/// <summary>
		/// Configures this to have the provided human-readable name.
		/// </summary>
		/// <param name="name">The human-readable name.</param>
		public Switch<TClass, TProp> WithName(string name)
		{
			Name = name;
			return this;
		}
		/// <summary>
		/// The converter to invoke on the provided value before assigning it to the property of <typeparamref name="TClass"/>.
		/// If not provided, no converter will be used. If no value is provided, the converter will not be invoked.
		/// </summary>
		/// <param name="converter">A convert that converts a string to <typeparamref name="TProp"/>.</param>
		public Switch<TClass, TProp> WithConverter(Func<bool, Converted<TProp>> converter)
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
