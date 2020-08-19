namespace CommandLineFluent
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	public interface IParseResult
	{
		bool Ok { get; }
		IVerb? Verb { get; }
		IReadOnlyCollection<Error> Errors { get; }
		void Invoke();
		Task InvokeAsync();
	}
}