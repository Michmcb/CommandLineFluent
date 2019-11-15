using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
		private bool _configuredRequiredness;
		private bool _hasDefaultValue;

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
		/// <summary>
		/// Dependencies on other properties which dictate whether or not this is required.
		/// </summary>
		public FluentDependencies<T, C> Dependencies { get; private set; }
		internal FluentSwitch(string shortName, string longName)
		{
			ShortName = shortName;
			LongName = longName;
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
			GotValue = false;
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
					GotValue = true;
				}
				else
				{
					TargetProperty.SetValue(target, value);
					GotValue = true;
				}
			}
			else
			{
				// A bit different for switches; getting false means we never got a value, in which case we use the default value.
				if (_hasDefaultValue)
				{
					TargetProperty.SetValue(target, DefaultValue);
				}
			}
			return null;
		}
		/// <summary>
		/// Configures this Switch to set the provided property of <typeparamref name="T"/>. Property has the type of <typeparamref name="C"/>.
		/// </summary>
		/// <param name="expression">The property to set</param>
		public FluentSwitch<T, C> ForProperty(Expression<Func<T, C>> expression)
		{
			TargetProperty = Util.PropertyInfoFromExpression(expression);
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
			_hasDefaultValue = true;
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
		/// <summary>
		/// Configures this Switch to only be required or must not appear under certain circumstances.
		/// If any rule is violated, parsing is considered to have failed. If all rules pass, then parsing is considered to have succeeded.
		/// You can specify that the user has to provide this Value depending upon the value of other properties (after parsing, validation, and conversion)
		/// </summary>
		public FluentSwitch<T, C> WithDependencies(Action<FluentDependencies<T, C>> config)
		{
			ThrowIfRequirednessAlreadyConfigured();
			if (config != null)
			{
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
			return Dependencies.EvaluateRelationship(obj, GotValue, FluentArgumentType.Switch);
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
				throw new InvalidOperationException($@"Do not call {nameof(WithDependencies)} more than once.");
			}
		}
	}
}
