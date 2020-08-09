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
				verb.AddOption("-o", "--option", o => o
					.ForProperty(x => x.Option)
					.IsRequired()
					.WithName("Option")
					.WithHelpText("Help me please"));

				verb.AddSwitch("-s", "--switch", s => s
					.ForProperty(x => x.Switch)
					.IsRequired()
					.WithName("Switch")
					.WithHelpText("really help meeeeee"));

				verb.AddValue(v => v
					.ForProperty(x => x.Value)
					.IsRequired()
					.WithName("Value")
					.WithHelpText("fine forget it"));
			})
			.AddVerb<OptMulti>("OptMulti", verb =>
			{
				verb.AddOption("-o1", "--option1", o => o
					.ForProperty(x => x.Option1)
					.IsRequired()
					.WithName("Option")
					.WithHelpText("Help me please"));

				verb.AddOption("-o2", "--option2", o => o
					.ForProperty(x => x.Option2)
					.IsRequired()
					.WithName("Option")
					.WithHelpText("Help me please"));

				verb.AddMultiValue(mv => mv
					.ForProperty(x => x.Values)
					.WithHelpText("help"));
			})
			.Build();
		[Fact]
		public void NoArgs_EmptyResult()
		{
			Maybe<IParseResult, IReadOnlyCollection<Error>> rParse = parser.Parse(Array.Empty<string>());
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
			Maybe<IParseResult, IReadOnlyCollection<Error>> result1 = fp.Parse(new string[] { "-?" });
			Maybe<IParseResult, IReadOnlyCollection<Error>> result2 = fp.Parse("--help");
			Error err = result1.ErrorOr(default).Single();
			Assert.Equal(ErrorCode.HelpRequested, err.ErrorCode);
			Assert.Empty(err.Message);
			err = result2.ErrorOr(default).Single();
			Assert.Equal(ErrorCode.HelpRequested, err.ErrorCode);
			Assert.Empty(err.Message);
		}
		[Fact]
		public void ParsingWithInvalidVerb_Error()
		{
			Maybe<IParseResult, IReadOnlyCollection<Error>> result = new CliParserBuilder()
				.AddVerb<Verb1>("verb1", verb => { })
				.Build()
				.Parse(new string[] { "InvalidVerb" });
			Assert.False(result.Ok);
			Assert.Equal(ErrorCode.InvalidVerb, result.ErrorOr(default).First().ErrorCode);
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
			Maybe<IParseResult, IReadOnlyCollection<Error>> ipr = parser.Parse(new string[] { "OptOneOfEach", "-o", "string", "--switch", "value" });
			Assert.True(ipr.Ok);
			ParseResult<OptOneOfEach> pr = Assert.IsType<ParseResult<OptOneOfEach>>(ipr.ValueOr(null));
			Assert.NotNull(pr);
			Assert.NotNull(pr.ParsedVerb);
			Assert.NotNull(pr.TypedParsedVerb);
			Assert.NotNull(pr.ParsedObject);
			OptOneOfEach obj = pr.ParsedObject;
			Assert.Equal("string", obj.Option);
			Assert.True(obj.Switch);
			Assert.Equal("value", obj.Value);
		}
		[Fact]
		public void GoodArgs_MultiValue()
		{
			Maybe<IParseResult, IReadOnlyCollection<Error>> ipr = parser.Parse(new string[] { "OptMulti", "-o1", "opt1", "value1", "value2", "--option2", "opt2", "value3" });
			Assert.True(ipr.Ok);
			ParseResult<OptMulti> pr = Assert.IsType<ParseResult<OptMulti>>(ipr.ValueOr(null));
			Assert.NotNull(pr);
			Assert.NotNull(pr.ParsedVerb);
			Assert.NotNull(pr.TypedParsedVerb);
			Assert.NotNull(pr.ParsedObject);
			OptMulti obj = pr.ParsedObject;
			Assert.Equal("opt1", obj.Option1);
			Assert.Equal("opt2", obj.Option2);
			Assert.Collection(obj.Values, x => Assert.Equal("value1", x), x => Assert.Equal("value2", x), x => Assert.Equal("value3", x));
		}
	}
}
