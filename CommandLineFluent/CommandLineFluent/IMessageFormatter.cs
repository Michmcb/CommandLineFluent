using System.Collections.Generic;

namespace CommandLineFluent
{
	public interface IMessageFormatter
	{
		void WriteErrors(IConsole console, IEnumerable<Error> errors);
		void WriteOverallHelp(IConsole console, IReadOnlyCollection<IVerb> verbs, CliParserConfig config);
		void WriteSpecificHelp<TClass>(IConsole console, Verb<TClass> verb, CliParserConfig config) where TClass : class, new();
	}
}