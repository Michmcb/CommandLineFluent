//using System.Text;
//using Xunit;

//namespace CommandLineFluent.Test
//{
//	public static class HelpFormatterTest
//	{
//		[Fact]
//		public static void HelpTextRequired()
//		{
//			Verb<ManyValuesVerb> ourVerb = null;
//			StringBuilder fullHelpText = new StringBuilder();
//			CliParser parserVerbless = new CliParserBuilder()
//			.AddVerb<ManyValuesVerb>("default", verb =>
//			{
//				ourVerb = verb;

//				verb.AddMultiValue()
//					.ForProperty(e => e.ManyValues)
//					.WithName("MyValues")
//					.WithHelpText("ValuesHelpText");

//				verb.AddOption("o", "opt")
//					.ForProperty(e => e.Option)
//					.WithName("OptionValue")
//					.WithHelpText("OptionHelpText");

//				verb.AddSwitch("s", "sw")
//					.ForProperty(e => e.Switch)
//					.WithHelpText("SwitchHelpText");
//			})
//			.Build();

//			parserVerbless.WriteOverallUsageAndHelp();
//			Assert.Contains("VerblessHelpText", fullHelpText.ToString());
//			fullHelpText.Clear();
//			parserVerbless.WriteUsageAndHelp(ourVerb);
//			Assert.Contains("VerblessHelpText", fullHelpText.ToString());
//			Assert.Contains("\"MyValues\"", fullHelpText.ToString());
//			Assert.Contains("-o|--opt \"OptionValue\"", fullHelpText.ToString());
//			Assert.Contains("[-s|--sw]", fullHelpText.ToString());
//		}
//		[Fact]
//		public static void HelpTextOptional()
//		{
//			Verb<ManyValuesVerb> ourVerb = null;
//			StringBuilder fullHelpText = new StringBuilder();
//			CliParser parserVerbless = new CliParserBuilder()
//			.AddVerb<ManyValuesVerb>("default", verb =>
//			{
//				ourVerb = verb;
//				verb.HelpText = "VerblessHelpText";

//				verb.AddMultiValue()
//					.IsOptional()
//					.ForProperty(e => e.ManyValues)
//					.WithName("MyValues")
//					.WithHelpText("ValuesHelpText");

//				verb.AddOption("o", "opt")
//					.IsOptional()
//					.ForProperty(e => e.Option)
//					.WithName("OptionValue")
//					.WithHelpText("OptionHelpText");

//				verb.AddSwitch("s", "sw")
//					.ForProperty(e => e.Switch)
//					.WithHelpText("SwitchHelpText");
//			})
//			.Build();

//			parserVerbless.WriteOverallUsageAndHelp();
//			Assert.Contains("VerblessHelpText", fullHelpText.ToString());
//			fullHelpText.Clear();
//			parserVerbless.WriteUsageAndHelp(ourVerb);
//			Assert.Contains("VerblessHelpText", fullHelpText.ToString());
//			Assert.Contains("[MyValues]", fullHelpText.ToString());
//			Assert.Contains("[-o|--opt \"OptionValue\"]", fullHelpText.ToString());
//			Assert.Contains("[-s|--sw]", fullHelpText.ToString());
//		}
//	}
//}
