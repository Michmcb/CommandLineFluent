namespace CommandLineFluent
{
	/// <summary>
	/// Represents an error case
	/// </summary>
	public enum ErrorCode
	{
		/// <summary>
		/// All is well, nothing went wrong
		/// </summary>
		Ok,
		/// <summary>
		/// A required value was specified but the user did not provide it
		/// </summary>
		MissingRequiredValue,
		/// <summary>
		/// Many required values were specified but the user did not provide at least 1
		/// </summary>
		MissingRequiredValues,
		/// <summary>
		/// A required option was specified but the user did not provide it
		/// </summary>
		MissingRequiredOption,
		/// <summary>
		/// The user specified more values than we were expecting
		/// </summary>
		TooManyValues,
		/// <summary>
		/// The user specified an option which we were not expecting
		/// </summary>
		OptionNotFound,
		/// <summary>
		/// The user specified a switch which we were not expecting
		/// </summary>
		SwitchNotFound,
		/// <summary>
		/// We were expecting to find something more, e.g. A value after an option, but we did not
		/// </summary>
		UnexpectedEndOfArguments,
		/// <summary>
		/// The user specified something we were not expecting
		/// </summary>
		UnexpectedArgument,
		/// <summary>
		/// The user specified an option multiple times
		/// </summary>
		DuplicateOption,
		/// <summary>
		/// The user specified a switch multiple times
		/// </summary>
		DuplicateSwitch,
		/// <summary>
		/// The value that the user provided for an option failed validation
		/// </summary>
		OptionFailedValidation,
		/// <summary>
		/// The value that the user provided for a value failed validation
		/// </summary>
		ValueFailedValidation,
		/// <summary>
		/// The values that the user provided for many values failed validation
		/// </summary>
		ValuesFailedValidation,
		/// <summary>
		/// The value that the user provided for an option failed conversion
		/// </summary>
		OptionFailedConversion,
		/// <summary>
		/// The value that the user provided for a value failed conversion
		/// </summary>
		ValueFailedConversion,
		/// <summary>
		/// The values that the user provided for many values failed conversion
		/// </summary>
		ValuesFailedConversion,
		/// <summary>
		/// The switch that the user specified failed conversion
		/// </summary>
		SwitchFailedConversion,
		/// <summary>
		/// The verb that the user specified was not a defined verb
		/// </summary>
		InvalidVerb,
		/// <summary>
		/// The user didn't specify a verb, but we were expecting one
		/// </summary>
		NoVerbFound,
		/// <summary>
		/// The user requested help
		/// </summary>
		HelpRequested,
		/// <summary>
		/// This error code value indicates a bug in your program, you'll only see this ErrorCode when an exception is raised.
		/// </summary>
		ProgrammerError,
		OptionMustNotBeProvided,
		MissingRequiredSwitch,
		SwitchMustNotBeProvided,
		ValueMustNotBeProvided,
		MissingRequiredManyValues,
		ManyValuesMustNotBeProvided
	}
}