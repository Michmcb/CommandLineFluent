namespace CommandLineFluent
{
	/// <summary>
	/// Represents an error which was encountered when parsing the command line arguments provided
	/// </summary>
	public readonly struct Error
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
		public override string ToString()
		{
			return string.Concat(ErrorCode.ToString(), " ", Message);
		}
	}
}
