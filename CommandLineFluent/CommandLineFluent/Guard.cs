namespace CommandLineFluent
{
	using System;
	public static class Guard
	{
		public static void ThrowIfNull<T>(T obj, string paramName)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(paramName, paramName + " cannot be null.");
			}
		}
	}
}
