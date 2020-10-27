namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Arguments.Config;
	using System;
	using System.Linq.Expressions;
	public sealed partial class Verb<TClass> : IVerb where TClass : class, new()
	{
		public Switch<TClass, bool> AddSwitch(Expression<Func<TClass, bool>> expression, Action<NamedArgConfig<TClass, bool, bool>> config)
		{
			var obj = new NamedArgConfig<TClass, bool, bool>(false, null);
			config(obj);
			return AddSwitchCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
		public Switch<TClass, TProp> AddSwitch<TProp>(Expression<Func<TClass, TProp>> expression, Action<NamedArgConfig<TClass, TProp, bool>> config)
		{
			var obj = new NamedArgConfig<TClass, TProp, bool>(false, null);
			config(obj);
			return AddSwitchCore(obj, ArgUtils.PropertyInfoFromExpression(expression));
		}
	}
}
