namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Threading.Tasks;

	public sealed class Verb<TClass> : IVerb where TClass : class, new()
	{
		/*
		TODO We can have simple Lists of all the switches/options, and a dictionary each that refers to them by shortname and by longname.
		*/
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
			allSwitchesByShortName = new Dictionary<string, ISwitch<TClass>>();
			allOptionsByShortName = new Dictionary<string, IOption<TClass>>();
			allSwitchesByLongName = new Dictionary<string, ISwitch<TClass>>();
			allOptionsByLongName = new Dictionary<string, IOption<TClass>>();
			HelpText = "";
		}
		public IMultiValue<TClass>? MultiValue { get; private set; }
		public IReadOnlyCollection<ISwitch<TClass>> AllSwitches => allSwitches;
		public IReadOnlyCollection<IOption<TClass>> AllOptions => allOptions;
		public IReadOnlyList<IValue<TClass>> AllValues => allValues;
		public Action<TClass>? Invoke { get; set; }
		public Func<TClass, Task>? InvokeAsync { get; set; }
		public string Name { get; }
		public string HelpText { get; set; }
		public Option<TClass, string> AddOption(string? shortName, string? longName)
		{
			return AddOption<string>(shortName, longName);
		}
		public Option<TClass, TProp> AddOption<TProp>(string? shortName, string? longName)
		{
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
			ApplyDefaultPrefix(ref shortName, ref longName);
			Option<TClass, TProp> thing = new Option<TClass, TProp>(shortName, longName);
			AddToDictionary(shortName, longName, "option", thing, allOptionsByShortName, allOptionsByLongName);
			allOptions.Add(thing);
			return thing;
		}
		public Switch<TClass, bool> AddSwitch(string? shortName, string? longName)
		{
			return AddSwitch<bool>(shortName, longName);
		}
		public Switch<TClass, TProp> AddSwitch<TProp>(string? shortName, string? longName)
		{
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
			ApplyDefaultPrefix(ref shortName, ref longName);
			Switch<TClass, TProp> thing = new Switch<TClass, TProp>(shortName, longName);
			AddToDictionary(shortName, longName, "switch", thing, allSwitchesByShortName, allSwitchesByLongName);
			allSwitches.Add(thing);
			return thing;
		}
		public Value<TClass, string> AddValue()
		{
			return AddValue<string>();
		}
		public Value<TClass, TProp> AddValue<TProp>()
		{
			Value<TClass, TProp> thing = new Value<TClass, TProp>();
			allValues.Add(thing);
			return thing;
		}
		public MultiValue<TClass, ICollection<string>> AddMultiValue()
		{
			return AddMultiValue<ICollection<string>>();
		}
		public MultiValue<TClass, TProp> AddMultiValue<TProp>()
		{
			if (MultiValue != null)
			{
				throw new CliParserBuilderException("MultiValue has already been added; you cannot add more than one MultiValue");
			}
			MultiValue<TClass, TProp> thing = new MultiValue<TClass, TProp>();
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
						return new ParseResult<TClass>(errors);
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
								errors.Add(new Error(ErrorCode.DuplicateOption, $"The Option {arg} appeared twice"));
							}
						}
						else
						{
							errors.Add(new Error(ErrorCode.UnexpectedEndOfArguments, $"Expected to find a value after Option {arg}, but found nothing"));
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
							errors.Add(new Error(ErrorCode.DuplicateSwitch, $"The Switch {arg} appeared twice"));
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
					else if (MultiValue != null && !MultiValue.HasIgnoredPrefix(arg))
					{
						multiValuesFound.Add(arg);
					}
					// It's something unrecognized
					else
					{
						errors.Add(new Error(ErrorCode.UnexpectedArgument, $"Found the string {arg} in an unexpected place"));
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
			// Make sure all of the stuff we've set so far is good, if not bail out
			if (errors.Count > 0)
			{
				return new ParseResult<TClass>(errors);
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
				return new ParseResult<TClass>(errors);
			}
			return new ParseResult<TClass>(this, parsedClass);
		}
		public ICollection<Error> Validate()
		{
			List<Error> errors = new List<Error>();
			foreach (IOption<TClass> value in allOptions)
			{
				errors.AddRange(value.Validate());
			}
			foreach (ISwitch<TClass> value in allSwitches)
			{
				errors.AddRange(value.Validate());
			}
			foreach (IValue<TClass> value in allValues)
			{
				errors.AddRange(value.Validate());
			}
			if (MultiValue != null)
			{
				errors.AddRange(MultiValue.Validate());
			}
			return errors;
		}
		private void ApplyDefaultPrefix(ref string? shortName, ref string? longName)
		{
			if (shortName != null && !shortName.StartsWith(config.DefaultShortPrefix))
			{
				shortName = config.DefaultShortPrefix + shortName;
			}
			if (longName != null && !longName.StartsWith(config.DefaultLongPrefix))
			{
				longName = config.DefaultLongPrefix + longName;
			}
		}
		/// <summary>
		/// Makes sure that at least one of shortName and longName is not null, that they are not empty or whitespace, 
		/// applies any prefixes required, and adds them to our map to ensure they're unique.
		/// </summary>
		/// <param name="shortName">The short name</param>
		/// <param name="longName">The long name</param>
		private void AddToDictionary<T>(string? shortName, string? longName, string type, T obj, Dictionary<string, T> shortNames, Dictionary<string, T> longNames)
		{
			if (shortName != null)
			{
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
				longNames.Add(longName, obj);
			}
			// Add the short name down here; if we add shortName earlier, we might throw an exception and mess up the dictionaries
			if (shortName != null)
			{
				shortNames.Add(shortName, obj);
			}
		}
	}
}
