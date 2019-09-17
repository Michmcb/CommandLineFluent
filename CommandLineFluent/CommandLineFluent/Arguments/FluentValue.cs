using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// A lone value, such as: Foo.exe MyFile.
	/// </summary>
	/// <typeparam name="T">The class of the property to which this argument maps</typeparam>
	/// <typeparam name="C">The type of the property this argument will set</typeparam>
	public class FluentValue<T, C> : FluentArgument, IFluentValue, IFluentSettable<T, string> where T : new()
	{
		private Func<string, Converted<C>> _converter;
		private Func<string, string> _validator;
		/// <summary>
		/// Whether or not this Value is required to be included. By default, it is required.
		/// </summary>
		public bool Required { get; private set; }
		/// <summary>
		/// If not required, this is the default value used when the Value is not provided
		/// </summary>
		public C DefaultValue { get; private set; }
		internal FluentValue()
		{
			Name = null;
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
			if (value != null)
			{
				string validateError = null;
				try
				{
					validateError = _validator?.Invoke(value);
					if (validateError != null)
					{
						return new Error(ErrorCode.ValueFailedValidation, true, validateError);
					}
				}
				catch (Exception ex)
				{
					return new Error(ErrorCode.ValueFailedValidation, false, $"Validator for Value {Name} threw an exception ({ex.Message})", ex);
				}
				if (_converter != null)
				{
					Converted<C> converted;
					try
					{
						converted = _converter.Invoke(value);
						if (!converted.Successful)
						{
							return new Error(ErrorCode.ValuesFailedConversion, true, converted.ErrorMessage);
						}
					}
					catch (Exception ex)
					{
						return new Error(ErrorCode.ValuesFailedConversion, false, $"Converter for Value {Name} threw an exception ({ex.Message})", ex);
					}
					TargetProperty.SetValue(target, converted.ConvertedValue);
					return null;
				}
				else
				{
					TargetProperty.SetValue(target, value);
				}
			}
			else if (!Required)
			{
				// It's not required, so assign its default value
				TargetProperty.SetValue(target, DefaultValue);
			}
			else
			{
				return new Error(ErrorCode.MissingRequiredValue, true, $"Value {Name} is required and did not have a value provided");
			}
			return null;
		}
		/// <summary>
		/// Configures this Value to set the provided property of <typeparamref name="T"/>.
		/// </summary>
		/// <param name="expression">The property to set</param>
		public FluentValue<T, C> ForProperty(Expression<Func<T, C>> expression)
		{
			if (!(expression.Body is MemberExpression me))
			{
				throw new ArgumentException($"Expression has to be a property of type {typeof(T)}", nameof(expression));
			}
			PropertyInfo prop = me.Member as PropertyInfo;
			TargetProperty = prop ?? throw new ArgumentException($"Expression has to be a property of type {typeof(T)}", nameof(expression));
			return this;
		}
		/// <summary>
		/// Configures this Value to be required. By default, Values are required unless you use IsOptional.
		/// </summary>
		public FluentValue<T, C> IsRequired()
		{
			Required = true;
			DefaultValue = default;
			return this;
		}
		/// <summary>
		/// Configures this Value as optional, with a default value when not provided. Note that if the 0th Value is optional and the 1st Value is required,
		/// and you provide a single value, that single value will map to the 0th Value and throw an error because the 1st Value was missing. It's recommended you use this judiciously.
		/// </summary>
		/// <param name="defaultValue">The value to use as a default value when this Value is not provided. If not provided, this is the default value for <typeparamref name="C"/></param>
		public FluentValue<T, C> IsOptional(C defaultValue)
		{
			Required = false;
			DefaultValue = defaultValue;
			return this;
		}
		/// <summary>
		/// Configures this Value to show the provided Help Text.
		/// </summary>
		/// <param name="helpText">The help text for this Value</param>
		public FluentValue<T, C> WithHelpText(string helpText)
		{
			HelpText = helpText;
			return this;
		}
		/// <summary>
		/// Configures this Value to have the provided human-readable name.
		/// By default this is used to produce Usage Text
		/// </summary>
		/// <param name="name">The human-readable name</param>
		public FluentValue<T, C> WithName(string name)
		{
			Name = name;
			return this;
		}
		/// <summary>
		/// The converter to invoke on the argument value before assigning it to the property of <typeparamref name="T"/>.
		/// If not provided, no converter will be used.
		/// The converter is considered to have failed if it throws an exception, or if the error message is not null.
		/// In all other cases, it is considered successful.
		/// </summary>
		/// <param name="converter">A convert that converts a string to <typeparamref name="C"/></param>
		public FluentValue<T, C> WithConverter(Func<string, Converted<C>> converter)
		{
			_converter = converter;
			return this;
		}
		/// <summary>
		/// The validator to invoke on the argument value before it is converted. This is mainly useful if you're
		/// not converting the string, and just need to validate it.
		/// The validator is considered to have succeeded if the returned string is null. If the returned string is not null,
		/// it is considered to have failed and the returned string is used as the error message.
		/// </summary>
		/// <param name="validator">The validator that validates the raw value</param>
		public FluentValue<T, C> WithValidator(Func<string, string> validator)
		{
			_validator = validator;
			return this;
		}
	}
}
