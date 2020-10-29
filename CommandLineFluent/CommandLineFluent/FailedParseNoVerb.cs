namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	/// <summary>
	/// The verb was not parsed successfully. Consequently, no arguments were parsed either.
	/// </summary>
	public sealed class FailedParseNoVerb : IParseResult
	{
		public FailedParseNoVerb(IReadOnlyCollection<Error> errors)
		{
			Errors = errors;
		}
		/// <summary>
		/// Always false
		/// </summary>
		public bool Ok => false;
		/// <summary>
		/// The parsed verb, if one was parsed. Otherwise, null.
		/// </summary>
		public IVerb? Verb => null;
		/// <summary>
		/// The errors encountered during parsing
		/// </summary>
		public IReadOnlyCollection<Error> Errors { get; }
		/// <summary>
		/// Throws an <see cref="InvalidOperationException"/>.
		/// </summary>
		public void Invoke()
		{
			throw new InvalidOperationException("Parsing failed; cannot invoke");
		}
		/// <summary>
		/// Throws an <see cref="InvalidOperationException"/>.
		/// </summary>
		public Task InvokeAsync()
		{
			throw new InvalidOperationException("Parsing failed; cannot invoke");
		}
	}
}