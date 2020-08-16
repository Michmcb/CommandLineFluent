namespace CommandLineFluent
{
	using System;
	public interface IConsole
	{
		int CurrentWidth { get; }
		ConsoleColor BackgroundColor { get; set; }
		ConsoleColor ForegroundColor { get; set; }
		void Write(string s);
		void WriteLine(string s);
		void WriteLine();
		string ReadLine();
	}
}