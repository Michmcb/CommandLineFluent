namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Arguments.Config;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Threading.Tasks;

	public sealed partial class Verb<TClass> : IVerb where TClass : class, new()
	{
		private readonly CliParserConfig config;
		private readonly List<IValue<TClass>> allValues;
		private readonly List<ISwitch<TClass>> allSwitches;
		private readonly List<IOption<TClass>> allOptions;
		private readonly Dictionary<string, ISwitch<TClass>> allSwitchesByShortName;
		private readonly Dictionary<string, IOption<TClass>> allOptionsByShortName;
		private readonly Dictionary<string, ISwitch<TClass>> allSwitchesByLongName;
		private readonly Dictionary<string, IOption<TClass>> allOptionsByLongName;
		internal Verb(string name, CliParserConfig config)
		{
			Name = name;
			this.config = config;
			allValues = new List<IValue<TClass>>();
			allSwitches = new List<ISwitch<TClass>>();
			allOptions = new List<IOption<TClass>>();
			allSwitchesByShortName = new Dictionary<string, ISwitch<TClass>>(config.StringComparer);
			allOptionsByShortName = new Dictionary<string, IOption<TClass>>(config.StringComparer);
			allSwitchesByLongName = new Dictionary<string, ISwitch<TClass>>(config.StringComparer);
			allOptionsByLongName = new Dictionary<string, IOption<TClass>>(config.StringComparer);
			HelpText = "No help available.";
			Invoke = x => throw new CliParserBuilderException(string.Concat("Invoke for verb ", Name, " has not been configured"));
			InvokeAsync = x => throw new CliParserBuilderException(string.Concat("InvokeAsync for verb ", Name, " has not been configured"));
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
		/// The action that's invoked when parsing is successful and this verb was provided.
		/// </summary>
		public Action<TClass> Invoke { get; set; }
		/// <summary>
		/// The asynchronous action that's invoked when parsing is successful and this verb was provided.
		/// </summary>
		public Func<TClass, Task> InvokeAsync { get; set; }
		public string Name { get; }
		public string HelpText { get; set; }
		public void WriteSpecificHelp(IConsole console, IMessageFormatter msgFormatter)
		{
			msgFormatter.WriteSpecificHelp(console, this);
		}
		/// <summary>
		/// Adds a new Option, without any predefined converter. If you're calling this, make sure you set a converter in <paramref name="optionConfig"/>!
		/// Alternatively, you can also create an extension method for this property, which calls <see cref="AddOptionWithConverter{TProp}(string?, string?, Action{OptionConfig{TClass, TProp}}, Func{string, Converted{TProp, string}}?)"/>.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="shortName">The short name used to specify this option. If it lacks the configured default short prefix, it's automatically prepended.</param>
		/// <param name="longName">The long name used to specify this option. If it lacks the configured default long prefix, it's automatically prepended.</param>
		/// <param name="optionConfig">The action used to configure the option.</param>
		/// <returns>The created option.</returns>
		public Option<TClass, TProp> AddOption<TProp>(string? shortName, string? longName, Action<OptionConfig<TClass, TProp>> optionConfig)
		{
			return AddOptionWithConverter(shortName, longName, optionConfig, null);
		}
		/// <summary>
		/// Adds a new Option, setting the <paramref name="converter"/>.
		/// Not intended that you call this directly (you can call WithConverter in the <paramref name="optionConfig"/>), it's provided for you to create extension methods
		/// which take specific types of <typeparamref name="TProp"/>, and call this method, providing the correct converter.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="shortName">The short name used to specify this option. If it lacks the configured default short prefix, it's automatically prepended.</param>
		/// <param name="longName">The long name used to specify this option. If it lacks the configured default long prefix, it's automatically prepended.</param>
		/// <param name="optionConfig">The action used to configure the option.</param>
		/// <param name="converter">The converter that the <paramref name="optionConfig"/> will be configured to use.</param>
		/// <returns>The created option.</returns>
		public Option<TClass, TProp> AddOptionWithConverter<TProp>(string? shortName, string? longName, Action<OptionConfig<TClass, TProp>> optionConfig, Func<string, Converted<TProp, string>>? converter)
		{
			if (optionConfig == null)
			{
				throw new ArgumentNullException(nameof(optionConfig), "optionConfig cannot be null");
			}
			if (shortName == null && longName == null)
			{
				throw new ArgumentException(string.Concat("Short Name and Long Name for a new option for verb ", Name, " cannot both be null"));
			}
			if (shortName != null && string.IsNullOrWhiteSpace(shortName))
			{
				throw new ArgumentException("Short name cannot be an empty string or only whitespace", nameof(shortName));
			}
			if (longName != null && string.IsNullOrWhiteSpace(longName))
			{
				throw new ArgumentException("Long name cannot be an empty string or only whitespace", nameof(longName));
			}
			ApplyDefaultPrefixAndCheck(ref shortName, ref longName, "option", allOptionsByShortName, allOptionsByLongName);
			OptionConfig<TClass, TProp> c = new OptionConfig<TClass, TProp>(shortName, longName, converter);
			optionConfig(c);
			Option<TClass, TProp> thing = c.Build();
			AddToDictionary(shortName, longName, thing, allOptionsByShortName, allOptionsByLongName);
			allOptions.Add(thing);
			return thing;
		}
		/// <summary>
		/// Adds a new Value, without any predefined converter. If you're calling this, make sure you set a converter in <paramref name="valueConfig"/>!
		/// Alternatively, you can also create an extension method for this property, which calls <see cref="AddValueWithConverter{TProp}(Action{ValueConfig{TClass, TProp}}, Func{string, Converted{TProp, string}}?)"/>.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="valueConfig">The action used to configure the value.</param>
		/// <returns>The created value.</returns>
		public Value<TClass, TProp> AddValue<TProp>(Action<ValueConfig<TClass, TProp>> valueConfig)
		{
			return AddValueWithConverter(valueConfig, null);
		}
		/// <summary>
		/// Adds a new Value, setting the <paramref name="converter"/>.
		/// Not intended that you call this directly (you can call WithConverter in the <paramref name="valueConfig"/>), it's provided for you to create extension methods
		/// which take specific types of <typeparamref name="TProp"/>, and call this method, providing the correct converter.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="valueConfig">The action used to configure the value.</param>
		/// <param name="converter">The converter that the <paramref name="valueConfig"/> will be configured to use.</param>
		/// <returns>The created value.</returns>
		public Value<TClass, TProp> AddValueWithConverter<TProp>(Action<ValueConfig<TClass, TProp>> valueConfig, Func<string, Converted<TProp, string>>? converter)
		{
			if (valueConfig == null)
			{
				throw new ArgumentNullException(nameof(valueConfig), "valueConfig cannot be null");
			}
			ValueConfig<TClass, TProp> c = new ValueConfig<TClass, TProp>(converter);
			valueConfig(c);
			Value<TClass, TProp> thing = c.Build();
			allValues.Add(thing);
			return thing;
		}
		/// <summary>
		/// Adds a new Switch, without any predefined converter. If you're calling this, make sure you set a converter in <paramref name="switchConfig"/>!
		/// Alternatively, you can also create an extension method for this property, which calls <see cref="AddSwitchWithConverter{TProp}(string?, string?, Action{SwitchConfig{TClass, TProp}}, Func{bool, Converted{TProp, string}}?)"/>.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="shortName">The short name used to specify this switch. If it lacks the configured default short prefix, it's automatically prepended.</param>
		/// <param name="longName">The long name used to specify this switch. If it lacks the configured default long prefix, it's automatically prepended.</param>
		/// <param name="switchConfig">The action used to configure the switch.</param>
		/// <returns>The created switch.</returns>
		public Switch<TClass, TProp> AddSwitch<TProp>(string? shortName, string? longName, Action<SwitchConfig<TClass, TProp>> switchConfig)
		{
			return AddSwitchWithConverter(shortName, longName, switchConfig, null);
		}
		/// <summary>
		/// Adds a new Switch, setting the <paramref name="converter"/>.
		/// Not intended that you call this directly (you can call WithConverter in the <paramref name="switchConfig"/>), it's provided for you to create extension methods
		/// which take specific types of <typeparamref name="TProp"/>, and call this method, providing the correct converter.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="shortName">The short name used to specify this switch. If it lacks the configured default short prefix, it's automatically prepended.</param>
		/// <param name="longName">The long name used to specify this switch. If it lacks the configured default long prefix, it's automatically prepended.</param>
		/// <param name="switchConfig">The action used to configure the switch.</param>
		/// <param name="converter">The converter that the <paramref name="switchConfig"/> will be configured to use.</param>
		/// <returns>The created switch.</returns>
		public Switch<TClass, TProp> AddSwitchWithConverter<TProp>(string? shortName, string? longName, Action<SwitchConfig<TClass, TProp>> switchConfig, Func<bool, Converted<TProp, string>>? converter)
		{
			if (switchConfig == null)
			{
				throw new ArgumentNullException(nameof(switchConfig), "switchConfig cannot be null");
			}
			if (shortName == null && longName == null)
			{
				throw new ArgumentException(string.Concat("Short Name and Long Name for a new switch for verb ", Name, " cannot both be null"));
			}
			if (shortName != null && shortName.Length == 0)
			{
				throw new ArgumentException("Short name cannot be an empty string", nameof(shortName));
			}
			if (longName != null && longName.Length == 0)
			{
				throw new ArgumentException("Short name cannot be an empty string", nameof(longName));
			}
			ApplyDefaultPrefixAndCheck(ref shortName, ref longName, "switch", allSwitchesByShortName, allSwitchesByLongName);
			SwitchConfig<TClass, TProp> c = new SwitchConfig<TClass, TProp>(shortName, longName, converter);
			switchConfig(c);
			Switch<TClass, TProp> thing = c.Build();
			AddToDictionary(shortName, longName, thing, allSwitchesByShortName, allSwitchesByLongName);
			allSwitches.Add(thing);
			return thing;
		}
		/// <summary>
		/// Adds a new MultiValue, without any predefined converter. If you're calling this, make sure you set a converter in <paramref name="multivalueConfig"/>!
		/// Alternatively, you can also create an extension method for this property, which calls <see cref="AddMultiValueWithConverter{TProp}(Action{MultiValueConfig{TClass, TProp}}, Func{string, Converted{TProp, string}}?)"/>.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="multivalueConfig">The action used to configure the multivalue.</param>
		/// <returns>The created multivalue.</returns>
		public MultiValue<TClass, TProp> AddMultiValue<TProp>(Action<MultiValueConfig<TClass, TProp>> multivalueConfig)
		{
			return AddMultiValueWithConverter(multivalueConfig, null);
		}
		/// <summary>
		/// Adds a new MultiValue, setting the <paramref name="converter"/>.
		/// Not intended that you call this directly (you can call WithConverter in the <paramref name="multivalueConfig"/>), it's provided for you to create extension methods
		/// which take specific types of <typeparamref name="TProp"/>, and call this method, providing the correct converter.
		/// </summary>
		/// <typeparam name="TProp">The type of the target property.</typeparam>
		/// <param name="multivalueConfig">The action used to configure the multivalue.</param>
		/// <param name="converter">The converter that the <paramref name="multivalueConfig"/> will be configured to use.</param>
		/// <returns>The created multivalue.</returns>
		public MultiValue<TClass, TProp> AddMultiValueWithConverter<TProp>(Action<MultiValueConfig<TClass, TProp>> multivalueConfig, Func<string, Converted<TProp, string>>? converter)
		{
			if (multivalueConfig == null)
			{
				throw new ArgumentNullException(nameof(multivalueConfig), "multivalueConfig cannot be null");
			}
			if (MultiValue != null)
			{
				throw new CliParserBuilderException("MultiValue has already been added; you cannot add more than one MultiValue");
			}
			MultiValueConfig<TClass, TProp> c = new MultiValueConfig<TClass, TProp>(converter);
			multivalueConfig(c);
			MultiValue<TClass, TProp> thing = c.Build();
			MultiValue = thing;
			return thing;
		}
		public IParseResult Parse(IEnumerable<string> args)
		{
			TClass parsedClass = new TClass();
			List<Error> errors = new List<Error>();
			HashSet<IOption<TClass>> optionsRemaining = new HashSet<IOption<TClass>>(allOptions);
			HashSet<ISwitch<TClass>> switchesRemaining = new HashSet<ISwitch<TClass>>(AllSwitches);
			List<string> multiValuesFound = new List<string>();
			int valuesFound = 0;

			using (IEnumerator<string> aEnum = args.GetEnumerator())
			{
				while (aEnum.MoveNext())
				{
					string arg = aEnum.Current;
					// If the user asks for help, immediately stop parsing
					if (arg == config.ShortHelpSwitch || arg == config.LongHelpSwitch)
					{
						errors.Add(new Error(ErrorCode.HelpRequested, string.Empty));
						return new FailedParseWithVerb<TClass>(this, errors);
					}

					if (allOptionsByShortName.TryGetValue(arg, out IOption<TClass>? oval) || allOptionsByLongName.TryGetValue(arg, out oval))
					{
						if (aEnum.MoveNext())
						{
							arg = aEnum.Current;
							// The option might only have a short or long name. However if both return false, that means we already saw it.
							if (optionsRemaining.Remove(oval))
							{
								Error error = oval.SetValue(parsedClass, arg);
								if (error.ErrorCode != ErrorCode.Ok)
								{
									errors.Add(error);
								}
							}
							else
							{
								errors.Add(new Error(ErrorCode.DuplicateOption, "An Option appeared twice: " + arg));
							}
						}
						else
						{
							errors.Add(new Error(ErrorCode.OptionMissingValue, "An Option was missing a value: " + arg));
						}
					}
					else if (allSwitchesByShortName.TryGetValue(arg, out ISwitch<TClass>? sval) || allSwitchesByLongName.TryGetValue(arg, out sval))
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
							errors.Add(new Error(ErrorCode.DuplicateSwitch, "A Switch appeared twice: " + arg));
						}
					}
					// Might be a Value
					else if (valuesFound < AllValues.Count)
					{
						Error error = AllValues[valuesFound++].SetValue(parsedClass, arg);
						if (error.ErrorCode != ErrorCode.Ok)
						{
							errors.Add(error);
						}
					}
					// Might be a MultiValue, unless it starts with something that should be ignored
					else if (MultiValue != null)// && !MultiValue.HasIgnoredPrefix(arg))
					{
						multiValuesFound.Add(arg);
					}
					// It's something unrecognized
					else
					{
						errors.Add(new Error(ErrorCode.UnexpectedArgument, "Found an unexpected argument: " + arg));
					}
				}
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
			return new SuccessfulParse<TClass>(this, parsedClass);
		}
		private void ApplyDefaultPrefixAndCheck<T>(ref string? shortName, ref string? longName, string type, Dictionary<string, T> shortNames, Dictionary<string, T> longNames)
		{
			if (shortName != null)
			{
				if (!string.IsNullOrEmpty(config.DefaultShortPrefix) && !shortName.StartsWith(config.DefaultShortPrefix))
				{
					shortName = config.DefaultShortPrefix + shortName;
				}
				if (string.IsNullOrWhiteSpace(shortName))
				{
					throw new ArgumentException($"Short Name for {type} for verb {Name} was empty or entirely whitespace");
				}
				if (shortName == config.ShortHelpSwitch)
				{
					throw new ArgumentException($"Short Name for {type} for verb {Name} is already used by the short help switch ({config.ShortHelpSwitch})");
				}
				if (shortNames.ContainsKey(shortName) || longNames.ContainsKey(shortName))
				{
					throw new ArgumentException($"The short name {shortName} for {type} for verb {Name} has already been used");
				}
			}
			if (longName != null)
			{
				if (!string.IsNullOrEmpty(config.DefaultLongPrefix) && !longName.StartsWith(config.DefaultLongPrefix))
				{
					longName = config.DefaultLongPrefix + longName;
				}
				if (string.IsNullOrWhiteSpace(longName))
				{
					throw new ArgumentException($"Short Name for {type} for verb {Name} was empty or entirely whitespace");
				}
				if (longName == config.LongHelpSwitch)
				{
					throw new ArgumentException($"Long Name for {type} for verb {Name} is already used by the long help switch ({config.LongHelpSwitch})");
				}
				if (shortNames.ContainsKey(longName) || longNames.ContainsKey(longName))
				{
					throw new ArgumentException($"The long name {longName} for {type} for verb {Name} has already been used");
				}
			}
		}
		private void AddToDictionary<T>(string? shortName, string? longName, T obj, Dictionary<string, T> shortNames, Dictionary<string, T> longNames)
		{
			if (longName != null)
			{
				longNames.Add(longName, obj);
			}
			if (shortName != null)
			{
				shortNames.Add(shortName, obj);
			}
		}
		// This stuff is useless and just adds clutter, so hide it
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}
	}
}
