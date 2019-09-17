using System;
using System.Collections.Generic;

namespace CommandLineFluent
{
	/// <summary>
	/// The result of parsing some command line arguments
	/// </summary>
	public class FluentParserResult
	{
		/// <summary>
		/// Any errors encountered when parsing
		/// </summary>
		public IReadOnlyCollection<Error> Errors { get; }
		/// <summary>
		/// The verb that was parsed
		/// </summary>
		public IFluentVerb ParsedVerb { get; }
		/// <summary>
		/// Whether or not parsing was successful
		/// </summary>
		public bool Success { get; }
		internal FluentParserResult()
		{
			Success = false;
			Errors = null;
		}
		internal FluentParserResult(IReadOnlyCollection<Error> errors, IFluentVerb parsedVerb)
		{
			Success = (errors?.Count ?? 0) == 0;
			Errors = errors;
			ParsedVerb = parsedVerb;
		}
		/// <summary>
		/// If parsing was successful and the type that was parsed matches the type specified, invokes the provided action.
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
		/// If parsing was successful and the type that was parsed matches the type specified, returns the object that was parsed.
		/// Otherwise, returns the default value for type <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">The type of the parsed class</typeparam>
		public T GetParsedObject<T>() where T : class, new()
		{
			if (Success)
			{
				if (typeof(T) == ParsedVerb.TargetType)
				{
					if (ParsedVerb.Successful == true)
					{
						return ((FluentVerb<T>)ParsedVerb).ParsedObject;
					}
				}
				else
				{
					return default;
				}
			}
			return default;
		}
		/// <summary>
		/// If the arguments failed to be parsed, invokes the callback specified.
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
		/// If the arguments failed to be parsed, invokes the callback and returns null. Allows you to do Parse().StopOnFailure()?.OnSuccess()... to stop the
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
	}
}
