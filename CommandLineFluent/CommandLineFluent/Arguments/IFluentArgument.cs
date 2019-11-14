using System.Reflection;

namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// A single argument on the command line. Can be a Switch, Option, or Value
	/// </summary>
	public interface IFluentArgument
	{
		/// <summary>
		/// A human-readable name which describes this
		/// </summary>
		string Name { get; }
		/// <summary>
		/// The Help Text to be shown to the user associated with this argument
		/// </summary>
		string HelpText { get; }
		/// <summary>
		/// True if the FluentArgument got a value from parsing arguments, false otherwise.
		/// </summary>
		bool GotValue { get; }
		/// <summary>
		/// The property that this argument maps to
		/// </summary>
		PropertyInfo TargetProperty { get; }
	}
}
