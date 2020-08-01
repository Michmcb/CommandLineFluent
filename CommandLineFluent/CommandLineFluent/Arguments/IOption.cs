namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// An option with one value, supplied like -t Value
	/// </summary>
	public interface IOption<TClass> : IArgument<TClass>
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
		/// The short and long name joined with a |
		/// </summary>
		string ShortAndLongName();
	}
}