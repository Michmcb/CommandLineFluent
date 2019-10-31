using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// Captures all lone values, such as: Foo.exe Value1 -t someswitch Value2 Value3. All 3 values there would be captured as an array: [Value1, Value2, Value3].
	/// Use this when you want to consume an indeterminate number of values, regardless of their position.
	/// </summary>
	/// <typeparam name="T">The class of the property to which this argument maps</typeparam>
	/// <typeparam name="C">The type of the property this argument will set</typeparam>
	public class FluentManyValues<T, C> : FluentArgument, IFluentManyValues, IFluentSettable<T, string[]> where T : new()
	{
		private Func<string[], Converted<C>> _converter;
		private Func<string[], string> _validator;
		/// <summary>
		/// The prefixes that are ignored when capturing values. This is useful if you use a consistent prefixing scheme,
		/// and want to avoid capturing possible user typos; an error will be thrown instead of using it as a value.
		/// </summary>
		public string[] IgnoredPrefixes { get; private set; }
		/// <summary>
		/// Whether or not at least 1 value is required
		/// </summary>
		public bool Required { get; private set; }
		/// <summary>
		/// If not required, this is the default value used when no Values are provided
		/// </summary>
		public C DefaultValue { get; private set; }
		internal FluentManyValues()
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
		/// <param name="values">The raw values of all arguments found. If necessary, will be converted to the <typeparamref name="C"/> using the Converter set</param>
		public Error SetValue(T target, string[] values)
		{
			if (values != null)
			{
				// If any value starts with any ignored prefix
				System.Collections.Generic.IEnumerable<string> badVals = values.Where(val => IgnoredPrefixes.Any(prefix => val.StartsWith(prefix)));
				if (badVals?.Any() == true)
				{
					return new Error(ErrorCode.UnexpectedArgument, true, $"Found some unexpected arguments: {string.Join(", ", badVals)}");
				}
				string validateError = null;
				try
				{
					validateError = _validator?.Invoke(values);
					if (validateError != null)
					{
						return new Error(ErrorCode.ValuesFailedValidation, true, validateError);
					}
				}
				catch (Exception ex)
				{
					return new Error(ErrorCode.ValuesFailedValidation, false, $"Validator for ManyValues threw an exception ({ex.Message})", ex);
				}
				if (_converter != null)
				{
					Converted<C> converted;
					try
					{
						converted = _converter.Invoke(values);
						if (!converted.Successful)
						{
							return new Error(ErrorCode.ValuesFailedConversion, true, converted.ErrorMessage);
						}
					}
					catch (Exception ex)
					{
						return new Error(ErrorCode.ValuesFailedConversion, false, $"Converter for ManyValues threw an exception ({ex.Message})", ex);
					}
					TargetProperty.SetValue(target, converted.ConvertedValue);
					return null;
				}
				else
				{
					TargetProperty.SetValue(target, values);
				}
			}
			else if (!Required)
			{
				// It's not required, so assign its default value
				TargetProperty.SetValue(target, DefaultValue);
			}
			else
			{
				// It is required, so throw an exception
				return new Error(ErrorCode.MissingRequiredValues, true, $"ManyValues are required and no values were provided");
			}
			return null;
		}
		/// <summary>
		/// Configures this Value to set the provided property of <typeparamref name="T"/>.
		/// </summary>
		/// <param name="expression">The property to set</param>
		public FluentManyValues<T, C> ForProperty(Expression<Func<T, C>> expression)
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
		/// Configures at least 1 Value to be required. By default, Values are required unless you use IsOptional.
		/// </summary>
		public FluentManyValues<T, C> IsRequired()
		{
			Required = true;
			DefaultValue = default;
			return this;
		}
		/// <summary>
		/// Configures these Values as optional, with a default value when not provided.
		/// </summary>
		/// <param name="defaultValue">The value to use as a default value when no Values are provided. If not provided, this is the default value for <typeparamref name="C"/></param>
		public FluentManyValues<T, C> IsOptional(C defaultValue = default)
		{
			Required = false;
			DefaultValue = defaultValue;
			return this;
		}
		/// <summary>
		/// Configures these Values to show the provided Help Text.
		/// </summary>
		/// <param name="helpText">The help text for these Values</param>
		public FluentManyValues<T, C> WithHelpText(string helpText)
		{
			HelpText = helpText;
			return this;
		}
		/// <summary>
		/// Configures these Values to have the provided human-readable name.
		/// By default this is used to produce Usage Text
		/// </summary>
		/// <param name="name">The human-readable name</param>
		public FluentManyValues<T, C> WithName(string name)
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
		/// <param name="converter">A convert that converts a string[] to <typeparamref name="C"/></param>
		public FluentManyValues<T, C> WithConverter(Func<string[], Converted<C>> converter)
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
		/// <param name="validator">The validator that validates the raw values</param>
		public FluentManyValues<T, C> WithValidator(Func<string[], string> validator)
		{
			_validator = validator;
			return this;
		}
		/// <summary>
		/// Prefixes to ignore when parsing values. Useful when your Options and Switches have a consistent prefix, and you
		/// don't want mistyped Options and Switches being parsed as Values.
		/// </summary>
		/// <param name="prefixes">The prefixes to ignore</param>
		public FluentManyValues<T, C> IgnorePrefixes(params string[] prefixes)
		{
			IgnoredPrefixes = prefixes;
			return this;
		}
	}
}
