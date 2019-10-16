namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// A converted value
	/// </summary>
	/// <typeparam name="C">The type of the converted value</typeparam>
	public class Converted<C>
	{
		/// <summary>
		/// The value converted to <typeparamref name="C"/>, if successful
		/// </summary>
		public C ConvertedValue { get; }
		/// <summary>
		/// An error message explaining why the value could not be converted to <typeparamref name="C"/>
		/// </summary>
		public string ErrorMessage { get; }
		/// <summary>
		/// Whether or not the conversion was successful
		/// </summary>
		public bool Successful { get; }
		/// <summary>
		/// Successfully converted the value
		/// </summary>
		public Converted(C convertedValue)
		{
			ConvertedValue = convertedValue;
			ErrorMessage = null;
			Successful = true;
		}
		/// <summary>
		/// Failed to convert the value
		/// </summary>
		public Converted(C convertedValue, string errorMessage)
		{
			ConvertedValue = convertedValue;
			ErrorMessage = errorMessage;
			Successful = false;
		}
	}
}
