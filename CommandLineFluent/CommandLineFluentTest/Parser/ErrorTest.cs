using CommandLineFluent;
using System;
using Xunit;

namespace CommandLineFluentTest.Parser
{
	public class ErrorTest
	{
		[Fact]
		public void Constructor()
		{
			Exception ex = new Exception("Test Exception");
			Error error = new Error(ErrorCode.DuplicateOption, true, "Test Message", ex);
			Assert.Equal(ErrorCode.DuplicateOption, error.ErrorCode);
			Assert.Equal("Test Message", error.Message);
			Assert.Equal(ex, error.Exception);
		}
	}
}
