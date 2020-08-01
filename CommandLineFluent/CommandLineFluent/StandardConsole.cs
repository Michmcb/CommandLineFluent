﻿namespace CommandLineFluent
{
	using System;

	public sealed class StandardConsole : IConsole
	{
		public StandardConsole() { }
		public void Write(string s)
		{
			Console.Write(s);
		}
		public void WriteLine(string s)
		{
			Console.WriteLine(s);
		}
	}
}