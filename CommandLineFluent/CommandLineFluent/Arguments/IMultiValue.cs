namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// Captures all lone values, such as: Foo.exe Value1 -t someswitch Value2 Value3. All 3 values there would be captured as an array: [Value1, Value2, Value3].
	/// Use this when you want to consume an indeterminate number of values, regardless of their position.
	/// </summary>
	public interface IMultiValue<TClass> : IArgument<TClass>
	{
		/// <summary>
		/// The prefixes that are ignored when capturing values. This is useful if you use a consistent prefixing scheme,
		/// and want to avoid capturing possible user typos; an error will be thrown instead of using it as a value.
		/// </summary>
		System.Collections.Generic.IReadOnlyCollection<string> IgnoredPrefixes { get; }
	}
}