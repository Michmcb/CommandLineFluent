namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Arguments.Config;
	using System;
	using System.Linq.Expressions;
	using static Converters;
	public sealed partial class Verb<TClass> : IVerb where TClass : class, new()
	{
		public Value<TClass, string?> AddValue(Expression<Func<TClass, string?>> expression, NamelessArgConfig<TClass, string?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), null);
		}
		public Value<TClass, short> AddValue(Expression<Func<TClass, short>> expression, NamelessArgConfig<TClass, short, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToShort);
		}
		public Value<TClass, ushort> AddValue(Expression<Func<TClass, ushort>> expression, NamelessArgConfig<TClass, ushort, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToUShort);
		}
		public Value<TClass, int> AddValue(Expression<Func<TClass, int>> expression, NamelessArgConfig<TClass, int, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToInt);
		}
		public Value<TClass, uint> AddValue(Expression<Func<TClass, uint>> expression, NamelessArgConfig<TClass, uint, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToUInt);
		}
		public Value<TClass, long> AddValue(Expression<Func<TClass, long>> expression, NamelessArgConfig<TClass, long, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToLong);
		}
		public Value<TClass, ulong> AddValue(Expression<Func<TClass, ulong>> expression, NamelessArgConfig<TClass, ulong, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToULong);
		}
		public Value<TClass, float> AddValue(Expression<Func<TClass, float>> expression, NamelessArgConfig<TClass, float, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToFloat);
		}
		public Value<TClass, double> AddValue(Expression<Func<TClass, double>> expression, NamelessArgConfig<TClass, double, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToDouble);
		}
		public Value<TClass, decimal> AddValue(Expression<Func<TClass, decimal>> expression, NamelessArgConfig<TClass, decimal, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToDecimal);
		}
		public Value<TClass, TEnum> AddValue<TEnum>(Expression<Func<TClass, TEnum>> expression, NamelessArgConfig<TClass, TEnum, string> config) where TEnum : struct, Enum
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToEnum<TEnum>);
		}
		public Value<TClass, DateTime> AddValue(Expression<Func<TClass, DateTime>> expression, NamelessArgConfig<TClass, DateTime, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToDateTime);
		}
		public Value<TClass, TimeSpan> AddValue(Expression<Func<TClass, TimeSpan>> expression, NamelessArgConfig<TClass, TimeSpan, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToTimeSpan);
		}
		public Value<TClass, Guid> AddValue(Expression<Func<TClass, Guid>> expression, NamelessArgConfig<TClass, Guid, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToGuid);
		}
		public Value<TClass, short?> AddValue(Expression<Func<TClass, short?>> expression, NamelessArgConfig<TClass, short?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableShort);
		}
		public Value<TClass, ushort?> AddValue(Expression<Func<TClass, ushort?>> expression, NamelessArgConfig<TClass, ushort?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableUShort);
		}
		public Value<TClass, int?> AddValue(Expression<Func<TClass, int?>> expression, NamelessArgConfig<TClass, int?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableInt);
		}
		public Value<TClass, uint?> AddValue(Expression<Func<TClass, uint?>> expression, NamelessArgConfig<TClass, uint?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableUInt);
		}
		public Value<TClass, long?> AddValue(Expression<Func<TClass, long?>> expression, NamelessArgConfig<TClass, long?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableLong);
		}
		public Value<TClass, ulong?> AddValue(Expression<Func<TClass, ulong?>> expression, NamelessArgConfig<TClass, ulong?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableULong);
		}
		public Value<TClass, float?> AddValue(Expression<Func<TClass, float?>> expression, NamelessArgConfig<TClass, float?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableFloat);
		}
		public Value<TClass, double?> AddValue(Expression<Func<TClass, double?>> expression, NamelessArgConfig<TClass, double?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableDouble);
		}
		public Value<TClass, decimal?> AddValue(Expression<Func<TClass, decimal?>> expression, NamelessArgConfig<TClass, decimal?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableDecimal);
		}
		public Value<TClass, TEnum?> AddValue<TEnum>(Expression<Func<TClass, TEnum?>> expression, NamelessArgConfig<TClass, TEnum?, string> config) where TEnum : struct, Enum
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableEnum<TEnum>);
		}
		public Value<TClass, DateTime?> AddValue(Expression<Func<TClass, DateTime?>> expression, NamelessArgConfig<TClass, DateTime?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableDateTime);
		}
		public Value<TClass, TimeSpan?> AddValue(Expression<Func<TClass, TimeSpan?>> expression, NamelessArgConfig<TClass, TimeSpan?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableTimeSpan);
		}
		public Value<TClass, Guid?> AddValue(Expression<Func<TClass, Guid?>> expression, NamelessArgConfig<TClass, Guid?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableGuid);
		}
		public Value<TClass, Uri?> AddValue(Expression<Func<TClass, Uri?>> expression, NamelessArgConfig<TClass, Uri?, string> config)
		{
			return AddValue(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableUri);
		}
		public Option<TClass, string?> AddOption(Expression<Func<TClass, string?>> expression, NamedArgConfig<TClass, string?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), null);
		}
		public Option<TClass, short> AddOption(Expression<Func<TClass, short>> expression, NamedArgConfig<TClass, short, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToShort);
		}
		public Option<TClass, ushort> AddOption(Expression<Func<TClass, ushort>> expression, NamedArgConfig<TClass, ushort, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToUShort);
		}
		public Option<TClass, int> AddOption(Expression<Func<TClass, int>> expression, NamedArgConfig<TClass, int, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToInt);
		}
		public Option<TClass, uint> AddOption(Expression<Func<TClass, uint>> expression, NamedArgConfig<TClass, uint, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToUInt);
		}
		public Option<TClass, long> AddOption(Expression<Func<TClass, long>> expression, NamedArgConfig<TClass, long, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToLong);
		}
		public Option<TClass, ulong> AddOption(Expression<Func<TClass, ulong>> expression, NamedArgConfig<TClass, ulong, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToULong);
		}
		public Option<TClass, float> AddOption(Expression<Func<TClass, float>> expression, NamedArgConfig<TClass, float, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToFloat);
		}
		public Option<TClass, double> AddOption(Expression<Func<TClass, double>> expression, NamedArgConfig<TClass, double, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToDouble);
		}
		public Option<TClass, decimal> AddOption(Expression<Func<TClass, decimal>> expression, NamedArgConfig<TClass, decimal, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToDecimal);
		}
		public Option<TClass, TEnum> AddOption<TEnum>(Expression<Func<TClass, TEnum>> expression, NamedArgConfig<TClass, TEnum, string> config) where TEnum : struct, Enum
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToEnum<TEnum>);
		}
		public Option<TClass, DateTime> AddOption(Expression<Func<TClass, DateTime>> expression, NamedArgConfig<TClass, DateTime, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToDateTime);
		}
		public Option<TClass, TimeSpan> AddOption(Expression<Func<TClass, TimeSpan>> expression, NamedArgConfig<TClass, TimeSpan, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToTimeSpan);
		}
		public Option<TClass, Guid> AddOption(Expression<Func<TClass, Guid>> expression, NamedArgConfig<TClass, Guid, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToGuid);
		}
		public Option<TClass, short?> AddOption(Expression<Func<TClass, short?>> expression, NamedArgConfig<TClass, short?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableShort);
		}
		public Option<TClass, ushort?> AddOption(Expression<Func<TClass, ushort?>> expression, NamedArgConfig<TClass, ushort?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableUShort);
		}
		public Option<TClass, int?> AddOption(Expression<Func<TClass, int?>> expression, NamedArgConfig<TClass, int?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableInt);
		}
		public Option<TClass, uint?> AddOption(Expression<Func<TClass, uint?>> expression, NamedArgConfig<TClass, uint?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableUInt);
		}
		public Option<TClass, long?> AddOption(Expression<Func<TClass, long?>> expression, NamedArgConfig<TClass, long?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableLong);
		}
		public Option<TClass, ulong?> AddOption(Expression<Func<TClass, ulong?>> expression, NamedArgConfig<TClass, ulong?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableULong);
		}
		public Option<TClass, float?> AddOption(Expression<Func<TClass, float?>> expression, NamedArgConfig<TClass, float?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableFloat);
		}
		public Option<TClass, double?> AddOption(Expression<Func<TClass, double?>> expression, NamedArgConfig<TClass, double?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableDouble);
		}
		public Option<TClass, decimal?> AddOption(Expression<Func<TClass, decimal?>> expression, NamedArgConfig<TClass, decimal?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableDecimal);
		}
		public Option<TClass, TEnum?> AddOption<TEnum>(Expression<Func<TClass, TEnum?>> expression, NamedArgConfig<TClass, TEnum?, string> config) where TEnum : struct, Enum
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableEnum<TEnum>);
		}
		public Option<TClass, DateTime?> AddOption(Expression<Func<TClass, DateTime?>> expression, NamedArgConfig<TClass, DateTime?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableDateTime);
		}
		public Option<TClass, TimeSpan?> AddOption(Expression<Func<TClass, TimeSpan?>> expression, NamedArgConfig<TClass, TimeSpan?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableTimeSpan);
		}
		public Option<TClass, Guid?> AddOption(Expression<Func<TClass, Guid?>> expression, NamedArgConfig<TClass, Guid?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableGuid);
		}
		public Option<TClass, Uri?> AddOption(Expression<Func<TClass, Uri?>> expression, NamedArgConfig<TClass, Uri?, string> config)
		{
			return AddOption(config, ArgUtils.PropertyInfoFromExpression(expression), ToNullableUri);
		}
		public Switch<TClass, bool> AddSwitch(Expression<Func<TClass, Uri?>> expression, NamedArgConfig<TClass, bool, bool> config)
		{
			return AddSwitch(config, ArgUtils.PropertyInfoFromExpression(expression), null);
		}
	}
}
