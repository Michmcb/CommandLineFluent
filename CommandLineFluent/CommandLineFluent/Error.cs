using System;

namespace CommandLineFluent
{
	/// <summary>
	/// Represents an error which was encountered when parsing the command line arguments provided
	/// </summary>
	public readonly struct Error : IEquatable<Error>
	{
		/// <summary>
		/// A human-readable error message
		/// </summary>
		public string Message { get; }
		/// <summary>
		/// What exactly went wrong
		/// </summary>
		public ErrorCode ErrorCode { get; }
		/// <summary>
		/// Creates a new instance of Error
		/// </summary>
		/// <param name="errorCode">The error code</param>
		/// <param name="message">The message for this error</param>
		public Error(ErrorCode errorCode, string message)
		{
			Message = message;
			ErrorCode = errorCode;
		}
		/// <summary>
		/// Returns <see cref="ErrorCode"/> - <see cref="Message"/>
		/// </summary>
		/// <returns><see cref="ErrorCode"/> - <see cref="Message"/></returns>
		public override string ToString()
		{
			return string.Concat(ErrorCode.ToString(), " - ", Message);
		}
		public override bool Equals(object? obj)
		{
			return obj is Error error && Equals(error);
		}
		public bool Equals(Error other)
		{
			return ErrorCode == other.ErrorCode;
		}
		public override int GetHashCode()
		{
			return 431791832 + ErrorCode.GetHashCode();
		}
		public static bool operator ==(Error left, Error right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(Error left, Error right)
		{
			return !(left == right);
		}
	}
}
