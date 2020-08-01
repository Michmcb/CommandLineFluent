using CommandLineFluent;
using CommandLineFluent.Arguments;
using System;
using System.Linq;
using Xunit;

namespace CommandLineFluentTest.Parser
{
	public class FluentVerbTest
	{
		[Fact]
		public void AddingOptionsValuesSwitches()
		{
			FluentParserBuilder fpb = new FluentParserBuilder().Configure(config =>
				{
					config.UseDefaultPrefixes("-", "--");
				})
				.WithoutVerbs<Verb1>(verb =>
				{
					verb.WithHelpText("hlep");
					verb.WithDescription("descr");
					Assert.Equal("hlep", verb.HelpText);
					Assert.Equal("descr", verb.Description);

					Option<Verb1, string> opt = verb.AddOption("o", "option")
						.WithHelpText("help")
						.WithName("name")
						.IsRequired();
					Assert.Equal(opt, verb.Options.First());
					Assert.Equal("-o", opt.ShortName);
					Assert.Equal("--option", opt.LongName);
					Assert.Equal("help", opt.HelpText);
					Assert.Equal("name", opt.Name);
					Assert.True(opt.Required);
					// Not allowed to set it more than once
					Assert.Throws<InvalidOperationException>(() => opt.IsOptional("test"));

					FluentValue<Verb1, string> val = verb.AddValue()
						.WithHelpText("help")
						.WithName("name")
						.IsRequired();
					Assert.Equal(val, verb.FluentValues.First());
					Assert.Equal("help", val.HelpText);
					Assert.Equal("name", val.Name);
					Assert.True(val.Required);
					// Not allowed to set it more than once
					Assert.Throws<InvalidOperationException>(() => val.IsOptional("test"));

					FluentSwitch<Verb1, bool> sw = verb.AddSwitch("s", "switch")
						.WithHelpText("help");
					Assert.Equal(sw, verb.Switches.First());
					Assert.Equal("help", sw.HelpText);
					sw.WithDefaultValue(true);
					Assert.True(sw.DefaultValue);
				});
		}
		[Fact]
		public void AddingManyValues()
		{
			FluentParser fp = new FluentParserBuilder().Configure(config =>
				{
					config.UseDefaultPrefixes("-", "--");
				})
				.WithoutVerbs<ManyValuesVerb>(verb =>
				{
					FluentManyValues<ManyValuesVerb, string[]> val = verb.AddManyValues()
						.ForProperty(o => o.ManyValues)
						.WithName("name")
						.WithHelpText("help")
						.IsRequired();

					Assert.Equal(val, verb.MultiValue);
					Assert.Equal("help", val.HelpText);
					Assert.Equal("name", val.Name);
					Assert.True(val.Required);
					Assert.Throws<InvalidOperationException>(() => val.IsOptional(new string[] { "test" }));
				}).Build();
		}
		[Fact]
		public void AddingDuplicateShortLongNames()
		{
			Assert.Throws<CliParserBuilderException>(() =>
			{
				FluentParser fp = new FluentParserBuilder()
					.WithoutVerbs<Verb1>(verb =>
					{
						verb.AddOption("-o", "--o");
						Assert.Throws<ArgumentException>(() => verb.AddOption("-o", "--o"));
						Assert.Throws<ArgumentException>(() => verb.AddOption("--o", "-o"));
					}).Build();
			});
		}
		[Fact]
		public void AddingNullOrWhitespaceShortLongNames()
		{
			FluentParser fp = new FluentParserBuilder()
				.WithoutVerbs<Verb1>(verb =>
				{
					Assert.Throws<ArgumentException>(() => verb.AddOption(null, null));
					Assert.Throws<ArgumentException>(() => verb.AddOption("", null));
					Assert.Throws<ArgumentException>(() => verb.AddOption(null, ""));
					Assert.Throws<ArgumentException>(() => verb.AddOption(" ", null));
					Assert.Throws<ArgumentException>(() => verb.AddOption(null, " "));
				}).Build();
		}
	}
}
