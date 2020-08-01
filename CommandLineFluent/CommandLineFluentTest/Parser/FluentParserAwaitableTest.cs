using CommandLineFluent;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineFluentTest.Parser
{
	public class FluentParserAwaitableTest
	{
		private bool taskStarted;
		private bool taskRun;
		[Fact]
		public async Task InvokeAwaitable()
		{
			taskRun = false;
			FluentParser fp = new FluentParserBuilder()
				.WithoutVerbs<Verb1>(verblessConfig =>
				{
					verblessConfig.AddValue()
						.ForProperty(o => o.Value);
				}).Build();

			FluentParserResultAwaitable fpra = fp.ParseAwaitable(new string[] { "MyValue" })
				.OnSuccess<Verb1>(Verb1Async);
			Assert.False(taskRun);
			Task t = fpra.Invoke();
			await Task.Delay(500);
			Assert.True(taskStarted);
			await t;
			Assert.True(taskRun);
		}
		[Fact]
		public async Task InvokeAwaitableAsync()
		{
			taskRun = false;
			FluentParser fp = new FluentParserBuilder()
				.WithoutVerbs<Verb1>(verblessConfig =>
				{
					verblessConfig.AddValue()
						.ForProperty(o => o.Value);
				}).Build();

			FluentParserResultAwaitable fpra = fp.ParseAwaitable(new string[] { "MyValue" })
				.OnSuccess<Verb1>(Verb1Async);
			Assert.False(taskRun);
			await fpra.InvokeAsync();
			Assert.True(taskRun);
		}
		[Fact]
		public async Task InvokeAwaitableOnFailure()
		{
			taskRun = false;
			FluentParser fp = new FluentParserBuilder()
				.WithoutVerbs<Verb1>(verblessConfig =>
				{
					verblessConfig.AddValue()
						.ForProperty(o => o.Value);
				}).Build();

			FluentParserResultAwaitable fpra = fp.ParseAwaitable(System.Array.Empty<string>())
				.OnSuccess<Verb1>(Verb1Async);
			Assert.False(taskRun);
			Task t = fpra.Invoke();
			await Task.Delay(500);
			Assert.False(taskStarted);
			await t;
			Assert.False(taskRun);
		}
		[Fact]
		public async Task InvokeAwaitableOnFailureAsync()
		{
			taskRun = false;
			FluentParser fp = new FluentParserBuilder()
				.WithoutVerbs<Verb1>(verblessConfig =>
				{
					verblessConfig.AddValue()
						.ForProperty(o => o.Value);
				}).Build();

			FluentParserResultAwaitable fpra = fp.ParseAwaitable(System.Array.Empty<string>())
				.OnSuccess<Verb1>(Verb1Async);
			Assert.False(taskRun);
			await fpra.InvokeAsync();
			Assert.False(taskRun);
		}
		[Fact]
		public async Task InvokeFailure()
		{
			taskRun = false;
			FluentParser fp = new FluentParserBuilder()
				.WithoutVerbs<Verb1>(verblessConfig =>
				{
					verblessConfig.AddValue()
						.ForProperty(o => o.Value);
				}).Build();

			FluentParserResultAwaitable fpra = fp.ParseAwaitable(System.Array.Empty<string>())
				.StopOnFailure()
				?.OnSuccess<Verb1>(Verb1Async);
			Assert.False(taskRun);
			if (fpra != null)
			{
				await fpra.InvokeAsync();
			}
			Assert.False(taskRun);
		}
		private async Task Verb1Async(Verb1 arg)
		{
			taskStarted = true;
			await Task.Delay(3000);
			taskRun = true;
		}
	}
}
