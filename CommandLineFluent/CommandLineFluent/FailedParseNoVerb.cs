namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	public sealed class FailedParseNoVerb : IParseResult
	{
		public FailedParseNoVerb(IReadOnlyCollection<Error> errors)
		{
			Errors = errors;
		}
		public IReadOnlyCollection<Error> Errors { get; }
		public bool Ok => false;
		public IVerb? Verb => null;
		public void Invoke()
		{
			throw new InvalidOperationException("Parsing failed; cannot invoke");
		}
		public Task InvokeAsync()
		{
			throw new InvalidOperationException("Parsing failed; cannot invoke");
		}
	}
}