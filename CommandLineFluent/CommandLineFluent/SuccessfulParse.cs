﻿namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public sealed class SuccessfulParse : IParseResult
	{
		private readonly Verb verb;
		public SuccessfulParse(Verb verb)
		{
			this.verb = verb;
		}
		public bool Ok => true;
		public IVerb? Verb => verb;
		public IReadOnlyCollection<Error> Errors => Array.Empty<Error>();
		public void Invoke()
		{
			verb.Invoke();
		}
		public async Task InvokeAsync()
		{
			await verb.InvokeAsync();
		}
	}
}