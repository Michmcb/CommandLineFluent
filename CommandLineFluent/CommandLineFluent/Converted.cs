namespace CommandLineFluent
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	/// <summary>
	/// A way of returning either a Value or an Error. Provides methods to safely get either the Value or Error.
	/// You can also use this type directly if an if statement; it evaluates to true/false based on the value of <see cref="Ok"/>.
	/// </summary>
	/// <typeparam name="TVal">The Type on success.</typeparam>
	public readonly struct Converted<TVal>
	{
		private readonly TVal value;
		private readonly string error;
		/// <summary>
		/// Creates a new instance. Either <paramref name="value"/> or <paramref name="error"/> may be null, but not both.
		/// You don't need to use this; this struct can be implicitly cast from objects of either <typeparamref name="TErr"/> or <typeparamref name="TVal"/>.
		/// </summary>
		/// <param name="value">The success value.</param>
		/// <param name="error">The failure value.</param>
		/// <param name="ok">If true, success. If false, failure.</param>
		private Converted([DisallowNull] TVal value, [DisallowNull] string error, bool ok)
		{
			this.value = value;
			this.error = error;
			Ok = ok;
		}
		/// <summary>
		/// If true, has a <typeparamref name="TVal"/>, otherwise has an error message.
		/// When this instance is used in an If statement, it produces this value.
		/// </summary>
		public bool Ok { get; }
		public static bool operator true(Converted<TVal> o) => o.Ok;
		public static bool operator false(Converted<TVal> o) => !o.Ok;
		public static bool operator &(Converted<TVal> lhs, Converted<TVal> rhs) => lhs.Ok && rhs.Ok;
		public static bool operator |(Converted<TVal> lhs, Converted<TVal> rhs) => lhs.Ok || rhs.Ok;
		public static implicit operator bool(Converted<TVal> opt) => opt.Ok;
		/// <summary>
		/// Gets the value, or <paramref name="ifNone"/> if <see cref="Ok"/> is false.
		/// </summary>
		public TVal ValueOr(TVal ifNone) => Ok ? value : ifNone;
		/// <summary>
		/// Gets the error, or <paramref name="ifNone"/> if <see cref="Ok"/> is true.
		/// </summary>
		public string ErrorOr(string ifNone) => Ok ? error : ifNone;
#pragma warning disable CS8762 // Parameter must have a non-null value when exiting in some condition.
		/// <summary>
		/// If <see cref="Ok"/> is true, sets <paramref name="val"/> to the Value for this instance and returns true.
		/// Otherwise, val is set to the default value for <typeparamref name="TVal"/> and returns false.
		/// </summary>
		public bool HasValue([NotNullWhen(true)] out TVal val)
		{
			val = value;
			return Ok;
		}
		/// <summary>
		/// If <see cref="Ok"/> is false, sets <paramref name="error"/> to the Value for this instance and returns true.
		/// Otherwise, val is set to an empty string and returns false.
		/// </summary>
		public bool HasError([NotNullWhen(true)] out string error)
		{
			error = this.error;
			return !Ok;
		}
		/// <summary>
		/// Returns the value of <see cref="Ok"/>. If true, then <paramref name="val"/> is set. Otherwise, <paramref name="error"/> is set.
		/// </summary>
		/// <param name="val">If <see cref="Ok"/> is true, the value. Otherwise, the default value for <typeparamref name="TVal"/>.</param>
		/// <param name="error">If <see cref="Ok"/> is false, the error. Otherwise, empty string.</param>
		public bool Get([NotNullWhen(true)] out TVal val, [NotNullWhen(false)] out string error)
		{
			val = value;
			error = this.error;
			return Ok;
		}
#pragma warning restore CS8762 // Parameter must have a non-null value when exiting in some condition.
		public static implicit operator Converted<TVal>([DisallowNull] TVal value)
		{
			return new Converted<TVal>(value, string.Empty, true);
		}
		public static implicit operator Converted<TVal>([DisallowNull] string error)
		{
			return new Converted<TVal>(default!, error, false);
		}
		public static Converted<TVal> Success(TVal value)
		{
			return new Converted<TVal>(value, string.Empty, true);
		}
		public static Converted<TVal> Failure(string error)
		{
			return new Converted<TVal>(default!, error, false);
		}
		/// <summary>
		/// Calls ToString() on the value if <see cref="Ok"/> is true, otherwise calls ToString() on the error.
		/// </summary>
		public override string ToString()
		{
			return Ok ? value?.ToString() ?? "" : error?.ToString() ?? "";
		}
		public override bool Equals(object obj)
		{
			throw new InvalidOperationException("You cannot compare Converted instances");
		}
		public override int GetHashCode()
		{
			throw new InvalidOperationException("You cannot get HashCodes of Converted instances");
		}
#pragma warning disable IDE0060 // Remove unused parameter
		public static bool operator ==(Converted<TVal> left, Converted<TVal> right)
		{
			throw new InvalidOperationException("You cannot compare Converted instances");
		}
		public static bool operator !=(Converted<TVal> left, Converted<TVal> right)
		{
			throw new InvalidOperationException("You cannot compare Converted instances");
		}
#pragma warning restore IDE0060 // Remove unused parameter
	}
}
