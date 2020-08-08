namespace CommandLineFluent.Test.ParseResult
{
	using CommandLineFluent;
	using CommandLineFluent.Test.Options;
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public sealed class Invoke
	{
		private static readonly CliParser parser = new CliParserBuilder()
			.AddVerb<InvokedTracker>("Invoke1", verb =>
			{
				verb.Invoke = SetInvoked;
				verb.InvokeAsync = SetInvokedAsync;
			})
			.AddVerb<OptEmpty>("Invoke2", verb =>
			{
				verb.Invoke = BadInvoke;
				verb.InvokeAsync = BadInvokeAsync;
			})
			.Build();
		[Fact]
		public async Task InvokeWorksFine()
		{
			ParseResult<InvokedTracker> ipr = Assert.IsType<ParseResult<InvokedTracker>>(parser.Parse("Invoke1"));

			ipr.Invoke();
			Assert.True(ipr.ParsedObject.DidIGetInvoked);
			ipr.ParsedObject.DidIGetInvoked = false;
			await ipr.InvokeAsync();
			Assert.True(ipr.ParsedObject.DidIGetInvoked);
		}
		[Fact]
		public async Task FailureCallsNothing()
		{
			var ipr = Assert.IsType<EmptyParseResult>(parser.Parse("Nothing"));
			Assert.False(ipr.Success);
			Assert.Throws<InvalidOperationException>(() => ipr.Invoke());
			await Assert.ThrowsAsync<InvalidOperationException>(() => ipr.InvokeAsync());
		}
		private static void SetInvoked(InvokedTracker opt)
		{
			opt.DidIGetInvoked = true;
		}
		private static Task SetInvokedAsync(InvokedTracker opt)
		{
			opt.DidIGetInvoked = true;
			return Task.CompletedTask;
		}
		private static void BadInvoke(OptEmpty opt)
		{
			Assert.False(true, "Invoked the bad evil method");
		}
		private static Task BadInvokeAsync(OptEmpty opt)
		{
			Assert.False(true, "Invoked the bad evil method");
			return Task.CompletedTask;
		}
	}
}
