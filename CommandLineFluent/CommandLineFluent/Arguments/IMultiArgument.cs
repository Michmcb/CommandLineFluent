namespace CommandLineFluent.Arguments
{
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Reflection;
	/// <summary>
	/// An argument which has multiple values on the command line.
	/// </summary>
	public interface IMultiArgument<TClass>
	{
		/// <summary>
		/// A human-readable name which describes this.
		/// </summary>
		string? Name { get; }
		/// <summary>
		/// Text that describes this.
		/// </summary>
		string HelpText { get; }
		/// <summary>
		/// If the argument is required or not, or if it's sometimes required based on dependencies.
		/// </summary>
		ArgumentRequired ArgumentRequired { get; }
		/// <summary>
		/// The property that this argument maps to.
		/// </summary>
		PropertyInfo TargetProperty { get; }
		Error EvaluateDependencies([DisallowNull] TClass obj, bool gotValue);
		/// <summary>
		/// Sets a property of <paramref name="obj"/> to <paramref name="rawValues"/>, after conversion.
		/// There may or may not be a converter set up to translate <paramref name="rawValues"/> into something else.
		/// </summary>
		/// <param name="obj">The object on which to set a property</param>
		/// <param name="rawValues">The value to set the property to (before any conversion)</param>
		Error SetValue([DisallowNull] TClass obj, IReadOnlyCollection<string> rawValues);
	}
}
