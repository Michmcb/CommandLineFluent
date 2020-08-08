namespace CommandLineFluent.Arguments
{
	public enum ArgumentRequired
	{
		/// <summary>
		/// Argument must be provided
		/// </summary>
		Required,
		/// <summary>
		/// Argument may or may not be provided
		/// </summary>
		Optional,
		/// <summary>
		/// Argument may be required to appear, be optional, or be required to not appear, depending on other arguments
		/// </summary>
		HasDependencies
	}
}
