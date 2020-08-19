namespace CommandLineFluent.Test.Error
{
	using CommandLineFluent;
	using Xunit;

	public sealed class Ctor
	{
		[Fact]
		public void Constructor()
		{
			Error error = new Error(ErrorCode.DuplicateOption, "Test Message");
			Assert.Equal(ErrorCode.DuplicateOption, error.ErrorCode);
			Assert.Equal("Test Message", error.Message);
		}
	}
}
