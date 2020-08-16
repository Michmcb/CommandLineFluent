namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Threading.Tasks;
	/// <summary>
	/// Parses arguments into classes.
	/// Create this class using a CliParserBuilder.
	/// </summary>
	public sealed class CliParser
	{
		private readonly Dictionary<string, IVerb> verbs;
		private readonly CliParserConfig config;
		/// <summary>
		/// Create this class using a CliParserBuilder.
		/// </summary>
		/// <param name="console"></param>
		/// <param name="tokenizer"></param>
		/// <param name="msgFormatter"></param>
		/// <param name="verbs"></param>
		/// <param name="config"></param>
		internal CliParser([DisallowNull] IConsole console, [DisallowNull] ITokenizer tokenizer, [DisallowNull] IMessageFormatter msgFormatter, Dictionary<string, IVerb> verbs, CliParserConfig config)
		{
			Console = console;
			Tokenizer = tokenizer;
			MsgFormatter = msgFormatter;
			this.verbs = verbs;
			this.config = config;
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
		/// Starts a loop that reads input from <see cref="Console"/>, splits it into tokens using <see cref="Tokenizer"/>, and then parses and invokes it synchronously (using <see cref="Handle(IParseResult)"/>).
		/// Writes <paramref name="prompt"/> to the console as a prompt when it is ready for input, and will stop looping once it encounters the string <paramref name="exitKeyword"/>.
		/// Compares with <paramref name="exitKeyword"/> using <see cref="CliParserConfig.StringComparer"/>.
		/// </summary>
		/// <param name="prompt">The prompt to write, e.g. MyShell>.</param>
		/// <param name="exitKeyword">The keyword to use to stop the loop. Cannot be the same as a Verb name.</param>
		/// <param name="color">The foreground color the console is set to just before writing the prompt.</param>
		public void Shell(string prompt, string exitKeyword = "exit", ConsoleColor color = ConsoleColor.Gray)
		{
			if (verbs.ContainsKey(exitKeyword))
			{
				throw new ArgumentException("The keyword used to exit the loop is already used by a verb: " + exitKeyword, nameof(exitKeyword));
			}
			while (true)
			{
				Console.ForegroundColor = color;
				Console.Write(prompt);
				string line = Console.ReadLine();
				if (!config.StringComparer.Equals(exitKeyword, line))
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
		/// Starts a loop that reads input from <see cref="Console"/>, splits it into tokens using <see cref="Tokenizer"/>, and then parses and invokes it synchronously (using <see cref="HandleAsync(IParseResult)"/>).
		/// Writes <paramref name="prompt"/> to the console as a prompt when it is ready for input, and will stop looping once it encounters the string <paramref name="exitKeyword"/>.
		/// Compares with <paramref name="exitKeyword"/> using <see cref="CliParserConfig.StringComparer"/>.
		/// </summary>
		/// <param name="prompt">The prompt to write, e.g. MyShell>.</param>
		/// <param name="exitKeyword">The keyword to use to stop the loop. Cannot be the same as a Verb name.</param>
		/// <param name="color">The foreground color the console is set to just before writing the prompt.</param>
		public async Task ShellAsync(string prompt, string exitKeyword = "exit", ConsoleColor color = ConsoleColor.Gray)
		{
			if (verbs.ContainsKey(exitKeyword))
			{
				throw new ArgumentException("The keyword used to exit the loop is already used by a verb: " + exitKeyword, nameof(exitKeyword));
			}
			while (true)
			{
				Console.ForegroundColor = color;
				Console.Write(prompt);
				string line = Console.ReadLine();
				if (!config.StringComparer.Equals(exitKeyword, line))
				{
					await HandleAsync(Parse(line));
				}
				else
				{
					break;
				}
			}
		}
		/// <summary>
		/// Parses the provided args, after splitting them into tokens using <see cref="Tokenizer"/>, and turns them into a class.
		/// </summary>
		/// <param name="args">The args to split into tokens and parse.</param>
		/// <returns>An IParseResult. You can call <see cref="Handle(IParseResult)"/>, to handle it automatically.</returns>
		public IParseResult Parse(string args)
		{
			if (args == null)
			{
				throw new ArgumentNullException(nameof(args));
			}
			return Parse(Tokenizer.Tokenize(args));
		}
		/// <summary>
		/// Parses the provided args and turns them into a class.
		/// </summary>
		/// <param name="args">The args to parse.</param>
		/// <returns>An IParseResult. You can call <see cref="Handle(IParseResult)"/>, to handle it automatically.</returns>
		public IParseResult Parse(IEnumerable<string> args)
		{
			if (args == null)
			{
				throw new ArgumentNullException(nameof(args));
			}
			string? firstArg = args.FirstOrDefault();
			if (firstArg != null)
			{
				if (verbs.TryGetValue(firstArg, out IVerb? verb))
				{
					return verb.Parse(args.Skip(1));
				}
				// If the verb isn't found, it might be just the help switch
				else if (firstArg.Equals(config.ShortHelpSwitch, StringComparison.OrdinalIgnoreCase) || firstArg.Equals(config.LongHelpSwitch, StringComparison.OrdinalIgnoreCase))
				{
					return new FailedParseNoVerb(new Error[] { new Error(ErrorCode.HelpRequested, "") });
				}
				return new FailedParseNoVerb(new Error[] { new Error(ErrorCode.InvalidVerb, "The verb provided was not valid: " + firstArg) });
			}
			return new FailedParseNoVerb(new Error[] { new Error(ErrorCode.NoVerbFound, "No input") });
		}
		/// <summary>
		/// Handles <paramref name="result"/>. If parsing was successful, calls <see cref="IParseResult.Invoke"/>.
		/// If parsing failed, any errors are written using <see cref="MsgFormatter"/>. Then, help is written. If a verb and a help switch was specified, specific
		/// help for that verb is written. Otherwise, overall help is written.
		/// </summary>
		/// <param name="result">The result to handle.</param>
		public void Handle(IParseResult result)
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
				if (result.Verb != null)
				{
					result.Verb.WriteSpecificHelp(Console, MsgFormatter);
				}
				else
				{
					MsgFormatter.WriteOverallHelp(Console, verbs.Values, config);
				}
			}
		}
		/// <summary>
		/// Handles <paramref name="result"/>. If parsing was successful, calls <see cref="IParseResult.InvokeAsync"/>.
		/// If parsing failed, any errors are written using <see cref="MsgFormatter"/>. Then, help is written. If a verb and a help switch was specified, specific
		/// help for that verb is written. Otherwise, overall help is written.
		/// </summary>
		/// <param name="result">The result to handle.</param>
		public async Task HandleAsync(IParseResult result)
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
				if (result.Verb != null)
				{
					result.Verb.WriteSpecificHelp(Console, MsgFormatter);
				}
				else
				{
					MsgFormatter.WriteOverallHelp(Console, verbs.Values, config);
				}
			}
		}
		/// <summary>
		/// Writes the provided Errors using <see cref="IMessageFormatter.WriteErrors(IConsole, IEnumerable{Error})"/>.
		/// </summary>
		/// <param name="errors">The errors to write.</param>
		public void WriteErrors(IEnumerable<Error> errors)
		{
			MsgFormatter.WriteErrors(Console, errors);
		}
		/// <summary>
		/// Writes overall help, i.e. using <see cref="IMessageFormatter.WriteOverallHelp(IConsole, IReadOnlyCollection{IVerb}, CliParserConfig)"/>.
		/// </summary>
		public void WriteOverallHelp()
		{
			MsgFormatter.WriteOverallHelp(Console, verbs.Values, config);
		}
		/// <summary>
		/// Writes help for a specific verb; i.e. using <see cref="IMessageFormatter.WriteSpecificHelp{TClass}(IConsole, Verb{TClass}, CliParserConfig)"/>.
		/// </summary>
		/// <param name="verb">The verb to write detailed usage/help for.</param>
		public void WriteHelp(IVerb verb)
		{
			verb.WriteSpecificHelp(Console, MsgFormatter);
		}
	}
}
