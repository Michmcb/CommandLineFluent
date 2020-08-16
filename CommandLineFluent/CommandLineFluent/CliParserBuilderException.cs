namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	public class CliParserBuilderException : Exception
	{
		public ICollection<Error> Errors { get; }
		/// <summary>
		/// Creates a new instance of BuildParserException
		/// </summary>
		public CliParserBuilderException() : base() { Errors = Array.Empty<Error>(); }
		/// <summary>
		/// Creates a new instance of BuildParserException
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public CliParserBuilderException(string message) : base(message) { Errors = Array.Empty<Error>(); }
		/// <summary>
		/// Creates a new instance of BuildParserException
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="errors">The errors encountered upon validation</param>
		public CliParserBuilderException(string message, ICollection<Error> errors) : base(message) { Errors = errors; }
		/// <summary>
		/// Creates a new instance of BuildParserException
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception</param>
		public CliParserBuilderException(string message, Exception innerException) : base(message, innerException) { Errors = Array.Empty<Error>(); }
		/// <summary>
		/// Creates a new instance of BuildParserException
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="errors">The errors encountered upon validation</param>
		/// <param name="innerException">The exception that is the cause of the current exception</param>
		public CliParserBuilderException(string message, ICollection<Error> errors, Exception innerException) : base(message, innerException) { Errors = errors; }
		public override string ToString()
		{
			return string.Concat(base.ToString(), Environment.NewLine, string.Join(Environment.NewLine, Errors));
		}
	}
}
