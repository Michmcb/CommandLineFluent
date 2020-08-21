namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// A switch toggled on or off, supplied like -s
	/// </summary>
	public interface ISwitch<TClass> : ISingleArgument<TClass> where TClass : class, new()
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