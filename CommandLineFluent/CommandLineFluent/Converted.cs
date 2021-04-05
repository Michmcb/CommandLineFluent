namespace CommandLineFluent
{
	using System;

	/// <summary>
	/// A way of returning either a Value or an Error. Provides methods to safely get either the Value or Error.
	/// If <see cref="Ok"/> is true, then only <typeparamref name="TVal"/> is valid.
	/// If <see cref="Ok"/> is false, then only <typeparamref name="TErr"/> is valid.
	/// </summary>
	/// <typeparam name="TVal">The Type on success.</typeparam>
	/// <typeparam name="TErr">The Type on failure.</typeparam>
	public readonly struct Converted<TVal, TErr>
	{
		private readonly TVal value;
		private readonly TErr error;
		/// <summary>
		/// Creates a new instance. Either <paramref name="value"/> or <paramref name="error"/> may be null, but not both.
		/// You don't need to use this; this struct can be implicitly cast from objects of either <typeparamref name="TErr"/> or <typeparamref name="TVal"/>.
		/// </summary>
		/// <param name="value">The success value.</param>
		/// <param name="error">The failure value.</param>
		/// <param name="ok">If true, success. If false, failure.</param>
		private Converted(TVal value, TErr error, bool ok)
		{
			if (ok && value == null)
			{
				throw new ArgumentNullException(nameof(value), "Can't make a Converted from a null value when ok is true");
			}
			if (!ok && error == null)
			{
				throw new ArgumentNullException(nameof(error), "Can't make a Converted from a null error when ok is false");
			}
			this.value = value;
			this.error = error;
			Ok = ok;
		}
		/// <summary>
		/// If true, has a <typeparamref name="TVal"/>, otherwise has a <typeparamref name="TErr"/>.
		/// When this instance is used in an If statement, it produces this value.
		/// </summary>
		public bool Ok { get; }
		/// <summary>
		/// Gets the value, or <paramref name="ifNone"/> if <see cref="Ok"/> is false.
		/// </summary>
		public TVal ValueOr(TVal ifNone)
		{
			return Ok ? value : ifNone!;
		}
		/// <summary>
		/// Gets the error, or <paramref name="ifNone"/> if <see cref="Ok"/> is true.
		/// </summary>
		public TErr ErrorOr(TErr ifNone)
		{
			return Ok ? ifNone! : error;
		}
		/// <summary>
		/// If <see cref="Ok"/> is true, sets <paramref name="val"/> to the Value for this instance and returns true.
		/// Otherwise, val is set to the default value for <typeparamref name="TVal"/> and returns false.
		/// </summary>
		/// <param name="val"></param>
		public bool HasValue(out TVal val)
		{
			val = value;
			return Ok;
		}
		/// <summary>
		/// If <see cref="Ok"/> is false, sets <paramref name="error"/> to the Value for this instance and returns true.
		/// Otherwise, val is set to the default value for <typeparamref name="TErr"/> and returns false.
		/// </summary>
		/// <param name="val"></param>
		public bool HasError(out TErr error)
		{
			error = this.error;
			return !Ok;
		}
		/// <summary>
		/// Returns the value of <see cref="Ok"/>. If true, then <paramref name="val"/> is set. Otherwise, <paramref name="error"/> is set.
		/// </summary>
		/// <param name="val">If <see cref="Ok"/> is true, the value. Otherwise, the default value for <typeparamref name="TVal"/>.</param>
		/// <param name="error">If <see cref="Ok"/> is false, the error. Otherwise, the default value for <typeparamref name="TErr"/>.</param>
		public bool Success(out TVal val, out TErr error)
		{
			val = value;
			error = this.error;
			return Ok;
		}
		public static Converted<TVal, TErr> Value(TVal value)
		{
			return new Converted<TVal, TErr>(value, default!, true);
		}
		public static Converted<TVal, TErr> Error(TErr error)
		{
			return new Converted<TVal, TErr>(default!, error, false);
		}
		/// <summary>
		/// Equivalent to new Converted(<paramref name="value"/>, default, true);
		/// </summary>
		public static implicit operator Converted<TVal, TErr>(TVal value) => new(value, default!, true);
		/// <summary>
		/// Equivalent to new Converted(default, <paramref name="error"/>, true);
		/// </summary>
		public static implicit operator Converted<TVal, TErr>(TErr error) => new(default!, error, false);
		/// <summary>
		/// Calls ToString() on the value if <see cref="Ok"/> is true, otherwise calls ToString() on the error.
		/// </summary>
		public override string ToString()
		{
			return Ok ? value?.ToString() ?? string.Empty : error?.ToString() ?? string.Empty;
		}
	}
}
