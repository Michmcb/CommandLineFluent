using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CommandLineFluent
{
	public class FluentParserValidationException : Exception
	{
		public ICollection<Error> Errors { get; }
		public FluentParserValidationException() : base() { }
		public FluentParserValidationException(string message) : base(message) { }
		public FluentParserValidationException(string message, ICollection<Error> errors) : base(message) { Errors = errors; }
		public FluentParserValidationException(string message, Exception innerException) : base(message, innerException) { }
		public FluentParserValidationException(string message, ICollection<Error> errors, Exception innerException) : base(message, innerException) { Errors = errors; }
	}
}
