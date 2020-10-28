namespace CommandLineFluent.Test.CliParserBuilder
{
	using CommandLineFluent;
	using CommandLineFluent.Arguments.Config;
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Test.Options;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Xunit;

	public sealed class Building
	{
		[Fact]
		public void AddingArguments()
		{
			CliParserBuilder fpb = new CliParserBuilder()
				.AddVerb<OptOneOfEach>("default", verb =>
				{
					Option<OptOneOfEach, int> opt = verb.AddOption(x => x.Option, x =>
					{
						x.ShortName = "o";
						x.LongName = "option";
						x.HelpText = "help";
						x.DescriptiveName = "name";
						x.DefaultValue = 98;
					});
					Assert.Equal(opt, verb.AllOptions.First());
					Assert.Equal("-o", opt.ShortName);
					Assert.Equal("--option", opt.LongName);
					Assert.Equal("help", opt.HelpText);
					Assert.Equal("name", opt.DescriptiveName);
					Assert.Equal(98, opt.DefaultValue);
					Assert.Equal(ArgumentRequired.Optional, opt.ArgumentRequired);

					Value<OptOneOfEach, string> val = verb.AddValue(x => x.Value, x =>
					{
						x.HelpText = "help";
						x.DescriptiveName = "name";
					});
					Assert.Equal(val, verb.AllValues.First());
					Assert.Equal("help", val.HelpText);
					Assert.Equal("name", val.DescriptiveName);
					Assert.Equal(default, val.DefaultValue);
					Assert.Equal(ArgumentRequired.Required, val.ArgumentRequired);

					Switch<OptOneOfEach, bool> sw = verb.AddSwitch(x => x.Switch, x =>
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

					MultiValue<OptOneOfEach, string, IReadOnlyCollection<string>> vals =
					verb.AddMultiValue(x => x.ManyValues, x =>
					{
						x.IsRequired = true;
						x.DescriptiveName = "Name";
						x.HelpText = "Help";
					});

					Assert.Equal(ArgumentRequired.Required, vals.ArgumentRequired);
					Assert.Equal("Help", vals.HelpText);
					Assert.Equal("Name", vals.DescriptiveName);
					Assert.Equal(ArgumentRequired.Required, vals.ArgumentRequired);
				});
		}
		[Fact]
		public void MissingConverter()
		{
			new CliParserBuilder()
				.AddVerb<ComplexVerb1>("default", verb =>
				{
					Assert.Throws<ArgumentException>(() => verb.AddOptionCore(x => x.ConvertedOption, new NamedArgConfig<ComplexVerb1, int, string>(true, null){ ShortName = "-o", LongName = "--o" }));

					Assert.Throws<ArgumentException>(() => verb.AddSwitchCore(x => x.ConvertedOption, new NamedArgConfig<ComplexVerb1, int, bool>(true, null){ ShortName = "-s", LongName = "--s" }));

					Assert.Throws<ArgumentException>(() => verb.AddValueCore(x => x.ConvertedOption, new NamelessArgConfig<ComplexVerb1, int>(true, null)));
				})
				.Build();
		}
		[Fact]
		public void AddingDuplicateShortLongNames()
		{
			new CliParserBuilder(new CliParserConfig()
			{
				LongHelpSwitch = "--help",
				ShortHelpSwitch = "-?"
			})
				.AddVerb<Verb1>("default", verb =>
				{
					verb.AddOption(x => x.Option, x => { x.ShortName = "-o"; x.LongName = "--o"; });
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = "-o"; x.LongName = "--o"; }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = "--o"; x.LongName = "-o"; }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = "-?"; x.LongName = "--help"; }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = "--help"; x.LongName = "-?"; }));

					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = "-o"; x.LongName = "--o"; }));
					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = "--o"; x.LongName = "-o"; }));
					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = "-?"; x.LongName = "--help"; }));
					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = "--help"; x.LongName = "-?"; }));
				})
				.Build();
		}
		[Fact]
		public void AddingNullOrWhitespaceShortLongNames()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<Verb1>("default", verb =>
				{
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = null; x.LongName = null; }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = ""; x.LongName = null; }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = " "; x.LongName = null; }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = null; x.LongName = ""; }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = null; x.LongName = " "; }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = ""; x.LongName = ""; }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = " "; x.LongName = ""; }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = ""; x.LongName = " "; }));
					Assert.Throws<ArgumentException>(() => verb.AddOption(x => x.Option, x => { x.ShortName = " "; x.LongName = " "; }));

					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = null; x.LongName = null; }));
					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = ""; x.LongName = null; }));
					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = " "; x.LongName = null; }));
					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = null; x.LongName = ""; }));
					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = null; x.LongName = " "; }));
					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = ""; x.LongName = ""; }));
					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = " "; x.LongName = ""; }));
					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = ""; x.LongName = " "; }));
					Assert.Throws<ArgumentException>(() => verb.AddSwitch(x => x.Switch, x => { x.ShortName = " "; x.LongName = " "; }));
				})
				.Build();
		}
	}
}
