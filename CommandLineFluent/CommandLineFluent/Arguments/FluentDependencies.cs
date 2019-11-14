using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CommandLineFluent.Arguments
{
	/// <summary>
	/// Defines a relationship between a FluentArgument and a property of the target object.
	/// It allows you to specify that certain FluentArguments are only required under certain circumstances.
	/// </summary>
	/// <typeparam name="T">The class of the property which this FluentRelationship is for</typeparam>
	/// <typeparam name="C">The type of the property</typeparam>
	public class FluentDependencies<T, C> where T : new()
	{
		private readonly List<IRelationshipRule<T>> rules;
		/// <summary>
		/// The rules which make up this relationship
		/// </summary>
		public IReadOnlyCollection<IRelationshipRule<T>> Rules => rules;
		internal FluentDependencies()
		{
			rules = new List<IRelationshipRule<T>>();
		}
		/// <summary>
		/// Specifies that a property of type <typeparamref name="V"/> of an object of type <typeparamref name="T"/>
		/// is required to be provided under specific circumstances.
		/// </summary>
		/// <typeparam name="V">The type of the property</typeparam>
		/// <param name="expression">The property that is required</param>
		public DependencyRule<T, V> RequiredIf<V>(Expression<Func<T, V>> expression)
		{
			DependencyRule<T, V> rule = new DependencyRule<T, V>(expression, Requiredness.Required);
			rules.Add(rule);
			return rule;
		}
		/// <summary>
		/// Specifies that a property of type <typeparamref name="V"/> of an object of type <typeparamref name="T"/>
		/// is required to NOT be provided under specific circumstances.
		/// </summary>
		/// <typeparam name="V">The type of the property</typeparam>
		/// <param name="expression">The property that is required</param>
		public DependencyRule<T, V> MustNotAppearIf<V>(Expression<Func<T, V>> expression)
		{
			DependencyRule<T, V> rule = new DependencyRule<T, V>(expression, Requiredness.MustNotAppear);
			rules.Add(rule);
			return rule;
		}
		/// <summary>
		/// Returns null if all rules of the relationship have been respected, and an Error otherwise.
		/// </summary>
		/// <param name="obj">The object to check</param>
		/// <param name="wasValueProvided">Whether or not the FluentArgument received a value from parsing</param>
		/// <param name="fluentArgumentType">The type of argument, used to return the correct error code</param>
		internal Error EvaluateRelationship(T obj, bool wasValueProvided, FluentArgumentType fluentArgumentType)
		{
			foreach (IRelationshipRule<T> rule in rules)
			{
				if (!rule.DoesSatifyRule(obj, wasValueProvided))
				{
					ErrorCode errorCode = 0;
					switch (fluentArgumentType)
					{
						case FluentArgumentType.Option:
							switch (rule.Requiredness)
							{
								case Requiredness.Required:
									errorCode = ErrorCode.MissingRequiredOption;
									break;
								case Requiredness.MustNotAppear:
									errorCode = ErrorCode.OptionMustNotBeProvided;
									break;
							}
							break;
						case FluentArgumentType.Switch:
							switch (rule.Requiredness)
							{
								case Requiredness.Required:
									errorCode = ErrorCode.MissingRequiredSwitch;
									break;
								case Requiredness.MustNotAppear:
									errorCode = ErrorCode.SwitchMustNotBeProvided;
									break;
							}
							break;
						case FluentArgumentType.Value:
							switch (rule.Requiredness)
							{
								case Requiredness.Required:
									errorCode = ErrorCode.MissingRequiredValue;
									break;
								case Requiredness.MustNotAppear:
									errorCode = ErrorCode.ValueMustNotBeProvided;
									break;
							}
							break;
						case FluentArgumentType.ManyValues:
							switch (rule.Requiredness)
							{
								case Requiredness.Required:
									errorCode = ErrorCode.MissingRequiredManyValues;
									break;
								case Requiredness.MustNotAppear:
									errorCode = ErrorCode.ManyValuesMustNotBeProvided;
									break;
							}
							break;
					}

					return new Error(errorCode, true, rule.ErrorMessage);
				}
			}
			return null;
		}
	}
}
