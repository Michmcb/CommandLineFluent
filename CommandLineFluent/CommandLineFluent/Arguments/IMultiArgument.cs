namespace CommandLineFluent.Arguments
{
	using System.Collections.Generic;
	/// <summary>
	/// An argument which has multiple values on the command line.
	/// </summary>
	public interface IMultiArgument<TClass> : IArgument<TClass>
	{
		/// <summary>
		/// Sets a property of <paramref name="obj"/> to <paramref name="rawValues"/>, after conversion.
		/// There may or may not be a converter set up to translate <paramref name="rawValues"/> into something else.
		/// </summary>
		/// <param name="obj">The object on which to set a property</param>
		/// <param name="rawValues">The value to set the property to (before any conversion)</param>
		Error SetValue(TClass obj, IReadOnlyCollection<string> rawValues);
	}
}
