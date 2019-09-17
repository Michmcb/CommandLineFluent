using CommandLineFluent;
using CommandLineFluent.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CommandLineFluentTest.Parser
{
	public class FluentParserTest
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
		[Theory]
		[MemberData(nameof(SimpleParsingData))]
		public void SimpleParsing(string id, bool outcome, Components components, HashSet<ErrorCode> errorCodes, params string[] args)
		{
			System.Console.WriteLine(id);
			FluentParser fp = new FluentParser()
				.WithoutVerbs<Verb1>(verblessConfig =>
				{
					if ((components & Components.Value) == Components.Value)
					{
						verblessConfig.AddValue()
							.ForProperty(o => o.Value);
					}
					if ((components & Components.Switch) == Components.Switch)
					{
						verblessConfig.AddSwitch("-s", "--switch")
							.ForProperty(o => o.Switch);
					}
					if ((components & Components.Option) == Components.Option)
					{
						verblessConfig.AddOption("-o", "--option")
							.ForProperty(o => o.Option);
					}
				});


			FluentParserResult parsed = fp.Parse(args);
			Assert.Equal(errorCodes.Count, parsed.Errors.Count);
			foreach (Error error in parsed.Errors)
			{
				if (!errorCodes.Contains(error.ErrorCode))
				{
					Assert.True(false, $"Parser had an unexpected error: {error.ErrorCode} {error.Message} {error.Exception.ToString()}");
				}
			}
			Assert.Equal(outcome, parsed.Success);
			Verb1 parsedObject = parsed.GetParsedObject<Verb1>();
			if (outcome)
			{
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
				Assert.Null(parsedObject);
			}
		}
		public static IEnumerable<object[]> SimpleParsingData()
		{
			yield return new object[] { "Just value", true, Components.Value, new HashSet<ErrorCode> { }, new string[] { "Value" } };
			yield return new object[] { "Just Option", true, Components.Option, new HashSet<ErrorCode> { }, new string[] { "-o", "Option" } };
			yield return new object[] { "Just Option", true, Components.Option, new HashSet<ErrorCode> { }, new string[] { "--option", "Option" } };
			yield return new object[] { "Just Switch", true, Components.Switch, new HashSet<ErrorCode> { }, new string[] { "-s" } };
			yield return new object[] { "Just Switch", true, Components.Switch, new HashSet<ErrorCode> { }, new string[] { "--switch" } };
			yield return new object[] { "Value and Option", true, Components.ValueOption, new HashSet<ErrorCode> { }, new string[] { "Value", "-o", "Option" } };
			yield return new object[] { "Value and Option", true, Components.ValueOption, new HashSet<ErrorCode> { }, new string[] { "Value", "--option", "Option" } };
			yield return new object[] { "Switch and Option", true, Components.SwitchOption, new HashSet<ErrorCode> { }, new string[] { "-s", "-o", "Option" } };
			yield return new object[] { "Switch and Option", true, Components.SwitchOption, new HashSet<ErrorCode> { }, new string[] { "--switch", "--option", "Option" } };
			yield return new object[] { "Value and Switch", true, Components.ValueSwitch, new HashSet<ErrorCode> { }, new string[] { "Value", "-s" } };
			yield return new object[] { "Value and Switch", true, Components.ValueSwitch, new HashSet<ErrorCode> { }, new string[] { "Value", "--switch" } };
			yield return new object[] { "All", true, Components.All, new HashSet<ErrorCode> { }, new string[] { "Value", "-s", "-o", "Option" } };
			yield return new object[] { "All", true, Components.All, new HashSet<ErrorCode> { }, new string[] { "Value", "-s", "--option", "Option" } };
			yield return new object[] { "All", true, Components.All, new HashSet<ErrorCode> { }, new string[] { "Value", "--switch", "-o", "Option" } };
			yield return new object[] { "All", true, Components.All, new HashSet<ErrorCode> { }, new string[] { "Value", "--switch", "--option", "Option" } };
		}
		[Theory]
		[MemberData(nameof(ComplexParsingData))]
		public void ComplexParsing(string id, string[] args, bool shouldBeSuccessful, ComplexVerb1 expectedResult)
		{
			FluentParser fp = new FluentParser()
				.Configure(config =>
				{
					config.ConfigureWithDefaults();
				})
				.WithoutVerbs<ComplexVerb1>(verb =>
				{
					verb.WithDescription("My Test Verb");

					verb.AddValue()
						.ForProperty(x => x.RequiredValue)
						.IsRequired();

					verb.AddValue<int>()
						.ForProperty(x => x.ConvertedValue)
						.WithConverter((v) => int.TryParse(v, out int r) ? new Converted<int>(r) : new Converted<int>(0, "Failed to parse"));

					verb.AddValue()
						.ForProperty(x => x.OptionalValue)
						.IsOptional(null);

					verb.AddSwitch("s1", "ss1")
						.ForProperty(x => x.Switch1);

					verb.AddSwitch("s2", "ss2")
						.ForProperty(x => x.DefaultValueSwitch)
						.WithDefaultValue(true);

					verb.AddSwitch<string>("s3", "ss3")
						.ForProperty(x => x.ConvertedSwitch)
						.WithConverter(v => new Converted<string>(v.ToString()));

					verb.AddSwitch<string>("s4", "ss4")
						.ForProperty(x => x.DefaultValueConvertedSwitch)
						.WithConverter(v => new Converted<string>(v.ToString()))
						.WithDefaultValue("Default");

					verb.AddOption("o1", "oo1")
						.ForProperty(x => x.RequiredOption)
						.IsRequired();

					verb.AddOption("o2", "oo2")
						.ForProperty(x => x.OptionalOption)
						.IsOptional("Default");

					verb.AddOption<int>("o3", "oo3")
						.ForProperty(x => x.ConvertedOption)
						.WithConverter(v => new Converted<int>(int.Parse(v)));
				});
			if (shouldBeSuccessful)
			{
				fp.Parse(args).OnFailure(x => Assert.True(false, id))
					.OnSuccess<ComplexVerb1>(x =>
					{
						Assert.True(expectedResult.RequiredValue == x.RequiredValue, id);
						Assert.True(expectedResult.ConvertedValue == x.ConvertedValue, id);
						Assert.True(expectedResult.OptionalValue == x.OptionalValue, id);
						Assert.True(expectedResult.Switch1 == x.Switch1, id);
						Assert.True(expectedResult.DefaultValueSwitch == x.DefaultValueSwitch, id);
						Assert.True(expectedResult.ConvertedSwitch == x.ConvertedSwitch, id);
						Assert.True(expectedResult.DefaultValueConvertedSwitch == x.DefaultValueConvertedSwitch, id);
						Assert.True(expectedResult.RequiredOption == x.RequiredOption, id);
						Assert.True(expectedResult.OptionalOption == x.OptionalOption, id);
						Assert.True(expectedResult.ConvertedOption == x.ConvertedOption, id);
					});
			}
			else
			{
				FluentParserResult result = fp.Parse(args).OnFailure(x => Assert.True(true, id)).OnSuccess<ComplexVerb1>(x => Assert.True(false, id));
			}
		}
		public static IEnumerable<object[]> ComplexParsingData()
		{
			yield return new object[] { "All normal", new string[] { "value1", "-s1", "-s2", "47", "-o1", "Option1", "-s3", "valuueee", "-s4", "-o2", "Option2", "-o3", "900" }, true,
				new ComplexVerb1() { RequiredValue = "value1", ConvertedValue = 47, OptionalValue = "valuueee", Switch1 = true, DefaultValueSwitch = true, ConvertedSwitch = "True",
				DefaultValueConvertedSwitch = "True", RequiredOption = "Option1", OptionalOption = "Option2", ConvertedOption = 900} };
			yield return new object[] { "Optional ones left out",  new string[] { "value1", "-s1", "47", "--oo1", "Option1", "-s3", "-o3", "900" }, true,
				new ComplexVerb1() { RequiredValue = "value1", ConvertedValue = 47, OptionalValue = null, Switch1 = true, DefaultValueSwitch = true, ConvertedSwitch = "True",
				DefaultValueConvertedSwitch = "Default", RequiredOption = "Option1", OptionalOption = "Default", ConvertedOption = 900} };
			yield return new object[] { "All values at the start", new string[] { "value1", "47", "valuueee", "-s1",  "-o1", "Option1", "--ss3", "-o3", "900" }, true,
				new ComplexVerb1() { RequiredValue = "value1", ConvertedValue = 47, OptionalValue = "valuueee", Switch1 = true, DefaultValueSwitch = true, ConvertedSwitch = "True",
				DefaultValueConvertedSwitch = "Default", RequiredOption = "Option1", OptionalOption = "Default", ConvertedOption = 900} };
			yield return new object[] { "All values at the end", new string[] { "-s1",  "-o1", "Option1", "-s3", "-o3", "900", "value1", "47", "valuueee" }, true,
				new ComplexVerb1() { RequiredValue = "value1", ConvertedValue = 47, OptionalValue = "valuueee", Switch1 = true, DefaultValueSwitch = true, ConvertedSwitch = "True",
				DefaultValueConvertedSwitch = "Default", RequiredOption = "Option1", OptionalOption = "Default", ConvertedOption = 900} };

			yield return new object[] { "Missing a required value", new string[] { "-s1", "-o1", "Option1", "-s3", "-o3", "900" }, false, null };
			yield return new object[] { "Missing a required option", new string[] { "--ss1", "-s3", "value1", "47", "valuueee" }, false, null };
			yield return new object[] { "Missing required options and values", new string[] { "-s1", "-s3" }, false, null };
			yield return new object[] { "Missing everything", new string[] { "-s1", "-s3" }, false, null };
			yield return new object[] { "Extra unrecognized option", new string[] { "-s1", "-o1", "Option1", "-s3", "--oo3", "900", "value1", "47", "valuueee", "-o4", "myValue" }, false, null };
			yield return new object[] { "Extra unrecognized switch", new string[] { "-s1", "-o1", "Option1", "-s3", "-o3", "900", "value1", "47", "valuueee", "--ss5" }, false, null };
			yield return new object[] { "Extra unrecognized value", new string[] { "-s1", "--oo1", "Option1", "-s3", "-o3", "900", "value1", "47", "valuueee", "ULTRAMEGAVALUE" }, false, null };
		}
		[Theory]
		[MemberData(nameof(ManyValueParsingData))]
		public void ManyValueParsing(string id, string[] args, bool shouldBeSuccessful, ManyValuesVerb expectedResult)
		{
			FluentParser fp = new FluentParser()
				.Configure(config =>
				{
					config.ConfigureWithDefaults();
				})
				.WithoutVerbs<ManyValuesVerb>(verb =>
				{
					verb.AddManyValues()
						.ForProperty(x => x.ManyValues)
						.IgnorePrefixes("-", "--");
					verb.AddOption("o", "oo")
						.ForProperty(x => x.Option)
						.IsOptional("default");
					verb.AddSwitch("s", "ss")
						.ForProperty(x => x.Switch);
				});
			if (shouldBeSuccessful)
			{
				FluentParserResult result = fp.Parse(args).OnFailure(x => Assert.True(false, id))
					.OnSuccess<ManyValuesVerb>(x =>
					{
						Assert.True(expectedResult.Option == x.Option, id);
						Assert.True(expectedResult.Switch == x.Switch, id);
						Assert.True(expectedResult.ManyValues.SequenceEqual(x.ManyValues), id);
					});
			}
			else
			{
				FluentParserResult result = fp.Parse(args)
					.OnFailure(x => Assert.True(true, id))
					.OnSuccess<ManyValuesVerb>(x => Assert.True(false, id));
			}
		}
		public static IEnumerable<object[]> ManyValueParsingData()
		{
			yield return new object[] { "All normal", new string[] { "value1", "value2", "-o", "Opt", "value3", "-s", "value4", "value5" }, true,
				new ManyValuesVerb() { Option = "Opt", Switch = true, ManyValues = new string[]{ "value1", "value2", "value3", "value4", "value5", } } };
			yield return new object[] { "Shuffled a bit", new string[] { "value1", "value2", "value3", "-s", "value4", "value5", "-o", "Opt" }, true,
				new ManyValuesVerb() { Option = "Opt", Switch = true, ManyValues = new string[]{ "value1", "value2", "value3", "value4", "value5", } } };
			yield return new object[] { "Even more values", new string[] { "value1", "value2", "-o", "Opt", "value3", "-s", "value4", "value5", "value6", "value7", "value8", "value9", "value10" }, true,
				new ManyValuesVerb() { Option = "Opt", Switch = true, ManyValues = new string[]{ "value1", "value2", "value3", "value4", "value5", "value6", "value7", "value8", "value9", "value10" } } };
			yield return new object[] { "Erronoeus extra thing with ignored prefix", new string[] { "value1", "value2", "-o", "Opt", "value3", "-s", "value4", "value5", "--sneakyErrorInDisguise", "-notAValue" }, false, null };
		}
		[Fact]
		public void VerbParsing()
		{
			FluentParser fp = new FluentParser()
				.AddVerb<Verb1>("verb1", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb2>("verb2", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb3>("verb3", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb4>("verb4", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb5>("verb5", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb6>("verb6", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb7>("verb7", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb8>("verb8", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb9>("verb9", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb10>("verb10", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb11>("verb11", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb12>("verb12", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb13>("verb13", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb14>("verb14", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb15>("verb15", v => { v.AddValue().ForProperty(x => x.Value); })
				.AddVerb<Verb16>("verb16", v => { v.AddValue().ForProperty(x => x.Value); });

			string[] args = new string[] { null, null };
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
				args[1] = $"Value {i}";
				FluentParserResult parsed = fp.Parse(args);
				Assert.True(parsed.Success);
				Assert.Equal(args[0], parsed.ParsedVerb.Name);
				parsed.OnSuccess<T>(x => Assert.Equal(args[1], x.Value));
				i++;
			}
		}
		[Fact]
		public void MixingUpVerbConfiguration()
		{
			Assert.Throws<FluentParserException>(() =>
				new FluentParser()
				.AddVerb<Verb1>("verb1", null)
				.WithoutVerbs<Verb2>(null));
			Assert.Throws<FluentParserException>(() =>
				new FluentParser()
				.WithoutVerbs<Verb1>(null)
				.AddVerb<Verb2>("verb2", null));
		}
		[Fact]
		public void ParsingWithInvalidVerb()
		{
			FluentParserResult result = new FluentParser()
				.AddVerb<Verb1>("verb1", null)
				.Parse(new string[] { "InvalidVerb" });
			Assert.Equal(ErrorCode.InvalidVerb, result.Errors.First().ErrorCode);
		}
		[Fact]
		public void AddingDuplicateVerbNames()
		{
			Assert.Throws<FluentParserException>(() => new FluentParser().AddVerb<Verb1>("Name", null).AddVerb<Verb2>("Name", null));
		}
		[Fact]
		public void ParsingWithoutProperConfiguration()
		{
			Assert.Throws<FluentParserException>(() => new FluentParser().Parse(new string[] { }));
		}
		[Fact]
		public void ParsingNullArgs()
		{
			Assert.Throws<ArgumentNullException>(() => new FluentParser().Parse(null));
		}
		[Fact]
		public void ParsingHelpSwitch()
		{
			FluentParser fp = new FluentParser()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<Verb1>(null);
			FluentParserResult result1 = fp.Parse(new string[] { "-?" });
			FluentParserResult result2 = fp.Parse(new string[] { "--help" });
			Assert.Contains(ErrorCode.HelpRequested, result1.Errors.Select(x => x.ErrorCode));
			Assert.Contains(ErrorCode.HelpRequested, result2.Errors.Select(x => x.ErrorCode));
		}
	}
}
