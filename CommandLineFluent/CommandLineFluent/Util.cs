using CommandLineFluent.Arguments;

namespace CommandLineFluent
{
	public static class Util
	{
		public static string ShortAndLongName(IFluentOption fluentOption)
		{
			return ShortAndLongName(fluentOption.ShortName, fluentOption.LongName);
		}
		public static string ShortAndLongName(IFluentSwitch fluentSwitch)
		{
			return ShortAndLongName(fluentSwitch.ShortName, fluentSwitch.LongName);
		}
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
