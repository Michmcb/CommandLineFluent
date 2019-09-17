using System;

namespace CommandLineFluent
{
	public class FluentParserException : Exception
	{
		public ErrorCode Error { get; }
		public FluentParserException() : base() { }
		public FluentParserException(string message) : base(message) { }
		public FluentParserException(string message, ErrorCode error) : base(message) { Error = error; }
		public FluentParserException(string message, Exception innerException) : base(message, innerException) { }
		public FluentParserException(string message, ErrorCode error, Exception innerException) : base(message, innerException) { Error = error; }
	}
}
