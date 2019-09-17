namespace CommandLineFluent.Arguments
{
	public interface IFluentManyValues : IFluentArgument
	{
		string[] IgnoredPrefixes { get; }
		bool Required { get; }
	}
}