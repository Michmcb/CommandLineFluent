using System.Collections.Generic;

namespace CommandLineFluent
{
	public interface IMessageFormatter
	{
		string FormatErrors(IEnumerable<Error> errors);
		void WriteOverallHelp(IConsole console, System.Collections.Generic.IReadOnlyCollection<IVerb> verbs, CliParserConfig config);
		void WriteSpecificHelp<TClass>(IConsole console, Verb<TClass> verb, CliParserConfig config) where TClass : class, new();
	}
}