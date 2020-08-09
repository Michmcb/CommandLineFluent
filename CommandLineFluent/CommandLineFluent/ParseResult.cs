namespace CommandLineFluent
{
	using System.Diagnostics.CodeAnalysis;
	using System.Threading.Tasks;

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