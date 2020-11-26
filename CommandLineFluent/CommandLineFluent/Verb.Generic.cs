namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Arguments.Config;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq.Expressions;
	using System.Reflection;
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
		internal Verb(string? shortName, string longName, CliParserConfig config)
		{
			ShortName = shortName;
			LongName = longName;
			this.config = config;
			allValues = new List<IValue<TClass>>();
			allSwitches = new List<ISwitch<TClass>>();
			allOptions = new List<IOption<TClass>>();
			allSwitchesByShortName = new Dictionary<string, ISwitch<TClass>>(config.StringComparer);
			allOptionsByShortName = new Dictionary<string, IOption<TClass>>(config.StringComparer);
			allSwitchesByLongName = new Dictionary<string, ISwitch<TClass>>(config.StringComparer);
			allOptionsByLongName = new Dictionary<string, IOption<TClass>>(config.StringComparer);
			HelpText = "No help available.";
			Invoke = x => throw new CliParserBuilderException(string.Concat("Invoke for verb ", ShortAndLongName(), " has not been configured"));
			InvokeAsync = x => throw new CliParserBuilderException(string.Concat("InvokeAsync for verb ", ShortAndLongName(), " has not been configured"));
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
		/// <summary>
		/// A function invoked after parsing is successful, for any additional validation.
		/// If a null/empty string is returned, that is taken as passing validation.
		/// Returning a non-null/empty string indicates failure (<see cref="ErrorCode.ObjectFailedValidation"/>), and the returned string will be shown to the user.
		/// </summary>
		public Func<TClass, string?>? ValidateObject { get; set; }
		public string? ShortName { get; }
		public string LongName { get; }
		public string HelpText { get; set; }
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
		/// Returns a string which has the <see cref="ShortName"/> and <see cref="LongName"/> separated by a pipe, like this: shortName|longName.
		/// Or just <see cref="LongName"/> if <see cref="ShortName"/> is null.
		/// </summary>
		/// <returns></returns>
		public string ShortAndLongName()
		{
			return ArgUtils.ShortAndLongName(ShortName, LongName);
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
			NamelessArgConfig<TClass, TProp> obj = new NamelessArgConfig<TClass, TProp>();
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
			Value<TClass, TProp> thing = new Value<TClass, TProp>(config.DescriptiveName, config.HelpText ?? "No help available.", ar, setter, config.DefaultValue, config.configuredDependencies, config.Converter);
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
			NamedArgConfig<TClass, TProp, string>? obj = new NamedArgConfig<TClass, TProp, string>();
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
			Option<TClass, TProp> arg = new Option<TClass, TProp>(shortName, longName, config.DescriptiveName, config.HelpText ?? "No help available.", ar, setter, config.DefaultValue, config.configuredDependencies, config.Converter);
			AddToDictionary(arg.ShortName, arg.LongName, arg, allOptionsByShortName, allOptionsByLongName);
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
			NamedArgConfig<TClass, TProp, bool>? obj = new NamedArgConfig<TClass, TProp, bool>();
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
			Switch<TClass, TProp> arg = new Switch<TClass, TProp>(shortName, longName, config.DescriptiveName, config.HelpText ?? "No help available.", ar, setter, config.DefaultValue, config.configuredDependencies, config.Converter);
			AddToDictionary(arg.ShortName, arg.LongName, arg, allSwitchesByShortName, allSwitchesByLongName);
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
			NamelessMultiArgConfig<TClass, TProp, TPropCollection>? obj = new NamelessMultiArgConfig<TClass, TProp, TPropCollection>();
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

			MultiValue<TClass, TProp, TPropCollection> arg = new MultiValue<TClass, TProp, TPropCollection>(config.DescriptiveName, config.HelpText ?? "No help available.", ar,
				setter, config.DefaultValue, config.configuredDependencies, config.Converter, config.Accumulator);
			MultiValue = arg;
			return arg;
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
		[Obsolete("Prefer using AddOption")]
		public Option<TClass, TProp> AddOptionWithConverter<TProp>(string? shortName, string? longName, Action<OptionConfig<TClass, TProp>> optionConfig, Func<string, Converted<TProp, string>> converter)
		{
			if (optionConfig == null)
			{
				throw new ArgumentNullException(nameof(optionConfig), "optionConfig cannot be null");
			}
			ApplyDefaultPrefixAndCheck(ref shortName, ref longName, "option");
			OptionConfig<TClass, TProp> c = new OptionConfig<TClass, TProp>(shortName, longName, converter);
			optionConfig(c);
			Option<TClass, TProp> thing = c.Build();
			AddToDictionary(shortName, longName, thing, allOptionsByShortName, allOptionsByLongName);
			allOptions.Add(thing);
			return thing;
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
		[Obsolete("Prefer using AddValue")]
		public Value<TClass, TProp> AddValueWithConverter<TProp>(Action<ValueConfig<TClass, TProp>> valueConfig, Func<string, Converted<TProp, string>> converter)
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
		[Obsolete("Prefer using AddSwitch")]
		public Switch<TClass, TProp> AddSwitchWithConverter<TProp>(string? shortName, string? longName, Action<SwitchConfig<TClass, TProp>> switchConfig, Func<bool, Converted<TProp, string>> converter)
		{
			if (switchConfig == null)
			{
				throw new ArgumentNullException(nameof(switchConfig), "switchConfig cannot be null");
			}
			if (shortName == null && longName == null)
			{
				throw new ArgumentNullException(string.Concat("Short Name and Long Name for a new switch for verb ", LongName, " cannot both be null"));
			}
			if (shortName != null && shortName.Length == 0)
			{
				throw new ArgumentException("Short name cannot be an empty string", nameof(shortName));
			}
			if (longName != null && longName.Length == 0)
			{
				throw new ArgumentException("Short name cannot be an empty string", nameof(longName));
			}
			ApplyDefaultPrefixAndCheck(ref shortName, ref longName, "switch");
			SwitchConfig<TClass, TProp> c = new SwitchConfig<TClass, TProp>(shortName, longName, converter);
			switchConfig(c);
			Switch<TClass, TProp> thing = c.Build();
			AddToDictionary(shortName, longName, thing, allSwitchesByShortName, allSwitchesByLongName);
			allSwitches.Add(thing);
			return thing;
		}
		public IParseResult Parse(IEnumerable<string> args)
		{
			using IEnumerator<string> e = args.GetEnumerator();
			return Parse(e);
		}
		public IParseResult Parse(IEnumerator<string> argsEnum)
		{
			TClass parsedClass = new TClass();
			List<Error> errors = new List<Error>();
			HashSet<IOption<TClass>> optionsRemaining = new HashSet<IOption<TClass>>(allOptions);
			HashSet<ISwitch<TClass>> switchesRemaining = new HashSet<ISwitch<TClass>>(allSwitches);
			List<string> multiValuesFound = new List<string>();
			int valuesFound = 0;

			while (argsEnum.MoveNext())
			{
				string arg = argsEnum.Current;
				// If the user asks for help, immediately stop parsing
				if (arg == config.ShortHelpSwitch || arg == config.LongHelpSwitch)
				{
					errors.Add(new Error(ErrorCode.HelpRequested, string.Empty));
					return new FailedParseWithVerb<TClass>(this, errors);
				}

				if (allOptionsByShortName.TryGetValue(arg, out IOption<TClass>? oval) || allOptionsByLongName.TryGetValue(arg, out oval))
				{
					if (argsEnum.MoveNext())
					{
						arg = argsEnum.Current;
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
				if (allOptionsByShortName.ContainsKey(shortName) || allOptionsByLongName.ContainsKey(shortName) || allSwitchesByShortName.ContainsKey(shortName) || allSwitchesByLongName.ContainsKey(shortName))
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
				if (allOptionsByShortName.ContainsKey(longName) || allOptionsByLongName.ContainsKey(longName) || allSwitchesByShortName.ContainsKey(longName) || allSwitchesByLongName.ContainsKey(longName))
				{
					throw new CliParserBuilderException($"The short name {longName} for {type} for verb {LongName} has already been used");
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
