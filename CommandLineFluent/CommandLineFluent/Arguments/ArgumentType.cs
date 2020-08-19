namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// The type of an argument
	/// </summary>
	public enum ArgumentType
	{
		/// <summary>
		/// e.g. -o Value or --option Value
		/// </summary>
		Option,
		/// <summary>
		/// e.g. -s or --switch
		/// </summary>
		Switch,
		/// <summary>
		/// A lone value
		/// </summary>
		Value,
		/// <summary>
		/// Many lone values
		/// </summary>
		MultiValue
	}
}
