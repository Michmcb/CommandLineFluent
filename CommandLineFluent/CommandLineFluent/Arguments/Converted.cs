namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// A converted value
	/// </summary>
	/// <typeparam name="C">The type of the converted value</typeparam>
	public class Converted<C>
	{
		public C ConvertedValue { get; }
		public string ErrorMessage { get; }
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
