namespace CommandLineFluent
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	public interface IParseResult
	{
		bool Success { get; }
		IReadOnlyCollection<Error> Errors { get; }
		IVerb? ParsedVerb { get; }
		void Invoke();
		Task InvokeAsync();
	}
}