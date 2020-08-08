using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// Defines a single rule of a FluentRelationship.
	/// </summary>
	/// <typeparam name="T">The class</typeparam>
	/// <typeparam name="V">The value</typeparam>
	public class DependencyRule<T, V> : IDependencyRule<T> where T : new()
	{
		/// <summary>
		/// Does this rule say it's required, or must NOT appear
		/// </summary>
		public Requiredness Requiredness { get; }
		/// <summary>
		/// The target property to use when evaluating this rule
		/// </summary>
		public PropertyInfo TargetProperty { get; }
		/// <summary>
		/// If IsEqualTo is used, this is the value this target property's value is compared to
		/// </summary>
		public V Value { get; private set; }
		/// <summary>
		/// The predicate used for this rule
		/// </summary>
		public Func<V, bool> Predicate { get; private set; }
		/// <summary>
		/// This rule applies when the predicate returns this value
		/// </summary>
		public bool AppliesWhenPredicate { get; private set; }
		/// <summary>
		/// The error message when this rule is violated
		/// </summary>
		public string ErrorMessage { get; private set; }

		internal DependencyRule(Expression<Func<T, V>> expression, Requiredness required)
		{
			TargetProperty = ArgUtils.PropertyInfoFromExpression(expression);
			Requiredness = required;
			Predicate = null;
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
		public DependencyRule<T, V> IsEqualTo(V value)
		{
			ThrowIfPredicateNotNull();
			Value = value;
			AppliesWhenPredicate = true;
			Predicate = PredicateEquals;
			return this;
		}
		/// <summary>
		/// Configures the dependency rule to apply when the property specified prior is not equal to the provided value
		/// </summary>
		/// <param name="value">The value to which the property value must not equal for the rule to apply</param>
		public DependencyRule<T, V> IsNotEqualTo(V value)
		{
			ThrowIfPredicateNotNull();
			Value = value;
			AppliesWhenPredicate = false;
			Predicate = PredicateEquals;
			return this;
		}
		/// <summary>
		/// Configures the dependency rule to apply when the provided predicate evaluates to true
		/// </summary>
		/// <param name="predicate">The predicate which determines whether or not this rule applies</param>
		public DependencyRule<T, V> When(Func<V, bool> predicate)
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
		public DependencyRule<T, V> WhenNot(Func<V, bool> predicate)
		{
			ThrowIfPredicateNotNull();
			Predicate = predicate;
			AppliesWhenPredicate = false;
			return this;
		}
		/// <summary>
		/// Configures the dependency rule to apply when the property specified prior is null
		/// </summary>
		public DependencyRule<T, V> IsNull()
		{
			ThrowIfPredicateNotNull();
			Predicate = PredicateNull;
			AppliesWhenPredicate = true;
			return this;
		}
		/// <summary>
		/// Configures the dependency rule to apply when the property specified prior is not null
		/// </summary>
		public DependencyRule<T, V> IsNotNull()
		{
			ThrowIfPredicateNotNull();
			Predicate = PredicateNull;
			AppliesWhenPredicate = false;
			return this;
		}
		/// <summary>
		/// Configures the error message to show when this rule is violated
		/// </summary>
		/// <param name="errorMessage"></param>
		public DependencyRule<T, V> WithErrorMessage(string errorMessage)
		{
			ErrorMessage = errorMessage;
			return this;
		}
		/// <summary>
		/// You don't need to call this; but this checks that the specified property of an object
		/// of type T satisfies the rule, given whether or not the FluentArgument on which this rule was configured
		/// had the string appear during parsing or not
		/// </summary>
		/// <param name="obj">The object</param>
		/// <param name="didAppear">If a string appeared during parsing</param>
		public bool DoesSatifyRule(T obj, bool didAppear)
		{
			// TargetProperty's value corresponds to the WhateverIf(e => e.Property) part they write.
			V propertyVal = (V)TargetProperty.GetValue(obj);
			// Requiredness depends on what they said. RequiredIf, OptionalIf, MustNotAppearIf.
			switch (Requiredness)
			{
				case Requiredness.Required:
					// In this case, RequiredIf(e => e.Property).Predicate();
					// Meaning it should be required if propertyVal satisfies the predicate.
					bool isRequired = AppliesWhenPredicate == Predicate(propertyVal);
					// If it was required, then it's true if it did appear, otherwise false.
					// If it wasn't required, then it's all good whether or not it appeared.
					return isRequired ? didAppear : true;
				case Requiredness.MustNotAppear:
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
		public Error Validate()
		{
			if (Predicate == null)
			{
				return new Error(ErrorCode.ProgrammerError, $@"A rule that is {Requiredness} for the property {TargetProperty.Name} has no predicate. Probably missing a When, IsEqualTo, or IsNull call.");
			}
			return default;
		}
		private bool PredicateEquals(V val)
		{
			return Equals(Value, val);
		}
		private bool PredicateNull(V val)
		{
			return val == null;
		}
	}
}
