namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// Defines a single rule of a FluentRelationship.
	/// </summary>
	/// <typeparam name="T">The class</typeparam>
	public interface IDependencyRule<T> where T : new()
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
		/// <summary>
		/// Does this rule say it's required, or must NOT appear
		/// </summary>
		Requiredness Requiredness { get; }
		/// <summary>
		/// Validates this rule. Returns an Error if something is invalid, or null otherwise.
		/// </summary>
		Error Validate();
	}
}
