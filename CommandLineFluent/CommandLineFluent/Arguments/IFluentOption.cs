namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// An option with one value, supplied like -t Value
	/// </summary>
	public interface IFluentOption : IFluentArgument
	{
		/// <summary>
		/// The Short Name for this FluentOption
		/// </summary>
		string ShortName { get; }
		/// <summary>
		/// The Long Name for this FluentOption
		/// </summary>
		string LongName { get; }
		/// <summary>
		/// Whether or not this Option is required to be included. By default, it is required.
		/// If null, then Dependencies will be used to determine whether or not this Option is required.
		/// </summary>
		bool? Required { get; }
		/// <summary>
		/// The short and long name joined with a |
		/// </summary>
		string ShortAndLongName();
	}
}