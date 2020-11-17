namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Arguments.Config;
	using System;
	using System.Linq.Expressions;
	using static Converters;
	public sealed partial class Verb<TClass> : IVerb where TClass : class, new()
	{
		/// <summary>
		/// Adds a new Value to set the string specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, string> AddValue(Expression<Func<TClass, string>> expression, Action<NamelessArgConfig<TClass, string>> config)
		{
			var obj = new NamelessArgConfig<TClass, string>(true, NoConversion);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the short specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, short> AddValue(Expression<Func<TClass, short>> expression, Action<NamelessArgConfig<TClass, short>> config)
		{
			var obj = new NamelessArgConfig<TClass, short>(true, ToShort);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the ushort specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, ushort> AddValue(Expression<Func<TClass, ushort>> expression, Action<NamelessArgConfig<TClass, ushort>> config)
		{
			var obj = new NamelessArgConfig<TClass, ushort>(true, ToUShort);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the int specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, int> AddValue(Expression<Func<TClass, int>> expression, Action<NamelessArgConfig<TClass, int>> config)
		{
			var obj = new NamelessArgConfig<TClass, int>(true, ToInt);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the uint specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, uint> AddValue(Expression<Func<TClass, uint>> expression, Action<NamelessArgConfig<TClass, uint>> config)
		{
			var obj = new NamelessArgConfig<TClass, uint>(true, ToUInt);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the long specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, long> AddValue(Expression<Func<TClass, long>> expression, Action<NamelessArgConfig<TClass, long>> config)
		{
			var obj = new NamelessArgConfig<TClass, long>(true, ToLong);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the ulong specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, ulong> AddValue(Expression<Func<TClass, ulong>> expression, Action<NamelessArgConfig<TClass, ulong>> config)
		{
			var obj = new NamelessArgConfig<TClass, ulong>(true, ToULong);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the float specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, float> AddValue(Expression<Func<TClass, float>> expression, Action<NamelessArgConfig<TClass, float>> config)
		{
			var obj = new NamelessArgConfig<TClass, float>(true, ToFloat);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the double specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, double> AddValue(Expression<Func<TClass, double>> expression, Action<NamelessArgConfig<TClass, double>> config)
		{
			var obj = new NamelessArgConfig<TClass, double>(true, ToDouble);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the decimal specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, decimal> AddValue(Expression<Func<TClass, decimal>> expression, Action<NamelessArgConfig<TClass, decimal>> config)
		{
			var obj = new NamelessArgConfig<TClass, decimal>(true, ToDecimal);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the TEnum specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, TEnum> AddValue<TEnum>(Expression<Func<TClass, TEnum>> expression, Action<NamelessArgConfig<TClass, TEnum>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessArgConfig<TClass, TEnum>(true, (x) => ToEnum<TEnum>(x, this.config.IsCaseSensitive));
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the DateTime specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, DateTime> AddValue(Expression<Func<TClass, DateTime>> expression, Action<NamelessArgConfig<TClass, DateTime>> config)
		{
			var obj = new NamelessArgConfig<TClass, DateTime>(true, ToDateTime);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the TimeSpan specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, TimeSpan> AddValue(Expression<Func<TClass, TimeSpan>> expression, Action<NamelessArgConfig<TClass, TimeSpan>> config)
		{
			var obj = new NamelessArgConfig<TClass, TimeSpan>(true, ToTimeSpan);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the Guid specified by <paramref name="expression"/>. By default this Value is required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, Guid> AddValue(Expression<Func<TClass, Guid>> expression, Action<NamelessArgConfig<TClass, Guid>> config)
		{
			var obj = new NamelessArgConfig<TClass, Guid>(true, ToGuid);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the string? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, string?> AddValueNullable(Expression<Func<TClass, string?>> expression, Action<NamelessArgConfig<TClass, string?>> config)
		{
			var obj = new NamelessArgConfig<TClass, string?>(false, NoConversionNullable);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the short? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, short?> AddValue(Expression<Func<TClass, short?>> expression, Action<NamelessArgConfig<TClass, short?>> config)
		{
			var obj = new NamelessArgConfig<TClass, short?>(false, ToNullableShort);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the ushort? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, ushort?> AddValue(Expression<Func<TClass, ushort?>> expression, Action<NamelessArgConfig<TClass, ushort?>> config)
		{
			var obj = new NamelessArgConfig<TClass, ushort?>(false, ToNullableUShort);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the int? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, int?> AddValue(Expression<Func<TClass, int?>> expression, Action<NamelessArgConfig<TClass, int?>> config)
		{
			var obj = new NamelessArgConfig<TClass, int?>(false, ToNullableInt);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the uint? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, uint?> AddValue(Expression<Func<TClass, uint?>> expression, Action<NamelessArgConfig<TClass, uint?>> config)
		{
			var obj = new NamelessArgConfig<TClass, uint?>(false, ToNullableUInt);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the long? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, long?> AddValue(Expression<Func<TClass, long?>> expression, Action<NamelessArgConfig<TClass, long?>> config)
		{
			var obj = new NamelessArgConfig<TClass, long?>(false, ToNullableLong);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the ulong? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, ulong?> AddValue(Expression<Func<TClass, ulong?>> expression, Action<NamelessArgConfig<TClass, ulong?>> config)
		{
			var obj = new NamelessArgConfig<TClass, ulong?>(false, ToNullableULong);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the float? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, float?> AddValue(Expression<Func<TClass, float?>> expression, Action<NamelessArgConfig<TClass, float?>> config)
		{
			var obj = new NamelessArgConfig<TClass, float?>(false, ToNullableFloat);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the double? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, double?> AddValue(Expression<Func<TClass, double?>> expression, Action<NamelessArgConfig<TClass, double?>> config)
		{
			var obj = new NamelessArgConfig<TClass, double?>(false, ToNullableDouble);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the decimal? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, decimal?> AddValue(Expression<Func<TClass, decimal?>> expression, Action<NamelessArgConfig<TClass, decimal?>> config)
		{
			var obj = new NamelessArgConfig<TClass, decimal?>(false, ToNullableDecimal);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the TEnum? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, TEnum?> AddValue<TEnum>(Expression<Func<TClass, TEnum?>> expression, Action<NamelessArgConfig<TClass, TEnum?>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessArgConfig<TClass, TEnum?>(false, (x) => ToNullableEnum<TEnum>(x, this.config.IsCaseSensitive));
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the DateTime? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, DateTime?> AddValue(Expression<Func<TClass, DateTime?>> expression, Action<NamelessArgConfig<TClass, DateTime?>> config)
		{
			var obj = new NamelessArgConfig<TClass, DateTime?>(false, ToNullableDateTime);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the TimeSpan? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, TimeSpan?> AddValue(Expression<Func<TClass, TimeSpan?>> expression, Action<NamelessArgConfig<TClass, TimeSpan?>> config)
		{
			var obj = new NamelessArgConfig<TClass, TimeSpan?>(false, ToNullableTimeSpan);
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value to set the Guid? specified by <paramref name="expression"/>. By default this Value is not required.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, Guid?> AddValue(Expression<Func<TClass, Guid?>> expression, Action<NamelessArgConfig<TClass, Guid?>> config)
		{
			var obj = new NamelessArgConfig<TClass, Guid?>(false, ToNullableGuid);
			config(obj);
			return AddValueCore(expression, obj);
		}
	}
}
