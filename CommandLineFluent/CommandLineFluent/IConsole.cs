namespace CommandLineFluent
{
	using System;
	/// <summary>
	/// A wrapper for <see cref="Console"/>. Mainly useful for unit testing.
	/// </summary>
	public interface IConsole
	{
		/// <summary>
		/// The width of the console window.
		/// </summary>
		int CurrentWidth { get; }
		/// <summary>
		/// Gets or sets the background color.
		/// </summary>
		ConsoleColor BackgroundColor { get; set; }
		/// <summary>
		/// Gets or sets the foreground color.
		/// </summary>
		ConsoleColor ForegroundColor { get; set; }
		/// <summary>
		/// Reads a line of characters from the input.
		/// </summary>
		/// <returns>The next line of characters, or null if the end has been reached.</returns>
		string? ReadLine();
		/// <summary>
		/// Writes the line terminator.
		/// </summary>
		void WriteLine();
		/// <summary>
		/// Writes <paramref name="c"/>.
		/// </summary>
		/// <param name="c">The char to write</param>
		void Write(char c);
		/// <summary>
		/// Writes <paramref name="str"/>.
		/// </summary>
		/// <param name="str">The string to write</param>
		void Write(string str);
		/// <summary>
		/// Writes <paramref name="c"/>, followed by the line terminator.
		/// </summary>
		/// <param name="c">The char to write</param>
		void WriteLine(char c);
		/// <summary>
		/// Writes <paramref name="str"/>, followed by the line terminator.
		/// </summary>
		/// <param name="str">The string to write</param>
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