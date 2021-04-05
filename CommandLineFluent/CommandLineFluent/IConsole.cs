namespace CommandLineFluent
{
	using System;
	/// <summary>
	/// A wrapper for <see cref="Console"/>. Mainly useful for unit testing.
	/// </summary>
	public interface IConsole
	{
		int CurrentWidth { get; }
		ConsoleColor BackgroundColor { get; set; }
		ConsoleColor ForegroundColor { get; set; }
		string? ReadLine();
		void WriteLine();
		void Write(char c);
		void Write(string str);
		void WriteLine(char c);
		void WriteLine(string str);
#if NETSTANDARD2_0
		void Write(ConsoleColor color, string str);
		void WriteLine(ConsoleColor color, string str);
#else
		void Write(ConsoleColor color, string str)
		{
			ForegroundColor = color;
			Write(str);
		}
		void WriteLine(ConsoleColor color, string str)
		{
			ForegroundColor = color;
			WriteLine(str);
		}
#endif
	}
}