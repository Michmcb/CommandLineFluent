using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// A switch toggled on or off, supplied like -s
	/// </summary>
	/// <typeparam name="T">The class of the property to which this argument maps</typeparam>
	/// <typeparam name="C">The type of the property this argument will set</typeparam>
	public class FluentSwitch<T, C> : FluentArgument, IFluentSwitch, IFluentSettable<T, bool> where T : new()
	{
		private Func<bool, Converted<C>> _converter;
		/// <summary>
		/// Short name for this switch
		/// </summary>
		public string ShortName { get; private set; }
		/// <summary>
		/// Long name for this switch
		/// </summary>
		public string LongName { get; private set; }
		/// <summary>
		/// If the switch is not present, this is the default value to use
		/// </summary>
		public C DefaultValue { get; private set; }
		internal FluentSwitch(string shortName, string longName)
		{
			ShortName = shortName;
			LongName = longName;
			Name = null;
		}
		/// <summary>
		/// Attempts to set the value of the target property of the target object to the provided value. Uses any converter/validator provided.
		/// Order is: Validate, Convert, Assign.
		/// You shouldn't call this, Parse will automatically invoke this with appropriate values.
		/// </summary>
		/// <param name="target">The instance of <typeparamref name="T"/> on which to set the property</param>
		/// <param name="value">If the switch is provided, this is true. Otherwise, false. If necessary, will be converted to type <typeparamref name="C"/> using the Converter</param>
		public Error SetValue(T target, bool value)
		{
			if (value)
			{
				if (_converter != null)
				{
					Converted<C> converted;
					try
					{
						converted = _converter.Invoke(value);
						if (!converted.Successful)
						{
							return new Error(ErrorCode.SwitchFailedConversion, true, converted.ErrorMessage);
						}
					}
					catch (Exception ex)
					{
						return new Error(ErrorCode.SwitchFailedConversion, false, $"Converter for Switch {Util.ShortAndLongName(this)} threw an exception ({ex.Message})", ex);
					}
					TargetProperty.SetValue(target, converted.ConvertedValue);
					return null;
				}
				else
				{
					TargetProperty.SetValue(target, value);
				}
			}
			else
			{
				TargetProperty.SetValue(target, DefaultValue);
			}
			return null;
		}
		/// <summary>
		/// Configures this Switch to set the provided property of <typeparamref name="T"/>. Property has the type of <typeparamref name="C"/>.
		/// </summary>
		/// <param name="expression">The property to set</param>
		public FluentSwitch<T, C> ForProperty(Expression<Func<T, C>> expression)
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
		/// Configures this Switch to show the provided Help Text.
		/// </summary>
		/// <param name="helpText">The help text for this Switch</param>
		public FluentSwitch<T, C> WithHelpText(string helpText)
		{
			HelpText = helpText;
			return this;
		}
		/// <summary>
		/// Configures this Switch to use a specific value when it is not provided
		/// </summary>
		/// <param name="defaultValue">The value to use as a default value when this Switch is not provided.</param>
		/// <returns></returns>
		public FluentSwitch<T, C> WithDefaultValue(C defaultValue)
		{
			DefaultValue = defaultValue;
			return this;
		}
		/// <summary>
		/// The converter to invoke on the provided value before assigning it to the property of <typeparamref name="T"/>.
		/// If not provided, no converter will be used. If this Option is not provided, the converter will not be invoked.
		/// The converter is considered to have failed if it throws an exception, or if the error message is not null.
		/// In all other cases, it is considered successful.
		/// </summary>
		/// <param name="converter">A convert that converts a bool to <typeparamref name="C"/></param>
		public FluentSwitch<T, C> WithConverter(Func<bool, Converted<C>> converter)
		{
			_converter = converter;
			return this;
		}
		/// <summary>
		/// The short and long name joined with a |
		/// </summary>
		public string ShortAndLongName()
		{
			return Util.ShortAndLongName(this);
		}
	}
}
