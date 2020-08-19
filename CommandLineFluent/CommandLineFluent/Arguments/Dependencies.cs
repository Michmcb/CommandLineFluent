namespace CommandLineFluent.Arguments
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq.Expressions;
	/// <summary>
	/// Defines a relationship between an Argument and a property of the target object.
	/// It allows you to specify that certain Arguments are only required under certain circumstances.
	/// </summary>
	/// <typeparam name="TClass">The class of the property which this set of Dependencies is for.</typeparam>
	/// <typeparam name="TProp">The type of the property.</typeparam>
	public sealed class Dependencies<TClass, TProp> where TClass : new()
	{
		private readonly List<IDependencyRule<TClass>> rules;
		/// <summary>
		/// The rules which make up this relationship
		/// </summary>
		public IReadOnlyCollection<IDependencyRule<TClass>> Rules => rules;
		internal Dependencies()
		{
			rules = new List<IDependencyRule<TClass>>();
		}
		/// <summary>
		/// Specifies that the property <paramref name="property"/> of an object of type <typeparamref name="TClass"/>
		/// is required to be provided under specific circumstances.
		/// </summary>
		/// <typeparam name="TOtherProp">The type of the <paramref name="property"/>.</typeparam>
		/// <param name="property">The rule stipulates this Argument is required if <paramref name="property"/> has a certain value.</param>
		public DependencyRule<TClass, TOtherProp> RequiredIf<TOtherProp>(Expression<Func<TClass, TOtherProp>> property)
		{
			DependencyRule<TClass, TOtherProp> rule = new DependencyRule<TClass, TOtherProp>(ArgUtils.PropertyInfoFromExpression(property), DependencyRequiredness.Required);
			rules.Add(rule);
			return rule;
		}
		/// <summary>
		/// Specifies that a property of type <typeparamref name="TOtherProp"/> of an object of type <typeparamref name="TClass"/>
		/// is required to NOT be provided under specific circumstances.
		/// </summary>
		/// <typeparam name="TOtherProp">The type of the property</typeparam>
		/// <param name="property">The rule stipulates this Argument must not appear if <paramref name="property"/> has a certain value.</param>
		public DependencyRule<TClass, TOtherProp> MustNotAppearIf<TOtherProp>(Expression<Func<TClass, TOtherProp>> property)
		{
			DependencyRule<TClass, TOtherProp> rule = new DependencyRule<TClass, TOtherProp>(ArgUtils.PropertyInfoFromExpression(property), DependencyRequiredness.MustNotAppear);
			rules.Add(rule);
			return rule;
		}
		/// <summary>
		/// Validates this rule. Throws an exception when something's wrong.
		/// </summary>
		public void Validate()
		{
			foreach (IDependencyRule<TClass> rule in rules)
			{
				rule.Validate();
			}
		}
		/// <summary>
		/// Returns null if all rules of the relationship have been respected, and an Error otherwise.
		/// </summary>
		/// <param name="obj">The object to check</param>
		/// <param name="wasValueProvided">Whether or not the FluentArgument received a value from parsing</param>
		/// <param name="fluentArgumentType">The type of argument, used to return the correct error code</param>
		internal Error EvaluateRelationship(TClass obj, bool wasValueProvided, ArgumentType fluentArgumentType)
		{
			foreach (IDependencyRule<TClass> rule in rules)
			{
				if (!rule.DoesSatifyRule(obj, wasValueProvided))
				{
					ErrorCode errorCode = 0;
					switch (fluentArgumentType)
					{
						case ArgumentType.Option:
							switch (rule.Requiredness)
							{
								case DependencyRequiredness.Required:
									errorCode = ErrorCode.MissingRequiredOption;
									break;
								case DependencyRequiredness.MustNotAppear:
									errorCode = ErrorCode.OptionNotAllowed;
									break;
							}
							break;
						case ArgumentType.Switch:
							switch (rule.Requiredness)
							{
								case DependencyRequiredness.Required:
									errorCode = ErrorCode.MissingRequiredSwitch;
									break;
								case DependencyRequiredness.MustNotAppear:
									errorCode = ErrorCode.SwitchNotAllowed;
									break;
							}
							break;
						case ArgumentType.Value:
							switch (rule.Requiredness)
							{
								case DependencyRequiredness.Required:
									errorCode = ErrorCode.MissingRequiredValue;
									break;
								case DependencyRequiredness.MustNotAppear:
									errorCode = ErrorCode.ValueNotAllowed;
									break;
							}
							break;
						case ArgumentType.MultiValue:
							switch (rule.Requiredness)
							{
								case DependencyRequiredness.Required:
									errorCode = ErrorCode.MissingRequiredMultiValue;
									break;
								case DependencyRequiredness.MustNotAppear:
									errorCode = ErrorCode.MultiValueNotAllowed;
									break;
							}
							break;
					}

					return new Error(errorCode, rule.ErrorMessage);
				}
			}
			return default;
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
