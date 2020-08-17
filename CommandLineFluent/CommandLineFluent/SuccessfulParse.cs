namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public sealed class SuccessfulParse<TClass> : IParseResult where TClass : class, new()
	{
		private readonly Verb<TClass> verb;
		public SuccessfulParse(Verb<TClass> verb, TClass opt)
		{
			this.verb = verb;
			this.Object = opt;
		}
		public bool Ok => true;
		public IVerb? Verb => verb;
		public TClass Object { get; }
		public IReadOnlyCollection<Error> Errors => Array.Empty<Error>();
		public void Invoke()
		{
			verb.Invoke(Object);
		}
		public async Task InvokeAsync()
		{
			await verb.InvokeAsync(Object);
		}
	}
}