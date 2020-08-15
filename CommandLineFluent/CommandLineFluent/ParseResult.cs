namespace CommandLineFluent
{
	using System.Diagnostics.CodeAnalysis;
	using System.Threading.Tasks;

	// TODO change ParseResult, so it's a struct similar to Maybe. It should always hold the Verb, so if they say "verb -?" we know which verb should be displaying help text.
	public sealed class ParseResult<TClass> : IParseResult where TClass : class, new()
	{
		public ParseResult([DisallowNull]Verb<TClass> parsedVerb, [DisallowNull]TClass opt)
		{
			TypedParsedVerb = parsedVerb;
			ParsedObject = opt;
		}
		public Verb<TClass> TypedParsedVerb { get; }
		public IVerb ParsedVerb => TypedParsedVerb;
		public TClass ParsedObject { get; }
		public void Invoke()
		{
			TypedParsedVerb.Invoke(ParsedObject);
		}
		public async Task InvokeAsync()
		{
			await TypedParsedVerb.InvokeAsync(ParsedObject);
		}
	}
}