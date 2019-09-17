namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// A switch toggled on or off, supplied like -s
	/// </summary>
	public interface IFluentSwitch : IFluentArgument
	{
		/// <summary>
		/// The Short Name for this FluentSwitch
		/// </summary>
		string ShortName { get; }
		/// <summary>
		/// The Long Name for this FluentSwitch
		/// </summary>
		string LongName { get; }
		/// <summary>
		/// The short and long name joined with a |
		/// </summary>
		string ShortAndLongName();
	}
}