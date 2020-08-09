namespace CommandLineFluent.Test.CliParserBuilder
{
	using CommandLineFluent;
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Test.Options;
	using System;
	using System.Linq;
	using Xunit;

	public sealed class Building
	{
		[Fact]
		public void AddingOptionsValuesSwitches()
		{
			CliParserBuilder fpb = new CliParserBuilder()
				.AddVerb<Verb1>("default", verb =>
				{
					Option<Verb1, string> opt = verb.AddOption("o", "option", o => o
						.ForProperty(x => x.Option)
						.WithHelpText("help")
						.WithName("name")
						.IsRequired());
					Assert.Equal(opt, verb.AllOptions.First());
					Assert.Equal("-o", opt.ShortName);
					Assert.Equal("--option", opt.LongName);
					Assert.Equal("help", opt.HelpText);
					Assert.Equal("name", opt.Name);
					Assert.Equal(ArgumentRequired.Required, opt.ArgumentRequired);
					

					Value<Verb1, string> val = verb.AddValue(v => v
						.ForProperty(x => x.Value)
						.WithHelpText("help")
						.WithName("name")
						.IsRequired());
					Assert.Equal(val, verb.AllValues.First());
					Assert.Equal("help", val.HelpText);
					Assert.Equal("name", val.Name);
					Assert.Equal(ArgumentRequired.Required, val.ArgumentRequired);

					Switch<Verb1, bool> sw = verb.AddSwitch("s", "switch", s => s
						.ForProperty(x => x.Switch)
						.WithHelpText("help")
						.IsOptional(true));
					Assert.Equal(sw, verb.AllSwitches.First());
					Assert.Equal("help", sw.HelpText);
					Assert.Equal(ArgumentRequired.Optional, sw.ArgumentRequired);
					Assert.True(sw.DefaultValue);
				});
		}
		[Fact]
		public void AddingManyValues()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<ManyValuesVerb>("default", verb =>
				{
					var val = verb.AddMultiValue(mv => mv
						.ForProperty(o => o.ManyValues)
						.WithName("name")
						.WithHelpText("help")
						.IsRequired());

					Assert.Equal(val, verb.MultiValue);
					Assert.Equal("help", val.HelpText);
					Assert.Equal("name", val.Name);
					Assert.Equal(ArgumentRequired.Required, val.ArgumentRequired);
				}).Build();
		}
		[Fact]
		public void AddingDuplicateShortLongNames()
		{
			new CliParserBuilder()
				.AddVerb<Verb1>("default", verb =>
				{
					verb.AddOption("-o", "--o", o => o.ForProperty(x => x.Option).WithHelpText("h"));
					Assert.Throws<ArgumentException>(() => verb.AddOption("-o", "--o", o => o.ForProperty(x => x.Option).WithHelpText("h")));
					Assert.Throws<ArgumentException>(() => verb.AddOption("--o", "-o", o => o.ForProperty(x => x.Option).WithHelpText("h")));
				});
		}
		[Fact]
		public void AddingNullOrWhitespaceShortLongNames()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<Verb1>("default", verb =>
				{
					Assert.Throws<ArgumentException>(() => verb.AddOption(null, null, o => { }));
					Assert.Throws<ArgumentException>(() => verb.AddOption("", null, o => { }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(null, "", o => { }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(" ", null, o => { }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(null, " ", o => { }));
				}).Build();
		}
	}
}
