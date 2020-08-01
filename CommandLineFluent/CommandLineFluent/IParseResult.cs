namespace CommandLineFluent
{
	using System.Threading.Tasks;
	public interface IParseResult
	{
		void Invoke();
		Task InvokeAsync();
		bool Success { get; }
	}
}