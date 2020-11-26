namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Arguments.Config;
	using System;
	using static Converters;
	public sealed partial class Verb<TClass> : IVerb where TClass : class, new()
	{
#pragma warning disable CS0612 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
		// TODO add a converter from hexadecimal strings to byte arrays, and methods here for that converter
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, string> AddOptionString(string? shortName, string? longName, Action<OptionConfig<TClass, string>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, NoConversion);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, short> AddOptionShort(string? shortName, string? longName, Action<OptionConfig<TClass, short>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToShort);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, ushort> AddOptionUShort(string? shortName, string? longName, Action<OptionConfig<TClass, ushort>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToUShort);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, int> AddOptionInt(string? shortName, string? longName, Action<OptionConfig<TClass, int>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToInt);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, uint> AddOptionUInt(string? shortName, string? longName, Action<OptionConfig<TClass, uint>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToUInt);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, long> AddOptionLong(string? shortName, string? longName, Action<OptionConfig<TClass, long>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToLong);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, ulong> AddOptionULong(string? shortName, string? longName, Action<OptionConfig<TClass, ulong>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToULong);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, float> AddOptionFloat(string? shortName, string? longName, Action<OptionConfig<TClass, float>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToFloat);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, double> AddOptionDouble(string? shortName, string? longName, Action<OptionConfig<TClass, double>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToDouble);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, decimal> AddOptionDecimal(string? shortName, string? longName, Action<OptionConfig<TClass, decimal>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToDecimal);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, TEnum> AddOptionEnum<TEnum>(string? shortName, string? longName, Action<OptionConfig<TClass, TEnum>> optionConfig) where TEnum : struct, Enum
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, x => ToEnum<TEnum>(x, config.IsCaseSensitive));
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, DateTime> AddOptionDateTime(string? shortName, string? longName, Action<OptionConfig<TClass, DateTime>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToDateTime);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, TimeSpan> AddOptionTimeSpan(string? shortName, string? longName, Action<OptionConfig<TClass, TimeSpan>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToTimeSpan);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, Guid> AddOptionGuid(string? shortName, string? longName, Action<OptionConfig<TClass, Guid>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToGuid);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, Uri> AddOptionUri(string? shortName, string? longName, Action<OptionConfig<TClass, Uri>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToAbsoluteUri);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, string?> AddOptionNullableString(string? shortName, string? longName, Action<OptionConfig<TClass, string?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, NoConversionNullable);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, short?> AddOptionNullableShort(string? shortName, string? longName, Action<OptionConfig<TClass, short?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableShort);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, ushort?> AddOptionNullableUShort(string? shortName, string? longName, Action<OptionConfig<TClass, ushort?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableUShort);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, int?> AddOptionNullableInt(string? shortName, string? longName, Action<OptionConfig<TClass, int?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableInt);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, uint?> AddOptionNullableUInt(string? shortName, string? longName, Action<OptionConfig<TClass, uint?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableUInt);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, long?> AddOptionNullableLong(string? shortName, string? longName, Action<OptionConfig<TClass, long?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableLong);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, ulong?> AddOptionNullableULong(string? shortName, string? longName, Action<OptionConfig<TClass, ulong?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableULong);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, float?> AddOptionNullableFloat(string? shortName, string? longName, Action<OptionConfig<TClass, float?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableFloat);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, double?> AddOptionNullableDouble(string? shortName, string? longName, Action<OptionConfig<TClass, double?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableDouble);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, decimal?> AddOptionNullableDecimal(string? shortName, string? longName, Action<OptionConfig<TClass, decimal?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableDecimal);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, TEnum?> AddOptionNullableEnum<TEnum>(string? shortName, string? longName, Action<OptionConfig<TClass, TEnum?>> optionConfig) where TEnum : struct, Enum
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, x => ToNullableEnum<TEnum>(x, config.IsCaseSensitive));
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, DateTime?> AddOptionNullableDateTime(string? shortName, string? longName, Action<OptionConfig<TClass, DateTime?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableDateTime);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, TimeSpan?> AddOptionNullableTimeSpan(string? shortName, string? longName, Action<OptionConfig<TClass, TimeSpan?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableTimeSpan);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, Guid?> AddOptionNullableGuid(string? shortName, string? longName, Action<OptionConfig<TClass, Guid?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToNullableGuid);
		}
		[Obsolete("Prefer AddOption and pass a NamedArgConfig instead")]public Option<TClass, Uri?> AddOptionNullableUri(string? shortName, string? longName, Action<OptionConfig<TClass, Uri?>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, ToAbsoluteNullableUri);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, string> AddValueString(Action<ValueConfig<TClass, string>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, NoConversion);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, short> AddValueShort(Action<ValueConfig<TClass, short>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToShort);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, ushort> AddValueUShort(Action<ValueConfig<TClass, ushort>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToUShort);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, int> AddValueInt(Action<ValueConfig<TClass, int>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToInt);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, uint> AddValueUInt(Action<ValueConfig<TClass, uint>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToUInt);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, long> AddValueLong(Action<ValueConfig<TClass, long>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToLong);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, ulong> AddValueULong(Action<ValueConfig<TClass, ulong>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToULong);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, float> AddValueFloat(Action<ValueConfig<TClass, float>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToFloat);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, double> AddValueDouble(Action<ValueConfig<TClass, double>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToDouble);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, decimal> AddValueDecimal(Action<ValueConfig<TClass, decimal>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToDecimal);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, TEnum> AddValueEnum<TEnum>(Action<ValueConfig<TClass, TEnum>> valueConfig) where TEnum : struct, Enum
		{
			return AddValueWithConverter(valueConfig, x => ToEnum<TEnum>(x, config.IsCaseSensitive));
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, DateTime> AddValueDateTime(Action<ValueConfig<TClass, DateTime>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToDateTime);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, TimeSpan> AddValueTimeSpan(Action<ValueConfig<TClass, TimeSpan>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToTimeSpan);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, Guid> AddValueGuid(Action<ValueConfig<TClass, Guid>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToGuid);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, Uri> AddValueUrl(Action<ValueConfig<TClass, Uri>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToAbsoluteUri);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, string?> AddValueNullableString(Action<ValueConfig<TClass, string?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, NoConversionNullable);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, short?> AddValueNullableShort(Action<ValueConfig<TClass, short?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableShort);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, ushort?> AddValueNullableUShort(Action<ValueConfig<TClass, ushort?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableUShort);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, int?> AddValueNullableInt(Action<ValueConfig<TClass, int?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableInt);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, uint?> AddValueNullableUInt(Action<ValueConfig<TClass, uint?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableUInt);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, long?> AddValueNullableLong(Action<ValueConfig<TClass, long?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableLong);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, ulong?> AddValueNullableULong(Action<ValueConfig<TClass, ulong?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableULong);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, float?> AddValueNullableFloat(Action<ValueConfig<TClass, float?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableFloat);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, double?> AddValueNullableDouble(Action<ValueConfig<TClass, double?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableDouble);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, decimal?> AddValueNullableDecimal(Action<ValueConfig<TClass, decimal?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableDecimal);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, TEnum?> AddValueNullableEnum<TEnum>(Action<ValueConfig<TClass, TEnum?>> valueConfig) where TEnum : struct, Enum
		{
			return AddValueWithConverter(valueConfig, x => ToNullableEnum<TEnum>(x, config.IsCaseSensitive));
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, DateTime?> AddValueNullableDateTime(Action<ValueConfig<TClass, DateTime?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableDateTime);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, TimeSpan?> AddValueNullableTimeSpan(Action<ValueConfig<TClass, TimeSpan?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableTimeSpan);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, Guid?> AddValueNullableGuid(Action<ValueConfig<TClass, Guid?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToNullableGuid);
		}
		[Obsolete("Prefer AddValue and pass a NamelessArgConfig instead")]public Value<TClass, Uri?> AddValueNullableUrl(Action<ValueConfig<TClass, Uri?>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, ToAbsoluteNullableUri);
		}
		[Obsolete("Prefer AddSwitch and pass a NamedArgConfig instead")]
		public Switch<TClass, bool> AddSwitchBool(string? shortName, string? longName, Action<SwitchConfig<TClass, bool>> switchConfig)
		{
			return AddSwitchWithConverter(shortName, longName, switchConfig, NoConversion);
		}
#pragma warning restore CS0612 // Type or member is obsolete
#pragma warning restore CS0618 // Type or member is obsolete
	}
}
