namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// A value that is supplied like foo.exe MyValue
	/// </summary>
	public interface IValue<TClass> : ISingleArgument<TClass> where TClass : class, new()
	{

	}
}