namespace CommandLineFluent.Arguments
{
	using System.Collections.Generic;
	using System.Reflection;

	/// <summary>
	/// A single argument on the command line.
	/// </summary>
	public interface ISingleArgument<TClass>
	{
		/// <summary>
		/// A human-readable name which describes this
		/// </summary>
		string? Name { get; }
		string HelpText { get; }
		ArgumentRequired ArgumentRequired { get; }
		/// <summary>
		/// The property that this argument maps to
		/// </summary>
		PropertyInfo? TargetProperty { get; }
		Error EvaluateDependencies(TClass obj, bool gotValue);
		/// <summary>
		/// Sets a property of <paramref name="obj"/> to <paramref name="rawValue"/>, after conversion.
		/// There may or may not be a converter set up to translate <paramref name="rawValue"/> into something else.
		/// </summary>
		/// <param name="obj">The object on which to set a property</param>
		/// <param name="rawValue">The value to set the property to (before any conversion)</param>
		Error SetValue(TClass obj, string? rawValue);
		IEnumerable<Error> Validate();
	}
}
