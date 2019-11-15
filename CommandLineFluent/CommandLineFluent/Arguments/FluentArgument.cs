using System.Reflection;

namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// A single argument on the command line. Can be a Switch, Option, or Value
	/// </summary>
	public abstract class FluentArgument : IFluentArgument
	{
		/// <summary>
		/// A human-readable name which describes this
		/// </summary>
		public string Name { get; protected set; }
		/// <summary>
		/// The Help Text to be shown to the user associated with this argument
		/// </summary>
		public string HelpText { get; protected set; }
		/// <summary>
		/// True if the FluentArgument got a value from parsing arguments, false otherwise.
		/// Returns false if the 
		/// </summary>
		public bool GotValue { get; internal set; }
		/// <summary>
		/// The property that this argument maps to
		/// </summary>
		public PropertyInfo TargetProperty { get; protected set; }
	}
}
