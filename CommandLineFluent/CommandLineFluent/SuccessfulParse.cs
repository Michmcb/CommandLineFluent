namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// A successfully parsed result for a non-generic verb, where there are no arguments.
	/// </summary>
	public sealed class SuccessfulParse : IParseResult
	{
		private readonly Verb verb;
		public SuccessfulParse(Verb verb)
		{
			this.verb = verb;
		}
		/// <summary>
		/// Always true
		/// </summary>
		public bool Ok => true;
		/// <summary>
		/// The verb that was parsed
		/// </summary>
		public IVerb? Verb => verb;
		/// <summary>
		/// Always empty
		/// </summary>
		public IReadOnlyCollection<Error> Errors => Array.Empty<Error>();
		/// <summary>
		/// Calls <see cref="Verb.Invoke"/>
		/// </summary>
		public void Invoke()
		{
			verb.Invoke();
		}
		/// <summary>
		/// Calls <see cref="Verb.InvokeAsync"/>
		/// </summary>
		public async Task InvokeAsync()
		{
			await verb.InvokeAsync();
		}
	}
}