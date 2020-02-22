using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// An option with one value, supplied like -t Value
	/// </summary>
	/// <typeparam name="T">The class of the property to which this argument maps</typeparam>
	/// <typeparam name="C">The type of the property this argument will set</typeparam>
	public class FluentOption<T, C> : FluentArgument, IFluentOption, IFluentSettable<T, string> where T : new()
	{
		private Func<string, Converted<C>> _converter;
		private Func<string, string> _validator;
		private bool _configuredRequiredness;
		private bool _hasDefaultValue;

		/// <summary>
		/// Short name for this Option
		/// </summary>
		public string ShortName { get; private set; }
		/// <summary>
		/// Long name for this Option
		/// </summary>
		public string LongName { get; private set; }
		/// <summary>
		/// Whether or not this Option is required to be included. By default, it is required.
		/// If null, then Dependencies will be used to determine whether or not this Option is required.
		/// </summary>
		public bool? Required { get; private set; }
		/// <summary>
		/// If not required, this is the default value used when the Option is not provided
		/// </summary>
		public C DefaultValue { get; private set; }
		/// <summary>
		/// Dependencies on other properties which dictate whether or not this is required.
		/// </summary>
		public FluentDependencies<T, C> Dependencies { get; private set; }
		internal FluentOption(string shortName, string longName)
		{
			ShortName = shortName;
			LongName = longName;
			Required = true;
		}
		/// <summary>
		/// Attempts to set the value of the target property of the target object to the provided value. Uses any converter/validator provided.
		/// Order is: Validate, Convert, Assign.
		/// You shouldn't call this, Parse will automatically invoke this with appropriate values.
		/// </summary>
		/// <param name="target">The instance of <typeparamref name="T"/> on which to set the property</param>
		/// <param name="value">The raw value of the argument. If necessary, will be converted to the <typeparamref name="C"/> using the Converter set</param>
		public Error SetValue(T target, string value)
		{
			GotValue = false;
			if (value != null)
			{
				string validateError = null;
				try
				{
					validateError = _validator?.Invoke(value);
					if (validateError != null)
					{
						return new Error(ErrorCode.OptionFailedValidation, true, validateError);
					}
				}
				catch (Exception ex)
				{
					return new Error(ErrorCode.OptionFailedValidation, false, $"Validator for Option {Util.ShortAndLongName(this, this.Name)} threw an exception ({ex.Message})", ex);
				}
				if (_converter != null)
				{
					Converted<C> converted;
					try
					{
						converted = _converter.Invoke(value);
						if (!converted.Successful)
						{
							return new Error(ErrorCode.OptionFailedConversion, true, converted.ErrorMessage);
						}
					}
					catch (Exception ex)
					{
						return new Error(ErrorCode.OptionFailedConversion, false, $"Converter for Option {Util.ShortAndLongName(this, this.Name)} threw an exception ({ex.Message})", ex);
					}
					TargetProperty.SetValue(target, converted.ConvertedValue);
					GotValue = true;
				}
				else
				{
					TargetProperty.SetValue(target, value);
					GotValue = true;
				}
			}
			else if (Dependencies != null || Required == false)
			{
				// Either it's not required, or we have dependencies. In either case, assign the default value for now.
				if (_hasDefaultValue)
				{
					TargetProperty.SetValue(target, DefaultValue);
				}
			}
			else
			{
				// It is required, so return an error
				return new Error(ErrorCode.MissingRequiredOption, true, $"Option {Util.ShortAndLongName(this, this.Name)} is required and did not have a value provided");
			}
			return null;
		}
		/// <summary>
		/// Configures this Option to set the provided property of <typeparamref name="T"/>.
		/// </summary>
		/// <param name="expression">The property to set</param>
		public FluentOption<T, C> ForProperty(Expression<Func<T, C>> expression)
		{
			TargetProperty = Util.PropertyInfoFromExpression(expression);
			return this;
		}
		/// <summary>
		/// Configures this Option to be required. By default, Options are required unless you use IsOptional.
		/// </summary>
		public FluentOption<T, C> IsRequired()
		{
			ThrowIfRequirednessAlreadyConfigured();
			_configuredRequiredness = true;
			Required = true;
			return this;
		}
		/// <summary>
		/// Configures this Option as optional, with a default value when not provided.
		/// If you are setting up Conditional requirements, use WithDefaultValue instead to specify a fallback value.
		/// </summary>
		/// <param name="defaultValue">The value to use as a default value when this Option is not provided. If not provided, this is the default value for <typeparamref name="C"/></param>
		public FluentOption<T, C> IsOptional(C defaultValue = default)
		{
			ThrowIfRequirednessAlreadyConfigured();
			_configuredRequiredness = true;
			Required = false;
			WithDefaultValue(defaultValue);
			return this;
		}
		/// <summary>
		/// Configures a default value without specifying that this Option is optional. Use this instead of IsOptional when you use ConditionallyRequired()
		/// </summary>
		/// <param name="defaultValue">The value to use as a default when nothing else has been provided</param>
		public FluentOption<T, C> WithDefaultValue(C defaultValue = default)
		{
			_hasDefaultValue = true;
			DefaultValue = defaultValue;
			return this;
		}
		/// <summary>
		/// Configures this Option to show the provided Help Text.
		/// </summary>
		/// <param name="helpText">The help text for this Option</param>
		public FluentOption<T, C> WithHelpText(string helpText)
		{
			HelpText = helpText;
			return this;
		}
		/// <summary>
		/// Configures this Option to have the provided human-readable name.
		/// By default this is used to produce Usage Text
		/// </summary>
		/// <param name="name">The human-readable name</param>
		public FluentOption<T, C> WithName(string name)
		{
			Name = name;
			return this;
		}
		/// <summary>
		/// The converter to invoke on the provided value before assigning it to the property of <typeparamref name="T"/>.
		/// If not provided, no converter will be used. If this Option is not provided, the converter will not be invoked.
		/// The converter is considered to have failed if it throws an exception, or if the error message is not null.
		/// In all other cases, it is considered successful.
		/// </summary>
		/// <param name="converter">A convert that converts a string to <typeparamref name="C"/></param>
		public FluentOption<T, C> WithConverter(Func<string, Converted<C>> converter)
		{
			_converter = converter;
			return this;
		}
		/// <summary>
		/// The validator to invoke on the provided value before it is converted. This is mainly useful if you're
		/// not converting the string, and just need to validate it. If this Option is not provided, the validator will not be invoked.
		/// The validator is considered to have succeeded if the returned string is null. If the returned string is not null,
		/// it is considered to have failed and the returned string is used as the error message.
		/// </summary>
		/// <param name="validator">The validator that validates the raw value</param>
		public FluentOption<T, C> WithValidator(Func<string, string> validator)
		{
			_validator = validator;
			return this;
		}
		/// <summary>
		/// The short and long name joined with a |
		/// </summary>
		public string ShortAndLongName()
		{
			return Util.ShortAndLongName(this, this.Name);
		}
		/// <summary>
		/// Configures this Option to only be required or must not appear under certain circumstances.
		/// If any rule is violated, parsing is considered to have failed. If all rules pass, then parsing is considered to have succeeded.
		/// You can specify that the user has to provide this Value depending upon the value of other properties (after parsing, validation, and conversion)
		/// </summary>
		public FluentOption<T, C> WithDependencies(Action<FluentDependencies<T, C>> config)
		{
			ThrowIfRequirednessAlreadyConfigured();
			if (config != null)
			{
				Required = null;
				_configuredRequiredness = true;
				config.Invoke(Dependencies = new FluentDependencies<T, C>());
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
		public Error EvaluateDependencies(T obj)
		{
			if (Dependencies == null)
			{
				return null;
			}
			return Dependencies.EvaluateRelationship(obj, GotValue, FluentArgumentType.Option);
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
		/// <summary>
		/// Throws if _configuredRequiredness is true.
		/// </summary>
		private void ThrowIfRequirednessAlreadyConfigured()
		{
			if (_configuredRequiredness)
			{
				throw new InvalidOperationException($@"Do not call {nameof(IsRequired)}, {nameof(IsOptional)}, or {nameof(WithDependencies)} more than once.");
			}
		}
	}
}