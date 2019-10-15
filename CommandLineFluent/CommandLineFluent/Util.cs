using CommandLineFluent.Arguments;
using System;
using System.Collections.Generic;

namespace CommandLineFluent
{
	/// <summary>
	/// Utility methods
	/// </summary>
	public static class Util
	{
		/// <summary>
		/// Makes a string like: -s|--long, to show the user both ways of writing the switch
		/// </summary>
		public static string ShortAndLongName(IFluentOption fluentOption)
		{
			return ShortAndLongName(fluentOption.ShortName, fluentOption.LongName);
		}
		/// <summary>
		/// Makes a string like: -s|--long, to show the user both ways of writing the switch
		/// </summary>
		public static string ShortAndLongName(IFluentSwitch fluentSwitch)
		{
			return ShortAndLongName(fluentSwitch.ShortName, fluentSwitch.LongName);
		}
		/// <summary>
		/// Makes a string like: -s|--long, to show the user both ways of writing the switch
		/// </summary>
		public static string ShortAndLongName(string shortName, string longName)
		{
			if (shortName != null && longName != null)
			{
				return $"{shortName}|{longName}";
			}
			return shortName ?? longName;
		}
		/// <summary>
		/// For each Error, if Error.ShouldBeShownToUser is true, invokes <paramref name="writeError"/> with Error.Message
		/// </summary>
		/// <param name="errors">The errors</param>
		/// <param name="writeError">The action to invoke to write Error.Message</param>
		public static void WriteErrors(IEnumerable<Error> errors, Action<string> writeError)
		{
			bool writeEndLine = false;
			foreach (Error err in errors)
			{
				if (err.ShouldBeShownToUser)
				{
					writeError(err.Message);
					writeEndLine = true;
				}
			}
			if (writeEndLine)
			{
				writeError("");
			}
		}
	}
}
