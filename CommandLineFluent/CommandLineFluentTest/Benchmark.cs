using CommandLineFluent;
using System.Diagnostics;
using Xunit;

namespace CommandLineFluentTest
{
	public class Benchmark
	{
		[Fact]
		public void Measure10000Iterations()
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			FluentParser fp = null;
			for (int i = 0; i < 10000; i++)
			{
				fp = new FluentParserBuilder()
					.Configure(config =>
					{
						config.ConfigureWithDefaults();
						config.WithExecuteCommand();
					})
					.AddVerb<Verb1>("verb1", verb =>
					{
						verb.WithDescription("A verb");

						verb.AddValue()
							.ForProperty(x => x.Value)
							.WithName("Value")
							.WithHelpText("Some help text");

						verb.AddOption("o", "option")
							.ForProperty(x => x.Option)
							.WithName("Option")
							.WithHelpText("Some help text");

						verb.AddSwitch("s", "switch")
							.ForProperty(x => x.Switch)
							.WithHelpText("Some help text");
					})
					.AddVerb<Verb2>("verb2", verb =>
					{
						verb.WithDescription("A verb");

						verb.AddValue()
							.ForProperty(x => x.Value)
							.WithName("Value")
							.WithHelpText("Some help text");

						verb.AddOption("o", "option")
							.ForProperty(x => x.Option)
							.WithName("Option")
							.WithHelpText("Some help text");

						verb.AddSwitch("s", "switch")
							.ForProperty(x => x.Switch)
							.WithHelpText("Some help text");
					})
					.AddVerb<Verb3>("verb3", verb =>
					{
						verb.WithDescription("A verb");

						verb.AddValue()
							.ForProperty(x => x.Value)
							.WithName("Value")
							.WithHelpText("Some help text");

						verb.AddOption("o", "option")
							.ForProperty(x => x.Option)
							.WithName("Option")
							.WithHelpText("Some help text");

						verb.AddSwitch("s", "switch")
							.ForProperty(x => x.Switch)
							.WithHelpText("Some help text");
					})
					.AddVerb<Verb4>("verb4", verb =>
					{
						verb.WithDescription("A verb");

						verb.AddValue()
							.ForProperty(x => x.Value)
							.WithName("Value")
							.WithHelpText("Some help text");

						verb.AddOption("o", "option")
							.ForProperty(x => x.Option)
							.WithName("Option")
							.WithHelpText("Some help text");

						verb.AddSwitch("s", "switch")
							.ForProperty(x => x.Switch)
							.WithHelpText("Some help text");
					})
					.AddVerb<Verb5>("verb5", verb =>
					{
						verb.WithDescription("A verb");

						verb.AddValue()
							.ForProperty(x => x.Value)
							.WithName("Value")
							.WithHelpText("Some help text");

						verb.AddOption("o", "option")
							.ForProperty(x => x.Option)
							.WithName("Option")
							.WithHelpText("Some help text");

						verb.AddSwitch("s", "switch")
							.ForProperty(x => x.Switch)
							.WithHelpText("Some help text");
					})
					.AddVerb<Verb6>("verb6", verb =>
					{
						verb.WithDescription("A verb");

						verb.AddValue()
							.ForProperty(x => x.Value)
							.WithName("Value")
							.WithHelpText("Some help text");

						verb.AddOption("o", "option")
							.ForProperty(x => x.Option)
							.WithName("Option")
							.WithHelpText("Some help text");

						verb.AddSwitch("s", "switch")
							.ForProperty(x => x.Switch)
							.WithHelpText("Some help text");
					})
					.AddVerb<Verb7>("verb7", verb =>
					{
						verb.WithDescription("A verb");

						verb.AddValue()
							.ForProperty(x => x.Value)
							.WithName("Value")
							.WithHelpText("Some help text");

						verb.AddOption("o", "option")
							.ForProperty(x => x.Option)
							.WithName("Option")
							.WithHelpText("Some help text");

						verb.AddSwitch("s", "switch")
							.ForProperty(x => x.Switch)
							.WithHelpText("Some help text");
					})
					.AddVerb<Verb8>("verb8", verb =>
					{
						verb.WithDescription("A verb");

						verb.AddValue()
							.ForProperty(x => x.Value)
							.WithName("Value")
							.WithHelpText("Some help text");

						verb.AddOption("o", "option")
							.ForProperty(x => x.Option)
							.WithName("Option")
							.WithHelpText("Some help text");

						verb.AddSwitch("s", "switch")
							.ForProperty(x => x.Switch)
							.WithHelpText("Some help text");
					})
					.AddVerb<Verb9>("verb9", verb =>
					{
						verb.WithDescription("A verb");

						verb.AddValue()
							.ForProperty(x => x.Value)
							.WithName("Value")
							.WithHelpText("Some help text");

						verb.AddOption("o", "option")
							.ForProperty(x => x.Option)
							.WithName("Option")
							.WithHelpText("Some help text");

						verb.AddSwitch("s", "switch")
							.ForProperty(x => x.Switch)
							.WithHelpText("Some help text");
					}).Build();
			}
			sw.Stop();
			double ticks = sw.ElapsedTicks / 10000d;
			double ms = sw.ElapsedMilliseconds / 10000d;
			sw.Reset();
			string[] args = new string[] { "verb4", "ValueValue", "-o", "OptionValue", "-s" };
			sw.Start();
			for (int i = 0; i < 10000; i++)
			{
				fp.Parse(args).StopOnFailure()
					?.OnSuccess<Verb1>(Dummy)
					.OnSuccess<Verb2>(Dummy)
					.OnSuccess<Verb3>(Dummy)
					.OnSuccess<Verb4>(Dummy)
					.OnSuccess<Verb5>(Dummy)
					.OnSuccess<Verb6>(Dummy)
					.OnSuccess<Verb7>(Dummy)
					.OnSuccess<Verb8>(Dummy)
					.OnSuccess<Verb9>(Dummy);
			}
			sw.Stop();
			ticks = sw.ElapsedTicks / 10000d;
			ms = sw.ElapsedMilliseconds / 10000d;
		}
		private void Dummy(object o) { }
	}
}
