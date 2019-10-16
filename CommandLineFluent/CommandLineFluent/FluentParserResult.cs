using System;
using System.Collections.Generic;

namespace CommandLineFluent
{
	/// <summary>
	/// The result of parsing some command line arguments
	/// </summary>
	public class FluentParserResult : FluentParserResultBase
	{
		internal FluentParserResult() : base() { }
		internal FluentParserResult(IReadOnlyCollection<Error> errors, IFluentVerb parsedVerb) : base(errors, parsedVerb) { }
		/// <summary>
		/// If parsing was successful and the type that was parsed matches the type specified, invokes <paramref name="action"/>.
		/// </summary>
		/// <typeparam name="T">The type of the parsed class</typeparam>
		public FluentParserResult OnSuccess<T>(Action<T> action) where T : class, new()
		{
			if (Success)
			{
				if (typeof(T) == ParsedVerb.TargetType)
				{
					if (ParsedVerb.Successful == true)
					{
						action?.Invoke(((FluentVerb<T>)ParsedVerb).ParsedObject);
					}
				}
			}
			return this;
		}
		/// <summary>
		/// If the arguments failed to be parsed, returns null. Allows you to do Parse().StopOnFailure()?.OnSuccess() to stop the
		/// fluent callchain immediately when there are errors, saving unnecessary checks.
		/// </summary>
		public FluentParserResult StopOnFailure()
		{
			if (!Success)
			{
				return null;
			}
			return this;
		}
		/// <summary>
		/// If the arguments failed to be parsed, invokes <paramref name="action"/>.
		/// Generally you don't need to use this often, the help/usage text can tell the user what they did wrong.
		/// </summary>
		/// <param name="action">The action to take. If null, no action is taken</param>
		public FluentParserResult OnFailure(Action<IReadOnlyCollection<Error>> action)
		{
			if (!Success)
			{
				action?.Invoke(Errors);
			}
			return this;
		}
		/// <summary>
		/// If the arguments failed to be parsed, invokes <paramref name="action"/> and returns null. Allows you to do Parse().OnFailureAndStop()?.OnSuccess()... to stop the
		/// fluent callchain immediately when there are errors, saving some unnecessary checks.
		/// Generally you don't need to use this often, the help/usage text can tell the user what they did wrong.
		/// </summary>
		/// <param name="action">The action to take. If null, no action is taken</param>
		public FluentParserResult OnFailureAndStop(Action<IReadOnlyCollection<Error>> action)
		{
			if (!Success)
			{
				action?.Invoke(Errors);
				return null;
			}
			return this;
		}
	}
}
