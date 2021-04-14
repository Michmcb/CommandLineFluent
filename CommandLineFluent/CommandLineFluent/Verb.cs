namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	public sealed class Verb : IVerb
	{
		private readonly List<IVerb> allVerbs;
		private readonly Dictionary<string, IVerb> verbsByName;
		private readonly CliParserConfig config;
		internal Verb(string? shortName, string longName, string? parentShortAndLongName, CliParserConfig config)
		{
			ShortName = shortName;
			LongName = longName;
			ShortAndLongName = ArgUtils.ShortAndLongName(shortName, longName);
			FullShortAndLongName = parentShortAndLongName == null ? ArgUtils.ShortAndLongName(shortName, longName) : string.Concat(parentShortAndLongName, ' ', ArgUtils.ShortAndLongName(shortName, longName) );
			Invoke = () => throw new CliParserBuilderException(string.Concat("Invoke for verb ", FullShortAndLongName, " has not been configured"));
			InvokeAsync = () => throw new CliParserBuilderException(string.Concat("InvokeAsync for verb ", FullShortAndLongName, " has not been configured"));
			this.config = config;
			HelpText = "No help available.";
			allVerbs = new();
			verbsByName = new(config.StringComparer);
		}
		public string? ShortName { get; }
		public string LongName { get; }
		public string ShortAndLongName { get; }
		public string FullShortAndLongName { get; set; }
		public string HelpText { get; set; }
		/// <summary>
		/// The action that's invoked when parsing is successful and this verb was provided.
		/// </summary>
		public Action Invoke { get; set; }
		/// <summary>
		/// The asynchronous action that's invoked when parsing is successful and this verb was provided.
		/// </summary>
		public Func<Task> InvokeAsync { get; set; }
		/// <summary>
		/// Any sub-verbs that this verb has
		/// </summary>
		public IReadOnlyList<IVerb> AllVerbs => allVerbs;
		public void AddVerb(string longName, Action<Verb> config)
		{
			Validate(verbsByName, longName, this.config);
			Verb v = new(null, longName, FullShortAndLongName, this.config);
			config(v);
			verbsByName.Add(longName, v);
			allVerbs.Add(v);
		}
		public void AddVerb(string longName, string shortName, Action<Verb> config)
		{
			Validate(verbsByName, shortName, longName, this.config);
			Verb v = new(shortName, longName, FullShortAndLongName, this.config);
			config(v);
			verbsByName.Add(shortName, v);
			verbsByName.Add(longName, v);
			allVerbs.Add(v);
		}
		public void AddVerb<TVerbClass>(string longName, Action<Verb<TVerbClass>> config) where TVerbClass : class, new()
		{
			Validate(verbsByName, longName, this.config);
			Verb<TVerbClass> v = new(null, longName, FullShortAndLongName, this.config);
			config(v);
			verbsByName.Add(longName, v);
			allVerbs.Add(v);
		}
		public void AddVerb<TVerbClass>(string longName, string shortName, Action<Verb<TVerbClass>> config) where TVerbClass : class, new()
		{
			Validate(verbsByName, shortName, longName, this.config);
			Verb<TVerbClass> v = new(shortName, longName, FullShortAndLongName, this.config);
			config(v);
			verbsByName.Add(shortName, v);
			verbsByName.Add(longName, v);
			allVerbs.Add(v);
		}
		/// <summary>
		/// If <paramref name="args"/> is empty, returns a <see cref="SuccessfulParse"/>.
		/// If <paramref name="args"/> contains a single string that's a verb, calls that verb's <see cref="Parse(IEnumerator{string})"/> method.
		/// Otherwise, returns a <see cref="FailedParseWithVerb"/>.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public IParseResult Parse(IEnumerator<string> args)
		{
			if (args.MoveNext())
			{
				string s = args.Current;
				return s == config.ShortHelpSwitch || s == config.LongHelpSwitch
					? new FailedParseWithVerb(this, new Error[] { new Error(ErrorCode.HelpRequested, string.Empty) })
					: verbsByName.TryGetValue(s, out IVerb? verb)
						? verb.Parse(args)
						: new FailedParseWithVerb(this, new Error[] { new Error(ErrorCode.UnexpectedArgument, "This verb does not take any arguments") });
			}
			else
			{
				return new SuccessfulParse(this);
			}
		}
		public void WriteSpecificHelp(IConsole console, IMessageFormatter msgFormatter)
		{
			msgFormatter.WriteSpecificHelp(console, this);
		}
		internal static void Validate(Dictionary<string, IVerb> verbsByName, string longName, CliParserConfig config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), "You cannot pass a null configuration action");
			}
			if (string.IsNullOrEmpty(longName))
			{
				throw new ArgumentNullException(nameof(longName), "Verb Long Name cannot be null or an empty string");
			}
			if (longName == config.ShortHelpSwitch || longName == config.LongHelpSwitch)
			{
				throw new CliParserBuilderException($"Short name for {longName} is already used by a help switch ({config.ShortHelpSwitch} or {config.LongHelpSwitch})");
			}
			if (verbsByName.ContainsKey(longName))
			{
				throw new CliParserBuilderException(string.Concat("The verb name ", longName, " has already been used, you may only use unique verb names"));
			}
		}
		internal static void Validate(Dictionary<string, IVerb> verbsByName, string shortName, string longName, CliParserConfig config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), "You cannot pass a null configuration action");
			}
			if (string.IsNullOrEmpty(longName))
			{
				throw new ArgumentNullException(nameof(longName), "Verb Long Name cannot be null or an empty string");
			}
			if (string.IsNullOrEmpty(shortName))
			{
				throw new ArgumentNullException(nameof(longName), "Verb Short Name cannot be null or an empty string");
			}
			if (longName == config.ShortHelpSwitch || longName == config.LongHelpSwitch)
			{
				throw new CliParserBuilderException($"Short name for {longName} is already used by a help switch ({config.ShortHelpSwitch} or {config.LongHelpSwitch})");
			}
			if (shortName == config.ShortHelpSwitch || shortName == config.LongHelpSwitch)
			{
				throw new CliParserBuilderException($"Long name for {shortName} is already used by a help switch ({config.ShortHelpSwitch} or {config.LongHelpSwitch})");
			}
			if (verbsByName.ContainsKey(longName))
			{
				throw new CliParserBuilderException(string.Concat("The verb name ", longName, " has already been used, you may only use unique verb names"));
			}
			if (verbsByName.ContainsKey(shortName))
			{
				throw new CliParserBuilderException(string.Concat("The verb name ", shortName, " has already been used, you may only use unique verb names"));
			}
		}
	}
}
