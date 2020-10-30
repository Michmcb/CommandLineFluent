namespace CommandLineFluent.Arguments
{
	using System.Collections.Generic;
	/// <summary>
	/// An argument which has multiple values on the command line.
	/// </summary>
	public interface IMultiArgument<TClass> : IArgument<TClass> where TClass : class, new()
	{
		/// <summary>
		/// Sets a property of <paramref name="obj"/> to <paramref name="rawValues"/>, after conversion.
		/// There may or may not be a converter set up to translate <paramref name="rawValues"/> into something else.
		/// The reason why we specifically take a List and not anything else, is because when we set the accumulator, to save an unnecessary collection copy, we just
		/// use the list directly (unless there's a converter configured, then we have to do a copy). That way, we cover the most common collection types.
		/// </summary>
		/// <param name="obj">The object on which to set a property</param>
		/// <param name="rawValues">The values to set the property to (before any conversion)</param>
		Error SetValue(TClass obj, List<string> rawValues);
	}
}
