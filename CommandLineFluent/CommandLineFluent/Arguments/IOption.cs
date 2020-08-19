namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// An option with one value, supplied like -t Value
	/// </summary>
	public interface IOption<TClass> : ISingleArgument<TClass>
	{
		/// <summary>
		/// The Short Name for this
		/// </summary>
		string? ShortName { get; }
		/// <summary>
		/// The Long Name for this
		/// </summary>
		string? LongName { get; }
		/// <summary>
		/// The short and long name joined with a |
		/// </summary>
		string ShortAndLongName();
	}
}