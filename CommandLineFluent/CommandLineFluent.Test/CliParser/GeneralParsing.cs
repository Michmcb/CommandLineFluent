namespace CommandLineFluent.Test.CliParser
{
	using CommandLineFluent;
	using CommandLineFluent.Arguments.Config;
	using CommandLineFluent.Test.Options;
	using System;
	using System.Collections.Generic;
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
		public void JustValue_Good() { SimpleParsing(true, Components.Value, new string[] { "default", "Value" }); }
		[Fact]
		public void JustShortOption_Good() { SimpleParsing(true, Components.Option, new string[] { "default", "-o", "Option" }); }
		[Fact]
		public void JustLongOption_Good() { SimpleParsing(true, Components.Option, new string[] { "default", "--option", "Option" }); }
		[Fact]
		public void JustShortSwitch_Good() { SimpleParsing(true, Components.Switch, new string[] { "default", "-s" }); }
		[Fact]
		public void JustLongSwitch_Good() { SimpleParsing(true, Components.Switch, new string[] { "default", "--switch" }); }
		[Fact]
		public void ShortValueAndOption_Good() { SimpleParsing(true, Components.ValueOption, new string[] { "default", "Value", "-o", "Option" }); }
		[Fact]
		public void LongValueAndOption_Good() { SimpleParsing(true, Components.ValueOption, new string[] { "default", "Value", "--option", "Option" }); }
		[Fact]
		public void ShortSwitchAndOption_Good() { SimpleParsing(true, Components.SwitchOption, new string[] { "default", "-s", "-o", "Option" }); }
		[Fact]
		public void LongSwitchAndOption_Good() { SimpleParsing(true, Components.SwitchOption, new string[] { "default", "--switch", "--option", "Option" }); }
		[Fact]
		public void ShortValueAndSwitch_Good() { SimpleParsing(true, Components.ValueSwitch, new string[] { "default", "Value", "-s" }); }
		[Fact]
		public void LongValueAndSwitch_Good() { SimpleParsing(true, Components.ValueSwitch, new string[] { "default", "Value", "--switch" }); }
		[Fact]
		public void ValueShortOptionSwitch_Good() { SimpleParsing(true, Components.All, new string[] { "default", "Value", "-s", "-o", "Option" }); }
		[Fact]
		public void ValueLongOptionShortSwitch_Good() { SimpleParsing(true, Components.All, new string[] { "default", "Value", "-s", "--option", "Option" }); }
		[Fact]
		public void ValueShortOptionLongSwitch_Good() { SimpleParsing(true, Components.All, new string[] { "default", "Value", "--switch", "-o", "Option" }); }
		[Fact]
		public void ValueLongOptionLongSwitch_Good() { SimpleParsing(true, Components.All, new string[] { "default", "Value", "--switch", "--option", "Option" }); }
		internal void SimpleParsing(bool outcome, Components components, string[] args)
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<OptOneOfEach>("default", verb =>
				{
					if ((components & Components.Value) == Components.Value)
					{
						verb.AddValueString(x => x.ForProperty(o => o.Value).WithHelpText("h"));
					}
					if ((components & Components.Switch) == Components.Switch)
					{
						verb.AddSwitchBool("-s", "--switch", x => x.ForProperty(o => o.Switch).WithHelpText("h"));
					}
					if ((components & Components.Option) == Components.Option)
					{
						verb.AddOptionString("-o", "--option", x => x.ForProperty(o => o.Option).WithHelpText("h"));
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
					Assert.Equal("Option", parsedObject.Option);
				}
			}
			else
			{
				// TODO test some crappy parsing
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

					verb.AddValueString(val =>
						val.ForProperty(x => x.RequiredValue)
						.IsRequired()
						.WithHelpText("h"));

					verb.AddValueInt(v =>
						v.ForProperty(x => x.ConvertedValue)
						.WithHelpText("h"));

					verb.AddValueString(v => v
						.ForProperty(x => x.OptionalValue)
						.IsOptional(null)
						.WithHelpText("h"));

					verb.AddSwitchBool("s1", "ss1", s => s
						.ForProperty(x => x.Switch1)
						.WithHelpText("h"));

					verb.AddSwitchBool("s2", "ss2", s => s
						.ForProperty(x => x.DefaultValueSwitch)
						.IsOptional(true)
						.WithHelpText("h"));

					verb.AddSwitch<string>("s3", "ss3", s => s
						.ForProperty(x => x.ConvertedSwitch)
						.WithConverter(v => Maybe<string, string>.Value(v.ToString()))
						.WithHelpText("h"));

					verb.AddSwitch<string>("s4", "ss4", s => s
						.ForProperty(x => x.DefaultValueConvertedSwitch)
						.WithConverter(v => Maybe<string, string>.Value(v.ToString()))
						.IsOptional("Default")
						.WithHelpText("h"));

					verb.AddOptionString("o1", "oo1", o => o
						.ForProperty(x => x.RequiredOption)
						.IsRequired()
						.WithHelpText("h"));

					verb.AddOptionString("o2", "oo2", o => o
						.ForProperty(x => x.OptionalOption)
						.IsOptional("Default")
						.WithHelpText("h"));

					verb.AddOptionInt("o3", "oo3", o => o
						.ForProperty(x => x.ConvertedOption)
						.WithHelpText("h"));
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
		[Fact]
		public void ManyValuesAllNormal_Good()
		{
			ManyValueParsing(new string[] { "default", "value1", "value2", "-o", "Opt", "value3", "-s", "value4", "value5" }, true, new ManyValuesVerb() { Option = "Opt", Switch = true, ManyValues = new string[] { "value1", "value2", "value3", "value4", "value5", } });
			ManyValueParsing(new string[] { "default", "value1", "value2", "value3", "-s", "value4", "value5", "-o", "Opt" }, true, new ManyValuesVerb() { Option = "Opt", Switch = true, ManyValues = new string[] { "value1", "value2", "value3", "value4", "value5", } });
			ManyValueParsing(new string[] { "default", "value1", "value2", "-o", "Opt", "value3", "-s", "value4", "value5", "value6", "value7", "value8", "value9", "value10" }, true, new ManyValuesVerb() { Option = "Opt", Switch = true, ManyValues = new string[] { "value1", "value2", "value3", "value4", "value5", "value6", "value7", "value8", "value9", "value10" } });
		}
		//[Fact]
		//public void ManyValuesIgnoredPrefix_Fail()
		//{
		//	ManyValueParsing(new string[] { "default", "value1", "value2", "-o", "Opt", "value3", "-s", "value4", "value5", "--sneakyErrorInDisguise", "-notAValue" }, false, null);
		//}
		internal void ManyValueParsing(string[] args, bool shouldBeSuccessful, ManyValuesVerb expectedResult)
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<ManyValuesVerb>("default", verb =>
				{
					verb.AddMultiValueString(mv => mv
						.ForProperty(x => x.ManyValues)
						//.IgnorePrefixes("-", "--")
						.WithHelpText("h"));
					verb.AddOptionString("o", "oo", o => o
						.ForProperty(x => x.Option)
						.IsOptional("default")
						.WithHelpText("h"));
					verb.AddSwitchBool("s", "ss", s => s
						.ForProperty(x => x.Switch)
						.WithHelpText("h"));
				}).Build();

			IParseResult result = fp.Parse(args);
			Assert.Equal(shouldBeSuccessful, result.Ok);
			if (shouldBeSuccessful)
			{
				SuccessfulParse<ManyValuesVerb> tResult = Assert.IsType<SuccessfulParse<ManyValuesVerb>>(result);
				ManyValuesVerb x = tResult.Object!;
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
				.AddVerb<Verb1>("verb1", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb2>("verb2", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb3>("verb3", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb4>("verb4", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb5>("verb5", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb6>("verb6", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb7>("verb7", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb8>("verb8", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb9>("verb9", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb10>("verb10", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb11>("verb11", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb12>("verb12", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb13>("verb13", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb14>("verb14", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb15>("verb15", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
				.AddVerb<Verb16>("verb16", v => v.AddValueString(v => v.ForProperty(x => x.Value).WithHelpText("h")))
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

			void Check<T>() where T : Verb, new()
			{
				args[0] = $"verb{i}";
				args[1] = "Value";
				IParseResult parsed = fp.Parse(args);
				Assert.NotNull(parsed.Verb);
				Assert.Equal(args[0], parsed.Verb!.Name);
				i++;
			}
		}
		[Fact]
		public void Conversion()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<OptPrimitives>("verb", verb =>
				{
					verb.AddOptionDateTime("o1", "opt1", o => o
						.ForProperty(x => x.DateTime)
						.WithHelpText("H"));
				})
				.Build();
		}
	}
}