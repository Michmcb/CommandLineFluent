namespace CommandLineFluent
{
	using System;
	public static class Converters
	{
		//public static Maybe<byte[], string> FromHexString(string s)
		//{
		//}
		/// <summary>
		/// If <paramref name="s"/> is 1 character, returns that. Otherwise, error.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<char, string> ToChar(string s)
		{
			if (s.Length == 1)
			{
				return s[0];
			}
			else
			{
				return s + " must be a single character";
			}
		}
		/// <summary>
		/// If <paramref name="s"/> is 1 character, returns that. Otherwise, error.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<char?, string> ToNullableChar(string s)
		{
			if (s.Length == 1)
			{
				return s[0];
			}
			else
			{
				return s + " must be a single character";
			}
		}
		/// <summary>
		/// Converts the provided string to short.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<short, string> ToShort(string s)
		{
			if (short.TryParse(s, out short v))
			{
				return v;
			}
			else
			{
				return s + " was not an integer";
			}
		}
		/// <summary>
		/// Converts the provided string to short?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<short?, string> ToNullableShort(string s)
		{
			if (short.TryParse(s, out short v))
			{
				return v;
			}
			else
			{
				return s + " was not an integer";
			}
		}
		/// <summary>
		/// Converts the provided string to ushort.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<ushort, string> ToUShort(string s)
		{
			if (ushort.TryParse(s, out ushort v))
			{
				return v;
			}
			else
			{
				return s + " was not an integer";
			}
		}
		/// <summary>
		/// Converts the provided string to ushort?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<ushort?, string> ToNullableUShort(string s)
		{
			if (ushort.TryParse(s, out ushort v))
			{
				return v;
			}
			else
			{
				return s + " was not an integer";
			}
		}
		/// <summary>
		/// Converts the provided string to int.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<int, string> ToInt(string s)
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
		/// Converts the provided string to int?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<int?, string> ToNullableInt(string s)
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
		/// Converts the provided string to uint.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<uint, string> ToUInt(string s)
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
		/// Converts the provided string to uint?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<uint?, string> ToNullableUInt(string s)
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
		/// Converts the provided string to long.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<long, string> ToLong(string s)
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
		/// Converts the provided string to long?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<long?, string> ToNullableLong(string s)
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
		/// Converts the provided string to ulong.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<ulong, string> ToULong(string s)
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
		/// Converts the provided string to ulong?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<ulong?, string> ToNullableULong(string s)
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
		/// Converts the provided string to float.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<float, string> ToFloat(string s)
		{
			if (float.TryParse(s, out float v))
			{
				return v;
			}
			else
			{
				return s + " was not a floating-point number";
			}
		}
		/// <summary>
		/// Converts the provided string to float?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<float?, string> ToNullableFloat(string s)
		{
			if (float.TryParse(s, out float v))
			{
				return v;
			}
			else
			{
				return s + " was not a floating-point number";
			}
		}
		/// <summary>
		/// Converts the provided string to double.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<double, string> ToDouble(string s)
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
		/// Converts the provided string to double?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<double?, string> ToNullableDouble(string s)
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
		/// Converts the provided string to decimal.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<decimal, string> ToDecimal(string s)
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
		/// Converts the provided string to decimal?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<decimal?, string> ToNullableDecimal(string s)
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
		/// Converts the provided string to the Enum <typeparamref name="TEnum"/>.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum to parse</typeparam>
		/// <param name="s">The string to convert</param>
		public static Maybe<TEnum, string> ToEnum<TEnum>(string s) where TEnum : struct, Enum
		{
			if (Enum.TryParse(s, out TEnum v))
			{
				return v;
			}
			else
			{
				return string.Concat(s, " should be one of: ", string.Join(", ", Enum.GetNames(typeof(TEnum))));
			}
		}
		/// <summary>
		/// Converts the provided string to the Enum <typeparamref name="TEnum"/>?.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum to parse</typeparam>
		/// <param name="s">The string to convert</param>
		public static Maybe<TEnum?, string> ToNullableEnum<TEnum>(string s) where TEnum : struct, Enum
		{
			if (Enum.TryParse(s, out TEnum v))
			{
				return v;
			}
			else
			{
				return string.Concat(s, " should be one of: ", string.Join(", ", Enum.GetNames(typeof(TEnum))));
			}
		}
		/// <summary>
		/// Converts the provided string to DateTime.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<DateTime, string> ToDateTime(string s)
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
		/// Converts the provided string to DateTime?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<DateTime?, string> ToNullableDateTime(string s)
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
		/// Converts the provided string to TimeSpan.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<TimeSpan, string> ToTimeSpan(string s)
		{
			if (TimeSpan.TryParse(s, out TimeSpan v))
			{
				return v;
			}
			else
			{
				return s + " was not a time";
			}
		}
		/// <summary>
		/// Converts the provided string to TimeSpan?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<TimeSpan?, string> ToNullableTimeSpan(string s)
		{
			if (TimeSpan.TryParse(s, out TimeSpan v))
			{
				return v;
			}
			else
			{
				return s + " was not a time";
			}
		}
		/// <summary>
		/// Converts the provided string to Guid.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<Guid, string> ToGuid(string s)
		{
			if (Guid.TryParse(s, out Guid v))
			{
				return v;
			}
			else
			{
				return s + " was not a GUID";
			}
		}
		/// <summary>
		/// Converts the provided string to Guid?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<Guid?, string> ToNullableGuid(string s)
		{
			if (Guid.TryParse(s, out Guid v))
			{
				return v;
			}
			else
			{
				return s + " was not a GUID";
			}
		}
		/// <summary>
		/// Converts the provided string to Uri.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<Uri, string> ToUri(string s)
		{
			if (Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out Uri v))
			{
				return v;
			}
			else
			{
				return s + " was not a URL";
			}
		}
		/// <summary>
		/// Converts the provided string to Uri?.
		/// </summary>
		/// <param name="s">The string to convert</param>
		public static Maybe<Uri?, string> ToNullableUri(string s)
		{
			if (Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out Uri v))
			{
				return v;
			}
			else
			{
				return s + " was not a URL";
			}
		}
		/// <summary>
		/// Splits the string into multiple strings based on separators.
		/// Uses s.Split()
		/// </summary>
		/// <param name="s">The string to split</param>
		/// <param name="separator">The separators to use</param>
		public static Maybe<string[], string> Split(string s, params char[] separator)
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
		public static Maybe<string[], string> Split(string s, char[] separator, int count)
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
		public static Maybe<string[], string> Split(string s, char[] separator, StringSplitOptions options)
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
		public static Maybe<string[], string> Split(string s, string[] separator, StringSplitOptions options)
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
		public static Maybe<string[], string> Split(string s, char[] separator, int count, StringSplitOptions options)
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
		public static Maybe<string[], string> Split(string s, string[] separator, int count, StringSplitOptions options)
		{
			return s.Split(separator, count, options);
		}
	}
}
