namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Threading.Tasks;

	public sealed class FailedParseWithVerb<TClass> : IParseResult where TClass : class, new()
	{
		private readonly Verb<TClass>? verb;
		public FailedParseWithVerb([AllowNull]Verb<TClass>? verb, IReadOnlyCollection<Error> errors)
		{
			this.verb = verb;
			Errors = errors;
		}
		public IReadOnlyCollection<Error> Errors { get; }

		public bool Ok => false;
		public IVerb? Verb => verb;
		public void Invoke()
		{
			throw new InvalidOperationException("Parsing failed; cannot invoke");
		}
		public Task InvokeAsync()
		{
			throw new InvalidOperationException("Parsing failed; cannot invoke");
		}
	}
	//public sealed class ParseResult<TClass> : IParseResult where TClass : class, new()
	//{
	//	internal ParseResult(IReadOnlyCollection<Error> errors)
	//	{
	//		ParsedVerb = null;
	//		ParsedObject = null;
	//		Errors = errors;
	//		Success = false;
	//	}
	//	
	//	internal ParseResult([DisallowNull]Verb<TClass> parsedVerb, [DisallowNull]TClass opt)
	//	{
	//		ParsedVerb = parsedVerb;
	//		ParsedObject = opt;
	//		Errors = Array.Empty<Error>();
	//		Success = true;
	//	}
	//	public Verb<TClass>? ParsedVerb { get; }
	//	public IVerb Verb { get; }
	//	public TClass? ParsedObject { get; }
	//	public IReadOnlyCollection<Error> Errors { get; }
	//	public bool Success { get; }
	//	public void Invoke()
	//	{
	//		ParsedVerb.Invoke(ParsedObject);
	//	}
	//	public async Task InvokeAsync()
	//	{
	//		await ParsedVerb.InvokeAsync(ParsedObject);
	//	}
	//}
}