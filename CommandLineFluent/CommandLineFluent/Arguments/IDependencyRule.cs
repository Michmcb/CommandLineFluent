namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// Defines a single rule of a Dependency.
	/// </summary>
	/// <typeparam name="TClass">The class.</typeparam>
	public interface IDependencyRule<TClass> where TClass : new()
	{
		/// <summary>
		/// You don't need to call this; but this checks that the specified property of an object
		/// of type T satisfies the rule, given whether or not the Argument on which this rule was configured
		/// had a value appear during parsing or not.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <param name="didAppear">If a value appeared during parsing.</param>
		bool DoesSatifyRule(TClass obj, bool didAppear);
		/// <summary>
		/// The error message when this rule is violated.
		/// </summary>
		string ErrorMessage { get; }
		/// <summary>
		/// When this rule applies, the requiredness; either required or must not appear.
		/// </summary>
		DependencyRequiredness Requiredness { get; }
		/// <summary>
		/// Validates this rule. Throws an exception if something is invalid.
		/// </summary>
		void Validate();
	}
}
