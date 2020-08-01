using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CommandLineFluent
{
	[Serializable]
	internal class CliParserException : Exception
	{
		public CliParserException()
		{
		}
		public CliParserException(string message) : base(message)
		{
		}
		public CliParserException(string message, Exception innerException) : base(message, innerException)
		{
		}
		protected CliParserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}