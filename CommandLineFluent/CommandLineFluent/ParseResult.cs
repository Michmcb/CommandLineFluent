namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public sealed class ParseResult<TClass> : IParseResult
	{
		private readonly Action<TClass> invoke;
		private readonly Func<TClass, Task> invokeAsync;
		public ParseResult(IReadOnlyCollection<Error> errors)
		{
			Object = default;
			Errors = errors;
			// TODO write exception messages
			invoke = (x) => throw new CliParserException();
			invokeAsync = (x) => throw new CliParserException();
		}
		public ParseResult(TClass opt, Action<TClass> invoke, Func<TClass, Task> invokeAsync)
		{
			Object = opt;
			Errors = Array.Empty<Error>();
			this.invoke = invoke;
			this.invokeAsync = invokeAsync;
		}
		public TClass Object { get; }
		public IReadOnlyCollection<Error> Errors { get; }
		public bool Success => Errors.Count == 0;
		public void Invoke()
		{
			invoke(Object);
		}
		public async Task InvokeAsync()
		{
			await invokeAsync(Object);
		}
	}
}