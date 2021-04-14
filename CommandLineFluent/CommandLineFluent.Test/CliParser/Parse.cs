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
				verb.AddOption(x => x.Option, x => { x.ShortName = "-o"; x.LongName = "--option"; x.DescriptiveName = "Option"; x.HelpText = "help me please"; });

				verb.AddSwitch(x => x.Switch, x => { x.ShortName = "-s"; x.LongName = "--switch"; x.DescriptiveName = "Switch"; x.HelpText = "really help meeeeee"; });

				verb.AddValue(x => x.Value, x => { x.DescriptiveName = "Value"; x.HelpText = "fine forget it"; });
			})
			.AddVerb<OptMulti>("OptMulti", verb =>
			{
				verb.AddOption(x => x.Option1, x => { x.ShortName = "-o1"; x.LongName = "--option1"; x.DescriptiveName = "Option"; x.HelpText = "help me please"; });
				verb.AddOption(x => x.Option2, x => { x.ShortName = "-o2"; x.LongName = "--option2"; x.DescriptiveName = "Option"; x.HelpText = "help me please"; });

				verb.AddMultiValue(a => a.Values, x => { x.HelpText = "help"; x.Required = true; });
			}
			).Build();
		[Fact]
		public void NoArgs_EmptyResult()
		{
			IParseResult rParse = parser.Parse(Array.Empty<string>());
			Assert.False(rParse.Ok);
			rParse = parser.Parse("");
			Assert.False(rParse.Ok);
		}
		[Fact]
		public void NullArgs_FailedResult()
		{
			Assert.IsType<FailedParseNoVerb>(parser.Parse((string)null!));
			Assert.IsType<FailedParseNoVerb>(parser.Parse((IEnumerable<string>)null!));
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
			IParseResult ipr = parser.Parse(new string[] { "OptOneOfEach", "-o", "1", "--switch", "value" });
			Assert.True(ipr.Ok);
			SuccessfulParse<OptOneOfEach> pr = Assert.IsType<SuccessfulParse<OptOneOfEach>>(ipr);
			Assert.NotNull(pr);
			Assert.NotNull(pr.Verb);
			Assert.NotNull(pr.Verb);
			Assert.NotNull(pr.Object);
			OptOneOfEach obj = pr.Object;
			Assert.Equal(1, obj.Option);
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
		[Fact]
		public void ParsingSubVerb_Ok()
		{
			IParseResult result = new CliParserBuilder()
				.AddVerb("verb1", verb =>
				{
					verb.AddVerb<Verb2>("verb2", verb2 =>
					{
						verb2.AddValue(x => x.Value, x =>
						{
						});
					});
				})
				.Build()
				.Parse(new string[] { "verb1", "verb2", "value" });
			Assert.True(result.Ok);
			SuccessfulParse<Verb2> pr = Assert.IsType<SuccessfulParse<Verb2>>(result);
			Assert.Equal("value", pr.Object.Value);
		}
		[Fact]
		public void SubVerbNotUsedWhenTheresAValue_Ok()
		{
			IParseResult result = new CliParserBuilder()
				.AddVerb<Verb1>("verb1", verb =>
				{
					verb.AddOption(x => x.Option, x => x.ShortName = "-o");

					verb.AddValue(x => x.Value, x =>
					{
					});

					verb.AddVerb<Verb2>("verb2", verb2 =>
					{
						verb2.AddValue(x => x.Value, x =>
						{
						});
					});
				})
				.Build()
				.Parse(new string[] { "verb1", "-o", "value", "verb2" });
			Assert.True(result.Ok);
			SuccessfulParse<Verb1> pr = Assert.IsType<SuccessfulParse<Verb1>>(result);
			Assert.Equal("value", pr.Object.Option);
			Assert.Equal("verb2", pr.Object.Value);
		}
	}
}
