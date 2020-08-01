namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using System;
	using System.Collections.Generic;

	public sealed class Verb<TClass> : IVerb where TClass : class, new()
	{
		private readonly CliParserConfig config;
		private readonly List<IValue<TClass>> allValues;
		private readonly Dictionary<string, ISwitch<TClass>> allSwitches;
		private readonly Dictionary<string, IOption<TClass>> allOptions;
		public Verb(string name, CliParserConfig config)
		{
			Name = name;
			this.config = config;
			allValues = new List<IValue<TClass>>();
			allSwitches = new Dictionary<string, ISwitch<TClass>>();
			allOptions = new Dictionary<string, IOption<TClass>>();
		}
		public IMultiValue<TClass> MultiValue { get; }
		public IReadOnlyDictionary<string, ISwitch<TClass>> AllSwitches => allSwitches;
		public IReadOnlyDictionary<string, IOption<TClass>> AllOptions => allOptions;
		public IReadOnlyCollection<IValue<TClass>> AllValues => allValues;
		public Action<IConsole> WriteHelpText { get; }
		public Action<IConsole> WriteUsageText { get; }
		public string Name { get; }
		public string HelpText { get; }
		public Option<TClass, TProp> AddOption<TProp>(string? shortName, string? longName)
		{
			if (shortName == null && longName == null)
			{
				throw new ArgumentException(string.Concat("Short Name and Long Name for a new option for verb ", Name, " cannot both be null"));
			}
			Option<TClass, TProp> thing = new Option<TClass, TProp>(shortName, longName);
			AddToDictionary(ref shortName, ref longName, "option", thing, allOptions);
			return thing;
		}
		public Switch<TClass, TProp> AddSwitch<TProp>(string? shortName, string? longName)
		{
			if (shortName == null && longName == null)
			{
				throw new ArgumentException(string.Concat("Short Name and Long Name for a new switch for verb ", Name, " cannot both be null"));
			}
			Switch<TClass, TProp> thing = new Switch<TClass, TProp>(shortName, longName);
			AddToDictionary(ref shortName, ref longName, "switch", thing, allSwitches);
			return thing;
		}
		//public Value<TClass, TProp> AddValue()
		//{
		//
		//}
		//public MultiValue<TClass, TProp> AddMultiValue()
		//{
		//
		//}
		public IParseResult Parse(IEnumerable<string> args)
		{
			TClass parsedClass = new TClass();
			List<Error> errors = new List<Error>();
			HashSet<string> optionNames = new HashSet<string>(allOptions.Keys);
			HashSet<string> switchNames = new HashSet<string>(allSwitches.Keys);
			List<string> valuesFound = new List<string>();

			using (IEnumerator<string> aEnum = args.GetEnumerator())
			{
				while (aEnum.MoveNext())
				{
					string arg = aEnum.Current;
					// If the user asks for help, immediately stop parsing
					if (arg == config.ShortHelpSwitch || arg == config.LongHelpSwitch)
					{
						errors.Add(new Error(ErrorCode.HelpRequested, false));
						return new ParseResult<TClass>(errors);
					}

					if (allOptions.TryGetValue(arg, out IOption<TClass>? oval))
					{
						if (aEnum.MoveNext())
						{
							arg = aEnum.Current;
							// The option might only have a short or long name. However if both return false, that means we already saw it.
							if (optionNames.Remove(oval.ShortName) || optionNames.Remove(oval.LongName))
							{
								Error? error = oval.SetValue(parsedClass, arg);
								if (error != null)
								{
									errors.Add(error);
								}
							}
							else
							{
								errors.Add(new Error(ErrorCode.DuplicateOption, true, $"The Option {arg} appeared twice"));
							}
						}
						else
						{
							errors.Add(new Error(ErrorCode.UnexpectedEndOfArguments, true, $"Expected to find a value after Option {arg}, but found nothing"));
						}
					}
					else if (allSwitches.TryGetValue(arg, out ISwitch<TClass>? sval))
					{
						// The switch might only have a short or long name. However if both return false, that means we already saw it.
						if (switchNames.Remove(sval.ShortName) || switchNames.Remove(sval.LongName))
						{
							Error? error = sval.SetValue(parsedClass, string.Empty);
							if (error != null)
							{
								errors.Add(error);
							}
						}
						else
						{
							errors.Add(new Error(ErrorCode.DuplicateSwitch, true, $"The Switch {arg} appeared twice"));
						}
					}
					// If we're using many values, or if we're using a determined number and still have more to find, it's a value
					else if (MultiValue != null || valuesFound.Count < allValues.Count)
					{
						valuesFound.Add(arg);
					}
					else
					{
						errors.Add(new Error(ErrorCode.UnexpectedArgument, true, $"Found the string {arg} in an unexpected place"));
					}
				}
			}

			// Now here, it's a bit tricky. The Options/Switches/Values may or may not be required and we may not know this
			// until we've assigned everything. So we have no choice but to go ahead and assign everything null.
			// Then after that, we need to check all of their advanced rules, and if any are bad then return those.


			throw new NotImplementedException();
		}
		public ICollection<Error> Validate()
		{
			List<Error> errors = new List<Error>();
			foreach (IOption<TClass> value in allOptions.Values)
			{
				errors.AddRange(value.Validate());
			}
			foreach (ISwitch<TClass> value in allSwitches.Values)
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
		/// <summary>
		/// Makes sure that at least one of shortName and longName is not null, that they are not empty or whitespace, 
		/// applies any prefixes required, and adds them to our map to ensure they're unique.
		/// </summary>
		/// <param name="shortName">The short name</param>
		/// <param name="longName">The long name</param>
		private void AddToDictionary<T>(ref string? shortName, ref string? longName, string type, T obj, Dictionary<string, T> mapping)
		{
			if (shortName != null)
			{
				if (string.IsNullOrWhiteSpace(shortName))
				{
					throw new ArgumentException($"Short Name for {type} for verb {Name} was empty or entirely whitespace");
				}
				if (config.DefaultShortPrefix != null && !shortName.StartsWith(config.DefaultShortPrefix))
				{
					shortName = config.DefaultShortPrefix + shortName;
				}
				if (shortName == config.ShortHelpSwitch)
				{
					throw new ArgumentException($"Short Name for {type} for verb {Name} is already used by the short help switch ({config.ShortHelpSwitch})");
				}
				if (mapping.ContainsKey(shortName))
				{
					throw new ArgumentException($"The short name {shortName} for {type} for verb {Name} has already been used");
				}
				mapping.Add(shortName, obj);
			}
			if (longName != null)
			{
				if (string.IsNullOrWhiteSpace(longName))
				{
					throw new ArgumentException($"Short Name for {type} for verb {Name} was empty or entirely whitespace");
				}
				if (config.DefaultLongPrefix != null && !longName.StartsWith(config.DefaultLongPrefix))
				{
					longName = config.DefaultLongPrefix + longName;
				}
				if (longName == config.LongHelpSwitch)
				{
					throw new ArgumentException($"Long Name for {type} for verb {Name} is already used by the long help switch ({config.LongHelpSwitch})");
				}
				if (mapping.ContainsKey(longName))
				{
					throw new ArgumentException($"The long name {longName} for {type} for verb {Name} has already been used");
				}
				mapping.Add(longName, obj);
			}
		}
	}
}
