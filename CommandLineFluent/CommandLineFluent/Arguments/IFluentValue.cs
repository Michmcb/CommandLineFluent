namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// A value that is supplied like foo.exe MyValue
	/// </summary>
	public interface IFluentValue : IFluentArgument
	{
		/// <summary>
		/// Whether or not this is required
		/// </summary>
		bool Required { get; }
	}
}