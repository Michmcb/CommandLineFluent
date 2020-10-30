namespace CommandLineFluent
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	/// <summary>
	/// Represents the result of parsing arguments.
	/// </summary>
	public interface IParseResult
	{
		/// <summary>
		/// True if parsing was successful, false otherwise.
		/// </summary>
		bool Ok { get; }
		/// <summary>
		/// The verb that was parsed, or null if <see cref="Ok"/> is false.
		/// </summary>
		IVerb? Verb { get; }
		/// <summary>
		/// Errors encountered during parsing, or empty is <see cref="Ok"/> is true.
		/// </summary>
		IReadOnlyCollection<Error> Errors { get; }
		/// <summary>
		/// Calls the parsed Verb's Invoke method, passing the object, if any is needed.
		/// </summary>
		void Invoke();
		/// <summary>
		/// Calls the parsed Verb's InvokeAsync method, passing the object, if any is needed.
		/// </summary>
		Task InvokeAsync();
	}
}