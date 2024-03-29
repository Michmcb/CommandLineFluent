﻿namespace CommandLineFluent.Arguments
{
	using System;

	/// <summary>
	/// An argument with a single value
	/// </summary>
	public sealed class Option<TClass, TProp> : IOption<TClass> where TClass : class, new()
	{
		public string? ShortName { get; }
		public string? LongName { get; }
		public string DescriptiveName { get; }
		public string HelpText { get; }
		public ArgumentRequired ArgumentRequired { get; }
		public Action<TClass, TProp> PropertySetter { get; }
		/// <summary>
		/// The default value to use when nothing is provided.
		/// </summary>
		public TProp DefaultValue { get; }
		/// <summary>
		/// Any dependencies upon other properties, if some have been set up. Otherwise, null.
		/// </summary>
		public Dependencies<TClass>? Dependencies { get; }
		/// <summary>
		/// Converts from a string into <typeparamref name="TProp"/>, or returns an error message.
		/// </summary>
		public Func<string, Converted<TProp, string>> Converter { get; }
		internal Option(string? shortName, string? longName, string descriptiveName, string helpText, ArgumentRequired argumentRequired, Action<TClass, TProp> propertySetter,
			TProp defaultValue, Dependencies<TClass>? dependencies, Func<string, Converted<TProp, string>> converter)
		{
			ShortName = shortName;
			LongName = longName;
			DescriptiveName = descriptiveName;
			HelpText = helpText;
			ArgumentRequired = argumentRequired;
			PropertySetter = propertySetter;
			DefaultValue = defaultValue;
			Dependencies = dependencies;
			Converter = converter;
		}
		public Error SetValue(TClass target, string? value)
		{
			if (value != null)
			{
				Converted<TProp, string> converted;
				try
				{
					converted = Converter.Invoke(value);
				}
				catch (Exception ex)
				{
					return new Error(ErrorCode.OptionFailedConversion, $"Converter for Option {DescriptiveName} threw an exception ({ex.ToString()})");
				}
				if (converted.Success(out TProp val, out string error))
				{
				PropertySetter(target, val);
				}
				else
				{
					return new Error(ErrorCode.OptionFailedConversion, error);
				}
			}
			else
			{
				if (ArgumentRequired == ArgumentRequired.Required)
				{
					return new Error(ErrorCode.MissingRequiredOption, $"Option {DescriptiveName} ({ArgUtils.ShortAndLongName(ShortName, LongName)}) is required and did not have a value provided");
				}
				else
				{
					PropertySetter(target, DefaultValue);
				}
			}
			return default;
		}
		/// <summary>
		/// Checks to make sure that all dependencies are respected. If they are not, returns an Error
		/// describing the first dependency that was violated.
		/// If no dependencies have been set up, returns null.
		/// </summary>
		/// <param name="obj">The object to check</param>
		public Error EvaluateDependencies(TClass obj, bool gotValue)
		{
			if (Dependencies == null)
			{
				return default;
			}
			return Dependencies.EvaluateRelationship(obj, gotValue, ArgumentType.Option);
		}
		/// <summary>
		/// The short and long name joined with a |
		/// </summary>
		public string ShortAndLongName()
		{
			return ArgUtils.ShortAndLongName(ShortName, LongName, ArgumentRequired != ArgumentRequired.Required);
		}
	}
}