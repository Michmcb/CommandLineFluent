using CommandLineFluent;
using Xunit;

namespace CommandLineFluentTest.Parser
{
	public class FluentParserResultTest
	{
		[Fact]
		public void SuccessCallsCorrectAction()
		{
			FluentParser fp = new FluentParserBuilder()
				.AddVerb<Verb1>("verb1", verb =>
				{
					verb.AddValue()
						.ForProperty(x => x.Value);
				}).AddVerb<Verb2>("verb2", verb =>
				{
					verb.AddValue()
						.ForProperty(x => x.Value);
				}).AddVerb<Verb3>("verb3", verb =>
				{
					verb.AddValue()
						.ForProperty(x => x.Value);
				}).Build();
			fp.Parse(new string[] { "verb1", "Value" })
				.OnFailure(x => Assert.True(false))
				.OnSuccess<Verb1>(x => Assert.True(true))
				.OnSuccess<Verb2>(x => Assert.True(false))
				.OnSuccess<Verb3>(x => Assert.True(false));
		}
		[Fact]
		public void FailureCallsCorrectAction()
		{
			FluentParser fp = new FluentParserBuilder()
				.AddVerb<Verb1>("verb1", verb =>
				{
					verb.AddValue()
						.ForProperty(x => x.Value);
				}).AddVerb<Verb2>("verb2", verb =>
				{
					verb.AddValue()
						.ForProperty(x => x.Value);
				}).AddVerb<Verb3>("verb3", verb =>
				{
					verb.AddValue()
						.ForProperty(x => x.Value);
				}).Build();
			fp.Parse(new string[] { "verb1" })
				.OnFailure(x => Assert.True(true))
				.OnSuccess<Verb1>(x => Assert.True(false))
				.OnSuccess<Verb2>(x => Assert.True(false))
				.OnSuccess<Verb3>(x => Assert.True(false));
		}
		[Fact]
		public void FailureAndStopCallsCorrectActionAndReturnsNull()
		{
			FluentParser fp = new FluentParserBuilder()
				.AddVerb<Verb1>("verb1", verb =>
				{
					verb.AddValue()
						.ForProperty(x => x.Value);
				}).AddVerb<Verb2>("verb2", verb =>
				{
					verb.AddValue()
						.ForProperty(x => x.Value);
				}).AddVerb<Verb3>("verb3", verb =>
				{
					verb.AddValue()
						.ForProperty(x => x.Value);
				}).Build();
			FluentParserResult fpr = fp.Parse(new string[] { "verb1" })
				.OnFailureAndStop(x => Assert.True(true));
			Assert.Null(fpr);
		}
		[Fact]
		public void StopOnFailureCallsCorrectReturnsNull()
		{
			FluentParser fp = new FluentParserBuilder()
				.AddVerb<Verb1>("verb1", verb =>
				{
					verb.AddValue()
						.ForProperty(x => x.Value);
				}).AddVerb<Verb2>("verb2", verb =>
				{
					verb.AddValue()
						.ForProperty(x => x.Value);
				}).AddVerb<Verb3>("verb3", verb =>
				{
					verb.AddValue()
						.ForProperty(x => x.Value);
				}).Build();
			FluentParserResult fpr = fp.Parse(new string[] { "verb1" })
				.StopOnFailure();
			Assert.Null(fpr);
		}
	}
}
