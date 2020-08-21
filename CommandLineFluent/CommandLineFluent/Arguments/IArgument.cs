namespace CommandLineFluent.Arguments
{
	using System.Reflection;

	/// <summary>
	/// An argument on the command line, which can have a single or multiple values.
	/// </summary>
	/// <typeparam name="TClass"></typeparam>
	public interface IArgument<TClass> where TClass : class, new()
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
		Dependencies<TClass>? Dependencies { get; }
		Error EvaluateDependencies(TClass obj, bool gotValue);
	}
}
