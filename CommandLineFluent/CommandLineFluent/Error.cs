using System;

namespace CommandLineFluent
{
	/// <summary>
	/// Represents an error which was encountered when parsing the command line arguments provided
	/// </summary>
	public class Error
	{
		/// <summary>
		/// A human-readable error message
		/// </summary>
		public string Message { get; }
		/// <summary>
		/// Whether or not this Error is relevant to the user and should be shown to them
		/// </summary>
		public bool ShouldBeShownToUser { get; }
		/// <summary>
		/// What exactly went wrong
		/// </summary>
		public ErrorCode ErrorCode { get; }
		/// <summary>
		/// Any exceptions thrown when parsing the value
		/// </summary>
		public Exception Exception { get; }
		/// <summary>
		/// Creates a new instance of Error
		/// </summary>
		/// <param name="errorCode">The error code</param>
		/// <param name="shouldBeShownToUser">Whether or not this error should be displayed to the user</param>
		/// <param name="message">The message for this error</param>
		/// <param name="exception">The Exception that caused this error</param>
		public Error(ErrorCode errorCode, bool shouldBeShownToUser, string message = null, Exception exception = null)
		{
			Message = message;
			ShouldBeShownToUser = shouldBeShownToUser;
			ErrorCode = errorCode;
			Exception = exception;
		}
		/// <summary>
		/// Converts this Error to a string. Contains ErrorCode, Message, and Exception.ToString()
		/// </summary>
		public override string ToString()
		{
			return $"{ErrorCode}: {Message} ({Exception.ToString()})";
		}
	}
}
