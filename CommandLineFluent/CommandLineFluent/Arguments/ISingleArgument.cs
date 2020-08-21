namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// An argument which a single value on the command line.
	/// </summary>
	public interface ISingleArgument<TClass> : IArgument<TClass>
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
