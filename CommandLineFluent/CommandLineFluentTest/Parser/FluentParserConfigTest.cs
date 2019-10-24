using CommandLineFluent;
using Xunit;

namespace CommandLineFluentTest.Parser
{
	public class FluentParserConfigTest
	{
		[Fact]
		public void Configuration()
		{
			new FluentParserBuilder()
				.Configure(config =>
				{
					// These are our defaults
					config.ConfigureWithDefaults();
					Assert.Equal("-", config.DefaultShortPrefix);
					Assert.Equal("--", config.DefaultLongPrefix);

					config.UseHelpSwitch("-1", "-2");
					Assert.Equal("-1", config.ShortHelpSwitch);
					Assert.Equal("-2", config.LongHelpSwitch);
					// Since this isn't using the console, it won't set Console.WriteLine for us
					//Assert.Null(config.WriteHelp);
					//Assert.Null(config.WriteUsage);

					// TODO once implemented, uncomment
					//config.UseDelimiters('=', ':');
					//Assert.Contains('=', config.Delimiters);
					//Assert.Contains(':', config.Delimiters);

					config.UseDefaultPrefixes("-", "--");
					Assert.Equal("-", config.DefaultShortPrefix);
					Assert.Equal("--", config.DefaultLongPrefix);
				});
		}
	}
}
