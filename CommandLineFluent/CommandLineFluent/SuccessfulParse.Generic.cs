namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	/// <summary>
	/// A successfully parsed result for a generic verb, where the type of the parsed object is <typeparamref name="TClass"/>.
	/// </summary>
	/// <typeparam name="TClass">The type of the parsed object.</typeparam>
	public sealed class SuccessfulParse<TClass> : IParseResult where TClass : class, new()
	{
		private readonly Verb<TClass> verb;
		public SuccessfulParse(Verb<TClass> verb, TClass obj)
		{
			this.verb = verb;
			Object = obj;
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
		/// The object that was parsed
		/// </summary>
		public TClass Object { get; }
		/// <summary>
		/// Always empty
		/// </summary>
		public IReadOnlyCollection<Error> Errors => Array.Empty<Error>();
		/// <summary>
		/// Calls <see cref="Verb{TClass}.Invoke"/>, passing <see cref="Object"/>.
		/// </summary>
		public void Invoke()
		{
			verb.Invoke(Object);
		}
		/// <summary>
		/// Calls <see cref="Verb{TClass}.InvokeAsync"/>, passing <see cref="Object"/>.
		/// </summary>
		public async Task InvokeAsync()
		{
			await verb.InvokeAsync(Object);
		}
	}
}