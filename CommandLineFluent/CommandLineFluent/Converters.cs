namespace CommandLineFluent
{
	using System;
	/// <summary>
	/// Built-in converters, which try and convert a string to some other type
	/// </summary>
	public static class Converters
	{
		/// <summary>
		/// Always returns a successfully converted <see cref="string"/>, which is just <paramref name="s"/>.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <returns><paramref name="s"/></returns>
		public static Converted<string, string> NoConversion(string s)
		{
			return Converted<string, string>.Value(s);
		}
		/// <summary>
		/// Always returns a successfully converted <see cref="string"/>, which is just <paramref name="s"/>.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <returns><paramref name="s"/></returns>
		public static Converted<string?, string> NoConversionNullable(string? s)
		{
			return Converted<string?, string>.Value(s);
		}
		/// <summary>
		/// Always returns a successfully converted <see cref="bool"/>, which is just <paramref name="b"/>.
		/// </summary>
		/// <param name="b">The boolean.</param>
		/// <returns><paramref name="b"/></returns>
		public static Converted<bool, string> NoConversion(bool b)
		{
			return b;
		}
		/// <summary>
		/// If <paramref name="s"/> is 1 character, returns that. Otherwise, error.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<char, string> ToChar(string s)
		{
			return s.Length == 1 ? s[0] : (Converted<char, string>)(s + " was not a single character");
		}
		/// <summary>
		/// If <paramref name="s"/> is 1 character, returns that. Otherwise, error.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<char?, string> ToNullableChar(string s)
		{
			return s.Length == 1 ? s[0] : (Converted<char?, string>)(s + " was not a single character");
		}
		/// <summary>
		/// Converts the provided string to <see cref="short"/>.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<short, string> ToShort(string s)
		{
			return short.TryParse(s, out short v) ? v : (Converted<short, string>)(s + " was not an integer");
		}
		/// <summary>
		/// Converts the provided string to <see cref="short"/>?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<short?, string> ToNullableShort(string s)
		{
			return short.TryParse(s, out short v) ? v : (Converted<short?, string>)(s + " was not an integer");
		}
		/// <summary>
		/// Converts the provided string to <see cref="ushort"/>.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<ushort, string> ToUShort(string s)
		{
			return ushort.TryParse(s, out ushort v) ? v : (Converted<ushort, string>)(s + " was not an integer");
		}
		/// <summary>
		/// Converts the provided string to <see cref="ushort"/>?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<ushort?, string> ToNullableUShort(string s)
		{
			return ushort.TryParse(s, out ushort v) ? v : (Converted<ushort?, string>)(s + " was not an integer");
		}
		/// <summary>
		/// Converts the provided string to <see cref="int"/>.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<int, string> ToInt(string s)
		{
			return int.TryParse(s, out int v) ? v : (Converted<int, string>)(s + " was not an integer");
		}
		/// <summary>
		/// Converts the provided string to <see cref="int"/>?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<int?, string> ToNullableInt(string s)
		{
			return int.TryParse(s, out int v) ? v : (Converted<int?, string>)(s + " was not an integer");
		}
		/// <summary>
		/// Converts the provided string to <see cref="uint"/>.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<uint, string> ToUInt(string s)
		{
			return uint.TryParse(s, out uint v) ? v : (Converted<uint, string>)(s + " was not a positive integer");
		}
		/// <summary>
		/// Converts the provided string to <see cref="uint"/>?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<uint?, string> ToNullableUInt(string s)
		{
			return uint.TryParse(s, out uint v) ? v : (Converted<uint?, string>)(s + " was not a positive integer");
		}
		/// <summary>
		/// Converts the provided string to <see cref="long"/>.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<long, string> ToLong(string s)
		{
			return long.TryParse(s, out long v) ? v : (Converted<long, string>)(s + " was not an integer");
		}
		/// <summary>
		/// Converts the provided string to <see cref="long"/>?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<long?, string> ToNullableLong(string s)
		{
			return long.TryParse(s, out long v) ? v : (Converted<long?, string>)(s + " was not an integer");
		}
		/// <summary>
		/// Converts the provided string to <see cref="ulong"/>.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<ulong, string> ToULong(string s)
		{
			return ulong.TryParse(s, out ulong v) ? v : (Converted<ulong, string>)(s + " was not a positive integer");
		}
		/// <summary>
		/// Converts the provided string to <see cref="ulong"/>?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<ulong?, string> ToNullableULong(string s)
		{
			return ulong.TryParse(s, out ulong v) ? v : (Converted<ulong?, string>)(s + " was not a positive integer");
		}
		/// <summary>
		/// Converts the provided string to <see cref="float"/>.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<float, string> ToFloat(string s)
		{
			return float.TryParse(s, out float v) ? v : (Converted<float, string>)(s + " was not a floating-point number");
		}
		/// <summary>
		/// Converts the provided string to <see cref="float"/>?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<float?, string> ToNullableFloat(string s)
		{
			return float.TryParse(s, out float v) ? v : (Converted<float?, string>)(s + " was not a floating-point number");
		}
		/// <summary>
		/// Converts the provided string to <see cref="double"/>.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<double, string> ToDouble(string s)
		{
			return double.TryParse(s, out double v) ? v : (Converted<double, string>)(s + " was not a floating-point number");
		}
		/// <summary>
		/// Converts the provided string to <see cref="double"/>?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<double?, string> ToNullableDouble(string s)
		{
			return double.TryParse(s, out double v) ? v : (Converted<double?, string>)(s + " was not a floating-point number");
		}
		/// <summary>
		/// Converts the provided string to <see cref="decimal"/>.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<decimal, string> ToDecimal(string s)
		{
			return decimal.TryParse(s, out decimal v) ? v : (Converted<decimal, string>)(s + " was not a decimal number");
		}
		/// <summary>
		/// Converts the provided string to <see cref="decimal"/>?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<decimal?, string> ToNullableDecimal(string s)
		{
			return decimal.TryParse(s, out decimal v) ? v : (Converted<decimal?, string>)(s + " was not a decimal number");
		}
		/// <summary>
		/// Converts the provided string to the Enum <typeparamref name="TEnum"/>.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum to parse</typeparam>
		/// <param name="s">The string to convert</param>
		public static Converted<TEnum, string> ToEnum<TEnum>(string s) where TEnum : struct, Enum
		{
			return Enum.TryParse(s, out TEnum v) ? v : (Converted<TEnum, string>)string.Concat(s, " should be one of: ", string.Join(", ", Enum.GetNames(typeof(TEnum))));
		}
		/// <summary>
		/// Converts the provided string to the Enum <typeparamref name="TEnum"/>?.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum to parse</typeparam>
		/// <param name="s">The string to convert</param>
		public static Converted<TEnum?, string> ToNullableEnum<TEnum>(string s) where TEnum : struct, Enum
		{
			return Enum.TryParse(s, out TEnum v) ? v : (Converted<TEnum?, string>)string.Concat(s, " should be one of: ", string.Join(", ", Enum.GetNames(typeof(TEnum))));
		}
		/// <summary>
		/// Converts the provided string to the Enum <typeparamref name="TEnum"/>.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum to parse</typeparam>
		/// <param name="s">The string to convert</param>
		/// <param name="caseSensitive">If true, case must match. Otherwise, it doesn't have to match.</param>
		public static Converted<TEnum, string> ToEnum<TEnum>(string s, bool caseSensitive) where TEnum : struct, Enum
		{
			return Enum.TryParse(s, !caseSensitive, out TEnum v) ? v : (Converted<TEnum, string>)string.Concat(s, " should be one of: ", string.Join(", ", Enum.GetNames(typeof(TEnum))));
		}
		/// <summary>
		/// Converts the provided string to the Enum <typeparamref name="TEnum"/>?.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum to parse</typeparam>
		/// <param name="s">The string to convert</param>
		/// <param name="caseSensitive">If true, case must match. Otherwise, it doesn't have to match.</param>
		public static Converted<TEnum?, string> ToNullableEnum<TEnum>(string s, bool caseSensitive) where TEnum : struct, Enum
		{
			return Enum.TryParse(s, !caseSensitive, out TEnum v) ? v : (Converted<TEnum?, string>)string.Concat(s, " should be one of: ", string.Join(", ", Enum.GetNames(typeof(TEnum))));
		}
		/// <summary>
		/// Converts the provided string to <see cref="DateTime"/>.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<DateTime, string> ToDateTime(string s)
		{
			return DateTime.TryParse(s, out DateTime v) ? v : (Converted<DateTime, string>)(s + " was not a date and time");
		}
		/// <summary>
		/// Converts the provided string to <see cref="DateTime"/>?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<DateTime?, string> ToNullableDateTime(string s)
		{
			return DateTime.TryParse(s, out DateTime v) ? v : (Converted<DateTime?, string>)(s + " was not a date and time");
		}
		/// <summary>
		/// Converts the provided string to <see cref="TimeSpan"/>.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<TimeSpan, string> ToTimeSpan(string s)
		{
			return TimeSpan.TryParse(s, out TimeSpan v) ? v : (Converted<TimeSpan, string>)(s + " was not a time");
		}
		/// <summary>
		/// Converts the provided string to <see cref="TimeSpan"/>?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<TimeSpan?, string> ToNullableTimeSpan(string s)
		{
			return TimeSpan.TryParse(s, out TimeSpan v) ? v : (Converted<TimeSpan?, string>)(s + " was not a time");
		}
		/// <summary>
		/// Converts the provided string to <see cref="Guid"/>.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<Guid, string> ToGuid(string s)
		{
			return Guid.TryParse(s, out Guid v) ? v : (Converted<Guid, string>)(s + " was not a GUID");
		}
		/// <summary>
		/// Converts the provided string to <see cref="Guid"/>?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<Guid?, string> ToNullableGuid(string s)
		{
			return Guid.TryParse(s, out Guid v) ? v : (Converted<Guid?, string>)(s + " was not a GUID");
		}
		/// <summary>
		/// Converts the provided string to <see cref="Uri"/>. Uses <see cref="UriKind.Absolute"/> as the expected kind.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<Uri, string> ToAbsoluteUri(string s)
		{
			return Uri.TryCreate(s, UriKind.Absolute, out Uri? v) ? v : (Converted<Uri, string>)(s + " was not a URI");
		}
		/// <summary>
		/// Converts the provided string to <see cref="Uri"/>?. Uses <see cref="UriKind.Absolute"/> as the expected kind.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<Uri?, string> ToAbsoluteNullableUri(string s)
		{
			return Uri.TryCreate(s, UriKind.Absolute, out Uri? v) ? v : (Converted<Uri?, string>)(s + " was not a URI");
		}
		/// <summary>
		/// Converts the provided string to <see cref="Uri"/>. Uses <see cref="UriKind.Relative"/> as the expected kind.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<Uri, string> ToRelativeUri(string s)
		{
			return Uri.TryCreate(s, UriKind.Relative, out Uri? v) ? v : (Converted<Uri, string>)(s + " was not a URI");
		}
		/// <summary>
		/// Converts the provided string to <see cref="Uri"/>?. Uses <see cref="UriKind.Relative"/> as the expected kind.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<Uri?, string> ToRelativeNullableUri(string s)
		{
			return Uri.TryCreate(s, UriKind.Relative, out Uri? v) ? v : (Converted<Uri?, string>)(s + " was not a URI");
		}
		/// <summary>
		/// Converts the provided string to <see cref="Uri"/>. Uses <see cref="UriKind.RelativeOrAbsolute"/> as the expected kind.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<Uri, string> ToUri(string s)
		{
			return Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out Uri? v) ? v : (Converted<Uri, string>)(s + " was not a URI");
		}
		/// <summary>
		/// Converts the provided string to <see cref="Uri"/>?. Uses <see cref="UriKind.RelativeOrAbsolute"/> as the expected kind.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Converted<Uri?, string> ToNullableUri(string s)
		{
			return Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out Uri? v) ? v : (Converted<Uri?, string>)(s + " was not a URI");
		}
		/// <summary>
		/// Splits the string into multiple strings based on separators.
		/// Uses s.Split()
		/// </summary>
		/// <param name="s">The string to split</param>
		/// <param name="separator">The separators to use</param>
		public static Converted<string[], string> Split(string s, params char[] separator)
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
		public static Converted<string[], string> Split(string s, char[] separator, int count)
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
		public static Converted<string[], string> Split(string s, char[] separator, StringSplitOptions options)
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
		public static Converted<string[], string> Split(string s, string[] separator, StringSplitOptions options)
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
		public static Converted<string[], string> Split(string s, char[] separator, int count, StringSplitOptions options)
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
		public static Converted<string[], string> Split(string s, string[] separator, int count, StringSplitOptions options)
		{
			return s.Split(separator, count, options);
		}
	}
}
