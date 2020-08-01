
namespace CommandLineFluent.Arguments
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;

	public static class ArgUtils
	{
		public static string ShortAndLongName(string? shortName, string? longName, bool encloseInBrackets = false)
		{
			if (shortName != null && longName != null)
			{
				return encloseInBrackets ? string.Concat("[", shortName, "|", longName, "]") : string.Concat(shortName, "|", longName);
			}
			return encloseInBrackets ? string.Concat("[", shortName ?? longName, "]") : shortName ?? longName ?? "";
		}
		public static string ShortAndLongName(string? shortName, string? longName, string valueName, bool encloseInBrackets = false)
		{
			if (shortName != null && longName != null)
			{
				return encloseInBrackets ? string.Concat("[", shortName, "|", longName, "] \"", valueName, "\"") : string.Concat(shortName, "|", longName, " \"", valueName, "\"");
			}
			return encloseInBrackets ? string.Concat("[", shortName ?? longName, "] \"", valueName, "\"") : string.Concat(shortName ?? longName, " \"", valueName, "\"");
		}
		/// <summary>
		/// Given an Expression, returns the corresponding Property. Throws an exception if <paramref name="expression"/> is not a MemberExpression
		/// which references an object property
		/// </summary>
		/// <typeparam name="TClass">Argument which is passed to the expression</typeparam>
		/// <typeparam name="TProp">What the expression returns</typeparam>
		/// <param name="expression">The expression to convert into a PropertyInfo</param>
		public static PropertyInfo PropertyInfoFromExpression<TClass, TProp>(Expression<Func<TClass, TProp>> expression)
		{
			if (!(expression.Body is MemberExpression me))
			{
				throw new ArgumentException($"Expression has to be a property of type {typeof(TProp)} of class {typeof(TClass)}", nameof(expression));
			}
			PropertyInfo? prop = me.Member as PropertyInfo;
			return prop ?? throw new ArgumentException($"Expression has to be a property of type {typeof(TProp)} of class {typeof(TClass)}", nameof(expression));
		}
	}
}