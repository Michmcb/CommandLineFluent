namespace CommandLineFluent.Test.CliParser
{
	using CommandLineFluent;
	using CommandLineFluent.Test.Options;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Xunit;

	public sealed class Parse
	{
		private static readonly CliParser parser = new CliParserBuilder()
			.AddVerb<OptOneOfEach>("OptOneOfEach", verb =>
			{
				verb.AddOptionString("-o", "--option", o => o
					.ForProperty(x => x.Option)
					.IsRequired()
					.WithName("Option")
					.WithHelpText("Help me please"));

				verb.AddSwitchBool("-s", "--switch", s => s
					.ForProperty(x => x.Switch)
					.IsRequired()
					.WithName("Switch")
					.WithHelpText("really help meeeeee"));

				verb.AddValueString(v => v
					.ForProperty(x => x.Value)
					.IsRequired()
					.WithName("Value")
					.WithHelpText("fine forget it"));
			})
			.AddVerb<OptMulti>("OptMulti", verb =>
			{
				verb.AddOptionString("-o1", "--option1", o => o
					.ForProperty(x => x.Option1)
					.IsRequired()
					.WithName("Option")
					.WithHelpText("Help me please"));

				verb.AddOptionString("-o2", "--option2", o => o
					.ForProperty(x => x.Option2)
					.IsRequired()
					.WithName("Option")
					.WithHelpText("Help me please"));

				verb.AddMultiValueInt(mv => mv
					.ForProperty(x => x.Values)
					.WithHelpText("help"));
			})
			.Build();
		[Fact]
		public void NoArgs_EmptyResult()
		{
			IParseResult rParse = parser.Parse(Array.Empty<string>());
			Assert.False(rParse.Ok);
			rParse = parser.Parse("");
			Assert.False(rParse.Ok);
		}
		[Fact]
		public void NullArgs_Exception()
		{
			Assert.Throws<ArgumentNullException>(() => parser.Parse((string)null!));
			Assert.Throws<ArgumentNullException>(() => parser.Parse((IEnumerable<string>)null!));
		}
		[Fact]
		public void DefaultHelpSwitch_ErrorCodeHelpRequested()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<OptEmpty>("blah", (verb) => { }).Build();
			IParseResult result1 = fp.Parse(new string[] { "-?" });
			IParseResult result2 = fp.Parse("--help");
			Error err = result1.Errors.Single();
			Assert.Equal(ErrorCode.HelpRequested, err.ErrorCode);
			Assert.Empty(err.Message);
			err = result2.Errors.Single();
			Assert.Equal(ErrorCode.HelpRequested, err.ErrorCode);
			Assert.Empty(err.Message);
		}
		[Fact]
		public void ParsingWithInvalidVerb_Error()
		{
			IParseResult result = new CliParserBuilder()
				.AddVerb<Verb1>("verb1", verb => { })
				.Build()
				.Parse(new string[] { "InvalidVerb" });
			Assert.False(result.Ok);
			Assert.Equal(ErrorCode.InvalidVerb, result.Errors.First().ErrorCode);
		}
		[Fact]
		public void NullConfigActionAddVerb_Exception()
		{
			Assert.Throws<ArgumentNullException>(() => new CliParserBuilder().AddVerb<Verb1>("verb1", null!));
		}
		[Fact]
		public void AddingDuplicateVerbNames_Exception()
		{
			Assert.Throws<CliParserBuilderException>(() => new CliParserBuilder().AddVerb<Verb1>("Name", x => { }).AddVerb<Verb2>("Name", x => { }));
		}
		[Fact]
		public void BuildingWithoutAnyConfiguration_Exception()
		{
			Assert.Throws<CliParserBuilderException>(() => new CliParserBuilder().Build());
		}
		[Fact]
		public void GoodArgs_NormalResult()
		{
			IParseResult ipr = parser.Parse(new string[] { "OptOneOfEach", "-o", "string", "--switch", "value" });
			Assert.True(ipr.Ok);
			SuccessfulParse<OptOneOfEach> pr = Assert.IsType<SuccessfulParse<OptOneOfEach>>(ipr);
			Assert.NotNull(pr);
			Assert.NotNull(pr.Verb);
			Assert.NotNull(pr.Verb);
			Assert.NotNull(pr.Object);
			OptOneOfEach obj = pr.Object;
			Assert.Equal("string", obj.Option);
			Assert.True(obj.Switch);
			Assert.Equal("value", obj.Value);
		}
		[Fact]
		public void GoodArgs_MultiValue()
		{
			IParseResult ipr = parser.Parse(new string[] { "OptMulti", "-o1", "opt1", "1", "2", "--option2", "opt2", "3" });
			Assert.True(ipr.Ok);
			SuccessfulParse<OptMulti> pr = Assert.IsType<SuccessfulParse<OptMulti>>(ipr);
			Assert.NotNull(pr);
			Assert.NotNull(pr.Verb);
			Assert.NotNull(pr.Verb);
			Assert.NotNull(pr.Object);
			OptMulti obj = pr.Object;
			Assert.Equal("opt1", obj.Option1);
			Assert.Equal("opt2", obj.Option2);
			Assert.Collection(obj.Values, x => Assert.Equal(1, x), x => Assert.Equal(2, x), x => Assert.Equal(3, x));
		}
	}
}
