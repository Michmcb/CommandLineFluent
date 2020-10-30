namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Arguments.Config;
	using System;
	using System.Linq.Expressions;
	using static Converters;
	public sealed partial class Verb<TClass> : IVerb where TClass : class, new()
	{
		public Switch<TClass, bool> AddSwitch(Expression<Func<TClass, bool>> expression, Action<NamedArgConfig<TClass, bool, bool>> config)
		{
			var obj = new NamedArgConfig<TClass, bool, bool>(false, NoConversion);
			config(obj);
			return AddSwitchCore(expression, obj);
		}
	}
}
