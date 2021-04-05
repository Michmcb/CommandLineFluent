﻿namespace CommandLineFluent.Arguments
{
	public interface ISwitch : IArgument
	{
		/// <summary>
		/// The Short Name for this
		/// </summary>
		string? ShortName { get; }
		/// <summary>
		/// The Long Name for this
		/// </summary>
		string? LongName { get; }
		/// <summary>
		/// The short and long name joined with a |
		/// </summary>
		string ShortAndLongName();
	}
	/// <summary>
	/// A switch toggled on or off, supplied like -s
	/// </summary>
	public interface ISwitch<TClass> : ISwitch, IArgument<TClass> where TClass : class, new()
	{
		/// <summary>
		/// Sets a property of <paramref name="obj"/> to <paramref name="rawValue"/>, after conversion.
		/// There may or may not be a converter set up to translate <paramref name="rawValue"/> into something else.
		/// </summary>
		/// <param name="obj">The object on which to set a property</param>
		/// <param name="rawValue">The value to set the property to (before any conversion)</param>
		Error SetValue(TClass obj, string? rawValue);
	}
}