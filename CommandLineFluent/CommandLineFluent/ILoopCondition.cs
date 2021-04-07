namespace CommandLineFluent
{
	/// <summary>
	/// Defines a condition for looping.
	/// See <see cref="StopOnKeywordOrCancel"/> for a simple implementation.
	/// </summary>
	public interface ILoopCondition
	{
		/// <summary>
		/// If true, the loop should continue. If false, the loop should stop.
		/// </summary>
		/// <param name="rawInput"></param>
		/// <returns>true to continue, false to stop processing and exit.</returns>
		bool ShouldGo(string? rawInput);
	}
}
