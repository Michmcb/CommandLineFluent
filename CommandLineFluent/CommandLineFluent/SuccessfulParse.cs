namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Threading.Tasks;

	public sealed class SuccessfulParse<TClass> : IParseResult where TClass : class, new()
	{
		private readonly Verb<TClass> verb;
		public SuccessfulParse([DisallowNull]Verb<TClass> verb, [DisallowNull]TClass opt)
		{
			this.verb = verb;
			this.Object = opt;
		}
		public bool Ok => true;
		public IVerb? Verb => verb;
		public TClass Object { get; }
		public IReadOnlyCollection<Error> Errors => Array.Empty<Error>();
		public void Invoke()
		{
			verb.Invoke(Object);
		}
		public async Task InvokeAsync()
		{
			await verb.InvokeAsync(Object);
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