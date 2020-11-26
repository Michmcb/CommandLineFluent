namespace CommandLineFluent.Arguments
{
	using System;
	using System.ComponentModel;
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
		/// The target property's getter to use when evaluating this rule.
		/// </summary>
		public Func<TClass, TOtherProp> PropertyGetter { get; }
		/// <summary>
		/// The predicate used for this rule
		/// </summary>
		public Func<TOtherProp, bool> Predicate { get; private set; }
		/// <summary>
		/// The error message when this rule is violated.
		/// </summary>
		public string ErrorMessage { get; private set; }
		internal DependencyRule(Func<TClass, TOtherProp> propertyGetter, DependencyRequiredness required)
		{
			PropertyGetter = propertyGetter;
			Requiredness = required;
			Predicate = null!;
			ErrorMessage = string.Empty;
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
			Predicate = (x) => !Equals(x, value);
			return this;
		}
		/// <summary>
		/// Configures the dependency rule to apply when the property specified prior is null
		/// </summary>
		public DependencyRule<TClass, TOtherProp> IsNull()
		{
			ThrowIfPredicateNotNull();
			Predicate = (x) => x == null;
			return this;
		}
		/// <summary>
		/// Configures the dependency rule to apply when the property specified prior is not null
		/// </summary>
		public DependencyRule<TClass, TOtherProp> IsNotNull()
		{
			ThrowIfPredicateNotNull();
			Predicate = (x) => x != null;
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
		public bool DoesSatifyRule(TClass obj, bool didAppear)
		{
			// TargetProperty's value corresponds to the WhateverIf(e => e.Property) part they write.
			TOtherProp propertyVal = PropertyGetter(obj);
			// Requiredness depends on what they said. RequiredIf, OptionalIf, MustNotAppearIf.
			switch (Requiredness)
			{
				case DependencyRequiredness.Required:
					// In this case, RequiredIf(e => e.Property).Predicate();
					// Meaning it should be required if propertyVal satisfies the predicate.
					// If it was required, then it's true if it did appear, otherwise false.
					// If it wasn't required, then it's all good whether or not it appeared.
					return Predicate(propertyVal) ? didAppear : true;
				case DependencyRequiredness.MustNotAppear:
					// In this case, MustNotAppearIf(e => e.Property).Predicate();
					// Meaning it must not be provided if propertyVal satisfies the predicate
					// If mustNotAppear is false, we don't care if it appears or not. It doesn't mean "must appear", it just means the rule of "must not appear" does not apply.
					return Predicate(propertyVal) ? !didAppear : true;
			}
			return Predicate(propertyVal);
		}
		/// <summary>
		/// Validates this rule. Returns an Error if something is invalid, or null otherwise.
		/// </summary>
		public void Validate()
		{
			if (Predicate == null)
			{
				throw new CliParserBuilderException($"A rule that is {Requiredness} for a property of type {typeof(TOtherProp).Name} of class {typeof(TClass).Name} has no predicate. Probably missing a When, IsEqualTo, or IsNull call.");
			}
		}
		// This stuff is useless and just adds clutter, so hide it
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}
	}
}
