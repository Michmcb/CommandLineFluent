namespace CommandLineFluent.Arguments
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Reflection;
	/// <summary>
	/// Defines a single rule of a Dependency.
	/// </summary>
	/// <typeparam name="TClass">The class.</typeparam>
	/// <typeparam name="TOtherProp">The type of the other property.</typeparam>
	public sealed class DependencyRule<TClass, TOtherProp> : IDependencyRule<TClass> where TClass : new()
	{
		/// <summary>
		/// When this rule applies, the requiredness; either required or must not appear.
		/// </summary>
		public DependencyRequiredness Requiredness { get; }
		/// <summary>
		/// The target property to use when evaluating this rule.
		/// </summary>
		public PropertyInfo TargetProperty { get; }
		/// <summary>
		/// The predicate used for this rule
		/// </summary>
		public Func<TOtherProp, bool> Predicate { get; private set; }
		/// <summary>
		/// This rule applies when the predicate returns this value.
		/// </summary>
		public bool AppliesWhenPredicate { get; private set; }
		/// <summary>
		/// The error message when this rule is violated.
		/// </summary>
		public string ErrorMessage { get; private set; }
		internal DependencyRule([DisallowNull] PropertyInfo targetProperty, [DisallowNull] DependencyRequiredness required)
		{
			TargetProperty = targetProperty;
			Requiredness = required;
			Predicate = null!;
			ErrorMessage = "";
		}
		/// <summary>
		/// A helper validation method that throws an exception if a DependencyRule gets configured twice
		/// </summary>
		public void ThrowIfPredicateNotNull()
		{
			if (Predicate != null)
			{
				throw new InvalidOperationException("This rule has already been configured");
			}
		}
		/// <summary>
		/// Configures the dependency rule to apply when the property specified prior is equal to the provided value
		/// </summary>
		/// <param name="value">The value to which the property value has to equal for the rule to apply</param>
		public DependencyRule<TClass, TOtherProp> IsEqualTo(TOtherProp value)
		{
			ThrowIfPredicateNotNull();
			AppliesWhenPredicate = true;
			Predicate = (x) => Equals(x, value);
			return this;
		}
		/// <summary>
		/// Configures the dependency rule to apply when the property specified prior is not equal to the provided value
		/// </summary>
		/// <param name="value">The value to which the property value must not equal for the rule to apply</param>
		public DependencyRule<TClass, TOtherProp> IsNotEqualTo(TOtherProp value)
		{
			ThrowIfPredicateNotNull();
			AppliesWhenPredicate = true;
			Predicate = (x) => !Equals(x, value);
			return this;
		}
		/// <summary>
		/// Configures the dependency rule to apply when the provided predicate evaluates to true
		/// </summary>
		/// <param name="predicate">The predicate which determines whether or not this rule applies</param>
		public DependencyRule<TClass, TOtherProp> When(Func<TOtherProp, bool> predicate)
		{
			ThrowIfPredicateNotNull();
			Predicate = predicate;
			AppliesWhenPredicate = true;
			return this;
		}
		/// <summary>
		/// Configures the dependency rule to apply when the provided predicate evaluates to false
		/// </summary>
		/// <param name="predicate">The predicate which determines whether or not this rule applies</param>
		public DependencyRule<TClass, TOtherProp> WhenNot(Func<TOtherProp, bool> predicate)
		{
			ThrowIfPredicateNotNull();
			Predicate = predicate;
			AppliesWhenPredicate = false;
			return this;
		}
		/// <summary>
		/// Configures the dependency rule to apply when the property specified prior is null
		/// </summary>
		public DependencyRule<TClass, TOtherProp> IsNull()
		{
			ThrowIfPredicateNotNull();
			Predicate = PredicateNull;
			AppliesWhenPredicate = true;
			return this;
		}
		/// <summary>
		/// Configures the dependency rule to apply when the property specified prior is not null
		/// </summary>
		public DependencyRule<TClass, TOtherProp> IsNotNull()
		{
			ThrowIfPredicateNotNull();
			Predicate = PredicateNotNull;
			AppliesWhenPredicate = true;
			return this;
		}
		/// <summary>
		/// Configures the error message to show when this rule is violated.
		/// </summary>
		public DependencyRule<TClass, TOtherProp> WithErrorMessage(string errorMessage)
		{
			ErrorMessage = errorMessage;
			return this;
		}
		/// <summary>
		/// You don't need to call this; but this checks that the specified property of an object
		/// of type T satisfies the rule, given whether or not the Argument on which this rule was configured
		/// had a value appear during parsing or not.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <param name="didAppear">If a value appeared during parsing.</param>
		public bool DoesSatifyRule([DisallowNull] TClass obj, bool didAppear)
		{
			// TargetProperty's value corresponds to the WhateverIf(e => e.Property) part they write.
			TOtherProp propertyVal = (TOtherProp)TargetProperty.GetValue(obj);
			// Requiredness depends on what they said. RequiredIf, OptionalIf, MustNotAppearIf.
			switch (Requiredness)
			{
				case DependencyRequiredness.Required:
					// In this case, RequiredIf(e => e.Property).Predicate();
					// Meaning it should be required if propertyVal satisfies the predicate.
					bool isRequired = AppliesWhenPredicate == Predicate(propertyVal);
					// If it was required, then it's true if it did appear, otherwise false.
					// If it wasn't required, then it's all good whether or not it appeared.
					return isRequired ? didAppear : true;
				case DependencyRequiredness.MustNotAppear:
					// In this case, MustNotAppearIf(e => e.Property).Predicate();
					// Meaning it must not be provided if propertyVal satisfies the predicate
					bool mustNotAppear = AppliesWhenPredicate == Predicate(propertyVal);
					// If mustNotAppear is false, we don't care if it appears or not. It doesn't mean "must appear", it just means the rule of "must not appear" does not apply.
					return mustNotAppear ? !didAppear : true;
			}
			return AppliesWhenPredicate == Predicate(propertyVal);
		}
		/// <summary>
		/// Validates this rule. Returns an Error if something is invalid, or null otherwise.
		/// </summary>
		public void Validate()
		{
			// TODO DependencyRule.Validate() should instead be replaced with a Build() method, and that throws an exception instead
			if (Predicate == null)
			{
				throw new CliParserBuilderException($@"A rule that is {Requiredness} for the property {TargetProperty.Name} has no predicate. Probably missing a When, IsEqualTo, or IsNull call.");
			}
		}
		private bool PredicateNull(TOtherProp val)
		{
			return val == null;
		}
		private bool PredicateNotNull(TOtherProp val)
		{
			return val != null;
		}
	}
}
