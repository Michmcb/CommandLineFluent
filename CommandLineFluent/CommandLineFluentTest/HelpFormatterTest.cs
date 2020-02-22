using CommandLineFluent;
using System.Text;
using Xunit;

namespace CommandLineFluentTest
{
	public static class HelpFormatterTest
	{
		[Fact]
		public static void HelpTextRequired()
		{
			FluentVerb<ManyValuesVerb> ourVerb = null;
			StringBuilder fullHelpText = new StringBuilder();
			FluentParser parserVerbless = new FluentParserBuilder()
			.Configure(config =>
			{
				config.ConfigureWithDefaults();
				config.WithTextWriter((str) =>
				{
					fullHelpText.Append(str);
				});
			})
			.WithoutVerbs<ManyValuesVerb>(verb =>
			{
				ourVerb = verb;

				verb.WithHelpText("VerblessHelpText");

				verb.AddManyValues()
					.ForProperty(e => e.ManyValues)
					.WithName("MyValues")
					.WithHelpText("ValuesHelpText");

				verb.AddOption("o", "opt")
					.ForProperty(e => e.Option)
					.WithName("OptionValue")
					.WithHelpText("OptionHelpText");

				verb.AddSwitch("s", "sw")
					.ForProperty(e => e.Switch)
					.WithHelpText("SwitchHelpText");
			})
			.Build();

			parserVerbless.WriteOverallUsageAndHelp();
			Assert.Contains("VerblessHelpText", fullHelpText.ToString());
			fullHelpText.Clear();
			parserVerbless.WriteUsageAndHelp(ourVerb);
			Assert.Contains("VerblessHelpText", fullHelpText.ToString());
			Assert.Contains("\"MyValues\"", fullHelpText.ToString());
			Assert.Contains("-o|--opt \"OptionValue\"", fullHelpText.ToString());
			Assert.Contains("[-s|--sw]", fullHelpText.ToString());
		}
		[Fact]
		public static void HelpTextOptional()
		{
			FluentVerb<ManyValuesVerb> ourVerb = null;
			StringBuilder fullHelpText = new StringBuilder();
			FluentParser parserVerbless = new FluentParserBuilder()
			.Configure(config =>
			{
				config.ConfigureWithDefaults();
				config.WithTextWriter((str) =>
				{
					fullHelpText.Append(str);
				});
			})
			.WithoutVerbs<ManyValuesVerb>(verb =>
			{
				ourVerb = verb;

				verb.WithHelpText("VerblessHelpText");

				verb.AddManyValues()
					.IsOptional()
					.ForProperty(e => e.ManyValues)
					.WithName("MyValues")
					.WithHelpText("ValuesHelpText");

				verb.AddOption("o", "opt")
					.IsOptional()
					.ForProperty(e => e.Option)
					.WithName("OptionValue")
					.WithHelpText("OptionHelpText");

				verb.AddSwitch("s", "sw")
					.ForProperty(e => e.Switch)
					.WithHelpText("SwitchHelpText");
			})
			.Build();

			parserVerbless.WriteOverallUsageAndHelp();
			Assert.Contains("VerblessHelpText", fullHelpText.ToString());
			fullHelpText.Clear();
			parserVerbless.WriteUsageAndHelp(ourVerb);
			Assert.Contains("VerblessHelpText", fullHelpText.ToString());
			Assert.Contains("[MyValues]", fullHelpText.ToString());
			Assert.Contains("[-o|--opt \"OptionValue\"]", fullHelpText.ToString());
			Assert.Contains("[-s|--sw]", fullHelpText.ToString());
		}
	}
}
