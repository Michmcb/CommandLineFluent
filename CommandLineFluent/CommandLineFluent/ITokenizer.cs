namespace CommandLineFluent
{
	using System.Collections.Generic;
	public interface ITokenizer
	{
		ICollection<string> Tokenize(string args);
	}
}