namespace CommandLineFluent
{
	using System;
	using System.IO;
	/// <summary>
	/// This class simply calls <see cref="Console"/> methods.
	/// </summary>
	public sealed class StandardConsole : IConsole
	{
		public StandardConsole(int defaultWidth = 80)
		{
			DefaultWidth = defaultWidth;
		}
		/// <summary>
		/// The default width to use, if the CurrentWidth can't be ascertained.
		/// </summary>
		public int DefaultWidth { get; set; }
		/// <summary>
		/// Returns <see cref="Console.WindowWidth"/>. If that throws an exception, uses <see cref="DefaultWidth"/> instead.
		/// </summary>
		public int CurrentWidth
		{
			get
			{
				try
				{
					return Console.WindowWidth;
				}
				catch (IOException) { }
				catch (PlatformNotSupportedException) { }
				return DefaultWidth;
			}
		}
		/// <summary>
		/// Gets or sets <see cref="Console.BackgroundColor"/>.
		/// </summary>
		public ConsoleColor BackgroundColor { get => Console.BackgroundColor; set => Console.BackgroundColor = value; }
		/// <summary>
		/// Gets or sets <see cref="Console.ForegroundColor"/>.
		/// </summary>
		public ConsoleColor ForegroundColor { get => Console.ForegroundColor; set => Console.ForegroundColor = value; }
		public string? ReadLine()
		{
			return Console.ReadLine();
		}
		public void WriteLine()
		{
			Console.WriteLine();
		}
		public void Write(char c)
		{
			Console.Write(c);
		}
		public void Write(string str)
		{
			Console.Write(str);
		}
		public void Write(ConsoleColor color, string str)
		{
			ForegroundColor = color;
			Console.Write(str);
		}
		public void WriteLine(char c)
		{
			Console.WriteLine(c);
		}
		public void WriteLine(string str)
		{
			Console.WriteLine(str);
		}
		public void WriteLine(ConsoleColor color, string str)
		{
			ForegroundColor = color;
			Console.WriteLine(str);
		}
	}
}