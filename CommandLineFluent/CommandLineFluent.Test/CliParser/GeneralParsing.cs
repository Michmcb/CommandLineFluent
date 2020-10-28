namespace CommandLineFluent.Test.CliParser
{
	using CommandLineFluent;
	using CommandLineFluent.Test.Options;
	using System;
	using System.Linq;
	using Xunit;

	public sealed class GeneralParsing
	{
		public enum Components
		{
			None = 0,
			Value = 1,
			Option = 2,
			Switch = 4,
			ValueOption = Value | Option,
			ValueSwitch = Value | Switch,
			SwitchOption = Switch | Option,
			All = Value | Option | Switch
		}
		[Fact]
		public void JustValue_Good() { SimpleParsing(true, true, Components.Value, new string[] { "default", "Value" }); }
		[Fact]
		public void JustShortOption_Good() { SimpleParsing(true, true, Components.Option, new string[] { "default", "-o", "30" }); }
		[Fact]
		public void JustLongOption_Good() { SimpleParsing(true, true, Components.Option, new string[] { "default", "--option", "30" }); }
		[Fact]
		public void JustShortSwitch_Good() { SimpleParsing(true, true, Components.Switch, new string[] { "default", "-s" }); }
		[Fact]
		public void JustLongSwitch_Good() { SimpleParsing(true, true, Components.Switch, new string[] { "default", "--switch" }); }
		[Fact]
		public void ShortValueAndOption_Good() { SimpleParsing(true, true, Components.ValueOption, new string[] { "default", "Value", "-o", "30" }); }
		[Fact]
		public void LongValueAndOption_Good() { SimpleParsing(true, true, Components.ValueOption, new string[] { "default", "Value", "--option", "30" }); }
		[Fact]
		public void ShortSwitchAndOption_Good() { SimpleParsing(true, true, Components.SwitchOption, new string[] { "default", "-s", "-o", "30" }); }
		[Fact]
		public void LongSwitchAndOption_Good() { SimpleParsing(true, true, Components.SwitchOption, new string[] { "default", "--switch", "--option", "30" }); }
		[Fact]
		public void ShortValueAndSwitch_Good() { SimpleParsing(true, true, Components.ValueSwitch, new string[] { "default", "Value", "-s" }); }
		[Fact]
		public void LongValueAndSwitch_Good() { SimpleParsing(true, true, Components.ValueSwitch, new string[] { "default", "Value", "--switch" }); }
		[Fact]
		public void ValueShortOptionSwitch_Good() { SimpleParsing(true, true, Components.All, new string[] { "default", "Value", "-s", "-o", "30" }); }
		[Fact]
		public void ValueLongOptionShortSwitch_Good() { SimpleParsing(true, true, Components.All, new string[] { "default", "Value", "-s", "--option", "30" }); }
		[Fact]
		public void ValueShortOptionLongSwitch_Good() { SimpleParsing(true, true, Components.All, new string[] { "default", "Value", "--switch", "-o", "30" }); }
		[Fact]
		public void ValueLongOptionLongSwitch_Good() { SimpleParsing(true, true, Components.All, new string[] { "default", "Value", "--switch", "--option", "30" }); }
		[Fact]
		public void Nothing_Bad() { SimpleParsing(false, false, Components.None, new string[] { "" }); }
		[Fact]
		public void InvalidVerb_Bad() { SimpleParsing(false, false, Components.None, new string[] { "knuckles" }); }
		[Fact]
		public void InvalidArgument_Bad() { SimpleParsing(false, true, Components.None, new string[] { "default", "Value", "--hey" }); }
		[Fact]
		public void OptionMissingvalue_Bad() { SimpleParsing(false, true, Components.None, new string[] { "default", "Value", "--option" }); }
		internal void SimpleParsing(bool outcome, bool isValidVerb, Components components, string[] args)
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<OptOneOfEach>("default", verb =>
				{
					if ((components & Components.Value) == Components.Value)
					{
						verb.AddValue(x => x.Value, x => { x.HelpText = "h"; });
					}
					if ((components & Components.Switch) == Components.Switch)
					{
						verb.AddSwitch(x => x.Switch, x => { x.ShortName = "-s"; x.LongName = "--switch"; x.HelpText = "h"; });
					}
					if ((components & Components.Option) == Components.Option)
					{
						verb.AddOption(x => x.Option, x => { x.ShortName = "-o"; x.LongName = "--option"; x.HelpText = "h"; });
					}
				}).Build();

			IParseResult parsed = fp.Parse(args);
			if (outcome)
			{
				Assert.True(parsed.Ok);
				SuccessfulParse<OptOneOfEach> parseResult = Assert.IsType<SuccessfulParse<OptOneOfEach>>(parsed);
				Assert.NotNull(parseResult.Object);
				OptOneOfEach parsedObject = parseResult.Object!;

				Assert.NotNull(parsedObject);
				if ((components & Components.Value) == Components.Value)
				{
					Assert.Equal("Value", parsedObject.Value);
				}
				if ((components & Components.Switch) == Components.Switch)
				{
					Assert.True(parsedObject.Switch, $"Switch was specified but didn't set property to true");
				}
				else
				{
					Assert.False(parsedObject.Switch, $"Switch was specified but didn't set property to false");
				}
				if ((components & Components.Option) == Components.Option)
				{
					Assert.Equal(30, parsedObject.Option);
				}
			}
			else
			{
				Assert.False(parsed.Ok);
				// Verb still shouldn't be null, since for our test cases we DO pass the verb
				if (isValidVerb)
				{
					Assert.NotNull(parsed.Verb);
				}
				else
				{
					Assert.Null(parsed.Verb);
				}
				Assert.NotEmpty(parsed.Errors);
			}
		}
		[Fact]
		public void AllNormal_Fine()
		{
			ComplexParsing(new string[] { "default", "value1", "-s1", "-s2", "47", "-o1", "Option1", "-s3", "valuueee", "-s4", "-o2", "Option2", "-o3", "900" }, true,
				new ComplexVerb1()
				{
					RequiredValue = "value1",
					ConvertedValue = 47,
					OptionalValue = "valuueee",
					Switch1 = true,
					DefaultValueSwitch = true,
					ConvertedSwitch = "True",
					DefaultValueConvertedSwitch = "True",
					RequiredOption = "Option1",
					OptionalOption = "Option2",
					ConvertedOption = 900
				});
		}
		[Fact]
		public void OptionalLeftOut_Fine()
		{
			ComplexParsing(new string[] { "default", "value1", "-s1", "47", "--oo1", "Option1", "-s3", "-o3", "900" }, true,
				new ComplexVerb1()
				{
					RequiredValue = "value1",
					ConvertedValue = 47,
					OptionalValue = null,
					Switch1 = true,
					DefaultValueSwitch = true,
					ConvertedSwitch = "True",
					DefaultValueConvertedSwitch = "Default",
					RequiredOption = "Option1",
					OptionalOption = "Default",
					ConvertedOption = 900
				});
		}
		[Fact]
		public void AllValuesAtTheStart_Fine()
		{
			ComplexParsing(new string[] { "default", "value1", "47", "valuueee", "-s1", "-o1", "Option1", "--ss3", "-o3", "900" }, true,
				new ComplexVerb1()
				{
					RequiredValue = "value1",
					ConvertedValue = 47,
					OptionalValue = "valuueee",
					Switch1 = true,
					DefaultValueSwitch = true,
					ConvertedSwitch = "True",
					DefaultValueConvertedSwitch = "Default",
					RequiredOption = "Option1",
					OptionalOption = "Default",
					ConvertedOption = 900
				});
		}
		[Fact]
		public void AllValuesAtTheEnd_Fine()
		{
			ComplexParsing(new string[] { "default", "-s1", "-o1", "Option1", "-s3", "-o3", "900", "value1", "47", "valuueee" }, true,
				new ComplexVerb1()
				{
					RequiredValue = "value1",
					ConvertedValue = 47,
					OptionalValue = "valuueee",
					Switch1 = true,
					DefaultValueSwitch = true,
					ConvertedSwitch = "True",
					DefaultValueConvertedSwitch = "Default",
					RequiredOption = "Option1",
					OptionalOption = "Default",
					ConvertedOption = 900
				});
		}
		[Fact]
		public void MissingRequiredValue_Fail()
		{
			ComplexParsing(new string[] { "default", "-s1", "-o1", "Option1", "-s3", "-o3", "900" }, false, null);
		}
		[Fact]
		public void MissingRequiredOption_Fail()
		{
			ComplexParsing(new string[] { "default", "--ss1", "-s3", "value1", "47", "valuueee" }, false, null);
		}
		[Fact]
		public void MissingRequiredOptionValue_Fail()
		{
			ComplexParsing(new string[] { "default", "-s1", "-s3" }, false, null);
		}
		[Fact]
		public void MissingEverything_Fail()
		{
			ComplexParsing(new string[] { "default", "-s1", "-s3" }, false, null);
		}
		[Fact]
		public void InvalidOption_Fail()
		{
			ComplexParsing(new string[] { "default", "-s1", "-o1", "Option1", "-s3", "--oo3", "900", "value1", "47", "valuueee", "-o4", "myValue" }, false, null);
		}
		[Fact]
		public void InvalidSwitch_Fail()
		{
			ComplexParsing(new string[] { "default", "-s1", "-o1", "Option1", "-s3", "-o3", "900", "value1", "47", "valuueee", "--ss5" }, false, null);
		}
		[Fact]
		public void InvalidValue_Fail()
		{
			ComplexParsing(new string[] { "default", "-s1", "--oo1", "Option1", "-s3", "-o3", "900", "value1", "47", "valuueee", "ULTRAMEGAVALUE" }, false, null);
		}
		internal void ComplexParsing(string[] args, bool shouldBeSuccessful, ComplexVerb1? expectedResult)
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<ComplexVerb1>("default", verb =>
				{
					verb.HelpText = "My Test Verb";

					verb.AddValue(x => x.RequiredValue, x => { x.HelpText = "h"; });

					verb.AddValue(x => x.ConvertedValue, x => { x.HelpText = "h"; });

					verb.AddValueNullable(x => x.OptionalValue, x => { x.DefaultValue = null; x.HelpText = "h"; });

					verb.AddSwitch(x => x.Switch1, x => { x.ShortName = "s1"; x.LongName = "ss1"; x.HelpText = "h"; });

					verb.AddSwitch(x => x.DefaultValueSwitch, x => { x.ShortName = "s2"; x.LongName = "ss2"; x.DefaultValue = true; x.HelpText = "h"; });

					verb.AddSwitchCore(a => a.ConvertedSwitch, new Arguments.Config.NamedArgConfig<ComplexVerb1, string, bool>()
					{
						Converter = v => Converted<string, string>.Value(v.ToString()),
						HelpText = "h",
						ShortName = "s3",
						LongName = "ss3"
					});

					verb.AddSwitchCore(a => a.DefaultValueConvertedSwitch, new Arguments.Config.NamedArgConfig<ComplexVerb1, string, bool>()
					{
						Converter = v => Converted<string, string>.Value(v.ToString()),
						DefaultValue = "Default",
						HelpText = "h",
						ShortName = "s4",
						LongName = "ss4"
					});

					verb.AddOption(x => x.RequiredOption, x => { x.ShortName = "o1"; x.LongName = "oo1"; x.HelpText = "h"; });
					verb.AddOption(x => x.OptionalOption, x => { x.ShortName = "o2"; x.LongName = "oo2"; x.DefaultValue = "Default"; x.HelpText = "h"; });
					verb.AddOption(x => x.ConvertedOption, x => { x.ShortName = "o3"; x.LongName = "oo3"; x.HelpText = "h"; });
				}).Build();

			IParseResult result = fp.Parse(args);
			Assert.Equal(shouldBeSuccessful, result.Ok);
			if (shouldBeSuccessful)
			{
				SuccessfulParse<ComplexVerb1> tResult = Assert.IsType<SuccessfulParse<ComplexVerb1>>(result);
				Assert.NotNull(tResult.Object);
				ComplexVerb1 x = tResult.Object!;
				Assert.True(expectedResult!.RequiredValue == x.RequiredValue);
				Assert.True(expectedResult.ConvertedValue == x.ConvertedValue);
				Assert.True(expectedResult.OptionalValue == x.OptionalValue);
				Assert.True(expectedResult.Switch1 == x.Switch1);
				Assert.True(expectedResult.DefaultValueSwitch == x.DefaultValueSwitch);
				Assert.True(expectedResult.ConvertedSwitch == x.ConvertedSwitch);
				Assert.True(expectedResult.DefaultValueConvertedSwitch == x.DefaultValueConvertedSwitch);
				Assert.True(expectedResult.RequiredOption == x.RequiredOption);
				Assert.True(expectedResult.OptionalOption == x.OptionalOption);
				Assert.True(expectedResult.ConvertedOption == x.ConvertedOption);
			}
		}

		internal void ParsingConverters()
		{
			// TODO test converters
		}

		internal void ManyValueAccumulators()
		{
			// TODO test accumulators
		}

		[Fact]
		public void ManyValuesAllNormal_Good()
		{
			ManyValueParsing(new string[] { "default", "value1", "value2", "-o", "55", "value3", "-s", "value4", "value5" }, true, new OptOneOfEach() { Option = 55, Switch = true, ManyValues = new string[] { "value1", "value2", "value3", "value4", "value5", } });
			ManyValueParsing(new string[] { "default", "value1", "value2", "value3", "-s", "value4", "value5", "-o", "55" }, true, new OptOneOfEach() { Option = 55, Switch = true, ManyValues = new string[] { "value1", "value2", "value3", "value4", "value5", } });
			ManyValueParsing(new string[] { "default", "value1", "value2", "-o", "55", "value3", "-s", "value4", "value5", "value6", "value7", "value8", "value9", "value10" }, true, new OptOneOfEach() { Option = 55, Switch = true, ManyValues = new string[] { "value1", "value2", "value3", "value4", "value5", "value6", "value7", "value8", "value9", "value10" } });
		}
		internal void ManyValueParsing(string[] args, bool shouldBeSuccessful, OptOneOfEach expectedResult)
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<OptOneOfEach>("default", verb =>
				{
					verb.AddMultiValue(x => x.ManyValues, x =>
					{
						x.HelpText = "h";
					});
					verb.AddOption(x => x.Option, x => { x.ShortName = "o"; x.LongName = "oo"; x.DefaultValue = 0; x.HelpText = "h"; });
					verb.AddSwitch(x => x.Switch, x => { x.ShortName = "s"; x.LongName = "ss"; x.HelpText = "h"; });
				}).Build();

			IParseResult result = fp.Parse(args);
			Assert.Equal(shouldBeSuccessful, result.Ok);
			if (shouldBeSuccessful)
			{
				SuccessfulParse<OptOneOfEach> tResult = Assert.IsType<SuccessfulParse<OptOneOfEach>>(result);
				OptOneOfEach x = tResult.Object!;
				Assert.NotNull(x);
				Assert.True(expectedResult.Option == x.Option);
				Assert.True(expectedResult.Switch == x.Switch);
				Assert.True(expectedResult.ManyValues.SequenceEqual(x.ManyValues));
			}
		}
		[Fact]
		public void VerbParsing()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<Verb1>("verb1", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb2>("verb2", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb3>("verb3", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb4>("verb4", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb5>("verb5", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb6>("verb6", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb7>("verb7", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb8>("verb8", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb9>("verb9", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb10>("verb10", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb11>("verb11", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb12>("verb12", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb13>("verb13", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb14>("verb14", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb15>("verb15", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.AddVerb<Verb16>("verb16", v => v.AddValue(v => v.Value, x => { x.HelpText = "h"; }))
				.Build();

			string[] args = new string[2];
			int i = 1;
			Check<Verb1>();
			Check<Verb2>();
			Check<Verb3>();
			Check<Verb4>();
			Check<Verb5>();
			Check<Verb6>();
			Check<Verb7>();
			Check<Verb8>();
			Check<Verb9>();
			Check<Verb10>();
			Check<Verb11>();
			Check<Verb12>();
			Check<Verb13>();
			Check<Verb14>();
			Check<Verb15>();
			Check<Verb16>();

			void Check<T>() where T : Options.Verb, new()
			{
				args[0] = $"verb{i}";
				args[1] = "Value";
				IParseResult parsed = fp.Parse(args);
				Assert.NotNull(parsed.Verb);
				Assert.Equal(args[0], parsed.Verb!.LongName);
				i++;
			}
		}
	}
}