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
			Maybe<IParseResult, System.Collections.Generic.IReadOnlyCollection<Error>> r = parser.Parse("Invoke1");
			ParseResult<InvokedTracker> ipr = Assert.IsType<ParseResult<InvokedTracker>>(r.ValueOr(null));
			ipr.Invoke();
			Assert.True(ipr.ParsedObject.DidIGetInvoked);
			ipr.ParsedObject.DidIGetInvoked = false;
			await ipr.InvokeAsync();
			Assert.True(ipr.ParsedObject.DidIGetInvoked);
		}
		[Fact]
		public void FailureGivesNothing()
		{
			Maybe<IParseResult, System.Collections.Generic.IReadOnlyCollection<Error>> r = parser.Parse("Nothing");
			Assert.False(r.Ok);
			Assert.Null(r.ValueOr(null));
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
