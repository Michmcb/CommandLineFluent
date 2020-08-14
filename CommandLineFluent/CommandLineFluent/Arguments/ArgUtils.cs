namespace CommandLineFluent.Arguments
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;

	/// <summary>
	/// Some utilities for arguments
	/// </summary>
	public static class ArgUtils
	{
		/// <summary>
		/// Returns a string which has the <paramref name="shortName"/> and <paramref name="longName"/> separated by a pipe, like this: shortName|longName.
		/// If <paramref name="encloseInBrackets"/> is true, it'll be like this: [shortName|longName].
		/// If shortName or longName is null, it will just show only the non-null name.
		/// </summary>
		/// <param name="shortName">The short name.</param>
		/// <param name="longName">The long name.</param>
		/// <param name="encloseInBrackets">If true, the string will be enclosed in [].</param>
		/// <returns>A string with short and long name, separated by a pipe. Or if one is null, only the non-null name.</returns>
		public static string ShortAndLongName(string? shortName, string? longName, bool encloseInBrackets = false)
		{
			if (shortName != null && longName != null)
			{
				return encloseInBrackets ? string.Concat("[", shortName, "|", longName, "]") : string.Concat(shortName, "|", longName);
			}
			return encloseInBrackets ? string.Concat("[", shortName ?? longName, "]") : shortName ?? longName ?? "";
		}
		/// <summary>
		/// Returns a string which has the <paramref name="shortName"/> and <paramref name="longName"/> separated by a pipe, followed by the <paramref name="valueName"/>, like this: shortName|longName valueName.
		/// If <paramref name="encloseInBrackets"/> is true, it'll be like this: [shortName|longName valueName].
		/// If shortName or longName is null, it will just show only the non-null name.
		/// </summary>
		/// <param name="shortName">The short name.</param>
		/// <param name="longName">The long name.</param>
		/// <param name="valueName">The name of the value.</param>
		/// <param name="encloseInBrackets">If true, the string will be enclosed in [].</param>
		/// <returns>A string with short and long name, separated by a pipe. Or if one is null, only the non-null name.</returns>
		public static string ShortAndLongName(string? shortName, string? longName, string valueName, bool encloseInBrackets = false)
		{
			if (shortName != null && longName != null)
			{
				return encloseInBrackets ? string.Concat("[", shortName, "|", longName, "] \"", valueName, "\"") : string.Concat(shortName, "|", longName, " \"", valueName, "\"");
			}
			return encloseInBrackets ? string.Concat("[", shortName ?? longName, "] \"", valueName, "\"") : string.Concat(shortName ?? longName, " \"", valueName, "\"");
		}
		/// <summary>
		/// Given an Expression, returns the corresponding Property. The <paramref name="expression"/> must be a <see cref="MemberExpression"/>, whose Member is a
		/// <see cref="PropertyInfo"/>. If not, throws an ArgumentException.
		/// </summary>
		/// <typeparam name="TClass">Argument which is passed to the expression.</typeparam>
		/// <typeparam name="TProp">What the expression returns</typeparam>
		/// <param name="expression">The expression which must be a MemberE to a PropertyInfo</param>
		public static PropertyInfo PropertyInfoFromExpression<TClass, TProp>(Expression<Func<TClass, TProp>> expression)
		{
			if (!(expression.Body is MemberExpression me))
			{
				throw new ArgumentException($"Expression has to be a property of type {typeof(TProp)} of class {typeof(TClass)}", nameof(expression));
			}
			if (!(me.Member is PropertyInfo prop))
			{
				throw new ArgumentException($"Expression has to be a property of type {typeof(TProp)} of class {typeof(TClass)}", nameof(expression));
			}
			//if (prop.PropertyType != typeof(TProp))
			//{
			//	throw new ArgumentException($"Expression has to be a property of type {typeof(TProp)} of class {typeof(TClass)}", nameof(expression));
			//}
			return prop;
		}
	}
}