namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// A value that is supplied like foo.exe MyValue
	/// </summary>
	public interface IFluentValue : IFluentArgument
	{
		/// <summary>
		/// Whether or not this Value is required to be provided. By default, it is required.
		/// If null, then Dependencies will be used to determine whether or not this Value is required.
		/// </summary>
		bool? Required { get; }
	}
}