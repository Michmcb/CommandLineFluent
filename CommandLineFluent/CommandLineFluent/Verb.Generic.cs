namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Arguments.Config;
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Threading.Tasks;

	public sealed partial class Verb<TClass> : IVerb where TClass : class, new()
	{
		private readonly CliParserConfig config;
		private readonly List<IValue<TClass>> allValues;
		private readonly List<ISwitch<TClass>> allSwitches;
		private readonly List<IOption<TClass>> allOptions;
		private readonly List<IVerb> allVerbs;
		private readonly Dictionary<string, IVerb> verbsByName;
		private readonly Dictionary<string, ISwitch<TClass>> allSwitchesByName;
		private readonly Dictionary<string, IOption<TClass>> allOptionsByName;
		internal Verb(string? shortName, string longName, string? parentShortAndLongName, CliParserConfig config)
		{
			ShortName = shortName;
			LongName = longName;
			ShortAndLongName = ArgUtils.ShortAndLongName(shortName, longName);
			FullShortAndLongName = parentShortAndLongName == null ? ArgUtils.ShortAndLongName(shortName, longName) : string.Concat(parentShortAndLongName, ' ', ArgUtils.ShortAndLongName(shortName, longName));
			Invoke = x => throw new CliParserBuilderException(string.Concat("Invoke for verb ", FullShortAndLongName, " has not been configured"));
			InvokeAsync = x => throw new CliParserBuilderException(string.Concat("InvokeAsync for verb ", FullShortAndLongName, " has not been configured"));
			this.config = config;
			verbsByName = new(config.StringComparer);
			allVerbs = new();
			allValues = new List<IValue<TClass>>();
			allSwitches = new List<ISwitch<TClass>>();
			allOptions = new List<IOption<TClass>>();
			allSwitchesByName = new Dictionary<string, ISwitch<TClass>>(config.StringComparer);
			allOptionsByName = new Dictionary<string, IOption<TClass>>(config.StringComparer);
			HelpText = "No help available.";
		}
		/// <summary>
		/// If not null, the MultiValue for this verb which picks up all extra arguments.
		/// </summary>
		public IMultiValue<TClass>? MultiValue { get; private set; }
		/// <summary>
		/// All the Switches that this Verb has.
		/// </summary>
		public IReadOnlyCollection<ISwitch<TClass>> AllSwitches => allSwitches;
		/// <summary>
		/// All the Options that this Verb has.
		/// </summary>
		public IReadOnlyCollection<IOption<TClass>> AllOptions => allOptions;
		/// <summary>
		/// All the Values that this Verb has.
		/// </summary>
		public IReadOnlyList<IValue<TClass>> AllValues => allValues;
		/// <summary>
		/// Any sub-verbs that this verb has.
		/// </summary>
		public IReadOnlyList<IVerb> AllVerbs => allVerbs;
		/// <summary>
		/// The action that's invoked when parsing is successful and this verb was provided.
		/// </summary>
		public Action<TClass> Invoke { get; set; }
		/// <summary>
		/// The asynchronous action that's invoked when parsing is successful and this verb was provided.
		/// </summary>
		public Func<TClass, Task> InvokeAsync { get; set; }
		/// <summary>
		/// A function invoked after parsing is successful, for any additional validation.
		/// If a null/empty string is returned, that is taken as passing validation.
		/// Returning a non-empty string indicates failure (<see cref="ErrorCode.ObjectFailedValidation"/>), and the returned string will be shown to the user.
		/// </summary>
		public Func<TClass, string?>? ValidateObject { get; set; }
		public string? ShortName { get; }
		public string LongName { get; }
		public string ShortAndLongName { get; }
		public string FullShortAndLongName { get; }
		public string HelpText { get; set; }
		public void AddVerb(string longName, Action<Verb> config)
		{
			Verb.Validate(verbsByName, longName, this.config);
			Verb v = new(null, longName, FullShortAndLongName, this.config);
			config(v);
			verbsByName.Add(longName, v);
			allVerbs.Add(v);
		}
		public void AddVerb(string longName, string shortName, Action<Verb> config)
		{
			Verb.Validate(verbsByName, shortName, longName, this.config);
			Verb v = new(shortName, longName, FullShortAndLongName, this.config);
			config(v);
			verbsByName.Add(shortName, v);
			verbsByName.Add(longName, v);
			allVerbs.Add(v);
		}
		public void AddVerb<TVerbClass>(string longName, Action<Verb<TVerbClass>> config) where TVerbClass : class, new()
		{
			Verb.Validate(verbsByName, longName, this.config);
			Verb<TVerbClass> v = new(null, longName, FullShortAndLongName, this.config);
			config(v);
			verbsByName.Add(longName, v);
			allVerbs.Add(v);
		}
		public void AddVerb<TVerbClass>(string longName, string shortName, Action<Verb<TVerbClass>> config) where TVerbClass : class, new()
		{
			Verb.Validate(verbsByName, shortName, longName, this.config);
			Verb<TVerbClass> v = new(shortName, longName, FullShortAndLongName, this.config);
			config(v);
			verbsByName.Add(shortName, v);
			verbsByName.Add(longName, v);
			allVerbs.Add(v);
		}
		/// <summary>
		/// Writes help to <paramref name="console"/>, formatted using <paramref name="msgFormatter"/>.
		/// </summary>
		/// <param name="console"></param>
		/// <param name="msgFormatter"></param>
		public void WriteSpecificHelp(IConsole console, IMessageFormatter msgFormatter)
		{
			msgFormatter.WriteSpecificHelp(console, this);
		}
		/// <summary>
		/// Adds a new Value.
		/// This is entirely unconfigured; you must set the converter and whether or not it is required.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Value.</returns>
		public Value<TClass, TProp> AddValueCore<TProp>(Expression<Func<TClass, TProp>> expression, Action<NamelessArgConfig<TClass, TProp>> config)
		{
			NamelessArgConfig<TClass, TProp> obj = new();
			config(obj);
			return AddValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Value.
		/// This is mainly useful to provide custom extension methods which take specific types of <typeparamref name="TProp"/>.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="expression">The property to set.</param>
		/// <param name="config">The configuration.</param>
		/// <returns>The created Value.</returns>
		public Value<TClass, TProp> AddValueCore<TProp>(Expression<Func<TClass, TProp>> expression, NamelessArgConfig<TClass, TProp> config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), nameof(config) + " cannot be null");
			}
			Action<TClass, TProp> setter = CliParserBuilder.GetSetMethodDelegateFromExpression(expression, out PropertyInfo pi);
			if (config.Converter == null)
			{
				throw new CliParserBuilderException(string.Concat("You need to provide a converter for the property ", pi.Name, " of the class ", typeof(TClass).Name));
			}
			config.configuredDependencies?.Validate();
			ArgumentRequired ar = config.configuredDependencies != null ? ArgumentRequired.HasDependencies : config.Required ? ArgumentRequired.Required : ArgumentRequired.Optional;
			Value<TClass, TProp> thing = new(config.DescriptiveName ?? pi.Name, config.HelpText ?? "No help available.", ar, setter, config.DefaultValue, config.configuredDependencies, config.Converter);
			allValues.Add(thing);
			return thing;
		}
		/// <summary>
		/// Adds a new Option.
		/// This is entirely unconfigured; you must set the converter and whether or not it is required.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Option.</returns>
		public Option<TClass, TProp> AddOptionCore<TProp>(Expression<Func<TClass, TProp>> expression, Action<NamedArgConfig<TClass, TProp, string>> config)
		{
			NamedArgConfig<TClass, TProp, string>? obj = new();
			config(obj);
			return AddOptionCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Option.
		/// This is mainly useful to provide custom extension methods which take specific types of <typeparamref name="TProp"/>.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="expression">The property to set.</param>
		/// <param name="config">The configuration.</param>
		/// <returns>The created Option.</returns>
		public Option<TClass, TProp> AddOptionCore<TProp>(Expression<Func<TClass, TProp>> expression, NamedArgConfig<TClass, TProp, string> config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), nameof(config) + " cannot be null");
			}
			Action<TClass, TProp> setter = CliParserBuilder.GetSetMethodDelegateFromExpression(expression, out PropertyInfo pi);
			if (config.Converter == null)
			{
				throw new CliParserBuilderException(string.Concat("You need to provide a converter for the property ", pi.Name, " of the class ", typeof(TClass).Name));
			}
			config.configuredDependencies?.Validate();
			string? shortName = config.ShortName;
			string? longName = config.LongName;
			ApplyDefaultPrefixAndCheck(ref shortName, ref longName, "option");
			ArgumentRequired ar = config.configuredDependencies != null ? ArgumentRequired.HasDependencies : config.Required ? ArgumentRequired.Required : ArgumentRequired.Optional;
			Option<TClass, TProp> arg = new(shortName, longName, config.DescriptiveName ?? pi.Name, config.HelpText ?? "No help available.", ar, setter, config.DefaultValue, config.configuredDependencies, config.Converter);
			AddToDictionary(arg.ShortName, arg.LongName, arg, allOptionsByName);
			allOptions.Add(arg);
			return arg;
		}
		/// <summary>
		/// Adds a new Switch.
		/// This is entirely unconfigured; you must set the converter and whether or not it is required.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured Switch.</returns>
		public Switch<TClass, TProp> AddSwitchCore<TProp>(Expression<Func<TClass, TProp>> expression, Action<NamedArgConfig<TClass, TProp, bool>> config)
		{
			NamedArgConfig<TClass, TProp, bool>? obj = new();
			config(obj);
			return AddSwitchCore(expression, obj);
		}
		/// <summary>
		/// Adds a new Switch.
		/// This is mainly useful to provide custom extension methods which take specific types of <typeparamref name="TProp"/>.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="expression">The property to set.</param>
		/// <param name="config">The configuration.</param>
		/// <returns>The created Switch.</returns>
		public Switch<TClass, TProp> AddSwitchCore<TProp>(Expression<Func<TClass, TProp>> expression, NamedArgConfig<TClass, TProp, bool> config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), nameof(config) + " cannot be null");
			}
			Action<TClass, TProp> setter = CliParserBuilder.GetSetMethodDelegateFromExpression(expression, out PropertyInfo pi);
			if (config.Converter == null)
			{
				throw new CliParserBuilderException(string.Concat("You need to provide a converter for the property ", pi.Name, " of the class ", typeof(TClass).Name));
			}
			config.configuredDependencies?.Validate();
			string? shortName = config.ShortName;
			string? longName = config.LongName;
			ApplyDefaultPrefixAndCheck(ref shortName, ref longName, "switch");
			ArgumentRequired ar = config.configuredDependencies != null ? ArgumentRequired.HasDependencies : config.Required ? ArgumentRequired.Required : ArgumentRequired.Optional;
			Switch<TClass, TProp> arg = new(shortName, longName, config.DescriptiveName ?? pi.Name, config.HelpText ?? "No help available.", ar, setter, config.DefaultValue, config.configuredDependencies, config.Converter);
			AddToDictionary(arg.ShortName, arg.LongName, arg, allSwitchesByName);
			allSwitches.Add(arg);
			return arg;
		}
		/// <summary>
		/// Adds a new MultiValue.
		/// This is entirely unconfigured; you must set the converter and whether or not it is required.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the Value.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TProp, TPropCollection> AddMultiValueCore<TProp, TPropCollection>(Expression<Func<TClass, TPropCollection>> expression, Action<NamelessMultiArgConfig<TClass, TProp, TPropCollection>> config)
		{
			NamelessMultiArgConfig<TClass, TProp, TPropCollection>? obj = new();
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue.
		/// This is mainly useful to provide custom extension methods which take specific types of <typeparamref name="TProp"/>.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="expression">The property to set.</param>
		/// <param name="config">The configuration.</param>
		/// <returns>The created MultiValue.</returns>
		public MultiValue<TClass, TProp, TPropCollection> AddMultiValueCore<TProp, TPropCollection>(Expression<Func<TClass, TPropCollection>> expression, NamelessMultiArgConfig<TClass, TProp, TPropCollection> config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), nameof(config) + " cannot be null");
			}
			Action<TClass, TPropCollection> setter = CliParserBuilder.GetSetMethodDelegateFromExpression(expression, out PropertyInfo pi);
			if (config.Converter == null)
			{
				throw new CliParserBuilderException(string.Concat("You need to provide a converter for the property ", pi.Name, " of the class ", typeof(TClass).Name));
			}
			config.configuredDependencies?.Validate();
			ArgumentRequired ar = config.configuredDependencies != null ? ArgumentRequired.HasDependencies : config.Required ? ArgumentRequired.Required : ArgumentRequired.Optional;

			MultiValue<TClass, TProp, TPropCollection> arg = new(config.DescriptiveName ?? pi.Name + "...", config.HelpText ?? "No help available.", ar,
				setter, config.DefaultValue, config.configuredDependencies, config.Converter, config.Accumulator);
			MultiValue = arg;
			return arg;
		}
		public IParseResult Parse(IEnumerator<string> args)
		{
			bool first = true;
			TClass parsedClass = new();
			List<Error> errors = new();
			HashSet<IOption<TClass>> optionsRemaining = new(allOptions);
			HashSet<ISwitch<TClass>> switchesRemaining = new(allSwitches);
			List<string> multiValuesFound = new();
			int valuesFound = 0;

			while (args.MoveNext())
			{
				string a = args.Current;
				// If the user asks for help, immediately stop parsing
				if (a == config.ShortHelpSwitch || a == config.LongHelpSwitch)
				{
					errors.Add(new Error(ErrorCode.HelpRequested, string.Empty));
					return new FailedParseWithVerb<TClass>(this, errors);
				}

				// It is POSSIBLE for the user to set up a verb with arguments that also has a sub-verb as much as I hate that idea
				// So only allow the first argument to be the verb. It should be interpreted as a value or whatever if found elsewhere.
				// If it happens to be a verb, we just hand over the parsing
				if (first && verbsByName.TryGetValue(a, out IVerb? verb))
				{
					return verb.Parse(args);
				}

				if (allOptionsByName.TryGetValue(a, out IOption<TClass>? oval))
				{
					if (args.MoveNext())
					{
						a = args.Current;
						// The option might only have a short or long name. However if both return false, that means we already saw it.
						if (optionsRemaining.Remove(oval))
						{
							Error error = oval.SetValue(parsedClass, a);
							if (error.ErrorCode != ErrorCode.Ok)
							{
								errors.Add(error);
							}
						}
						else
						{
							errors.Add(new Error(ErrorCode.DuplicateOption, "An Option appeared twice: " + a));
						}
					}
					else
					{
						errors.Add(new Error(ErrorCode.OptionMissingValue, "An Option was missing a value: " + a));
					}
				}
				else if (allSwitchesByName.TryGetValue(a, out ISwitch<TClass>? sval))
				{
					// The switch might only have a short or long name. However if both return false, that means we already saw it.
					if (switchesRemaining.Remove(sval))
					{
						Error error = sval.SetValue(parsedClass, string.Empty);
						if (error.ErrorCode != ErrorCode.Ok)
						{
							errors.Add(error);
						}
					}
					else
					{
						errors.Add(new Error(ErrorCode.DuplicateSwitch, "A Switch appeared twice: " + a));
					}
				}
				// Might be a Value
				else if (valuesFound < AllValues.Count)
				{
					Error error = AllValues[valuesFound++].SetValue(parsedClass, a);
					if (error.ErrorCode != ErrorCode.Ok)
					{
						errors.Add(error);
					}
				}
				// Might be a MultiValue, unless it starts with something that should be ignored
				else if (MultiValue != null)// && !MultiValue.HasIgnoredPrefix(arg))
				{
					multiValuesFound.Add(a);
				}
				// It's something unrecognized
				else
				{
					errors.Add(new Error(ErrorCode.UnexpectedArgument, "Found an unexpected argument: " + a));
				}
				first = false;
			}
			// Set all remaining values to default
			for (int i = valuesFound; i < AllValues.Count; i++)
			{
				Error error = AllValues[i].SetValue(parsedClass, null);
				if (error.ErrorCode != ErrorCode.Ok)
				{
					errors.Add(error);
				}
			}
			bool gotMultiValue = false;
			// Set the MultiValue
			if (MultiValue != null)
			{
				Error error = MultiValue.SetValue(parsedClass, multiValuesFound);
				gotMultiValue = true;
				if (error.ErrorCode != ErrorCode.Ok)
				{
					errors.Add(error);
				}
			}
			// Set all remaining options and switches to their default values
			foreach (IOption<TClass> opt in optionsRemaining)
			{
				Error error = opt.SetValue(parsedClass, null);
				if (error.ErrorCode != ErrorCode.Ok)
				{
					errors.Add(error);
				}
			}
			foreach (ISwitch<TClass> sw in switchesRemaining)
			{
				Error error = sw.SetValue(parsedClass, null);
				if (error.ErrorCode != ErrorCode.Ok)
				{
					errors.Add(error);
				}
			}
			// Make sure all of the stuff we've set so far is good, if not then bail out
			if (errors.Count > 0)
			{
				return new FailedParseWithVerb<TClass>(this, errors);
			}

			// Evaluate dependencies; we know we got the value if it doesn't appear in our lists of remaining stuff
			foreach (IOption<TClass> opt in allOptions)
			{
				Error error = opt.EvaluateDependencies(parsedClass, !optionsRemaining.Contains(opt));
				if (error.ErrorCode != ErrorCode.Ok)
				{
					errors.Add(error);
				}
			}
			foreach (ISwitch<TClass> sw in allSwitches)
			{
				Error error = sw.EvaluateDependencies(parsedClass, !switchesRemaining.Contains(sw));
				if (error.ErrorCode != ErrorCode.Ok)
				{
					errors.Add(error);
				}
			}
			// As for values, we can use the valuesFound variable
			for (int i = 0; i < allValues.Count; i++)
			{
				IValue<TClass> val = allValues[i];
				Error error = val.EvaluateDependencies(parsedClass, valuesFound > i);
				if (error.ErrorCode != ErrorCode.Ok)
				{
					errors.Add(error);
				}
			}
			if (MultiValue != null)
			{
				Error error = MultiValue.EvaluateDependencies(parsedClass, gotMultiValue);
				if (error.ErrorCode != ErrorCode.Ok)
				{
					errors.Add(error);
				}
			}
			if (errors.Count > 0)
			{
				return new FailedParseWithVerb<TClass>(this, errors);
			}
			string? errMsg = ValidateObject?.Invoke(parsedClass);
			if (string.IsNullOrEmpty(errMsg))
			{
				return new SuccessfulParse<TClass>(this, parsedClass);
			}
			else
			{
				errors.Add(new Error(ErrorCode.ObjectFailedValidation, errMsg));
				return new FailedParseWithVerb<TClass>(this, errors);
			}
		}
		private void ApplyDefaultPrefixAndCheck(ref string? shortName, ref string? longName, string type)
		{
			if (shortName == null && longName == null)
			{
				throw new CliParserBuilderException(string.Concat("Short name and long name for a new ", type, " for verb ", LongName, " cannot both be null"));
			}
			if (shortName != null)
			{
				if (string.IsNullOrWhiteSpace(shortName))
				{
					throw new CliParserBuilderException($"Short name for {type} for verb {LongName} was empty or entirely whitespace");
				}
				if (!string.IsNullOrEmpty(config.DefaultShortPrefix) && !shortName.StartsWith(config.DefaultShortPrefix))
				{
					shortName = config.DefaultShortPrefix + shortName;
				}
				if (shortName == config.ShortHelpSwitch || shortName == config.LongHelpSwitch)
				{
					throw new CliParserBuilderException($"Short name for {type} for verb {LongName} is already used by a help switch ({config.ShortHelpSwitch} or {config.LongHelpSwitch})");
				}
				if (allOptionsByName.ContainsKey(shortName) || allSwitchesByName.ContainsKey(shortName))
				{
					throw new CliParserBuilderException($"The short name {shortName} for {type} for verb {LongName} has already been used");
				}
			}
			if (longName != null)
			{
				if (string.IsNullOrWhiteSpace(longName))
				{
					throw new CliParserBuilderException($"Long name for {type} for verb {LongName} was empty or entirely whitespace");
				}
				if (!string.IsNullOrEmpty(config.DefaultLongPrefix) && !longName.StartsWith(config.DefaultLongPrefix))
				{
					longName = config.DefaultLongPrefix + longName;
				}
				if (longName == config.ShortHelpSwitch || longName == config.LongHelpSwitch)
				{
					throw new CliParserBuilderException($"Long name for {type} for verb {LongName} is already used by a help switch ({config.ShortHelpSwitch} or {config.LongHelpSwitch})");
				}
				if (allOptionsByName.ContainsKey(longName) || allSwitchesByName.ContainsKey(longName))
				{
					throw new CliParserBuilderException($"The short name {longName} for {type} for verb {LongName} has already been used");
				}
			}
		}
		private static void AddToDictionary<T>(string? shortName, string? longName, T obj, Dictionary<string, T> names)
		{
			if (longName != null)
			{
				names.Add(longName, obj);
			}
			if (shortName != null)
			{
				names.Add(shortName, obj);
			}
		}
	}
}
