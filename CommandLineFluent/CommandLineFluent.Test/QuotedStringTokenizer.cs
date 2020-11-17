namespace CommandLineFluent.Test
{
	using System.Collections.Generic;
	using System.Linq;
	using Xunit;

	public sealed class Tokenize
	{
		[Fact]
		public void NoQuotes()
		{
			QuotedStringTokenizer tok = new ();
			Assert.Collection(tok.Tokenize("String1 String2 String 3 'String4' 'String 5' \"String6\" \"String 7\" `String8` `String 9`"),
				x => Assert.Equal("String1", x),
				x => Assert.Equal("String2", x),
				x => Assert.Equal("String", x),
				x => Assert.Equal("3", x),
				x => Assert.Equal("String4", x),
				x => Assert.Equal("String 5", x),
				x => Assert.Equal("String6", x),
				x => Assert.Equal("String 7", x),
				x => Assert.Equal("String8", x),
				x => Assert.Equal("String 9", x));
		}
		[Fact]
		public void ImplicitEndQuote()
		{
			QuotedStringTokenizer tok = new();
			Assert.Collection(tok.Tokenize("String1 \"String 2"),
				x => Assert.Equal("String1", x),
				x => Assert.Equal("String 2", x));

			Assert.Collection(tok.Tokenize("String1 'String 2"),
				x => Assert.Equal("String1", x),
				x => Assert.Equal("String 2", x));

			Assert.Collection(tok.Tokenize("String1 `String 2"),
				x => Assert.Equal("String1", x),
				x => Assert.Equal("String 2", x));
		}
		[Fact]
		public void NullEmptyWhitespaceString_NoTokens()
		{
			QuotedStringTokenizer tok = new();
			Assert.Empty(tok.Tokenize(null!));
			Assert.Empty(tok.Tokenize(""));
			Assert.Empty(tok.Tokenize("    "));
		}
		[Fact]
		public void EmptyToken()
		{
			QuotedStringTokenizer tok = new();
			List<string> tokens = tok.Tokenize("\"\"").ToList();
			Assert.Single(tokens);
			Assert.Empty(tokens[0]);
		}
	}
}
