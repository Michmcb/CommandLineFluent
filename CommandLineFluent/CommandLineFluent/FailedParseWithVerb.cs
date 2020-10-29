namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	/// <summary>
	/// There was a verb parsed successfully, but the arguments were not parsed successfully.
	/// This is specifically for the case where the user provided arguments to a verb that does not expect any.
	/// </summary>
	public sealed class FailedParseWithVerb : IParseResult
	{
		private readonly Verb? verb;
		public FailedParseWithVerb(Verb? verb, IReadOnlyCollection<Error> errors)
		{
			this.verb = verb;
			Errors = errors;
		}
		/// <summary>
		/// Always false
		/// </summary>
		public bool Ok => false;
		/// <summary>
		/// The parsed verb, if one was parsed. Otherwise, null.
		/// </summary>
		public IVerb? Verb => verb;
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