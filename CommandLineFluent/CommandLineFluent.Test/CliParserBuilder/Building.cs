namespace CommandLineFluent.Test.CliParserBuilder
{
	using CommandLineFluent;
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Test.Options;
	using System;
	using System.Collections.Generic;
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
					Option<Verb1, string> opt = verb.AddOption(x => x.Option, x =>
					{
						x.ShortName = "o";
						x.LongName = "option";
						x.HelpText = "help";
						x.DescriptiveName = "name";
						x.DefaultValue = "defaultValue";
					});
					Assert.Equal(opt, verb.AllOptions.First());
					Assert.Equal("-o", opt.ShortName);
					Assert.Equal("--option", opt.LongName);
					Assert.Equal("help", opt.HelpText);
					Assert.Equal("name", opt.DescriptiveName);
					Assert.Equal("defaultValue", opt.DefaultValue);
					Assert.Equal(ArgumentRequired.Optional, opt.ArgumentRequired);

					Value<Verb1, string> val = verb.AddValue(x => x.Value, x =>
					{
						x.HelpText = "help";
						x.DescriptiveName = "name";
					});
					Assert.Equal(val, verb.AllValues.First());
					Assert.Equal("help", val.HelpText);
					Assert.Equal("name", val.DescriptiveName);
					Assert.Equal(default, val.DefaultValue);
					Assert.Equal(ArgumentRequired.Required, val.ArgumentRequired);

					Switch<Verb1, bool> sw = verb.AddSwitch(x => x.Switch, x =>
					{
						x.ShortName = "s";
						x.LongName = "switch";
						x.HelpText = "help";
						x.DefaultValue = true;
					});
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
					MultiValue<ManyValuesVerb, string, IReadOnlyCollection<string>>? val =
					verb.AddMultiValue(x => x.ManyValues, x =>
					{
						x.IsRequired = true;
						x.DescriptiveName = "Name";
						x.HelpText = "Help";
					});

					Assert.Equal(ArgumentRequired.Required, val.ArgumentRequired);
					Assert.Equal("Help", val.HelpText);
					Assert.Equal("Name", val.DescriptiveName);
					Assert.Equal(ArgumentRequired.Required, val.ArgumentRequired);
				}).Build();
		}
		[Fact]
		public void AddingDuplicateShortLongNames()
		{
			new CliParserBuilder()
				.AddVerb<Verb1>("default", verb =>
				{
					verb.AddOptionString("-o", "--o", o => o.ForProperty(x => x.Option).WithHelpText("h"));
					Assert.Throws<ArgumentException>(() => verb.AddOptionString("-o", "--o", o => o.ForProperty(x => x.Option).WithHelpText("h")));
					Assert.Throws<ArgumentException>(() => verb.AddOptionString("--o", "-o", o => o.ForProperty(x => x.Option).WithHelpText("h")));
				});
		}
		[Fact]
		public void AddingNullOrWhitespaceShortLongNames()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<Verb1>("default", verb =>
				{
					Assert.Throws<ArgumentException>(() => verb.AddOptionString(null, null, o => { }));
					Assert.Throws<CliParserBuilderException>(() => verb.AddOptionString("", null, o => { }));
					Assert.Throws<CliParserBuilderException>(() => verb.AddOptionString(null, "", o => { }));
					Assert.Throws<CliParserBuilderException>(() => verb.AddOptionString(" ", null, o => { }));
					Assert.Throws<CliParserBuilderException>(() => verb.AddOptionString(null, " ", o => { }));
				}).Build();
		}
	}
}
