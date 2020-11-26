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
		#region option
		/// <summary>
		/// User did not provide a required option
		/// </summary>
		MissingRequiredOption,
		/// <summary>
		/// An option is disallowed due to a dependency rule
		/// </summary>
		OptionNotAllowed,
		/// <summary>
		/// The user provided an option multiple times
		/// </summary>
		DuplicateOption,
		/// <summary>
		/// The user provided an option's short or long name, but no value after it
		/// </summary>
		OptionMissingValue,
		/// <summary>
		/// The value that the user provided for an option failed conversion
		/// </summary>
		OptionFailedConversion,
		#endregion

		#region switch
		/// <summary>
		/// User did not provide a required switch
		/// </summary>
		MissingRequiredSwitch,
		/// <summary>
		/// A switch is disallowed due to a dependency rule
		/// </summary>
		SwitchNotAllowed,
		/// <summary>
		/// The user provided a switch multiple times
		/// </summary>
		DuplicateSwitch,
		/// <summary>
		/// The switch that the user specified failed conversion
		/// </summary>
		SwitchFailedConversion,
		#endregion

		#region value
		/// <summary>
		/// User did not provide a required value
		/// </summary>
		MissingRequiredValue,
		/// <summary>
		/// A value is disallowed due to a dependency rule
		/// </summary>
		ValueNotAllowed,
		/// <summary>
		/// The user provided more values than we were expecting
		/// </summary>
		//TooManyValues,
		/// <summary>
		/// The value that the user provided for a value failed conversion
		/// </summary>
		ValueFailedConversion,
		#endregion

		#region multivalue
		/// <summary>
		/// User did not provide a required multivalue
		/// </summary>
		MissingRequiredMultiValue,
		/// <summary>
		/// A multivalue is disallowed due to a dependency rule
		/// </summary>
		MultiValueNotAllowed,
		/// <summary>
		/// One of the values provided for a multivalue failed conversion
		/// </summary>
		MultiValueFailedConversion,
		#endregion

		#region misc
		/// <summary>
		/// The user provided an argument we were not expecting
		/// </summary>
		UnexpectedArgument,
		/// <summary>
		/// The verb that the user specified was not a defined verb
		/// </summary>
		InvalidVerb,
		/// <summary>
		/// The user didn't specify a verb, but we were expecting one
		/// </summary>
		NoVerbFound,
		/// <summary>
		/// The object was successfully parsed, but the resultant object failed to be validated.
		/// </summary>
		ObjectFailedValidation,
		/// <summary>
		/// The user requested help
		/// </summary>
		HelpRequested,
		#endregion
	}
}