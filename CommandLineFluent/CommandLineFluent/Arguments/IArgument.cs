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
		/// A human-readable name which describes this argument.
		/// </summary>
		string? DescriptiveName { get; }
		/// <summary>
		/// Text that describes this.
		/// </summary>
		string HelpText { get; }
		/// <summary>
		/// If the argument is required or not, or if it's sometimes required based on dependencies.
		/// </summary>
		ArgumentRequired ArgumentRequired { get; }
		/// <summary>
		/// Any dependencies that this argument has
		/// </summary>
		Dependencies<TClass>? Dependencies { get; }
		/// <summary>
		/// If a dependency is violated, returns an <see cref="Error"/> with an errorcode and message.
		/// </summary>
		/// <param name="obj">The object to check dependencies on.</param>
		/// <param name="gotValue">True if this argument got a value, false otherwise.</param>
		/// <returns>An <see cref="Error"/></returns>
		Error EvaluateDependencies(TClass obj, bool gotValue);
	}
}
