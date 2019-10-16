using System;
using System.Collections.Generic;

namespace CommandLineFluent
{
	/// <summary>
	/// An exception that occurred when validating that a FluentParserBuilder was set up correctly.
	/// </summary>
	public class FluentParserValidationException : Exception
	{
		/// <summary>
		/// Errors encountered upon validation
		/// </summary>
		public ICollection<Error> Errors { get; }
		/// <summary>
		/// Creates a new instance of FluentParserValidationException
		/// </summary>
		public FluentParserValidationException() : base() { }
		/// <summary>
		/// Creates a new instance of FluentParserValidationException
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public FluentParserValidationException(string message) : base(message) { }
		/// <summary>
		/// Creates a new instance of FluentParserValidationException
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="errors">The errors encountered upon validation</param>
		public FluentParserValidationException(string message, ICollection<Error> errors) : base(message) { Errors = errors; }
		/// <summary>
		/// Creates a new instance of FluentParserValidationException
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception</param>
		public FluentParserValidationException(string message, Exception innerException) : base(message, innerException) { }
		/// <summary>
		/// Creates a new instance of FluentParserValidationException
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="errors">The errors encountered upon validation</param>
		/// <param name="innerException">The exception that is the cause of the current exception</param>
		public FluentParserValidationException(string message, ICollection<Error> errors, Exception innerException) : base(message, innerException) { Errors = errors; }
	}
}
