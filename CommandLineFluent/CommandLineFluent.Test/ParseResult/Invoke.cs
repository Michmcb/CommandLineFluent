namespace CommandLineFluent.Test.ParseResult
{
	using CommandLineFluent;
	using CommandLineFluent.Test.Options;
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
			IParseResult r = parser.Parse("Invoke1");
			SuccessfulParse<InvokedTracker> ipr = Assert.IsType<SuccessfulParse<InvokedTracker>>(r);
			ipr.Invoke();
			Assert.True(ipr.Object.DidIGetInvoked);
			ipr.Object.DidIGetInvoked = false;
			await ipr.InvokeAsync();
			Assert.True(ipr.Object.DidIGetInvoked);
		}
		[Fact]
		public void FailureGivesNothing()
		{
			IParseResult r = parser.Parse("Nothing");
			Assert.False(r.Ok);
			Assert.Null(r.Verb);
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
