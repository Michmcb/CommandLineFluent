namespace CommandLineFluent.Arguments
{
	using System.Collections.Generic;
	using System.Reflection;

	/// <summary>
	/// A single argument on the command line.
	/// </summary>
	public interface IArgument<TClass>
	{
		/// <summary>
		/// A human-readable name which describes this
		/// </summary>
		string? Name { get; }
		ArgumentRequired ArgumentRequired { get; }
		/// <summary>
		/// The property that this argument maps to
		/// </summary>
		PropertyInfo? TargetProperty { get; }
		IEnumerable<Error> Validate();
		/// <summary>
		/// Sets a property of <paramref name="obj"/> to <paramref name="value"/>, after conversion.
		/// There may or may not be a converter set up to translate <paramref name="value"/> into something else.
		/// </summary>
		/// <param name="obj">The object on which to set a property</param>
		/// <param name="value">The value to set the property to (before any conversion)</param>
		Error SetValue(TClass obj, string? value);
	}
}
