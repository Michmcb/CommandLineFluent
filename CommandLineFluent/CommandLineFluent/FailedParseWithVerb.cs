﻿namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public sealed class FailedParseWithVerb : IParseResult
	{
		private readonly Verb? verb;
		public FailedParseWithVerb(Verb? verb, IReadOnlyCollection<Error> errors)
		{
			this.verb = verb;
			Errors = errors;
		}
		public IReadOnlyCollection<Error> Errors { get; }
		public bool Ok => false;
		public IVerb? Verb => verb;
		public void Invoke()
		{
			throw new InvalidOperationException("Parsing failed; cannot invoke");
		}
		public Task InvokeAsync()
		{
			throw new InvalidOperationException("Parsing failed; cannot invoke");
		}
	}
}