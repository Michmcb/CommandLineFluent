namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Arguments.Config;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using static Converters;
	public sealed partial class Verb<TClass> : IVerb where TClass : class, new()
	{
		// TODO add a converter from hexadecimal strings to byte arrays, and methods here for that converter
		public Option<TClass, string> AddOptionString(string? shortName, string? longName, Action<OptionConfig<TClass, string>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, null);
		}
		public Option<TClass, short> AddOptionShort(string? shortName, string? longName, Action<OptionConfig<TClass, short>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToShort);
		}
		public Option<TClass, ushort> AddOptionUShort(string? shortName, string? longName, Action<OptionConfig<TClass, ushort>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToUShort);
		}
		public Option<TClass, int> AddOptionInt(string? shortName, string? longName, Action<OptionConfig<TClass, int>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToInt);
		}
		public Option<TClass, uint> AddOptionUInt(string? shortName, string? longName, Action<OptionConfig<TClass, uint>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToUInt);
		}
		public Option<TClass, long> AddOptionLong(string? shortName, string? longName, Action<OptionConfig<TClass, long>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToLong);
		}
		public Option<TClass, ulong> AddOptionULong(string? shortName, string? longName, Action<OptionConfig<TClass, ulong>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToULong);
		}
		public Option<TClass, float> AddOptionFloat(string? shortName, string? longName, Action<OptionConfig<TClass, float>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToFloat);
		}
		public Option<TClass, double> AddOptionDouble(string? shortName, string? longName, Action<OptionConfig<TClass, double>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToDouble);
		}
		public Option<TClass, decimal> AddOptionDecimal(string? shortName, string? longName, Action<OptionConfig<TClass, decimal>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToDecimal);
		}
		public Option<TClass, TEnum> AddOptionEnum<TEnum>(string? shortName, string? longName, Action<OptionConfig<TClass, TEnum>> optionConfig) where TEnum : struct, Enum
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToEnum<TEnum>);
		}
		public Option<TClass, DateTime> AddOptionDateTime(string? shortName, string? longName, Action<OptionConfig<TClass, DateTime>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToDateTime);
		}
		public Option<TClass, TimeSpan> AddOptionTimeSpan(string? shortName, string? longName, Action<OptionConfig<TClass, TimeSpan>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToTimeSpan);
		}
		public Option<TClass, Guid> AddOptionGuid(string? shortName, string? longName, Action<OptionConfig<TClass, Guid>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToGuid);
		}
		public Option<TClass, Uri> AddOptionUri(string? shortName, string? longName, Action<OptionConfig<TClass, Uri>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToUri);
		}
		public Option<TClass, string?> AddOptionNullableString(string? shortName, string? longName, Action<OptionConfig<TClass, string?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, null);
		}
		public Option<TClass, short?> AddOptionNullableShort(string? shortName, string? longName, Action<OptionConfig<TClass, short?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableShort);
		}
		public Option<TClass, ushort?> AddOptionNullableUShort(string? shortName, string? longName, Action<OptionConfig<TClass, ushort?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableUShort);
		}
		public Option<TClass, int?> AddOptionNullableInt(string? shortName, string? longName, Action<OptionConfig<TClass, int?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableInt);
		}
		public Option<TClass, uint?> AddOptionNullableUInt(string? shortName, string? longName, Action<OptionConfig<TClass, uint?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableUInt);
		}
		public Option<TClass, long?> AddOptionNullableLong(string? shortName, string? longName, Action<OptionConfig<TClass, long?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableLong);
		}
		public Option<TClass, ulong?> AddOptionNullableULong(string? shortName, string? longName, Action<OptionConfig<TClass, ulong?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableULong);
		}
		public Option<TClass, float?> AddOptionNullableFloat(string? shortName, string? longName, Action<OptionConfig<TClass, float?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableFloat);
		}
		public Option<TClass, double?> AddOptionNullableDouble(string? shortName, string? longName, Action<OptionConfig<TClass, double?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableDouble);
		}
		public Option<TClass, decimal?> AddOptionNullableDecimal(string? shortName, string? longName, Action<OptionConfig<TClass, decimal?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableDecimal);
		}
		public Option<TClass, TEnum?> AddOptionNullableEnum<TEnum>(string? shortName, string? longName, Action<OptionConfig<TClass, TEnum?>> optionConfig) where TEnum : struct, Enum
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableEnum<TEnum>);
		}
		public Option<TClass, DateTime?> AddOptionNullableDateTime(string? shortName, string? longName, Action<OptionConfig<TClass, DateTime?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableDateTime);
		}
		public Option<TClass, TimeSpan?> AddOptionNullableTimeSpan(string? shortName, string? longName, Action<OptionConfig<TClass, TimeSpan?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableTimeSpan);
		}
		public Option<TClass, Guid?> AddOptionNullableGuid(string? shortName, string? longName, Action<OptionConfig<TClass, Guid?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableGuid);
		}
		public Option<TClass, Uri?> AddOptionNullableUri(string? shortName, string? longName, Action<OptionConfig<TClass, Uri?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableUri);
		}

		public Value<TClass, string> AddValueString(Action<ValueConfig<TClass, string>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, null);
		}
		public Value<TClass, short> AddValueShort(Action<ValueConfig<TClass, short>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToShort);
		}
		public Value<TClass, ushort> AddValueUShort(Action<ValueConfig<TClass, ushort>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToUShort);
		}
		public Value<TClass, int> AddValueInt(Action<ValueConfig<TClass, int>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToInt);
		}
		public Value<TClass, uint> AddValueUInt(Action<ValueConfig<TClass, uint>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToUInt);
		}
		public Value<TClass, long> AddValueLong(Action<ValueConfig<TClass, long>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToLong);
		}
		public Value<TClass, ulong> AddValueULong(Action<ValueConfig<TClass, ulong>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToULong);
		}
		public Value<TClass, float> AddValueFloat(Action<ValueConfig<TClass, float>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToFloat);
		}
		public Value<TClass, double> AddValueDouble(Action<ValueConfig<TClass, double>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToDouble);
		}
		public Value<TClass, decimal> AddValueDecimal(Action<ValueConfig<TClass, decimal>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToDecimal);
		}
		public Value<TClass, TEnum> AddValueEnum<TEnum>(Action<ValueConfig<TClass, TEnum>> valueConfig) where TEnum : struct, Enum
		{
			return AddValueWithConverter(valueConfig, ToEnum<TEnum>);
		}
		public Value<TClass, DateTime> AddValueDateTime(Action<ValueConfig<TClass, DateTime>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToDateTime);
		}
		public Value<TClass, TimeSpan> AddValueTimeSpan(Action<ValueConfig<TClass, TimeSpan>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToTimeSpan);
		}
		public Value<TClass, Guid> AddValueGuid(Action<ValueConfig<TClass, Guid>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToGuid);
		}
		public Value<TClass, Uri> AddValueUrl(Action<ValueConfig<TClass, Uri>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToUri);
		}
		public Value<TClass, string?> AddValueNullableString(Action<ValueConfig<TClass, string?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, null);
		}
		public Value<TClass, short?> AddValueNullableShort(Action<ValueConfig<TClass, short?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableShort);
		}
		public Value<TClass, ushort?> AddValueNullableUShort(Action<ValueConfig<TClass, ushort?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableUShort);
		}
		public Value<TClass, int?> AddValueNullableInt(Action<ValueConfig<TClass, int?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableInt);
		}
		public Value<TClass, uint?> AddValueNullableUInt(Action<ValueConfig<TClass, uint?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableUInt);
		}
		public Value<TClass, long?> AddValueNullableLong(Action<ValueConfig<TClass, long?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableLong);
		}
		public Value<TClass, ulong?> AddValueNullableULong(Action<ValueConfig<TClass, ulong?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableULong);
		}
		public Value<TClass, float?> AddValueNullableFloat(Action<ValueConfig<TClass, float?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableFloat);
		}
		public Value<TClass, double?> AddValueNullableDouble(Action<ValueConfig<TClass, double?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableDouble);
		}
		public Value<TClass, decimal?> AddValueNullableDecimal(Action<ValueConfig<TClass, decimal?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableDecimal);
		}
		public Value<TClass, TEnum?> AddValueNullableEnum<TEnum>(Action<ValueConfig<TClass, TEnum?>> valueConfig) where TEnum : struct, Enum
		{
			return AddValueWithConverter(valueConfig, ToNullableEnum<TEnum>);
		}
		public Value<TClass, DateTime?> AddValueNullableDateTime(Action<ValueConfig<TClass, DateTime?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableDateTime);
		}
		public Value<TClass, TimeSpan?> AddValueNullableTimeSpan(Action<ValueConfig<TClass, TimeSpan?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableTimeSpan);
		}
		public Value<TClass, Guid?> AddValueNullableGuid(Action<ValueConfig<TClass, Guid?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableGuid);
		}
		public Value<TClass, Uri?> AddValueNullableUrl(Action<ValueConfig<TClass, Uri?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableUri);
		}
		public Switch<TClass, bool> AddSwitchBool(string? shortName, string? longName, Action<SwitchConfig<TClass, bool>> switchConfig)
		{
			return AddSwitchWithConverter(shortName, longName, switchConfig, null);
		}
	}
}
