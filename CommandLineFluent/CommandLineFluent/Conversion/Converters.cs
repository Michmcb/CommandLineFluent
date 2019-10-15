using CommandLineFluent.Arguments;
using System;

namespace CommandLineFluent.Conversion
{
	/// <summary>
	/// Has some commonly-used converters. For those that just take one string, you can do this:
	/// verb.WithConverter(Converters.ToInt32)
	/// </summary>
	public static class Converters
	{
		/// <summary>
		/// Converts the provided string to a bool.
		/// Uses bool.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<bool> ToBool(string s)
		{
			if (bool.TryParse(s, out bool v))
			{
				return new Converted<bool>(v);
			}
			else
			{
				return new Converted<bool>(false, $@"""{s}"" could not be parsed as a true/false value");
			}
		}
		/// <summary>
		/// Converts the provided string to a nullable bool. Identical to ToBool, but can be used with Optional parameters.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<bool?> ToNullableBool(string s)
		{
			if (bool.TryParse(s, out bool v))
			{
				return new Converted<bool?>(v);
			}
			else
			{
				return new Converted<bool?>(false, $@"""{s}"" could not be parsed as a true/false value");
			}
		}
		/// <summary>
		/// Converts the provided string to a bool. y, yes, or true will convert to true. n, no, or false will convert to false.
		/// Case insensitive.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<bool> ToBoolYesNo(string s)
		{
			if (s != null)
			{
				string v = s.Trim();
				if (v.Equals("y", StringComparison.OrdinalIgnoreCase) || v.Equals("yes", StringComparison.OrdinalIgnoreCase) || v.Equals("true", StringComparison.OrdinalIgnoreCase))
				{
					return new Converted<bool>(true);
				}
				else if (v.Equals("n", StringComparison.OrdinalIgnoreCase) || v.Equals("no", StringComparison.OrdinalIgnoreCase) || v.Equals("false", StringComparison.OrdinalIgnoreCase))
				{
					return new Converted<bool>(false);
				}
			}
			return new Converted<bool>(false, $@"""{s}"" was not y, yes, true, or n, no, false");
		}
		/// <summary>
		/// Converts the provided string to a nullable bool. Identical to ToBoolYesNo, but can be used with Optional parameters.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<bool?> ToNullableBoolYesNo(string s)
		{
			if (s != null)
			{
				string v = s.Trim();
				if (v.Equals("y", StringComparison.OrdinalIgnoreCase) || v.Equals("yes", StringComparison.OrdinalIgnoreCase) || v.Equals("true", StringComparison.OrdinalIgnoreCase))
				{
					return new Converted<bool?>(true);
				}
				else if (v.Equals("n", StringComparison.OrdinalIgnoreCase) || v.Equals("no", StringComparison.OrdinalIgnoreCase) || v.Equals("false", StringComparison.OrdinalIgnoreCase))
				{
					return new Converted<bool?>(false);
				}
			}
			return new Converted<bool?>(false, $@"""{s}"" was not y, yes, true, or n, no, false");
		}
		/// <summary>
		/// Converts the provided string to an int
		/// Uses int.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<int> ToInt(string s)
		{
			if (int.TryParse(s, out int v))
			{
				return new Converted<int>(v);
			}
			else
			{
				return new Converted<int>(0, $@"""{s}"" was not an integer");
			}
		}
		/// <summary>
		/// Converts the provided string to a nullable int. Identical to ToInt, but can be used with Optional parameters.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<int?> ToNullableInt(string s)
		{
			if (int.TryParse(s, out int v))
			{
				return new Converted<int?>(v);
			}
			else
			{
				return new Converted<int?>(0, $@"""{s}"" was not an integer");
			}
		}
		/// <summary>
		/// Converts the provided string to an uint
		/// Uses uint.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<uint> ToUInt(string s)
		{
			if (uint.TryParse(s, out uint v))
			{
				return new Converted<uint>(v);
			}
			else
			{
				return new Converted<uint>(0, $@"""{s}"" was not a positive integer");
			}
		}
		/// <summary>
		/// Converts the provided string to a nullable uint. Identical to ToUInt, but can be used with Optional parameters.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<uint?> ToNullableUInt(string s)
		{
			if (uint.TryParse(s, out uint v))
			{
				return new Converted<uint?>(v);
			}
			else
			{
				return new Converted<uint?>(0, $@"""{s}"" was not a positive integer");
			}
		}
		/// <summary>
		/// Converts the provided string to a long
		/// Uses long.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<long> ToLong(string s)
		{
			if (long.TryParse(s, out long v))
			{
				return new Converted<long>(v);
			}
			else
			{
				return new Converted<long>(0, $@"""{s}"" was not an integer");
			}
		}
		/// <summary>
		/// Converts the provided string to a nullable long. Identical to ToLong, but can be used with Optional parameters.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<long?> ToNullableLong(string s)
		{
			if (long.TryParse(s, out long v))
			{
				return new Converted<long?>(v);
			}
			else
			{
				return new Converted<long?>(0, $@"""{s}"" was not an integer");
			}
		}
		/// <summary>
		/// Converts the provided string to an ulong
		/// Uses ulong.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<ulong> ToULong(string s)
		{
			if (ulong.TryParse(s, out ulong v))
			{
				return new Converted<ulong>(v);
			}
			else
			{
				return new Converted<ulong>(0, $@"""{s}"" was not a positive integer");
			}
		}
		/// <summary>
		/// Converts the provided string to a nullable ulong. Identical to ToULong, but can be used with Optional parameters.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<ulong?> ToNullableULong(string s)
		{
			if (ulong.TryParse(s, out ulong v))
			{
				return new Converted<ulong?>(v);
			}
			else
			{
				return new Converted<ulong?>(0, $@"""{s}"" was not a positive integer");
			}
		}
		/// <summary>
		/// Converts the provided string to a double.
		/// Uses double.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<double> ToDouble(string s)
		{
			if (double.TryParse(s, out double v))
			{
				return new Converted<double>(v);
			}
			else
			{
				return new Converted<double>(0, $@"""{s}"" was not a floating-point number");
			}
		}
		/// <summary>
		/// Converts the provided string to a nullable double. Identical to ToDouble, but can be used with Optional parameters.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<double?> ToNullableDouble(string s)
		{
			if (double.TryParse(s, out double v))
			{
				return new Converted<double?>(v);
			}
			else
			{
				return new Converted<double?>(0, $@"""{s}"" was not a floating-point number");
			}
		}
		/// <summary>
		/// Converts the provided string to a decimal.
		/// Uses decimal.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<decimal> ToDecimal(string s)
		{
			if (decimal.TryParse(s, out decimal v))
			{
				return new Converted<decimal>(v);
			}
			else
			{
				return new Converted<decimal>(0, $@"""{s}"" was not a decimal number");
			}
		}
		/// <summary>
		/// Converts the provided string to a nullable decimal. Identical to ToDecimal, but can be used with Optional parameters.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<decimal?> ToNullableDecimal(string s)
		{
			if (decimal.TryParse(s, out decimal v))
			{
				return new Converted<decimal?>(v);
			}
			else
			{
				return new Converted<decimal?>(0, $@"""{s}"" was not a decimal number");
			}
		}
		/// <summary>
		/// Converts the provided string to a Date, at midnight. Returns an error if a Time component was specified.
		/// Uses DateTime.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<DateTime> ToDate(string s)
		{
			if (DateTime.TryParse(s, out DateTime v))
			{
				if (v.TimeOfDay.Ticks == 0)
				{
					return new Converted<DateTime>(v);
				}
				else
				{
					return new Converted<DateTime>(DateTime.MinValue, $@"""{s}"" was not a date (It should not have a time of day specified)");
				}
			}
			else
			{
				return new Converted<DateTime>(DateTime.MinValue, $@"""{s}"" was not a date");
			}
		}
		/// <summary>
		/// Converts the provided string to a nullable DateTime. Identical to ToDate, but can be used with Optional parameters.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<DateTime?> ToNullableDate(string s)
		{
			if (DateTime.TryParse(s, out DateTime v))
			{
				if (v.TimeOfDay.Ticks == 0)
				{
					return new Converted<DateTime?>(v);
				}
				else
				{
					return new Converted<DateTime?>(DateTime.MinValue, $@"""{s}"" was not a date (It should not have a time of day specified)");
				}
			}
			else
			{
				return new Converted<DateTime?>(DateTime.MinValue, $@"""{s}"" was not a date");
			}
		}
		/// <summary>
		/// Converts the provided string to a DateTime.
		/// Uses DateTime.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<DateTime> ToDateTime(string s)
		{
			if (DateTime.TryParse(s, out DateTime v))
			{
				return new Converted<DateTime>(v);
			}
			else
			{
				return new Converted<DateTime>(DateTime.MinValue, $@"""{s}"" was not a date and time");
			}
		}
		/// <summary>
		/// Converts the provided string to a nullable DateTime. Identical to ToDateTime, but can be used with Optional parameters.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<DateTime?> ToNullableDateTime(string s)
		{
			if (DateTime.TryParse(s, out DateTime v))
			{
				return new Converted<DateTime?>(v);
			}
			else
			{
				return new Converted<DateTime?>(DateTime.MinValue, $@"""{s}"" was not a date and time");
			}
		}
		/// <summary>
		/// Converts the provided string to an Enum.
		/// Uses Enum.TryParse&lt;<typeparamref name="T"/>>()
		/// </summary>
		/// <typeparam name="T">The type of the enum to parse</typeparam>
		/// <param name="s">The string to convert</param>
		public static Converted<T> ToEnum<T>(string s) where T : struct, Enum
		{
			if (Enum.TryParse(s, out T v))
			{
				return new Converted<T>(v);
			}
			else
			{
				return new Converted<T>(default, $@"""{s}"" should be one of: {String.Join(", ", Enum.GetNames(typeof(T)))}");
			}
		}
		/// <summary>
		/// Splits the string into multiple strings based on separators.
		/// Uses s.Split()
		/// </summary>
		/// <param name="s">The string to split</param>
		/// <param name="separator">The separators to use</param>
		public static Converted<string[]> Split(string s, params char[] separator)
		{
			return new Converted<string[]>(s.Split(separator));
		}
		/// <summary>
		/// Splits the string into multiple strings based on separators.
		/// Uses s.Split()
		/// </summary>
		/// <param name="s">The string to split</param>
		/// <param name="separator">The separators to use</param>
		/// <param name="count">The maximum number of substrings to return</param>
		public static Converted<string[]> Split(string s, char[] separator, int count)
		{
			return new Converted<string[]>(s.Split(separator, count));
		}
		/// <summary>
		/// Splits the string into multiple strings based on separators.
		/// Uses s.Split()
		/// </summary>
		/// <param name="s">The string to split</param>
		/// <param name="separator">The separators to use</param>
		/// <param name="options">System.StringSplitOptions.RemoveEmptyEntries to omit empty array elements from the array returned; or System.StringSplitOptions.None to include empty array
		/// elements in the array returned.</param>
		public static Converted<string[]> Split(string s, char[] separator, StringSplitOptions options)
		{
			return new Converted<string[]>(s.Split(separator, options));
		}
		/// <summary>
		/// Splits the string into multiple strings based on separators.
		/// Uses s.Split()
		/// </summary>
		/// <param name="s">The string to split</param>
		/// <param name="separator">The separators to use</param>
		/// <param name="options">System.StringSplitOptions.RemoveEmptyEntries to omit empty array elements from the array returned; or System.StringSplitOptions.None to include empty array
		/// elements in the array returned.</param>
		public static Converted<string[]> Split(string s, string[] separator, StringSplitOptions options)
		{
			return new Converted<string[]>(s.Split(separator, options));
		}
		/// <summary>
		/// Splits the string into multiple strings based on separators.
		/// Uses s.Split()
		/// </summary>
		/// <param name="s">The string to split</param>
		/// <param name="separator">The separators to use</param>
		/// <param name="count">The maximum number of substrings to return</param>
		/// <param name="options">System.StringSplitOptions.RemoveEmptyEntries to omit empty array elements from the array returned; or System.StringSplitOptions.None to include empty array
		/// elements in the array returned.</param>
		public static Converted<string[]> Split(string s, char[] separator, int count, StringSplitOptions options)
		{
			return new Converted<string[]>(s.Split(separator, count, options));
		}
		/// <summary>
		/// Splits the string into multiple strings based on separators.
		/// Uses s.Split()
		/// </summary>
		/// <param name="s">The string to split</param>
		/// <param name="separator">The separators to use</param>
		/// <param name="count">The maximum number of substrings to return</param>
		/// <param name="options">System.StringSplitOptions.RemoveEmptyEntries to omit empty array elements from the array returned; or System.StringSplitOptions.None to include empty array
		/// elements in the array returned.</param>
		public static Converted<string[]> Split(string s, string[] separator, int count, StringSplitOptions options)
		{
			return new Converted<string[]>(s.Split(separator, count, options));
		}
	}
}
