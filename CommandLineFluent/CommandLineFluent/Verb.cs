namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	public sealed class Verb : IVerb
	{
		internal Verb(string? shortName, string longName)
		{
			ShortName = shortName;
			LongName = longName;
		}
		public string? ShortName { get; }
		public string LongName { get; }
		public string HelpText { get; set; }
		/// <summary>
		/// The action that's invoked when parsing is successful and this verb was provided.
		/// </summary>
		public Action Invoke { get; set; }
		/// <summary>
		/// The asynchronous action that's invoked when parsing is successful and this verb was provided.
		/// </summary>
		public Func<Task> InvokeAsync { get; set; }
		public IParseResult Parse(IEnumerable<string> args)
		{
			if (args.Any())
			{
				return new FailedParseWithVerb(this, new Error[] { new Error(ErrorCode.UnexpectedArgument, "This verb does not take any arguments") });
			}
			return new SuccessfulParse(this);
		}
		public string ShortAndLongName()
		{
			return ArgUtils.ShortAndLongName(ShortName, LongName);
		}
		public void WriteSpecificHelp(IConsole console, IMessageFormatter msgFormatter)
		{
			console.WriteLine(HelpText);
		}
	}
}
