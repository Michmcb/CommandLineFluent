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
		/// Makes a string like: -s|--long, to show the user both ways of writing the switch
		/// </summary>
		public static string ShortAndLongName(IFluentOption fluentOption)
		{
			return ShortAndLongName(fluentOption.ShortName, fluentOption.LongName);
		}
		/// <summary>
		/// Makes a string like: -s|--long, to show the user both ways of writing the switch
		/// </summary>
		public static string ShortAndLongName(IFluentSwitch fluentSwitch)
		{
			return ShortAndLongName(fluentSwitch.ShortName, fluentSwitch.LongName);
		}
		/// <summary>
		/// Makes a string like: -s|--long, to show the user both ways of writing the switch
		/// </summary>
		public static string ShortAndLongName(string shortName, string longName)
		{
			if (shortName != null && longName != null)
			{
				return $"{shortName}|{longName}";
			}
			return shortName ?? longName;
		}
		/// <summary>
		/// Given an Expression, returns the corresponding Property. Throws an exception if <paramref name="expression"/> is not a MemberExpression
		/// which references an object property
		/// </summary>
		/// <typeparam name="T">Argument which is passed to the expression</typeparam>
		/// <typeparam name="V">What the expression returns</typeparam>
		/// <param name="expression">The expression to convert into a PropertyInfo</param>
		public static PropertyInfo PropertyInfoFromExpression<T, V>(Expression<Func<T, V>> expression)
		{
			if (!(expression.Body is MemberExpression me))
			{
				throw new ArgumentException($"Expression has to be a property of type {typeof(T)}", nameof(expression));
			}
			PropertyInfo prop = me.Member as PropertyInfo;
			return prop ?? throw new ArgumentException($"Expression has to be a property of type {typeof(T)}", nameof(expression));
		}

		internal static void ThrowIfRequirednessAlreadyConfigured(bool configuredRequiredness)
		{
			throw new NotImplementedException();
		}
	}
}
