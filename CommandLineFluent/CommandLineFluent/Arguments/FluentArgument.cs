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
		/// The property that this argument maps to
		/// </summary>
		public PropertyInfo TargetProperty { get; protected set; }
		/// <summary>
		/// Validates this FluentArgument, returning an Error object if something is invalid.
		/// </summary>
		public virtual Error Validate()
		{
			if (TargetProperty == null)
			{
				return new Error(ErrorCode.ProgrammerError, false, $"{Name} does not have a target property set");
			}
			return null;
		}
	}
}
