namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public sealed class EmptyParseResult : IParseResult
	{
		public EmptyParseResult(IReadOnlyCollection<Error> errors)
		{
			Errors = errors;
		}
		public EmptyParseResult(Error error)
		{
			Errors = new Error[] { error };
		}
		public IReadOnlyCollection<Error> Errors { get; }
		public bool Success => Errors.Count == 0;
		public IVerb? ParsedVerb => null;
		public void Invoke()
		{
			throw new InvalidOperationException("No verb was parsed, cannot invoke this");
		}
		public Task InvokeAsync()
		{
			throw new InvalidOperationException("No verb was parsed, cannot invoke this");
		}
	}
}
