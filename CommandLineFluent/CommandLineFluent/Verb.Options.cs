namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Arguments.Config;
	using System;
	using System.Linq.Expressions;
	using static Converters;
	public sealed partial class Verb<TClass> : IVerb where TClass : class, new()
	{
		public Option<TClass, string> AddOption(Expression<Func<TClass, string>> expression, Action<NamedArgConfig<TClass, string, string>> config)
		{
			var obj = new NamedArgConfig<TClass, string, string>(true, Converters.ToString);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, short> AddOption(Expression<Func<TClass, short>> expression, Action<NamedArgConfig<TClass, short, string>> config)
		{
			var obj = new NamedArgConfig<TClass, short, string>(true, ToShort);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, ushort> AddOption(Expression<Func<TClass, ushort>> expression, Action<NamedArgConfig<TClass, ushort, string>> config)
		{
			var obj = new NamedArgConfig<TClass, ushort, string>(true, ToUShort);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, int> AddOption(Expression<Func<TClass, int>> expression, Action<NamedArgConfig<TClass, int, string>> config)
		{
			var obj = new NamedArgConfig<TClass, int, string>(true, ToInt);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, uint> AddOption(Expression<Func<TClass, uint>> expression, Action<NamedArgConfig<TClass, uint, string>> config)
		{
			var obj = new NamedArgConfig<TClass, uint, string>(true, ToUInt);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, long> AddOption(Expression<Func<TClass, long>> expression, Action<NamedArgConfig<TClass, long, string>> config)
		{
			var obj = new NamedArgConfig<TClass, long, string>(true, ToLong);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, ulong> AddOption(Expression<Func<TClass, ulong>> expression, Action<NamedArgConfig<TClass, ulong, string>> config)
		{
			var obj = new NamedArgConfig<TClass, ulong, string>(true, ToULong);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, float> AddOption(Expression<Func<TClass, float>> expression, Action<NamedArgConfig<TClass, float, string>> config)
		{
			var obj = new NamedArgConfig<TClass, float, string>(true, ToFloat);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, double> AddOption(Expression<Func<TClass, double>> expression, Action<NamedArgConfig<TClass, double, string>> config)
		{
			var obj = new NamedArgConfig<TClass, double, string>(true, ToDouble);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, decimal> AddOption(Expression<Func<TClass, decimal>> expression, Action<NamedArgConfig<TClass, decimal, string>> config)
		{
			var obj = new NamedArgConfig<TClass, decimal, string>(true, ToDecimal);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, TEnum> AddOption<TEnum>(Expression<Func<TClass, TEnum>> expression, Action<NamedArgConfig<TClass, TEnum, string>> config) where TEnum : struct, Enum
		{
			var obj = new NamedArgConfig<TClass, TEnum, string>(true, ToEnum<TEnum>);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, DateTime> AddOption(Expression<Func<TClass, DateTime>> expression, Action<NamedArgConfig<TClass, DateTime, string>> config)
		{
			var obj = new NamedArgConfig<TClass, DateTime, string>(true, ToDateTime);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, TimeSpan> AddOption(Expression<Func<TClass, TimeSpan>> expression, Action<NamedArgConfig<TClass, TimeSpan, string>> config)
		{
			var obj = new NamedArgConfig<TClass, TimeSpan, string>(true, ToTimeSpan);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, Guid> AddOption(Expression<Func<TClass, Guid>> expression, Action<NamedArgConfig<TClass, Guid, string>> config)
		{
			var obj = new NamedArgConfig<TClass, Guid, string>(true, ToGuid);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, string?> AddOptionNullable(Expression<Func<TClass, string?>> expression, Action<NamedArgConfig<TClass, string?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, string?, string>(false, ToNullableString);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, short?> AddOption(Expression<Func<TClass, short?>> expression, Action<NamedArgConfig<TClass, short?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, short?, string>(false, ToNullableShort);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, ushort?> AddOption(Expression<Func<TClass, ushort?>> expression, Action<NamedArgConfig<TClass, ushort?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, ushort?, string>(false, ToNullableUShort);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, int?> AddOption(Expression<Func<TClass, int?>> expression, Action<NamedArgConfig<TClass, int?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, int?, string>(false, ToNullableInt);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, uint?> AddOption(Expression<Func<TClass, uint?>> expression, Action<NamedArgConfig<TClass, uint?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, uint?, string>(false, ToNullableUInt);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, long?> AddOption(Expression<Func<TClass, long?>> expression, Action<NamedArgConfig<TClass, long?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, long?, string>(false, ToNullableLong);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, ulong?> AddOption(Expression<Func<TClass, ulong?>> expression, Action<NamedArgConfig<TClass, ulong?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, ulong?, string>(false, ToNullableULong);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, float?> AddOption(Expression<Func<TClass, float?>> expression, Action<NamedArgConfig<TClass, float?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, float?, string>(false, ToNullableFloat);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, double?> AddOption(Expression<Func<TClass, double?>> expression, Action<NamedArgConfig<TClass, double?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, double?, string>(false, ToNullableDouble);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, decimal?> AddOption(Expression<Func<TClass, decimal?>> expression, Action<NamedArgConfig<TClass, decimal?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, decimal?, string>(false, ToNullableDecimal);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, TEnum?> AddOption<TEnum>(Expression<Func<TClass, TEnum?>> expression, Action<NamedArgConfig<TClass, TEnum?, string>> config) where TEnum : struct, Enum
		{
			var obj = new NamedArgConfig<TClass, TEnum?, string>(false, ToNullableEnum<TEnum>);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, DateTime?> AddOption(Expression<Func<TClass, DateTime?>> expression, Action<NamedArgConfig<TClass, DateTime?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, DateTime?, string>(false, ToNullableDateTime);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, TimeSpan?> AddOption(Expression<Func<TClass, TimeSpan?>> expression, Action<NamedArgConfig<TClass, TimeSpan?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, TimeSpan?, string>(false, ToNullableTimeSpan);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Option<TClass, Guid?> AddOption(Expression<Func<TClass, Guid?>> expression, Action<NamedArgConfig<TClass, Guid?, string>> config)
		{
			var obj = new NamedArgConfig<TClass, Guid?, string>(false, ToNullableGuid);
			config(obj);
			return AddOptionCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
	}
}
