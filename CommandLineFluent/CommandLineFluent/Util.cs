using CommandLineFluent.Arguments;

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
	}
}
