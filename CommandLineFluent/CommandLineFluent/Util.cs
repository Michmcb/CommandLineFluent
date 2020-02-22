using CommandLineFluent.Arguments;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CommandLineFluent
{
	/// <summary>
	/// Utility methods
	/// </summary>
	public static class Util
	{
		/// <summary>
		/// Makes a string like: -s|--long, to show the user both ways of writing the option
		/// </summary>
		public static string ShortAndLongName(IFluentOption fluentOption, string valueName)
		{
			// If it's true or null, don't enclose. If it's false, do enclose.
			return ShortAndLongNameWithValueName(fluentOption.ShortName, fluentOption.LongName, valueName, (fluentOption.Required == null || fluentOption.Required == true) ? false : true);
		}
		/// <summary>
		/// Makes a string like: -s|--long, to show the user both ways of writing the option
		/// </summary>
		public static string ShortAndLongName(IFluentOption fluentOption)
		{
			// If it's true or null, don't enclose. If it's false, do enclose.
			return ShortAndLongName(fluentOption.ShortName, fluentOption.LongName, (fluentOption.Required == null || fluentOption.Required == true) ? false : true);
		}
		/// <summary>
		/// Makes a string like: -s|--long, to show the user both ways of writing the switch
		/// </summary>
		public static string ShortAndLongName(IFluentSwitch fluentSwitch)
		{
			// Switches are always 
			return ShortAndLongName(fluentSwitch.ShortName, fluentSwitch.LongName, true);
		}
		/// <summary>
		/// Makes a string like: -s|--long, to show the user both ways of writing the switch
		/// </summary>
		public static string ShortAndLongName(string shortName, string longName, bool encloseInBrackets = false)
		{
			if (shortName != null && longName != null)
			{
				return encloseInBrackets ? $"[{shortName}|{longName}]" : $"{shortName}|{longName}";
			}
			return encloseInBrackets ? "[" + (shortName ?? longName) + "]" : shortName ?? longName;
		}
		public static string ShortAndLongNameWithValueName(string shortName, string longName, string valueName, bool encloseInBrackets = false)
		{
			if (shortName != null && longName != null)
			{
				return encloseInBrackets ? $"[{shortName}|{longName} \"{valueName}\"]" : $"{shortName}|{longName} \"{valueName}\"";
			}
			return encloseInBrackets ? "[" + (shortName ?? longName) + $" \"{valueName}\"]" : (shortName ?? longName) + "\"" + valueName + "\"";
		}
		/// <summary>
		/// Given an Expression, returns the corresponding Property. Throws an exception if <paramref name="expression"/> is not a MemberExpression
		/// which references an object property
		/// </summary>
		/// <typeparam name="T">Argument which is passed to the expression</typeparam>
		/// <typeparam name="C">What the expression returns</typeparam>
		/// <param name="expression">The expression to convert into a PropertyInfo</param>
		public static PropertyInfo PropertyInfoFromExpression<T, C>(Expression<Func<T, C>> expression)
		{
			if (!(expression.Body is MemberExpression me))
			{
				throw new ArgumentException($"Expression has to be a property of type {typeof(C)} of class {typeof(T)}", nameof(expression));
			}
			PropertyInfo prop = me.Member as PropertyInfo;
			return prop ?? throw new ArgumentException($"Expression has to be a property of type {typeof(C)} of class {typeof(T)}", nameof(expression));
		}
	}
}
