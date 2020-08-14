namespace CommandLineFluent
{
	using System;
	using System.IO;

	public sealed class StandardConsole : IConsole
	{
		public StandardConsole(int defaultWidth = 80)
		{
			DefaultWidth = defaultWidth;
		}
		public int DefaultWidth { get; set; }
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
		public ConsoleColor BackgroundColor { get => Console.BackgroundColor; set => Console.BackgroundColor = value; }
		public ConsoleColor ForegroundColor { get => Console.ForegroundColor; set => Console.ForegroundColor = value; }
		public void Write(string s)
		{
			Console.Write(s);
		}
		public void WriteLine(string s)
		{
			Console.WriteLine(s);
		}
		public void WriteLine()
		{
			Console.WriteLine();
		}
	}
}