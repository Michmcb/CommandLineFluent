using System.Collections.Generic;

namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// A Fluent Argument that is able to have its value set to something
	/// </summary>
	/// <typeparam name="T">The type of the target class whose property will be set</typeparam>
	/// <typeparam name="V">The type of the value provided. May be converted to something else</typeparam>
	internal interface IFluentSettable<T, V>
	{
		/// <summary>
		/// Sets a property of object <typeparamref name="T"/> to the value of <typeparamref name="V"/>.
		/// There may or may not be a converter set up to translate <paramref name="value"/> into something else
		/// </summary>
		/// <param name="target">The object on which to set a property</param>
		/// <param name="value">The value to set the property to (before any conversion)</param>
		Error SetValue(T target, V value);
		/// <summary>
		/// Returns an Error for each invalid thing.
		/// </summary>
		IEnumerable<Error> Validate();
		/// <summary>
		/// Checks to make sure that all dependencies are respected. If they are not, returns an Error
		/// describing the first dependency that was violated.
		/// If no dependencies have been set up, returns null.
		/// </summary>
		/// <param name="obj">The object to check</param>
		Error EvaluateDependencies(T obj);
	}
}
