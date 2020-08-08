namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public sealed class ParseResult<TClass> : IParseResult where TClass : class, new()
	{
		// TODO maybe change this so it's more like the Maybe class, and can enforce that you do the correct thing
		public ParseResult(IReadOnlyCollection<Error> errors)
		{
			ParsedObject = null;
			Errors = errors;
		}
		public ParseResult(Verb<TClass> parsedVerb , TClass opt)
		{
			TypedParsedVerb = parsedVerb;
			ParsedObject = opt;
			Errors = Array.Empty<Error>();
		}
		public Verb<TClass>? TypedParsedVerb { get; }
		public IVerb? ParsedVerb => TypedParsedVerb;
		public TClass? ParsedObject { get; }
		public IReadOnlyCollection<Error> Errors { get; }
		public bool Success => Errors.Count == 0;
		public void Invoke()
		{
			if (!Success)
			{
				throw new InvalidOperationException("Cannot invoke, parsing failed");
			}
			TypedParsedVerb!.Invoke(ParsedObject!);
		}
		public async Task InvokeAsync()
		{
			if (!Success)
			{
				throw new InvalidOperationException("Cannot invoke, parsing failed");
			}
			await TypedParsedVerb!.InvokeAsync(ParsedObject!);
		}
	}
}