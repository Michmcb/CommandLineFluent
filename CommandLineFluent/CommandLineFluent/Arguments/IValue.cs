namespace CommandLineFluent.Arguments
{
	public interface IValue : IArgument
	{

	}
	/// <summary>
	/// A value that is supplied like foo.exe MyValue
	/// </summary>
	public interface IValue<TClass> : IValue, IArgument<TClass> where TClass : class, new()
	{
		/// <summary>
		/// Sets a property of <paramref name="obj"/> to <paramref name="rawValue"/>, after conversion.
		/// There may or may not be a converter set up to translate <paramref name="rawValue"/> into something else.
		/// </summary>
		/// <param name="obj">The object on which to set a property</param>
		/// <param name="rawValue">The value to set the property to (before any conversion)</param>
		Error SetValue(TClass obj, string? rawValue);
	}
}