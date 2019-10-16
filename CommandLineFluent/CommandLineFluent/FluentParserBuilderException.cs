using System;

namespace CommandLineFluent
{
	/// <summary>
	/// An exception that occured during setting up a FluentParserBuilder
	/// </summary>
	public class FluentParserBuilderException : Exception
	{
		/// <summary>
		/// Creates a new instance of FluentParserBuilderException
		/// </summary>
		public FluentParserBuilderException() : base() { }
		/// <summary>
		/// Creates a new instance of FluentParserBuilderException
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public FluentParserBuilderException(string message) : base(message) { }
		/// <summary>
		/// Creates a new instance of FluentParserBuilderException
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception</param>
		public FluentParserBuilderException(string message, Exception innerException) : base(message, innerException) { }
	}
}
