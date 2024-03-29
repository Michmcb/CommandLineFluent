﻿namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	/// <summary>
	/// Parses arguments into classes.
	/// Create this class using a CliParserBuilder.
	/// </summary>
	public sealed class CliParser
	{
		private readonly Dictionary<string, IVerb> verbsByName;

		/// <summary>
		/// Create this class using a CliParserBuilder.
		/// </summary>
		internal CliParser(IConsole console, ITokenizer tokenizer, IMessageFormatter msgFormatter, Dictionary<string, IVerb> verbsByName, List<IVerb> verbs, CliParserConfig config)
		{
			Console = console;
			Tokenizer = tokenizer;
			MsgFormatter = msgFormatter;
			this.verbsByName = verbsByName;
			Verbs = verbs;
			Config = config;
		}
		/// <summary>
		/// Used to write to the console. Mostly just exists to simplify unit testing.
		/// </summary>
		public IConsole Console { get; }
		/// <summary>
		/// Only used if you call <see cref="Parse(string)"/>. It splits up the string into arguments.
		/// </summary>
		public ITokenizer Tokenizer { get; }
		/// <summary>
		/// Used to create messages for the user.
		/// </summary>
		public IMessageFormatter MsgFormatter { get; }
		/// <summary>
		/// All verbs that have been added to this.
		/// </summary>
		public IReadOnlyCollection<IVerb> Verbs { get; }
		/// <summary>
		/// The configuration that this CliParser has been created with.
		/// </summary>
		public CliParserConfig Config { get; }
		/// <summary>
		/// Parses the provided args, after splitting them into tokens using <see cref="Tokenizer"/>, and turns them into a class.
		/// </summary>
		/// <param name="args">The args to split into tokens and parse.</param>
		/// <returns>An IParseResult. You can call <see cref="Handle(IParseResult, bool)"/>, to handle it automatically.</returns>
		public IParseResult Parse(string args)
		{
			if (args == null)
			{
				return new FailedParseNoVerb(new Error[] { new Error(ErrorCode.NoVerbFound, "No input") });
			}
			IEnumerable<string> tokens = Tokenizer.Tokenize(args);
			using IEnumerator<string> e = tokens.GetEnumerator();
			return Parse(e);
		}
		/// <summary>
		/// Parses the provided args and turns them into a class.
		/// </summary>
		/// <param name="args">The args to parse.</param>
		/// <returns>An IParseResult. You can call <see cref="Handle(IParseResult, bool)"/>, to handle it automatically.</returns>
		public IParseResult Parse(IEnumerable<string> args)
		{
			if (args == null)
			{
				return new FailedParseNoVerb(new Error[] { new Error(ErrorCode.NoVerbFound, "No input") });
			}
			using IEnumerator<string> e = args.GetEnumerator();
			return Parse(e);
		}
		/// <summary>
		/// Parses the provided args and turns them into a class.
		/// </summary>
		/// <param name="argsEnum">An enumerator providing the args to parse.</param>
		/// <returns>An IParseResult. You can call <see cref="Handle(IParseResult, bool)"/>, to handle it automatically.</returns>
		public IParseResult Parse(IEnumerator<string> argsEnum)
		{
			if (argsEnum.MoveNext())
			{
				string? firstArg = argsEnum.Current;
				if (firstArg != null)
				{
					if (verbsByName.TryGetValue(firstArg, out IVerb? verb))
					{
						return verb.Parse(argsEnum);
					}
					// If the verb isn't found, it might be just the help switch
					else if (Config.StringComparer.Equals(firstArg, Config.ShortHelpSwitch) || Config.StringComparer.Equals(firstArg, Config.LongHelpSwitch))
					{
						return new FailedParseNoVerb(new Error[] { new Error(ErrorCode.HelpRequested, string.Empty) });
					}
					return new FailedParseNoVerb(new Error[] { new Error(ErrorCode.InvalidVerb, "The verb provided was not valid: " + firstArg) });
				}
			}
			return new FailedParseNoVerb(new Error[] { new Error(ErrorCode.NoVerbFound, "No input") });
		}
		/// <summary>
		/// Handles <paramref name="result"/>. If parsing was successful, calls <see cref="IParseResult.Invoke"/>.
		/// If parsing failed, any errors are written using <see cref="MsgFormatter"/>.
		/// </summary>
		/// <param name="result">The result to handle.</param>
		/// <param name="alwaysWriteHelpOnError">Always writes help when there is an error. If false, only writes help if an Error was <see cref="ErrorCode.HelpRequested"/>.</param>
		public void Handle(IParseResult result, bool alwaysWriteHelpOnError = true)
		{
			if (result.Ok)
			{
				result.Invoke();
			}
			else
			{
				if (result.Errors.Count > 0)
				{
					MsgFormatter.WriteErrors(Console, result.Errors);
				}
				if (alwaysWriteHelpOnError || result.Errors.Any(x => x.ErrorCode == ErrorCode.HelpRequested))
				{
					if (result.Verb != null)
					{
						result.Verb.WriteSpecificHelp(Console, MsgFormatter);
					}
					else
					{
						MsgFormatter.WriteOverallHelp(Console, Verbs, Config);
					}
				}
			}
		}
		/// <summary>
		/// Handles <paramref name="result"/>. If parsing was successful, calls <see cref="IParseResult.InvokeAsync"/>.
		/// If parsing failed, any errors are written using <see cref="MsgFormatter"/>.
		/// </summary>
		/// <param name="result">The result to handle.</param>
		/// <param name="alwaysWriteHelpOnError">Always writes help when there is an error. If false, only writes help if an Error was <see cref="ErrorCode.HelpRequested"/>.</param>
		public async Task HandleAsync(IParseResult result, bool alwaysWriteHelpOnError = true)
		{
			if (result.Ok)
			{
				await result.InvokeAsync();
			}
			else
			{
				if (result.Errors.Count > 0)
				{
					MsgFormatter.WriteErrors(Console, result.Errors);
				}
				if (alwaysWriteHelpOnError || result.Errors.Any(x => x.ErrorCode == ErrorCode.HelpRequested))
				{
					if (result.Verb != null)
					{
						result.Verb.WriteSpecificHelp(Console, MsgFormatter);
					}
					else
					{
						MsgFormatter.WriteOverallHelp(Console, Verbs, Config);
					}
				}
			}
		}
		/// <summary>
		/// Starts a loop that reads input from <see cref="Console"/>, parses (using <see cref="Parse(string)"/>), and invokes it synchronously (using <see cref="Handle(IParseResult, bool)"/>).
		/// Writes <paramref name="prompt"/> to the console as a prompt when it is ready for input, and will stop looping once <see cref="ILoopCondition.ShouldGo(string)"/> returns false.
		/// Any null strings received from inputs are not parsed, but still checked using <paramref name="condition"/>.
		/// </summary>
		/// <param name="prompt">The prompt to write, e.g. MyShell>.</param>
		/// <param name="condition">Loops based on this condition.</param>
		/// <param name="alwaysWriteHelpOnError">Always writes help when there is an error. If false, only writes help if an Error was <see cref="ErrorCode.HelpRequested"/>.</param>
		/// <param name="promptColor">The foreground color of <paramref name="prompt"/>.</param>
		/// <param name="commandColor">The foreground color of the text that the user enters after <paramref name="prompt"/> is written.</param>
		public void InputLoop(string prompt, ILoopCondition condition, bool alwaysWriteHelpOnError = true, ConsoleColor promptColor = ConsoleColor.White, ConsoleColor commandColor = ConsoleColor.Gray)
		{
			while (true)
			{
				Console.ForegroundColor = promptColor;
				Console.Write(prompt);
				Console.ForegroundColor = commandColor;
				string? line = Console.ReadLine();
				if (!condition.ShouldGo(line)) break;
				if (line != null)
				{
					Handle(Parse(line), alwaysWriteHelpOnError);
				}
			}
		}
		/// <summary>
		/// Starts a loop that reads input from <see cref="Console"/>, parses (using <see cref="Parse(string)"/>), and invokes it asynchronously (using <see cref="HandleAsync(IParseResult, bool)"/>).
		/// Writes <paramref name="prompt"/> to the console as a prompt when it is ready for input, and will stop looping once <see cref="ILoopCondition.ShouldGo(string)"/> returns false.
		/// Any null strings received from inputs are not parsed, but still checked using <paramref name="condition"/>.
		/// </summary>
		/// <param name="prompt">The prompt to write, e.g. MyShell>.</param>
		/// <param name="condition">Loops based on this condition.</param>
		/// <param name="alwaysWriteHelpOnError">Always writes help when there is an error. If false, only writes help if an Error was <see cref="ErrorCode.HelpRequested"/>.</param>
		/// <param name="promptColor">The foreground color of <paramref name="prompt"/>.</param>
		/// <param name="commandColor">The foreground color of the text that the user enters after <paramref name="prompt"/> is written.</param>
		public async Task InputLoopAsync(string prompt, ILoopCondition condition, bool alwaysWriteHelpOnError = true, ConsoleColor promptColor = ConsoleColor.White, ConsoleColor commandColor = ConsoleColor.Gray)
		{
			while (true)
			{
				Console.ForegroundColor = promptColor;
				Console.Write(prompt);
				Console.ForegroundColor = commandColor;
				string? line = Console.ReadLine();
				if (!condition.ShouldGo(line)) break;
				if (line != null)
				{
					await HandleAsync(Parse(line), alwaysWriteHelpOnError);
				}
			}
		}
		/// <summary>
		/// Starts a loop that reads input from <see cref="Console"/>, splits it into tokens using <see cref="Tokenizer"/>, and then parses and invokes it synchronously (using <see cref="Handle(IParseResult, bool)"/>).
		/// Writes <paramref name="prompt"/> to the console as a prompt when it is ready for input, and will stop looping once it encounters the string <paramref name="exitKeyword"/>, or <see cref="Console.ReadLine"/> returns null.
		/// Compares with <paramref name="exitKeyword"/> using <see cref="CliParserConfig.StringComparer"/>.
		/// </summary>
		/// <param name="prompt">The prompt to write, e.g. MyShell>.</param>
		/// <param name="exitKeyword">The keyword to use to stop the loop. Cannot be the same as a Verb name.</param>
		/// <param name="promptColor">The foreground color of <paramref name="prompt"/>.</param>
		/// <param name="commandColor">The foreground color of the text that the user enters after <paramref name="prompt"/> is written.</param>
		[Obsolete("Prefer using InputLoop instead")]
		public void Shell(string prompt, string exitKeyword = "exit", ConsoleColor promptColor = ConsoleColor.White, ConsoleColor commandColor = ConsoleColor.Gray)
		{
			if (verbsByName.ContainsKey(exitKeyword))
			{
				throw new ArgumentException("The keyword used to exit the loop is already used by a verb: " + exitKeyword, nameof(exitKeyword));
			}
			while (true)
			{
				Console.ForegroundColor = promptColor;
				Console.Write(prompt);
				Console.ForegroundColor = commandColor;
				string? line = Console.ReadLine();
				if (line != null && !Config.StringComparer.Equals(exitKeyword, line))
				{
					Handle(Parse(line));
				}
				else
				{
					break;
				}
			}
		}
		/// <summary>
		/// Starts a loop that reads input from <see cref="Console"/>, splits it into tokens using <see cref="Tokenizer"/>, and then parses and invokes it asynchronously (using <see cref="HandleAsync(IParseResult, bool)"/>).
		/// Writes <paramref name="prompt"/> to the console as a prompt when it is ready for input, and will stop looping once it encounters the string <paramref name="exitKeyword"/>, or <see cref="Console.ReadLine"/> returns null.
		/// Compares with <paramref name="exitKeyword"/> using <see cref="CliParserConfig.StringComparer"/>.
		/// </summary>
		/// <param name="prompt">The prompt to write, e.g. MyShell>.</param>
		/// <param name="exitKeyword">The keyword to use to stop the loop. Cannot be the same as a Verb name.</param>
		/// <param name="promptColor">The foreground color of <paramref name="prompt"/>.</param>
		/// <param name="commandColor">The foreground color of the text that the user enters after <paramref name="prompt"/> is written.</param>
		[Obsolete("Prefer using InputLoopAsync instead")]
		public async Task ShellAsync(string prompt, string exitKeyword = "exit", ConsoleColor promptColor = ConsoleColor.White, ConsoleColor commandColor = ConsoleColor.Gray)
		{
			if (verbsByName.ContainsKey(exitKeyword))
			{
				throw new ArgumentException("The keyword used to exit the loop is already used by a verb: " + exitKeyword, nameof(exitKeyword));
			}
			while (true)
			{
				Console.ForegroundColor = promptColor;
				Console.Write(prompt);
				Console.ForegroundColor = commandColor;
				string? line = Console.ReadLine();
				if (line != null && !Config.StringComparer.Equals(exitKeyword, line))
				{
					await HandleAsync(Parse(line));
				}
				else
				{
					break;
				}
			}
		}
	}
}
