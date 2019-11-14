namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// Defines a single rule of a FluentRelationship.
	/// </summary>
	/// <typeparam name="T">The class</typeparam>
	public interface IRelationshipRule<T> where T : new()
	{
		/// <summary>
		/// You don't need to call this; but this checks that the specified property of an object
		/// of type T satisfies the rule, given whether or not the FluentArgument on which this rule was configured
		/// had the string appear during parsing or not
		/// </summary>
		/// <param name="obj">The object</param>
		/// <param name="didAppear">If a string appeared during parsing</param>
		bool DoesSatifyRule(T obj, bool didAppear);
		/// <summary>
		/// The error message when this rule is violated
		/// </summary>
		string ErrorMessage { get; }
		Requiredness Requiredness { get; }
	}
}
