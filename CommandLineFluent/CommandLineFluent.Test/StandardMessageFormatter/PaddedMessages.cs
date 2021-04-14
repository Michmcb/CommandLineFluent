namespace CommandLineFluent.Test.StandardMessageFormatter
{
	using System;
	using System.Text;
	using Xunit;
	using CommandLineFluent;
	using System.Collections.Generic;
	using System.Linq;

	public sealed class WritePaddedKeywordDescriptions
	{
		[Fact]
		public void WrittenLines()
		{
			StringBuilderConsole console = new(60);
			StandardMessageFormatter.WritePaddedKeywordDescriptions(console, ConsoleColor.Gray, new List<KeywordAndDescription>()
			{
				new KeywordAndDescription("Lorem", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."),
			});
			//string str = console.Written.ToString();
		}
		[Fact]
		public void ExplicitNewLinesLineBreaks()
		{
			string lorem = @"Lorem ipsum dolor sit amet,
consectetur adipiscing elit, sed do eiusmod tempor
incididunt ut labore et dolore magna aliqua.
Ut enim ad minim veniam, quis nostrud
exercitation ullamco laboris nisi ut aliquip
ex ea commodo consequat. Duis aute irure
dolor in reprehenderit in voluptate velit
esse cillum dolore eu fugiat nulla pariatur. Excepteur
sint occaecat cupidatat non proident,
sunt in culpa qui officia deserunt mollit
anim id est laborum.";
			IList<(int from, int length)> breaks = StandardMessageFormatter.GetLineBreaks(lorem, 80);
			Assert.Equal(11, breaks.Count);
			List<string> lines = breaks.Select(x => lorem.Substring(x.from, x.length)).ToList();
			Assert.All(lines, line => Assert.True(line.Length <= 80));
		}
		[Fact]
		public void LineBreaks()
		{
			string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
			IList<(int from, int length)> breaks = StandardMessageFormatter.GetLineBreaks(lorem, 40);
			Assert.Equal(12, breaks.Count);
			List<string> lines = breaks.Select(x => lorem.Substring(x.from, x.length)).ToList();
			Assert.All(lines, line => Assert.True(line.Length <= 40));
		}
		[Fact]
		public void LineBreaksButOneIsHuge()
		{
			string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscingelit,seddoeiusmodtemporincididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
			IList<(int from, int length)> breaks = StandardMessageFormatter.GetLineBreaks(lorem, 40);
			Assert.Equal(12, breaks.Count);
			List<string> lines = breaks.Select(x => lorem.Substring(x.from, x.length)).ToList();
			Assert.All(lines, line => Assert.True(line.Length <= 40));
		}
		[Fact]
		public void NoSpacesStillLineBreaks()
		{
			string lorem = "Loremipsumdolorsitamet,consecteturadipiscingelit,seddoeiusmodtemporincididuntutlaboreetdoloremagnaaliqua.Utenimadminimveniam,quisnostrudexercitationullamcolaborisnisiutaliquipexeacommodoconsequat.Duisauteiruredolorinreprehenderitinvoluptatevelitessecillumdoloreeufugiatnullapariatur.Excepteursintoccaecatcupidatatnonproident,suntinculpaquiofficiadeseruntmollitanimidestlaborum.";
			IList<(int from, int length)> breaks = StandardMessageFormatter.GetLineBreaks(lorem, 40);
			List<string> lines = breaks.Select(x => lorem.Substring(x.from, x.length)).ToList();
			Assert.All(lines, line => Assert.True(line.Length <= 40));
			Assert.Equal(lorem, string.Concat(lines));
		}
	}
	public sealed class StringBuilderConsole : IConsole
	{
		public StringBuilderConsole(int currentWidth)
		{
			CurrentWidth = currentWidth;
			Written = new StringBuilder();
		}
		public int CurrentWidth { get; set; }
		public ConsoleColor BackgroundColor { get; set;}
		public ConsoleColor ForegroundColor { get; set; }
		public string ReadLine()
		{
			return "";
		}
		public StringBuilder Written { get; }
		public void Write(string s)
		{
			Written.Append(s);
		}
		public void WriteLine(string s)
		{
			Written.AppendLine(s);
		}
		public void WriteLine()
		{
			Written.AppendLine();
		}
		public void Write(char c)
		{
			Written.Append(c);
		}
		public void WriteLine(char c)
		{
			Written.Append(c);
		}
	}
}
