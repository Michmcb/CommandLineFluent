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
			FluentParser fp = new FluentParser().Configure(config =>
				{
					config.UseDefaultPrefixes("-", "--");
				})
				.WithoutVerbs<Verb1>(verb =>
				{
					verb.WithUsageText("ugase");
					verb.WithHelpText("hlep");
					verb.WithDescription("descr");
					Assert.Equal("ugase", verb.UsageText);
					Assert.Equal("hlep", verb.HelpText);
					Assert.Equal("descr", verb.Description);

					FluentOption<Verb1, string> opt = verb.AddOption("o", "option")
						.WithHelpText("help")
						.WithName("name")
						.IsRequired();
					Assert.Equal(opt, verb.FluentOptions.First());
					Assert.Equal("-o", opt.ShortName);
					Assert.Equal("--option", opt.LongName);
					Assert.Equal("help", opt.HelpText);
					Assert.Equal("name", opt.Name);
					Assert.True(opt.Required);
					opt.IsOptional("test");
					Assert.Equal("test", opt.DefaultValue);
					Assert.False(opt.Required);

					FluentValue<Verb1, string> val = verb.AddValue()
						.WithHelpText("help")
						.WithName("name")
						.IsRequired();
					Assert.Equal(val, verb.FluentValues.First());
					Assert.Equal("help", val.HelpText);
					Assert.Equal("name", val.Name);
					Assert.True(val.Required);
					val.IsOptional("test");
					Assert.Equal("test", val.DefaultValue);
					Assert.False(val.Required);

					FluentSwitch<Verb1, bool> sw = verb.AddSwitch("s", "switch")
						.WithHelpText("help");
					Assert.Equal(sw, verb.FluentSwitches.First());
					Assert.Equal("help", sw.HelpText);
					sw.WithDefaultValue(true);
					Assert.True(sw.DefaultValue);
				});
		}
		[Fact]
		public void AddingManyValues()
		{
			FluentParser fp = new FluentParser().Configure(config =>
				{
					config.UseDefaultPrefixes("-", "--");
				})
				.WithoutVerbs<Verb1>(verb =>
				{
					FluentManyValues<Verb1, string[]> val = verb.AddManyValues()
						.WithName("name")
						.WithHelpText("help")
						.IsRequired();

					Assert.Equal(val, verb.FluentManyValues);
					Assert.Equal("help", val.HelpText);
					Assert.Equal("name", val.Name);
					Assert.True(val.Required);
					val.IsOptional(new string[] { "test" });
					Assert.Equal("test", val.DefaultValue[0]);
					Assert.False(val.Required);
				});
		}
		[Fact]
		public void AddingDuplicateShortLongNames()
		{
			FluentParser fp = new FluentParser()
				.WithoutVerbs<Verb1>(verb =>
				{
					verb.AddOption("-o", "--o");
					Assert.Throws<ArgumentException>(() => verb.AddOption("-o", "--o"));
					Assert.Throws<ArgumentException>(() => verb.AddOption("--o", "-o"));
				});
		}
		[Fact]
		public void AddingNullOrWhitespaceShortLongNames()
		{
			FluentParser fp = new FluentParser()
				.WithoutVerbs<Verb1>(verb =>
				{
					Assert.Throws<ArgumentException>(() => verb.AddOption(null, null));
					Assert.Throws<ArgumentException>(() => verb.AddOption("", null));
					Assert.Throws<ArgumentException>(() => verb.AddOption(null, ""));
					Assert.Throws<ArgumentException>(() => verb.AddOption(" ", null));
					Assert.Throws<ArgumentException>(() => verb.AddOption(null, " "));
				});
		}
	}
}
