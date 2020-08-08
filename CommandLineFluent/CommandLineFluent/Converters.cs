namespace CommandLineFluent
{
	using System;

	public static class Converters
	{
		/// <summary>
		/// Converts the provided string to an int.
		/// Uses int.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<int> ToInt(string s)
		{
			if (int.TryParse(s, out int v))
			{
				return v;
			}
			else
			{
				return s + " was not an integer";
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
				return v;
			}
			else
			{
				return s + " was not an integer";
			}
		}
		/// <summary>
		/// Converts the provided string to an uint.
		/// Uses uint.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<uint> ToUInt(string s)
		{
			if (uint.TryParse(s, out uint v))
			{
				return v;
			}
			else
			{
				return s + " was not a positive integer";
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
				return v;
			}
			else
			{
				return s + " was not a positive integer";
			}
		}
		/// <summary>
		/// Converts the provided string to a long.
		/// Uses long.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<long> ToLong(string s)
		{
			if (long.TryParse(s, out long v))
			{
				return v;
			}
			else
			{
				return s + " was not an integer";
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
				return v;
			}
			else
			{
				return s + " was not an integer";
			}
		}
		/// <summary>
		/// Converts the provided string to an ulong.
		/// Uses ulong.TryParse()
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<ulong> ToULong(string s)
		{
			if (ulong.TryParse(s, out ulong v))
			{
				return v;
			}
			else
			{
				return s + " was not a positive integer";
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
				return v;
			}
			else
			{
				return s + " was not a positive integer";
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
				return v;
			}
			else
			{
				return s + " was not a floating-point number";
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
				return v;
			}
			else
			{
				return s + " was not a floating-point number";
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
				return v;
			}
			else
			{
				return s + " was not a decimal number";
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
				return v;
			}
			else
			{
				return s + " was not a decimal number";
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
					return v;
				}
				else
				{
					return s + " was not a date (It should not have a time of day specified)";
				}
			}
			else
			{
				return s + " was not a date";
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
					return v;
				}
				else
				{
					return s + " was not a date (It should not have a time of day specified)";
				}
			}
			else
			{
				return s + " was not a date";
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
				return v;
			}
			else
			{
				return s + " was not a date and time";
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
				return v;
			}
			else
			{
				return s + " was not a date and time";
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
				return v;
			}
			else
			{
				return string.Concat(s, " should be one of: ", string.Join(", ", Enum.GetNames(typeof(T))));
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
			return s.Split(separator);
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
			return s.Split(separator, count);
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
			return s.Split(separator, options);
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
			return s.Split(separator, options);
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
			return s.Split(separator, count, options);
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
			return s.Split(separator, count, options);
		}
	}
}
