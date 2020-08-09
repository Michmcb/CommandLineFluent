namespace CommandLineFluent
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	public interface IParseResult
	{
		IVerb ParsedVerb { get; }
		void Invoke();
		Task InvokeAsync();
	}
}