using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommandLineFluent
{
	/// <summary>
	/// The result of parsing some command line arguments.
	/// Designed for invoking awaitable actions. Use this the same way you would the FluentParserResult,
	/// except at the end of the fluent callchain, call .Invoke() or .InvokeAsync() and await the returned Task.
	/// </summary>
	public class FluentParserResultAwaitable : FluentParserResultBase
	{
		internal FluentParserResultAwaitable() : base() { }
		internal FluentParserResultAwaitable(IReadOnlyCollection<Error> errors, IFluentVerb parsedVerb) : base(errors, parsedVerb) { }
		/// <summary>
		/// The Func that will be invoked on calling .Invoke() or .InvokeAsync(), which calls the correct Func supplied by OnSuccess() or OnFailure().
		/// </summary>
		public Func<Task> Action { get; private set; }
		/// <summary>
		/// If parsing was successful and the type that was parsed matches the type specified, then <paramref name="action"/> will
		/// be run upon calling .Invoke() or .InvokeAsync()
		/// </summary>
		/// <typeparam name="T">The type of the parsed class</typeparam>
		public FluentParserResultAwaitable OnSuccess<T>(Func<T, Task> action) where T : class, new()
		{
			if (Success)
			{
				if (typeof(T) == ParsedVerb.TargetType)
				{
					if (ParsedVerb.Successful == true)
					{
						Action = () => action?.Invoke(((FluentVerb<T>)ParsedVerb).ParsedObject);
					}
				}
			}
			return this;
		}
		/// <summary>
		/// If the arguments failed to be parsed, invokes <paramref name="action"/> on calling .Invoke().
		/// Generally you don't need to use this often, the help/usage text can tell the user what they did wrong.
		/// </summary>
		/// <param name="action">The action to take. If null, no action is taken</param>
		public FluentParserResultAwaitable OnFailure(Func<IReadOnlyCollection<Error>, Task> action)
		{
			if (!Success)
			{
				Action = () => action?.Invoke(Errors);
			}
			return this;
		}
		/// <summary>
		/// If the arguments failed to be parsed, returns null. Allows you to do Parse().StopOnFailure()?.OnSuccess() to stop the
		/// fluent callchain immediately when there are errors, saving unnecessary checks.
		/// It's not recommended you use this, because this will make awaiting the Task returned by Invoke() or InvokeAsync() trickier, since
		/// you need to check for null before awaiting.
		/// </summary>
		public FluentParserResultAwaitable StopOnFailure()
		{
			if (!Success)
			{
				return null;
			}
			return this;
		}
		/// <summary>
		/// Invokes the Action specified by the relevant OnSuccess() call, and returns the resultant Task
		/// </summary>
		public Task Invoke()
		{
			return Action?.Invoke() ?? Task.CompletedTask;
		}
		/// <summary>
		/// Invokes the Action specified by the relevant OnSuccess() call, and awaits the resultant Task
		/// </summary>
		public async Task InvokeAsync()
		{
			if (Action != null)
			{
				await Action();
			}
		}
	}
}
