using CommandLineFluent.Arguments;
using System;
using System.Collections.Generic;

namespace CommandLineFluent
{
	/// <summary>
	/// A single verb, for example, foo.exe add.
	/// Verbs have their own Options, Values, and Switches which they can parse and create an instance of <typeparamref name="T"/>
	/// </summary>
	public class FluentVerb<T> : IFluentVerb where T : class, new()
	{
		private readonly FluentParserConfig _config;
		private readonly List<IFluentValue> _fluentValues;
		private readonly List<IFluentSwitch> _fluentSwitches;
		private readonly List<IFluentOption> _fluentOptions;
		private readonly Dictionary<string, ArgumentNameAndType> _nameMapping;
		private readonly Dictionary<string, IFluentSettable<T, string>> _options;
		private readonly Dictionary<string, IFluentSettable<T, bool>> _switches;
		private List<IFluentSettable<T, string>> _values;
		private IFluentSettable<T, string[]> _manyValues;

		/// <summary>
		/// An indeterminate number of values
		/// </summary>
		public IFluentManyValues FluentManyValues => (IFluentManyValues)_manyValues;
		/// <summary>
		/// All Values which have been added to this verb
		/// </summary>
		public IReadOnlyCollection<IFluentValue> FluentValues => _fluentValues;
		/// <summary>
		/// All Switches which have been added to this verb
		/// </summary>
		public IReadOnlyCollection<IFluentSwitch> FluentSwitches => _fluentSwitches;
		/// <summary>
		/// All Options which have been added to this verb
		/// </summary>
		public IReadOnlyCollection<IFluentOption> FluentOptions => _fluentOptions;
		/// <summary>
		/// The custom function to use when displaying help information, if HelpText is null.
		/// </summary>
		public Func<IFluentVerb, string> HelpTextCreator { get; private set; }
		/// <summary>
		/// The custom function to use when displaying usage information, if UsageText is null.
		/// </summary>s
		public Func<IFluentVerb, string> UsageTextCreator { get; private set; }
		/// <summary>
		/// The object that the verb has successfully parsed. If no object has been parsed yet, this is the default
		/// value for an object of type <typeparamref name="T"/>. This is also reset to default when Reset is invoked.
		/// </summary>
		public T ParsedObject { get; private set; }
		/// <summary>
		/// The type of the object that this verb will provide the parsed the arguments to
		/// </summary>
		public Type TargetType { get; }
		/// <summary>
		/// The name used to invoke the verb. This must be unique.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Human-readable text that provides a description as to what this verb is used for. This is used when displaying Help.
		/// </summary>
		public string HelpText { get; private set; }
		/// <summary>
		/// Human-readable text that provides a description as to what this verb is used for. This is used when displaying Help.
		/// </summary>
		[Obsolete("This is no longer used, HelpText is instead. Use WithHelpText() to set the text that is displayed when writing help/usage information")]
		public string Description { get; private set; }
		/// <summary>
		/// True if the last parsing attempt was successful, false if it was unsuccessful. Null if the verb was not parsed.
		/// This is automatically reset each time you invoke Parse on a FluentParser that owns this verb
		/// </summary>
		public bool? Successful { get; private set; }
		internal FluentVerb(FluentParserConfig config, string name)
		{
			_fluentValues = new List<IFluentValue>();
			_fluentSwitches = new List<IFluentSwitch>();
			_fluentOptions = new List<IFluentOption>();
			_options = new Dictionary<string, IFluentSettable<T, string>>();
			_switches = new Dictionary<string, IFluentSettable<T, bool>>();
			_nameMapping = new Dictionary<string, ArgumentNameAndType>();
			_values = null;
			_manyValues = null;
			Name = name;
			_config = config;
			TargetType = typeof(T);
			HelpTextCreator = GetHelpTextDefault;
			UsageTextCreator = GetUsageTextDefault;
		}
		/// <summary>
		/// Returns errors if improperly configured, or an empty collection if all is well
		/// </summary>
		public ICollection<Error> Validate()
		{
			List<Error> errors = new List<Error>();
			foreach (KeyValuePair<string, IFluentSettable<T, string>> opt in _options)
			{
				errors.AddRange(opt.Value.Validate());
			}
			foreach (KeyValuePair<string, IFluentSettable<T, bool>> sw in _switches)
			{
				errors.AddRange(sw.Value.Validate());
			}
			if (_values != null)
			{
				foreach (IFluentSettable<T, string> value in _values)
				{
					errors.AddRange(value.Validate());
				}
			}
			else if (_manyValues != null)
			{
				errors.AddRange(_manyValues.Validate());
			}
			return errors;
		}
		/// <summary>
		/// Clears the Verb's parsed instance and sets Successful to null.
		/// </summary>
		public void Reset()
		{
			Successful = null;
			ParsedObject = default;
		}
		/// <summary>
		/// The help text to be used when dispalying help information.
		/// </summary>
		/// <param name="helpText">The help text</param>
		public void WithHelpText(string helpText)
		{
			HelpText = helpText;
		}
		/// <summary>
		/// Configures a description for this verb. This is used, by default, when writing help/usage information
		/// when the user did not specify a verb.
		/// </summary>
		/// <param name="description">The description</param>
		[Obsolete("This is no longer used. Instead, use WithHelpText() to set the text that is displayed when writing help/usage information")]
		public void WithDescription(string description)
		{
			Description = description;
		}
		/// <summary>
		/// Configures a custom help text creator
		/// </summary>
		/// <param name="helpTextCreator">This takes an instance of this verb, and should return a string which is the help text</param>
		public void WithHelpFormatter(Func<IFluentVerb, string> helpTextCreator)
		{
			HelpTextCreator = helpTextCreator;
		}
		/// <summary>
		/// Configures a custom usage text creator
		/// </summary>
		/// <param name="usageTextCreator">This takes an instance of this verb, and should return a string which is the help text</param>
		public void WithUsageFormatter(Func<IFluentVerb, string> usageTextCreator)
		{
			UsageTextCreator = usageTextCreator;
		}
		/// <summary>
		/// Creates a new Option and adds it to this verb. Its plain text value will be assigned to the target property.
		/// Either <paramref name="shortName"/> or <paramref name="longName"/> can be null to not use a short or long name, but both cannot be null.
		/// </summary>
		/// <param name="shortName">The short name for the Option</param>
		/// <param name="longName">The long name for the Option</param>
		public FluentOption<T, string> AddOption(string shortName, string longName = null)
		{
			return AddOption<string>(shortName, longName);
		}
		/// <summary>
		/// Creates a new Option and adds it to this verb. A converted value will be assigned to the target property,
		/// based on the converter with which the Option is configured.
		/// Either <paramref name="shortName"/> or <paramref name="longName"/> can be null to not use a short or long name, but both cannot be null.
		/// </summary>
		/// <param name="shortName">The short name for the Option</param>
		/// <param name="longName">The long name for the Option</param>
		public FluentOption<T, C> AddOption<C>(string shortName, string longName = null)
		{
			PrefixAndCheckAndMap(ref shortName, ref longName, FluentArgumentType.Option);
			FluentOption<T, C> f = new FluentOption<T, C>(shortName, longName);
			_fluentOptions.Add(f);
			_options.Add(shortName ?? longName, f);
			return f;
		}
		/// <summary>
		/// Creates a new Value and adds it to this verb. Its plain text value will be assigned to the target property.
		/// The order in which you add Values determines which properties get what values. The first Value added will use the first
		/// value found in the arguments, the second Value will get the next, etc.
		/// </summary>
		public FluentValue<T, string> AddValue()
		{
			return AddValue<string>();
		}
		/// <summary>
		/// Creates a new Value and adds it to this verb. A converted value will be assigned to the target property,
		/// based on the converter with which the Value is configured.
		/// The order in which you add Values determines which properties get what values. The first Value added will use the first
		/// value found in the arguments, the second Value will get the next, etc.
		/// </summary>
		public FluentValue<T, C> AddValue<C>()
		{
			if (_manyValues != null)
			{
				throw new InvalidOperationException("You cannot configure individual values if you have configured many values already");
			}
			if (_values == null)
			{
				_values = new List<IFluentSettable<T, string>>();
			}
			FluentValue<T, C> f = new FluentValue<T, C>();
			_fluentValues.Add(f);
			_values.Add(f);
			return f;
		}
		/// <summary>
		/// Creates a new Values and adds it to this verb. This Values will accept all arguments as plain text not recognizd as an Option or Switch as a Value.
		/// Be careful using this, anything not otherwise recognized as an Option or Switch will be parsed as a Value!
		/// You may want to call IgnorePrefixes as well, so it isn't too greedy.
		/// </summary>
		public FluentManyValues<T, string[]> AddManyValues()
		{
			return AddManyValues<string[]>();
		}
		/// <summary>
		/// Creates a new Values and adds it to this verb. This Values will accept all converted arguments not recognizd as an Option or Switch as a Value.
		/// Be careful using this, anything not otherwise recognized as an Option or Switch will be parsed as a Value!
		/// You may want to call IgnorePrefixes as well, so it isn't too greedy.
		/// </summary>
		public FluentManyValues<T, C> AddManyValues<C>()
		{
			if (_values != null)
			{
				throw new InvalidOperationException("You cannot configure many values if you have configured individual values already");
			}
			FluentManyValues<T, C> f = new FluentManyValues<T, C>();
			_manyValues = f;
			return f;
		}
		/// <summary>
		/// Creates a new Switch and adds it to this verb. True or False will be assigned to the target property,
		/// depending on whether or not the Switch is present
		/// Either <paramref name="shortName"/> or <paramref name="longName"/> can be null to not use a short or long name, but both cannot be null.
		/// </summary>
		/// <param name="shortName">The short name for the Switch</param>
		/// <param name="longName">The long name for the Switch</param>
		public FluentSwitch<T, bool> AddSwitch(string shortName, string longName = null)
		{
			return AddSwitch<bool>(shortName, longName);
		}
		/// <summary>
		/// Creates a new Switch and adds it to this verb. A converted value will be assigned to the target property,
		/// based on the converter with which the Switch is configured.
		/// Either <paramref name="shortName"/> or <paramref name="longName"/> can be null to not use a short or long name, but both cannot be null.
		/// </summary>
		/// <param name="shortName">The short name for the Switch</param>
		/// <param name="longName">The long name for the Switch</param>
		public FluentSwitch<T, C> AddSwitch<C>(string shortName, string longName = null)
		{
			PrefixAndCheckAndMap(ref shortName, ref longName, FluentArgumentType.Switch);
			FluentSwitch<T, C> f = new FluentSwitch<T, C>(shortName, longName);
			_fluentSwitches.Add(f);
			_switches.Add(shortName ?? longName, f);
			return f;
		}
		/// <summary>
		/// Parses the provided arguments using this Verb's rules. The verb's name should not be the first element of <paramref name="args"/>.
		/// Please check this and if necessary, skip the first one before invoking this.
		/// </summary>
		/// <param name="args">The arguments to parse</param>
		public IReadOnlyCollection<Error> Parse(IEnumerable<string> args)
		{
			Successful = null;
			T parsed = new T();
			List<Error> errors = new List<Error>();
			IReadOnlyDictionary<string, ArgumentNameAndType> names = _nameMapping;
			HashSet<string> remainingOptions = new HashSet<string>(_options.Keys);
			HashSet<string> remainingSwitches = new HashSet<string>(_switches.Keys);
			List<string> valuesFound = new List<string>();
			int totalValues = _values?.Count ?? -1;
			string shortHelp = _config.ShortHelpSwitch;
			string longHelp = _config.LongHelpSwitch;
			//char[] delimiters = parser.Config.Delimiters;

			bool go = true;
			using (IEnumerator<string> aEnum = args.GetEnumerator())
			{
				while (aEnum.MoveNext() && go)
				{
					string arg = aEnum.Current;
					// If the user asks for help, immediately stop parsing
					if (arg == shortHelp || arg == longHelp)
					{
						errors = new List<Error> { new Error(ErrorCode.HelpRequested, false) };
						parsed = null;
						return errors;
					}
					// TODO delimiters, so we can parse arguments with the = or : properly
					//if (delimiters.Length > 0)
					//{
					//	int index = arg.IndexOfAny(delimiters);
					//	if (index != -1)
					//	{
					//		// If the arg is 
					//	}
					//}

					if (names.TryGetValue(arg, out ArgumentNameAndType fanat))
					{
						// It's either a switch or an Option
						switch (fanat.Type)
						{
							case FluentArgumentType.Option:
								if (aEnum.MoveNext())
								{
									string argValue = aEnum.Current;
									Error oErr = Option(fanat.Name, parsed, argValue);
									if (oErr != null)
									{
										errors.Add(oErr);
										go = false;
									}
									if (!remainingOptions.Remove(fanat.Name))
									{
										errors.Add(new Error(ErrorCode.DuplicateOption, true, $"The Option {arg} appeared twice"));
									}
								}
								else
								{
									errors.Add(new Error(ErrorCode.UnexpectedEndOfArguments, true, $"Expected to find a value after Option {arg}, but found nothing"));
								}
								break;
							case FluentArgumentType.Switch:
								Error sErr = Switch(fanat.Name, parsed, true);
								if (sErr != null)
								{
									errors.Add(sErr);
									go = false;
								}
								if (!remainingSwitches.Remove(fanat.Name))
								{
									errors.Add(new Error(ErrorCode.DuplicateSwitch, true, $"The Switch {arg} appeared twice"));
								}
								break;
							default:
								System.Diagnostics.Debug.Assert(false, "Should never happen");
								break;
						}
					}
					// If we're using many values, or if we're using a determined number and still have more to find, it's a value
					else if (_manyValues != null || valuesFound.Count < totalValues)
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

			// Oh yeah it's very important we assign -everything- at least something. That sets their "GotValue" property correctly,
			// which is what allows us to calculate the Dependencies correctly.

			// For all of our remaining options, and values, give them null, which means set them to their default value
			// If they haven't been given a default value, they'll return an error
			foreach (string name in remainingOptions)
			{
				Error oErr = Option(name, parsed, null);
				if (oErr != null)
				{
					errors.Add(oErr);
					break;
				}
			}

			// Give it all of the values, and suck in all of the errors we had, if any
			errors.AddRange(Values(parsed, valuesFound.ToArray()));

			// For switches, their abscence means false.
			foreach (string name in remainingSwitches)
			{
				Error sErr = Switch(name, parsed, false);
				if (sErr != null)
				{
					errors.Add(sErr);
					break;
				}
			}

			if (_values != null)
			{
				// Evaluate all dependencies for the values
				foreach (IFluentSettable<T, string> value in _values)
				{
					Error err = value.EvaluateDependencies(parsed);
					if (err != null)
					{
						errors.Add(err);
					}
				}
			}
			// And all the options
			foreach (IFluentSettable<T, string> option in _options.Values)
			{
				Error err = option.EvaluateDependencies(parsed);
				if (err != null)
				{
					errors.Add(err);
				}
			}

			// And all dependencies for switches
			foreach (IFluentSettable<T, bool> swatch in _switches.Values)
			{
				// (switch is a keyword, swatch is close enough right?)
				Error err = swatch.EvaluateDependencies(parsed);
				if (err != null)
				{
					errors.Add(err);
				}
			}

			// And all dependencies for manyvalues
			if (_manyValues != null)
			{
				Error err = _manyValues.EvaluateDependencies(parsed);
				if (err != null)
				{
					errors.Add(err);
				}
			}

			ParsedObject = ((Successful = errors.Count == 0) == true) ? parsed : null;
			return errors;
		}
		/// <summary>
		/// Sets the property of <paramref name="target"/> to the value of <paramref name="value"/> (which may be converted first).
		/// </summary>
		/// <param name="name">The name of the Option which will set the property of <paramref name="target"/></param>
		/// <param name="target">The object whose property will be set</param>
		/// <param name="value">The value to provide to the property, which the Option may convert</param>
		internal Error Option(string name, T target, string value)
		{
			if (_options.TryGetValue(name, out IFluentSettable<T, string> theOption))
			{
				return theOption.SetValue(target, value);
			}
			return new Error(ErrorCode.OptionNotFound, false, $"Option with the name {name} for type {typeof(T)} was not found");
		}
		/// <summary>
		/// Sets the property of <paramref name="target"/> to the value of <paramref name="value"/> (which may be converted first).
		/// </summary>
		/// <param name="name">The name of the Switch which will set the property of <paramref name="target"/></param>
		/// <param name="target">The object whose property will be set</param>
		/// <param name="value">The value to provide to the property, which the Switch may convert</param>
		internal Error Switch(string name, T target, bool value)
		{
			if (_switches.TryGetValue(name, out IFluentSettable<T, bool> theSwitch))
			{
				return theSwitch.SetValue(target, value);
			}
			return new Error(ErrorCode.SwitchNotFound, false, $"Switch with the name {name} for type {typeof(T)} was not found");
		}
		/// <summary>
		/// Sets the properties of <paramref name="target"/> to the values of <paramref name="values"/> (which may be converted first).
		/// If ManyValues were configured, all of <paramref name="values"/> will be accepted. If not, and the length of <paramref name="values"/> is more than
		/// the number of configured Values, an Error is returned. If the length of <paramref name="values"/> is less than the number of configured Values,
		/// the remaining configured Values are assigned null, which may result in an Error if they are required.
		/// </summary>
		/// <param name="target">The object whose properties will be set</param>
		/// <param name="values">The values to provide to the property, which the Value or Values may convert</param>
		internal ICollection<Error> Values(T target, string[] values)
		{
			if (_values != null)
			{
				// We're only expecting at most, as many values as we were told about
				if (values.Length <= _values.Count)
				{
					List<Error> errors = new List<Error>(values.Length);
					int i = 0;
					foreach (string value in values)
					{
						IFluentSettable<T, string> theValue = _values[i++];
						Error error = theValue.SetValue(target, value);
						if (error != null)
						{
							errors.Add(error);
						}
					}
					// If there are some leftover, we have to assign them null. If they're required they'll return an error.
					for (; i < _values.Count; i++)
					{
						IFluentSettable<T, string> theValue = _values[i];
						Error error = theValue.SetValue(target, null);
						if (error != null)
						{
							errors.Add(error);
						}
					}
					return errors;
				}
				else
				{
					return new Error[] { new Error(ErrorCode.TooManyValues, true, $"Expected at most {_values.Count} values, but found {values.Length} values ({string.Join(",", values)})") };
				}
			}
			else if (_manyValues != null)
			{
				// We're expecting as many values as was provided
				Error error = _manyValues.SetValue(target, values);
				if (error != null)
				{
					return new Error[] { error };
				}
				return Array.Empty<Error>();
			}
			else if (values.Length > 0)
			{
				// We are expecting no values, so give back an error if we got any values at all
				return new Error[] { new Error(ErrorCode.TooManyValues, true, $"Expected 0 values, but found {values.Length} values ({string.Join(",", values)})") };
			}
			else
			{
				// Otherwise, we're all good
				return Array.Empty<Error>();
			}
		}
		/// <summary>
		/// Makes sure that at least one of shortName and longName is not null, that they are not empty or whitespace, 
		/// applies any prefixes required, and adds them to our map to ensure they're unique.
		/// </summary>
		/// <param name="shortName">The short name</param>
		/// <param name="longName">The long name</param>
		/// <param name="type">The type of the FluentArgument</param>
		private void PrefixAndCheckAndMap(ref string shortName, ref string longName, FluentArgumentType type)
		{
			if (shortName == null && longName == null)
			{
				throw new ArgumentException($"Short Name and Long Name for verb {Name ?? "parsing"} cannot both be null");
			}
			if (shortName != null)
			{
				if (string.IsNullOrWhiteSpace(shortName))
				{
					throw new ArgumentException($"Short Name for verb {Name ?? "parsing"} was empty or entirely whitespace");
				}
				// Apply the prefix if it doesn't already start with it. The user really shouldn't be prefixing them if they configured it though...
				if (_config.DefaultShortPrefix != null && !shortName.StartsWith(_config.DefaultShortPrefix))
				{
					shortName = _config.DefaultShortPrefix + shortName;
				}
				if (shortName == _config.ShortHelpSwitch)
				{
					throw new ArgumentException($"Short Name for verb {Name ?? "parsing"} is already used by the short help Switch ({_config.ShortHelpSwitch})");
				}
			}
			if (longName != null)
			{
				if (string.IsNullOrWhiteSpace(longName))
				{
					throw new ArgumentException($"Short Name for verb {Name ?? "parsing"} was empty or entirely whitespace");
				}
				if (_config.DefaultLongPrefix != null && !longName.StartsWith(_config.DefaultLongPrefix))
				{
					longName = _config.DefaultLongPrefix + longName;
				}
				if (longName == _config.ShortHelpSwitch)
				{
					throw new ArgumentException($"Long Name for verb {Name ?? "parsing"} is already used by the long help Switch ({_config.LongHelpSwitch})");
				}
			}
			// Callers can ask to parse any name they like, short or long.
			// We'll prefer using the short name, but will use the long name if only that is provided.
			if (shortName != null)
			{
				// Short name isn't null but long name might be null
				if (_nameMapping.ContainsKey(shortName))
				{
					throw new ArgumentException($"The short name {shortName} for verb {Name ?? "parsing"} has already been used");
				}
				if (longName != null)
				{
					// Both are not null, so map both of them to the short name
					if (_nameMapping.ContainsKey(longName))
					{
						throw new ArgumentException($"The long name {longName} for verb {Name ?? "parsing"} has already been used");
					}
					_nameMapping.Add(longName, new ArgumentNameAndType(shortName, type));
				}
				_nameMapping.Add(shortName, new ArgumentNameAndType(shortName, type));
			}
			else
			{
				// Short name is null, so we can logically say long name isn't null, due to the very first check we made
				if (_nameMapping.ContainsKey(longName))
				{
					throw new ArgumentException($"The long name {longName} for verb {Name ?? "parsing"} has already been used");
				}
				_nameMapping.Add(longName, new ArgumentNameAndType(longName, type));
			}
		}
		internal string GetUsageTextDefault(IFluentVerb verb)
		{
			return HelpFormatter.FormatVerbUsage(this, _config.ExeceuteCommand);
		}
		internal string GetHelpTextDefault(IFluentVerb verb)
		{
			return HelpFormatter.FormatVerbHelp(this, _config.MaxLineLength);
		}
	}
}
