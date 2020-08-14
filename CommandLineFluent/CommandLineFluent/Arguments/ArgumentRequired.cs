namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// Defines if a partiular argument is required or not. Or, if it's only sometimes required because it has dependencies.
	/// </summary>
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
