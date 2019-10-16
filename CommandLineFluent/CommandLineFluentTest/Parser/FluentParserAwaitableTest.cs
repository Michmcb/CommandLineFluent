using CommandLineFluent;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineFluentTest.Parser
{
	public class FluentParserAwaitableTest
	{
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
			Task x = fpra.Invoke();
			await x;
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

			FluentParserResultAwaitable fpra = fp.ParseAwaitable(new string[] { })
				.OnSuccess<Verb1>(Verb1Async);
			Assert.False(taskRun);
			await fpra.Invoke();
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

			FluentParserResultAwaitable fpra = fp.ParseAwaitable(new string[] { })
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

			FluentParserResultAwaitable fpra = fp.ParseAwaitable(new string[] { })
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
			await Task.Delay(1000);
			taskRun = true;
		}
	}
}
