namespace CommandLineFluent
{
	using CommandLineFluent.Arguments;
	using CommandLineFluent.Arguments.Config;
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using static Converters;
	using static Accumulators;
	public sealed partial class Verb<TClass> : IVerb where TClass : class, new()
	{
		/// <summary>
		/// Adds a new MultiValue to set the string[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string, string[]> AddMultiValue(Expression<Func<TClass, string[]>> expression, Action<NamelessMultiArgConfig<TClass, string, string[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string, string[]>(false, NoConversion, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;string&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string, List<string>> AddMultiValue(Expression<Func<TClass, List<string>>> expression, Action<NamelessMultiArgConfig<TClass, string, List<string>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string, List<string>>(false, NoConversion, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;string&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string, IList<string>> AddMultiValue(Expression<Func<TClass, IList<string>>> expression, Action<NamelessMultiArgConfig<TClass, string, IList<string>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string, IList<string>>(false, NoConversion, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;string&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string, IReadOnlyList<string>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<string>>> expression, Action<NamelessMultiArgConfig<TClass, string, IReadOnlyList<string>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string, IReadOnlyList<string>>(false, NoConversion, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;string&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string, ICollection<string>> AddMultiValue(Expression<Func<TClass, ICollection<string>>> expression, Action<NamelessMultiArgConfig<TClass, string, ICollection<string>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string, ICollection<string>>(false, NoConversion, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;string&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string, IReadOnlyCollection<string>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<string>>> expression, Action<NamelessMultiArgConfig<TClass, string, IReadOnlyCollection<string>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string, IReadOnlyCollection<string>>(false, NoConversion, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;string&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string, IEnumerable<string>> AddMultiValue(Expression<Func<TClass, IEnumerable<string>>> expression, Action<NamelessMultiArgConfig<TClass, string, IEnumerable<string>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string, IEnumerable<string>>(false, NoConversion, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;string&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string, HashSet<string>> AddMultiValue(Expression<Func<TClass, HashSet<string>>> expression, Action<NamelessMultiArgConfig<TClass, string, HashSet<string>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string, HashSet<string>>(false, NoConversion, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;string&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string, Stack<string>> AddMultiValue(Expression<Func<TClass, Stack<string>>> expression, Action<NamelessMultiArgConfig<TClass, string, Stack<string>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string, Stack<string>>(false, NoConversion, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;string&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string, Queue<string>> AddMultiValue(Expression<Func<TClass, Queue<string>>> expression, Action<NamelessMultiArgConfig<TClass, string, Queue<string>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string, Queue<string>>(false, NoConversion, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the short[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short, short[]> AddMultiValue(Expression<Func<TClass, short[]>> expression, Action<NamelessMultiArgConfig<TClass, short, short[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short, short[]>(false, ToShort, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;short&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short, List<short>> AddMultiValue(Expression<Func<TClass, List<short>>> expression, Action<NamelessMultiArgConfig<TClass, short, List<short>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short, List<short>>(false, ToShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;short&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short, IList<short>> AddMultiValue(Expression<Func<TClass, IList<short>>> expression, Action<NamelessMultiArgConfig<TClass, short, IList<short>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short, IList<short>>(false, ToShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;short&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short, IReadOnlyList<short>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<short>>> expression, Action<NamelessMultiArgConfig<TClass, short, IReadOnlyList<short>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short, IReadOnlyList<short>>(false, ToShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;short&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short, ICollection<short>> AddMultiValue(Expression<Func<TClass, ICollection<short>>> expression, Action<NamelessMultiArgConfig<TClass, short, ICollection<short>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short, ICollection<short>>(false, ToShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;short&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short, IReadOnlyCollection<short>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<short>>> expression, Action<NamelessMultiArgConfig<TClass, short, IReadOnlyCollection<short>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short, IReadOnlyCollection<short>>(false, ToShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;short&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short, IEnumerable<short>> AddMultiValue(Expression<Func<TClass, IEnumerable<short>>> expression, Action<NamelessMultiArgConfig<TClass, short, IEnumerable<short>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short, IEnumerable<short>>(false, ToShort, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;short&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short, HashSet<short>> AddMultiValue(Expression<Func<TClass, HashSet<short>>> expression, Action<NamelessMultiArgConfig<TClass, short, HashSet<short>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short, HashSet<short>>(false, ToShort, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;short&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short, Stack<short>> AddMultiValue(Expression<Func<TClass, Stack<short>>> expression, Action<NamelessMultiArgConfig<TClass, short, Stack<short>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short, Stack<short>>(false, ToShort, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;short&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short, Queue<short>> AddMultiValue(Expression<Func<TClass, Queue<short>>> expression, Action<NamelessMultiArgConfig<TClass, short, Queue<short>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short, Queue<short>>(false, ToShort, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ushort[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort, ushort[]> AddMultiValue(Expression<Func<TClass, ushort[]>> expression, Action<NamelessMultiArgConfig<TClass, ushort, ushort[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort, ushort[]>(false, ToUShort, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;ushort&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort, List<ushort>> AddMultiValue(Expression<Func<TClass, List<ushort>>> expression, Action<NamelessMultiArgConfig<TClass, ushort, List<ushort>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort, List<ushort>>(false, ToUShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;ushort&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort, IList<ushort>> AddMultiValue(Expression<Func<TClass, IList<ushort>>> expression, Action<NamelessMultiArgConfig<TClass, ushort, IList<ushort>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort, IList<ushort>>(false, ToUShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;ushort&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort, IReadOnlyList<ushort>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<ushort>>> expression, Action<NamelessMultiArgConfig<TClass, ushort, IReadOnlyList<ushort>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort, IReadOnlyList<ushort>>(false, ToUShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;ushort&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort, ICollection<ushort>> AddMultiValue(Expression<Func<TClass, ICollection<ushort>>> expression, Action<NamelessMultiArgConfig<TClass, ushort, ICollection<ushort>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort, ICollection<ushort>>(false, ToUShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;ushort&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort, IReadOnlyCollection<ushort>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<ushort>>> expression, Action<NamelessMultiArgConfig<TClass, ushort, IReadOnlyCollection<ushort>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort, IReadOnlyCollection<ushort>>(false, ToUShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;ushort&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort, IEnumerable<ushort>> AddMultiValue(Expression<Func<TClass, IEnumerable<ushort>>> expression, Action<NamelessMultiArgConfig<TClass, ushort, IEnumerable<ushort>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort, IEnumerable<ushort>>(false, ToUShort, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;ushort&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort, HashSet<ushort>> AddMultiValue(Expression<Func<TClass, HashSet<ushort>>> expression, Action<NamelessMultiArgConfig<TClass, ushort, HashSet<ushort>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort, HashSet<ushort>>(false, ToUShort, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;ushort&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort, Stack<ushort>> AddMultiValue(Expression<Func<TClass, Stack<ushort>>> expression, Action<NamelessMultiArgConfig<TClass, ushort, Stack<ushort>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort, Stack<ushort>>(false, ToUShort, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;ushort&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort, Queue<ushort>> AddMultiValue(Expression<Func<TClass, Queue<ushort>>> expression, Action<NamelessMultiArgConfig<TClass, ushort, Queue<ushort>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort, Queue<ushort>>(false, ToUShort, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the int[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int, int[]> AddMultiValue(Expression<Func<TClass, int[]>> expression, Action<NamelessMultiArgConfig<TClass, int, int[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int, int[]>(false, ToInt, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;int&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int, List<int>> AddMultiValue(Expression<Func<TClass, List<int>>> expression, Action<NamelessMultiArgConfig<TClass, int, List<int>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int, List<int>>(false, ToInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;int&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int, IList<int>> AddMultiValue(Expression<Func<TClass, IList<int>>> expression, Action<NamelessMultiArgConfig<TClass, int, IList<int>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int, IList<int>>(false, ToInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;int&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int, IReadOnlyList<int>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<int>>> expression, Action<NamelessMultiArgConfig<TClass, int, IReadOnlyList<int>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int, IReadOnlyList<int>>(false, ToInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;int&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int, ICollection<int>> AddMultiValue(Expression<Func<TClass, ICollection<int>>> expression, Action<NamelessMultiArgConfig<TClass, int, ICollection<int>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int, ICollection<int>>(false, ToInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;int&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int, IReadOnlyCollection<int>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<int>>> expression, Action<NamelessMultiArgConfig<TClass, int, IReadOnlyCollection<int>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int, IReadOnlyCollection<int>>(false, ToInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;int&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int, IEnumerable<int>> AddMultiValue(Expression<Func<TClass, IEnumerable<int>>> expression, Action<NamelessMultiArgConfig<TClass, int, IEnumerable<int>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int, IEnumerable<int>>(false, ToInt, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;int&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int, HashSet<int>> AddMultiValue(Expression<Func<TClass, HashSet<int>>> expression, Action<NamelessMultiArgConfig<TClass, int, HashSet<int>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int, HashSet<int>>(false, ToInt, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;int&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int, Stack<int>> AddMultiValue(Expression<Func<TClass, Stack<int>>> expression, Action<NamelessMultiArgConfig<TClass, int, Stack<int>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int, Stack<int>>(false, ToInt, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;int&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int, Queue<int>> AddMultiValue(Expression<Func<TClass, Queue<int>>> expression, Action<NamelessMultiArgConfig<TClass, int, Queue<int>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int, Queue<int>>(false, ToInt, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the uint[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint, uint[]> AddMultiValue(Expression<Func<TClass, uint[]>> expression, Action<NamelessMultiArgConfig<TClass, uint, uint[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint, uint[]>(false, ToUInt, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;uint&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint, List<uint>> AddMultiValue(Expression<Func<TClass, List<uint>>> expression, Action<NamelessMultiArgConfig<TClass, uint, List<uint>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint, List<uint>>(false, ToUInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;uint&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint, IList<uint>> AddMultiValue(Expression<Func<TClass, IList<uint>>> expression, Action<NamelessMultiArgConfig<TClass, uint, IList<uint>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint, IList<uint>>(false, ToUInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;uint&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint, IReadOnlyList<uint>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<uint>>> expression, Action<NamelessMultiArgConfig<TClass, uint, IReadOnlyList<uint>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint, IReadOnlyList<uint>>(false, ToUInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;uint&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint, ICollection<uint>> AddMultiValue(Expression<Func<TClass, ICollection<uint>>> expression, Action<NamelessMultiArgConfig<TClass, uint, ICollection<uint>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint, ICollection<uint>>(false, ToUInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;uint&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint, IReadOnlyCollection<uint>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<uint>>> expression, Action<NamelessMultiArgConfig<TClass, uint, IReadOnlyCollection<uint>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint, IReadOnlyCollection<uint>>(false, ToUInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;uint&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint, IEnumerable<uint>> AddMultiValue(Expression<Func<TClass, IEnumerable<uint>>> expression, Action<NamelessMultiArgConfig<TClass, uint, IEnumerable<uint>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint, IEnumerable<uint>>(false, ToUInt, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;uint&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint, HashSet<uint>> AddMultiValue(Expression<Func<TClass, HashSet<uint>>> expression, Action<NamelessMultiArgConfig<TClass, uint, HashSet<uint>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint, HashSet<uint>>(false, ToUInt, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;uint&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint, Stack<uint>> AddMultiValue(Expression<Func<TClass, Stack<uint>>> expression, Action<NamelessMultiArgConfig<TClass, uint, Stack<uint>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint, Stack<uint>>(false, ToUInt, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;uint&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint, Queue<uint>> AddMultiValue(Expression<Func<TClass, Queue<uint>>> expression, Action<NamelessMultiArgConfig<TClass, uint, Queue<uint>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint, Queue<uint>>(false, ToUInt, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the long[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long, long[]> AddMultiValue(Expression<Func<TClass, long[]>> expression, Action<NamelessMultiArgConfig<TClass, long, long[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long, long[]>(false, ToLong, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;long&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long, List<long>> AddMultiValue(Expression<Func<TClass, List<long>>> expression, Action<NamelessMultiArgConfig<TClass, long, List<long>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long, List<long>>(false, ToLong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;long&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long, IList<long>> AddMultiValue(Expression<Func<TClass, IList<long>>> expression, Action<NamelessMultiArgConfig<TClass, long, IList<long>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long, IList<long>>(false, ToLong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;long&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long, IReadOnlyList<long>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<long>>> expression, Action<NamelessMultiArgConfig<TClass, long, IReadOnlyList<long>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long, IReadOnlyList<long>>(false, ToLong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;long&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long, ICollection<long>> AddMultiValue(Expression<Func<TClass, ICollection<long>>> expression, Action<NamelessMultiArgConfig<TClass, long, ICollection<long>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long, ICollection<long>>(false, ToLong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;long&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long, IReadOnlyCollection<long>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<long>>> expression, Action<NamelessMultiArgConfig<TClass, long, IReadOnlyCollection<long>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long, IReadOnlyCollection<long>>(false, ToLong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;long&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long, IEnumerable<long>> AddMultiValue(Expression<Func<TClass, IEnumerable<long>>> expression, Action<NamelessMultiArgConfig<TClass, long, IEnumerable<long>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long, IEnumerable<long>>(false, ToLong, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;long&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long, HashSet<long>> AddMultiValue(Expression<Func<TClass, HashSet<long>>> expression, Action<NamelessMultiArgConfig<TClass, long, HashSet<long>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long, HashSet<long>>(false, ToLong, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;long&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long, Stack<long>> AddMultiValue(Expression<Func<TClass, Stack<long>>> expression, Action<NamelessMultiArgConfig<TClass, long, Stack<long>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long, Stack<long>>(false, ToLong, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;long&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long, Queue<long>> AddMultiValue(Expression<Func<TClass, Queue<long>>> expression, Action<NamelessMultiArgConfig<TClass, long, Queue<long>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long, Queue<long>>(false, ToLong, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ulong[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong, ulong[]> AddMultiValue(Expression<Func<TClass, ulong[]>> expression, Action<NamelessMultiArgConfig<TClass, ulong, ulong[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong, ulong[]>(false, ToULong, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;ulong&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong, List<ulong>> AddMultiValue(Expression<Func<TClass, List<ulong>>> expression, Action<NamelessMultiArgConfig<TClass, ulong, List<ulong>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong, List<ulong>>(false, ToULong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;ulong&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong, IList<ulong>> AddMultiValue(Expression<Func<TClass, IList<ulong>>> expression, Action<NamelessMultiArgConfig<TClass, ulong, IList<ulong>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong, IList<ulong>>(false, ToULong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;ulong&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong, IReadOnlyList<ulong>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<ulong>>> expression, Action<NamelessMultiArgConfig<TClass, ulong, IReadOnlyList<ulong>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong, IReadOnlyList<ulong>>(false, ToULong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;ulong&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong, ICollection<ulong>> AddMultiValue(Expression<Func<TClass, ICollection<ulong>>> expression, Action<NamelessMultiArgConfig<TClass, ulong, ICollection<ulong>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong, ICollection<ulong>>(false, ToULong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;ulong&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong, IReadOnlyCollection<ulong>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<ulong>>> expression, Action<NamelessMultiArgConfig<TClass, ulong, IReadOnlyCollection<ulong>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong, IReadOnlyCollection<ulong>>(false, ToULong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;ulong&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong, IEnumerable<ulong>> AddMultiValue(Expression<Func<TClass, IEnumerable<ulong>>> expression, Action<NamelessMultiArgConfig<TClass, ulong, IEnumerable<ulong>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong, IEnumerable<ulong>>(false, ToULong, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;ulong&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong, HashSet<ulong>> AddMultiValue(Expression<Func<TClass, HashSet<ulong>>> expression, Action<NamelessMultiArgConfig<TClass, ulong, HashSet<ulong>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong, HashSet<ulong>>(false, ToULong, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;ulong&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong, Stack<ulong>> AddMultiValue(Expression<Func<TClass, Stack<ulong>>> expression, Action<NamelessMultiArgConfig<TClass, ulong, Stack<ulong>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong, Stack<ulong>>(false, ToULong, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;ulong&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong, Queue<ulong>> AddMultiValue(Expression<Func<TClass, Queue<ulong>>> expression, Action<NamelessMultiArgConfig<TClass, ulong, Queue<ulong>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong, Queue<ulong>>(false, ToULong, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the float[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float, float[]> AddMultiValue(Expression<Func<TClass, float[]>> expression, Action<NamelessMultiArgConfig<TClass, float, float[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float, float[]>(false, ToFloat, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;float&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float, List<float>> AddMultiValue(Expression<Func<TClass, List<float>>> expression, Action<NamelessMultiArgConfig<TClass, float, List<float>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float, List<float>>(false, ToFloat, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;float&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float, IList<float>> AddMultiValue(Expression<Func<TClass, IList<float>>> expression, Action<NamelessMultiArgConfig<TClass, float, IList<float>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float, IList<float>>(false, ToFloat, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;float&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float, IReadOnlyList<float>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<float>>> expression, Action<NamelessMultiArgConfig<TClass, float, IReadOnlyList<float>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float, IReadOnlyList<float>>(false, ToFloat, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;float&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float, ICollection<float>> AddMultiValue(Expression<Func<TClass, ICollection<float>>> expression, Action<NamelessMultiArgConfig<TClass, float, ICollection<float>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float, ICollection<float>>(false, ToFloat, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;float&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float, IReadOnlyCollection<float>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<float>>> expression, Action<NamelessMultiArgConfig<TClass, float, IReadOnlyCollection<float>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float, IReadOnlyCollection<float>>(false, ToFloat, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;float&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float, IEnumerable<float>> AddMultiValue(Expression<Func<TClass, IEnumerable<float>>> expression, Action<NamelessMultiArgConfig<TClass, float, IEnumerable<float>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float, IEnumerable<float>>(false, ToFloat, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;float&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float, HashSet<float>> AddMultiValue(Expression<Func<TClass, HashSet<float>>> expression, Action<NamelessMultiArgConfig<TClass, float, HashSet<float>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float, HashSet<float>>(false, ToFloat, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;float&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float, Stack<float>> AddMultiValue(Expression<Func<TClass, Stack<float>>> expression, Action<NamelessMultiArgConfig<TClass, float, Stack<float>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float, Stack<float>>(false, ToFloat, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;float&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float, Queue<float>> AddMultiValue(Expression<Func<TClass, Queue<float>>> expression, Action<NamelessMultiArgConfig<TClass, float, Queue<float>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float, Queue<float>>(false, ToFloat, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the double[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double, double[]> AddMultiValue(Expression<Func<TClass, double[]>> expression, Action<NamelessMultiArgConfig<TClass, double, double[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double, double[]>(false, ToDouble, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;double&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double, List<double>> AddMultiValue(Expression<Func<TClass, List<double>>> expression, Action<NamelessMultiArgConfig<TClass, double, List<double>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double, List<double>>(false, ToDouble, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;double&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double, IList<double>> AddMultiValue(Expression<Func<TClass, IList<double>>> expression, Action<NamelessMultiArgConfig<TClass, double, IList<double>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double, IList<double>>(false, ToDouble, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;double&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double, IReadOnlyList<double>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<double>>> expression, Action<NamelessMultiArgConfig<TClass, double, IReadOnlyList<double>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double, IReadOnlyList<double>>(false, ToDouble, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;double&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double, ICollection<double>> AddMultiValue(Expression<Func<TClass, ICollection<double>>> expression, Action<NamelessMultiArgConfig<TClass, double, ICollection<double>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double, ICollection<double>>(false, ToDouble, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;double&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double, IReadOnlyCollection<double>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<double>>> expression, Action<NamelessMultiArgConfig<TClass, double, IReadOnlyCollection<double>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double, IReadOnlyCollection<double>>(false, ToDouble, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;double&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double, IEnumerable<double>> AddMultiValue(Expression<Func<TClass, IEnumerable<double>>> expression, Action<NamelessMultiArgConfig<TClass, double, IEnumerable<double>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double, IEnumerable<double>>(false, ToDouble, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;double&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double, HashSet<double>> AddMultiValue(Expression<Func<TClass, HashSet<double>>> expression, Action<NamelessMultiArgConfig<TClass, double, HashSet<double>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double, HashSet<double>>(false, ToDouble, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;double&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double, Stack<double>> AddMultiValue(Expression<Func<TClass, Stack<double>>> expression, Action<NamelessMultiArgConfig<TClass, double, Stack<double>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double, Stack<double>>(false, ToDouble, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;double&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double, Queue<double>> AddMultiValue(Expression<Func<TClass, Queue<double>>> expression, Action<NamelessMultiArgConfig<TClass, double, Queue<double>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double, Queue<double>>(false, ToDouble, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the decimal[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal, decimal[]> AddMultiValue(Expression<Func<TClass, decimal[]>> expression, Action<NamelessMultiArgConfig<TClass, decimal, decimal[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal, decimal[]>(false, ToDecimal, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;decimal&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal, List<decimal>> AddMultiValue(Expression<Func<TClass, List<decimal>>> expression, Action<NamelessMultiArgConfig<TClass, decimal, List<decimal>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal, List<decimal>>(false, ToDecimal, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;decimal&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal, IList<decimal>> AddMultiValue(Expression<Func<TClass, IList<decimal>>> expression, Action<NamelessMultiArgConfig<TClass, decimal, IList<decimal>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal, IList<decimal>>(false, ToDecimal, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;decimal&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal, IReadOnlyList<decimal>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<decimal>>> expression, Action<NamelessMultiArgConfig<TClass, decimal, IReadOnlyList<decimal>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal, IReadOnlyList<decimal>>(false, ToDecimal, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;decimal&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal, ICollection<decimal>> AddMultiValue(Expression<Func<TClass, ICollection<decimal>>> expression, Action<NamelessMultiArgConfig<TClass, decimal, ICollection<decimal>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal, ICollection<decimal>>(false, ToDecimal, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;decimal&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal, IReadOnlyCollection<decimal>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<decimal>>> expression, Action<NamelessMultiArgConfig<TClass, decimal, IReadOnlyCollection<decimal>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal, IReadOnlyCollection<decimal>>(false, ToDecimal, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;decimal&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal, IEnumerable<decimal>> AddMultiValue(Expression<Func<TClass, IEnumerable<decimal>>> expression, Action<NamelessMultiArgConfig<TClass, decimal, IEnumerable<decimal>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal, IEnumerable<decimal>>(false, ToDecimal, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;decimal&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal, HashSet<decimal>> AddMultiValue(Expression<Func<TClass, HashSet<decimal>>> expression, Action<NamelessMultiArgConfig<TClass, decimal, HashSet<decimal>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal, HashSet<decimal>>(false, ToDecimal, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;decimal&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal, Stack<decimal>> AddMultiValue(Expression<Func<TClass, Stack<decimal>>> expression, Action<NamelessMultiArgConfig<TClass, decimal, Stack<decimal>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal, Stack<decimal>>(false, ToDecimal, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;decimal&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal, Queue<decimal>> AddMultiValue(Expression<Func<TClass, Queue<decimal>>> expression, Action<NamelessMultiArgConfig<TClass, decimal, Queue<decimal>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal, Queue<decimal>>(false, ToDecimal, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the TEnum[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum, TEnum[]> AddMultiValue<TEnum>(Expression<Func<TClass, TEnum[]>> expression, Action<NamelessMultiArgConfig<TClass, TEnum, TEnum[]>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum, TEnum[]>(false, ToEnum<TEnum>, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;TEnum&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum, List<TEnum>> AddMultiValue<TEnum>(Expression<Func<TClass, List<TEnum>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum, List<TEnum>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum, List<TEnum>>(false, ToEnum<TEnum>, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;TEnum&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum, IList<TEnum>> AddMultiValue<TEnum>(Expression<Func<TClass, IList<TEnum>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum, IList<TEnum>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum, IList<TEnum>>(false, ToEnum<TEnum>, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;TEnum&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum, IReadOnlyList<TEnum>> AddMultiValue<TEnum>(Expression<Func<TClass, IReadOnlyList<TEnum>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum, IReadOnlyList<TEnum>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum, IReadOnlyList<TEnum>>(false, ToEnum<TEnum>, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;TEnum&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum, ICollection<TEnum>> AddMultiValue<TEnum>(Expression<Func<TClass, ICollection<TEnum>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum, ICollection<TEnum>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum, ICollection<TEnum>>(false, ToEnum<TEnum>, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;TEnum&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum, IReadOnlyCollection<TEnum>> AddMultiValue<TEnum>(Expression<Func<TClass, IReadOnlyCollection<TEnum>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum, IReadOnlyCollection<TEnum>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum, IReadOnlyCollection<TEnum>>(false, ToEnum<TEnum>, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;TEnum&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum, IEnumerable<TEnum>> AddMultiValue<TEnum>(Expression<Func<TClass, IEnumerable<TEnum>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum, IEnumerable<TEnum>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum, IEnumerable<TEnum>>(false, ToEnum<TEnum>, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;TEnum&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum, HashSet<TEnum>> AddMultiValue<TEnum>(Expression<Func<TClass, HashSet<TEnum>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum, HashSet<TEnum>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum, HashSet<TEnum>>(false, ToEnum<TEnum>, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;TEnum&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum, Stack<TEnum>> AddMultiValue<TEnum>(Expression<Func<TClass, Stack<TEnum>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum, Stack<TEnum>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum, Stack<TEnum>>(false, ToEnum<TEnum>, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;TEnum&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum, Queue<TEnum>> AddMultiValue<TEnum>(Expression<Func<TClass, Queue<TEnum>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum, Queue<TEnum>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum, Queue<TEnum>>(false, ToEnum<TEnum>, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the DateTime[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime, DateTime[]> AddMultiValue(Expression<Func<TClass, DateTime[]>> expression, Action<NamelessMultiArgConfig<TClass, DateTime, DateTime[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime, DateTime[]>(false, ToDateTime, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;DateTime&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime, List<DateTime>> AddMultiValue(Expression<Func<TClass, List<DateTime>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime, List<DateTime>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime, List<DateTime>>(false, ToDateTime, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;DateTime&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime, IList<DateTime>> AddMultiValue(Expression<Func<TClass, IList<DateTime>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime, IList<DateTime>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime, IList<DateTime>>(false, ToDateTime, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;DateTime&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime, IReadOnlyList<DateTime>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<DateTime>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime, IReadOnlyList<DateTime>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime, IReadOnlyList<DateTime>>(false, ToDateTime, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;DateTime&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime, ICollection<DateTime>> AddMultiValue(Expression<Func<TClass, ICollection<DateTime>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime, ICollection<DateTime>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime, ICollection<DateTime>>(false, ToDateTime, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;DateTime&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime, IReadOnlyCollection<DateTime>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<DateTime>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime, IReadOnlyCollection<DateTime>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime, IReadOnlyCollection<DateTime>>(false, ToDateTime, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;DateTime&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime, IEnumerable<DateTime>> AddMultiValue(Expression<Func<TClass, IEnumerable<DateTime>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime, IEnumerable<DateTime>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime, IEnumerable<DateTime>>(false, ToDateTime, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;DateTime&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime, HashSet<DateTime>> AddMultiValue(Expression<Func<TClass, HashSet<DateTime>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime, HashSet<DateTime>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime, HashSet<DateTime>>(false, ToDateTime, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;DateTime&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime, Stack<DateTime>> AddMultiValue(Expression<Func<TClass, Stack<DateTime>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime, Stack<DateTime>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime, Stack<DateTime>>(false, ToDateTime, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;DateTime&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime, Queue<DateTime>> AddMultiValue(Expression<Func<TClass, Queue<DateTime>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime, Queue<DateTime>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime, Queue<DateTime>>(false, ToDateTime, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the TimeSpan[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan, TimeSpan[]> AddMultiValue(Expression<Func<TClass, TimeSpan[]>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan, TimeSpan[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan, TimeSpan[]>(false, ToTimeSpan, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;TimeSpan&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan, List<TimeSpan>> AddMultiValue(Expression<Func<TClass, List<TimeSpan>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan, List<TimeSpan>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan, List<TimeSpan>>(false, ToTimeSpan, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;TimeSpan&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan, IList<TimeSpan>> AddMultiValue(Expression<Func<TClass, IList<TimeSpan>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan, IList<TimeSpan>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan, IList<TimeSpan>>(false, ToTimeSpan, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;TimeSpan&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan, IReadOnlyList<TimeSpan>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<TimeSpan>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan, IReadOnlyList<TimeSpan>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan, IReadOnlyList<TimeSpan>>(false, ToTimeSpan, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;TimeSpan&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan, ICollection<TimeSpan>> AddMultiValue(Expression<Func<TClass, ICollection<TimeSpan>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan, ICollection<TimeSpan>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan, ICollection<TimeSpan>>(false, ToTimeSpan, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;TimeSpan&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan, IReadOnlyCollection<TimeSpan>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<TimeSpan>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan, IReadOnlyCollection<TimeSpan>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan, IReadOnlyCollection<TimeSpan>>(false, ToTimeSpan, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;TimeSpan&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan, IEnumerable<TimeSpan>> AddMultiValue(Expression<Func<TClass, IEnumerable<TimeSpan>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan, IEnumerable<TimeSpan>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan, IEnumerable<TimeSpan>>(false, ToTimeSpan, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;TimeSpan&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan, HashSet<TimeSpan>> AddMultiValue(Expression<Func<TClass, HashSet<TimeSpan>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan, HashSet<TimeSpan>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan, HashSet<TimeSpan>>(false, ToTimeSpan, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;TimeSpan&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan, Stack<TimeSpan>> AddMultiValue(Expression<Func<TClass, Stack<TimeSpan>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan, Stack<TimeSpan>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan, Stack<TimeSpan>>(false, ToTimeSpan, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;TimeSpan&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan, Queue<TimeSpan>> AddMultiValue(Expression<Func<TClass, Queue<TimeSpan>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan, Queue<TimeSpan>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan, Queue<TimeSpan>>(false, ToTimeSpan, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Guid[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid, Guid[]> AddMultiValue(Expression<Func<TClass, Guid[]>> expression, Action<NamelessMultiArgConfig<TClass, Guid, Guid[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid, Guid[]>(false, ToGuid, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;Guid&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid, List<Guid>> AddMultiValue(Expression<Func<TClass, List<Guid>>> expression, Action<NamelessMultiArgConfig<TClass, Guid, List<Guid>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid, List<Guid>>(false, ToGuid, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;Guid&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid, IList<Guid>> AddMultiValue(Expression<Func<TClass, IList<Guid>>> expression, Action<NamelessMultiArgConfig<TClass, Guid, IList<Guid>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid, IList<Guid>>(false, ToGuid, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;Guid&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid, IReadOnlyList<Guid>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<Guid>>> expression, Action<NamelessMultiArgConfig<TClass, Guid, IReadOnlyList<Guid>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid, IReadOnlyList<Guid>>(false, ToGuid, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;Guid&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid, ICollection<Guid>> AddMultiValue(Expression<Func<TClass, ICollection<Guid>>> expression, Action<NamelessMultiArgConfig<TClass, Guid, ICollection<Guid>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid, ICollection<Guid>>(false, ToGuid, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;Guid&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid, IReadOnlyCollection<Guid>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<Guid>>> expression, Action<NamelessMultiArgConfig<TClass, Guid, IReadOnlyCollection<Guid>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid, IReadOnlyCollection<Guid>>(false, ToGuid, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;Guid&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid, IEnumerable<Guid>> AddMultiValue(Expression<Func<TClass, IEnumerable<Guid>>> expression, Action<NamelessMultiArgConfig<TClass, Guid, IEnumerable<Guid>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid, IEnumerable<Guid>>(false, ToGuid, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;Guid&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid, HashSet<Guid>> AddMultiValue(Expression<Func<TClass, HashSet<Guid>>> expression, Action<NamelessMultiArgConfig<TClass, Guid, HashSet<Guid>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid, HashSet<Guid>>(false, ToGuid, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;Guid&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid, Stack<Guid>> AddMultiValue(Expression<Func<TClass, Stack<Guid>>> expression, Action<NamelessMultiArgConfig<TClass, Guid, Stack<Guid>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid, Stack<Guid>>(false, ToGuid, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;Guid&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid, Queue<Guid>> AddMultiValue(Expression<Func<TClass, Queue<Guid>>> expression, Action<NamelessMultiArgConfig<TClass, Guid, Queue<Guid>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid, Queue<Guid>>(false, ToGuid, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the string?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string?, string?[]> AddMultiValueNullable(Expression<Func<TClass, string?[]>> expression, Action<NamelessMultiArgConfig<TClass, string?, string?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string?, string?[]>(false, NoConversionNullable, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;string?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string?, List<string?>> AddMultiValueNullable(Expression<Func<TClass, List<string?>>> expression, Action<NamelessMultiArgConfig<TClass, string?, List<string?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string?, List<string?>>(false, NoConversionNullable, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;string?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string?, IList<string?>> AddMultiValueNullable(Expression<Func<TClass, IList<string?>>> expression, Action<NamelessMultiArgConfig<TClass, string?, IList<string?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string?, IList<string?>>(false, NoConversionNullable, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;string?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string?, IReadOnlyList<string?>> AddMultiValueNullable(Expression<Func<TClass, IReadOnlyList<string?>>> expression, Action<NamelessMultiArgConfig<TClass, string?, IReadOnlyList<string?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string?, IReadOnlyList<string?>>(false, NoConversionNullable, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;string?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string?, ICollection<string?>> AddMultiValueNullable(Expression<Func<TClass, ICollection<string?>>> expression, Action<NamelessMultiArgConfig<TClass, string?, ICollection<string?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string?, ICollection<string?>>(false, NoConversionNullable, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;string?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string?, IReadOnlyCollection<string?>> AddMultiValueNullable(Expression<Func<TClass, IReadOnlyCollection<string?>>> expression, Action<NamelessMultiArgConfig<TClass, string?, IReadOnlyCollection<string?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string?, IReadOnlyCollection<string?>>(false, NoConversionNullable, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;string?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string?, IEnumerable<string?>> AddMultiValueNullable(Expression<Func<TClass, IEnumerable<string?>>> expression, Action<NamelessMultiArgConfig<TClass, string?, IEnumerable<string?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string?, IEnumerable<string?>>(false, NoConversionNullable, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;string?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string?, HashSet<string?>> AddMultiValueNullable(Expression<Func<TClass, HashSet<string?>>> expression, Action<NamelessMultiArgConfig<TClass, string?, HashSet<string?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string?, HashSet<string?>>(false, NoConversionNullable, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;string?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string?, Stack<string?>> AddMultiValueNullable(Expression<Func<TClass, Stack<string?>>> expression, Action<NamelessMultiArgConfig<TClass, string?, Stack<string?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string?, Stack<string?>>(false, NoConversionNullable, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;string?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to string? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, string?, Queue<string?>> AddMultiValueNullable(Expression<Func<TClass, Queue<string?>>> expression, Action<NamelessMultiArgConfig<TClass, string?, Queue<string?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, string?, Queue<string?>>(false, NoConversionNullable, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the short?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short?, short?[]> AddMultiValue(Expression<Func<TClass, short?[]>> expression, Action<NamelessMultiArgConfig<TClass, short?, short?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short?, short?[]>(false, ToNullableShort, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;short?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short?, List<short?>> AddMultiValue(Expression<Func<TClass, List<short?>>> expression, Action<NamelessMultiArgConfig<TClass, short?, List<short?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short?, List<short?>>(false, ToNullableShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;short?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short?, IList<short?>> AddMultiValue(Expression<Func<TClass, IList<short?>>> expression, Action<NamelessMultiArgConfig<TClass, short?, IList<short?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short?, IList<short?>>(false, ToNullableShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;short?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short?, IReadOnlyList<short?>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<short?>>> expression, Action<NamelessMultiArgConfig<TClass, short?, IReadOnlyList<short?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short?, IReadOnlyList<short?>>(false, ToNullableShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;short?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short?, ICollection<short?>> AddMultiValue(Expression<Func<TClass, ICollection<short?>>> expression, Action<NamelessMultiArgConfig<TClass, short?, ICollection<short?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short?, ICollection<short?>>(false, ToNullableShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;short?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short?, IReadOnlyCollection<short?>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<short?>>> expression, Action<NamelessMultiArgConfig<TClass, short?, IReadOnlyCollection<short?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short?, IReadOnlyCollection<short?>>(false, ToNullableShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;short?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short?, IEnumerable<short?>> AddMultiValue(Expression<Func<TClass, IEnumerable<short?>>> expression, Action<NamelessMultiArgConfig<TClass, short?, IEnumerable<short?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short?, IEnumerable<short?>>(false, ToNullableShort, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;short?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short?, HashSet<short?>> AddMultiValue(Expression<Func<TClass, HashSet<short?>>> expression, Action<NamelessMultiArgConfig<TClass, short?, HashSet<short?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short?, HashSet<short?>>(false, ToNullableShort, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;short?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short?, Stack<short?>> AddMultiValue(Expression<Func<TClass, Stack<short?>>> expression, Action<NamelessMultiArgConfig<TClass, short?, Stack<short?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short?, Stack<short?>>(false, ToNullableShort, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;short?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to short? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, short?, Queue<short?>> AddMultiValue(Expression<Func<TClass, Queue<short?>>> expression, Action<NamelessMultiArgConfig<TClass, short?, Queue<short?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, short?, Queue<short?>>(false, ToNullableShort, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ushort?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort?, ushort?[]> AddMultiValue(Expression<Func<TClass, ushort?[]>> expression, Action<NamelessMultiArgConfig<TClass, ushort?, ushort?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort?, ushort?[]>(false, ToNullableUShort, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;ushort?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort?, List<ushort?>> AddMultiValue(Expression<Func<TClass, List<ushort?>>> expression, Action<NamelessMultiArgConfig<TClass, ushort?, List<ushort?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort?, List<ushort?>>(false, ToNullableUShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;ushort?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort?, IList<ushort?>> AddMultiValue(Expression<Func<TClass, IList<ushort?>>> expression, Action<NamelessMultiArgConfig<TClass, ushort?, IList<ushort?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort?, IList<ushort?>>(false, ToNullableUShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;ushort?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort?, IReadOnlyList<ushort?>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<ushort?>>> expression, Action<NamelessMultiArgConfig<TClass, ushort?, IReadOnlyList<ushort?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort?, IReadOnlyList<ushort?>>(false, ToNullableUShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;ushort?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort?, ICollection<ushort?>> AddMultiValue(Expression<Func<TClass, ICollection<ushort?>>> expression, Action<NamelessMultiArgConfig<TClass, ushort?, ICollection<ushort?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort?, ICollection<ushort?>>(false, ToNullableUShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;ushort?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort?, IReadOnlyCollection<ushort?>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<ushort?>>> expression, Action<NamelessMultiArgConfig<TClass, ushort?, IReadOnlyCollection<ushort?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort?, IReadOnlyCollection<ushort?>>(false, ToNullableUShort, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;ushort?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort?, IEnumerable<ushort?>> AddMultiValue(Expression<Func<TClass, IEnumerable<ushort?>>> expression, Action<NamelessMultiArgConfig<TClass, ushort?, IEnumerable<ushort?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort?, IEnumerable<ushort?>>(false, ToNullableUShort, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;ushort?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort?, HashSet<ushort?>> AddMultiValue(Expression<Func<TClass, HashSet<ushort?>>> expression, Action<NamelessMultiArgConfig<TClass, ushort?, HashSet<ushort?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort?, HashSet<ushort?>>(false, ToNullableUShort, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;ushort?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort?, Stack<ushort?>> AddMultiValue(Expression<Func<TClass, Stack<ushort?>>> expression, Action<NamelessMultiArgConfig<TClass, ushort?, Stack<ushort?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort?, Stack<ushort?>>(false, ToNullableUShort, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;ushort?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ushort? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ushort?, Queue<ushort?>> AddMultiValue(Expression<Func<TClass, Queue<ushort?>>> expression, Action<NamelessMultiArgConfig<TClass, ushort?, Queue<ushort?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ushort?, Queue<ushort?>>(false, ToNullableUShort, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the int?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int?, int?[]> AddMultiValue(Expression<Func<TClass, int?[]>> expression, Action<NamelessMultiArgConfig<TClass, int?, int?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int?, int?[]>(false, ToNullableInt, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;int?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int?, List<int?>> AddMultiValue(Expression<Func<TClass, List<int?>>> expression, Action<NamelessMultiArgConfig<TClass, int?, List<int?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int?, List<int?>>(false, ToNullableInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;int?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int?, IList<int?>> AddMultiValue(Expression<Func<TClass, IList<int?>>> expression, Action<NamelessMultiArgConfig<TClass, int?, IList<int?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int?, IList<int?>>(false, ToNullableInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;int?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int?, IReadOnlyList<int?>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<int?>>> expression, Action<NamelessMultiArgConfig<TClass, int?, IReadOnlyList<int?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int?, IReadOnlyList<int?>>(false, ToNullableInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;int?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int?, ICollection<int?>> AddMultiValue(Expression<Func<TClass, ICollection<int?>>> expression, Action<NamelessMultiArgConfig<TClass, int?, ICollection<int?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int?, ICollection<int?>>(false, ToNullableInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;int?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int?, IReadOnlyCollection<int?>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<int?>>> expression, Action<NamelessMultiArgConfig<TClass, int?, IReadOnlyCollection<int?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int?, IReadOnlyCollection<int?>>(false, ToNullableInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;int?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int?, IEnumerable<int?>> AddMultiValue(Expression<Func<TClass, IEnumerable<int?>>> expression, Action<NamelessMultiArgConfig<TClass, int?, IEnumerable<int?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int?, IEnumerable<int?>>(false, ToNullableInt, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;int?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int?, HashSet<int?>> AddMultiValue(Expression<Func<TClass, HashSet<int?>>> expression, Action<NamelessMultiArgConfig<TClass, int?, HashSet<int?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int?, HashSet<int?>>(false, ToNullableInt, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;int?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int?, Stack<int?>> AddMultiValue(Expression<Func<TClass, Stack<int?>>> expression, Action<NamelessMultiArgConfig<TClass, int?, Stack<int?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int?, Stack<int?>>(false, ToNullableInt, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;int?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to int? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, int?, Queue<int?>> AddMultiValue(Expression<Func<TClass, Queue<int?>>> expression, Action<NamelessMultiArgConfig<TClass, int?, Queue<int?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, int?, Queue<int?>>(false, ToNullableInt, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the uint?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint?, uint?[]> AddMultiValue(Expression<Func<TClass, uint?[]>> expression, Action<NamelessMultiArgConfig<TClass, uint?, uint?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint?, uint?[]>(false, ToNullableUInt, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;uint?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint?, List<uint?>> AddMultiValue(Expression<Func<TClass, List<uint?>>> expression, Action<NamelessMultiArgConfig<TClass, uint?, List<uint?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint?, List<uint?>>(false, ToNullableUInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;uint?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint?, IList<uint?>> AddMultiValue(Expression<Func<TClass, IList<uint?>>> expression, Action<NamelessMultiArgConfig<TClass, uint?, IList<uint?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint?, IList<uint?>>(false, ToNullableUInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;uint?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint?, IReadOnlyList<uint?>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<uint?>>> expression, Action<NamelessMultiArgConfig<TClass, uint?, IReadOnlyList<uint?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint?, IReadOnlyList<uint?>>(false, ToNullableUInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;uint?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint?, ICollection<uint?>> AddMultiValue(Expression<Func<TClass, ICollection<uint?>>> expression, Action<NamelessMultiArgConfig<TClass, uint?, ICollection<uint?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint?, ICollection<uint?>>(false, ToNullableUInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;uint?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint?, IReadOnlyCollection<uint?>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<uint?>>> expression, Action<NamelessMultiArgConfig<TClass, uint?, IReadOnlyCollection<uint?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint?, IReadOnlyCollection<uint?>>(false, ToNullableUInt, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;uint?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint?, IEnumerable<uint?>> AddMultiValue(Expression<Func<TClass, IEnumerable<uint?>>> expression, Action<NamelessMultiArgConfig<TClass, uint?, IEnumerable<uint?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint?, IEnumerable<uint?>>(false, ToNullableUInt, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;uint?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint?, HashSet<uint?>> AddMultiValue(Expression<Func<TClass, HashSet<uint?>>> expression, Action<NamelessMultiArgConfig<TClass, uint?, HashSet<uint?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint?, HashSet<uint?>>(false, ToNullableUInt, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;uint?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint?, Stack<uint?>> AddMultiValue(Expression<Func<TClass, Stack<uint?>>> expression, Action<NamelessMultiArgConfig<TClass, uint?, Stack<uint?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint?, Stack<uint?>>(false, ToNullableUInt, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;uint?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to uint? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, uint?, Queue<uint?>> AddMultiValue(Expression<Func<TClass, Queue<uint?>>> expression, Action<NamelessMultiArgConfig<TClass, uint?, Queue<uint?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, uint?, Queue<uint?>>(false, ToNullableUInt, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the long?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long?, long?[]> AddMultiValue(Expression<Func<TClass, long?[]>> expression, Action<NamelessMultiArgConfig<TClass, long?, long?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long?, long?[]>(false, ToNullableLong, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;long?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long?, List<long?>> AddMultiValue(Expression<Func<TClass, List<long?>>> expression, Action<NamelessMultiArgConfig<TClass, long?, List<long?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long?, List<long?>>(false, ToNullableLong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;long?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long?, IList<long?>> AddMultiValue(Expression<Func<TClass, IList<long?>>> expression, Action<NamelessMultiArgConfig<TClass, long?, IList<long?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long?, IList<long?>>(false, ToNullableLong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;long?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long?, IReadOnlyList<long?>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<long?>>> expression, Action<NamelessMultiArgConfig<TClass, long?, IReadOnlyList<long?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long?, IReadOnlyList<long?>>(false, ToNullableLong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;long?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long?, ICollection<long?>> AddMultiValue(Expression<Func<TClass, ICollection<long?>>> expression, Action<NamelessMultiArgConfig<TClass, long?, ICollection<long?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long?, ICollection<long?>>(false, ToNullableLong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;long?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long?, IReadOnlyCollection<long?>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<long?>>> expression, Action<NamelessMultiArgConfig<TClass, long?, IReadOnlyCollection<long?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long?, IReadOnlyCollection<long?>>(false, ToNullableLong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;long?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long?, IEnumerable<long?>> AddMultiValue(Expression<Func<TClass, IEnumerable<long?>>> expression, Action<NamelessMultiArgConfig<TClass, long?, IEnumerable<long?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long?, IEnumerable<long?>>(false, ToNullableLong, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;long?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long?, HashSet<long?>> AddMultiValue(Expression<Func<TClass, HashSet<long?>>> expression, Action<NamelessMultiArgConfig<TClass, long?, HashSet<long?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long?, HashSet<long?>>(false, ToNullableLong, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;long?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long?, Stack<long?>> AddMultiValue(Expression<Func<TClass, Stack<long?>>> expression, Action<NamelessMultiArgConfig<TClass, long?, Stack<long?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long?, Stack<long?>>(false, ToNullableLong, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;long?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to long? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, long?, Queue<long?>> AddMultiValue(Expression<Func<TClass, Queue<long?>>> expression, Action<NamelessMultiArgConfig<TClass, long?, Queue<long?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, long?, Queue<long?>>(false, ToNullableLong, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ulong?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong?, ulong?[]> AddMultiValue(Expression<Func<TClass, ulong?[]>> expression, Action<NamelessMultiArgConfig<TClass, ulong?, ulong?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong?, ulong?[]>(false, ToNullableULong, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;ulong?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong?, List<ulong?>> AddMultiValue(Expression<Func<TClass, List<ulong?>>> expression, Action<NamelessMultiArgConfig<TClass, ulong?, List<ulong?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong?, List<ulong?>>(false, ToNullableULong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;ulong?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong?, IList<ulong?>> AddMultiValue(Expression<Func<TClass, IList<ulong?>>> expression, Action<NamelessMultiArgConfig<TClass, ulong?, IList<ulong?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong?, IList<ulong?>>(false, ToNullableULong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;ulong?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong?, IReadOnlyList<ulong?>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<ulong?>>> expression, Action<NamelessMultiArgConfig<TClass, ulong?, IReadOnlyList<ulong?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong?, IReadOnlyList<ulong?>>(false, ToNullableULong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;ulong?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong?, ICollection<ulong?>> AddMultiValue(Expression<Func<TClass, ICollection<ulong?>>> expression, Action<NamelessMultiArgConfig<TClass, ulong?, ICollection<ulong?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong?, ICollection<ulong?>>(false, ToNullableULong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;ulong?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong?, IReadOnlyCollection<ulong?>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<ulong?>>> expression, Action<NamelessMultiArgConfig<TClass, ulong?, IReadOnlyCollection<ulong?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong?, IReadOnlyCollection<ulong?>>(false, ToNullableULong, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;ulong?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong?, IEnumerable<ulong?>> AddMultiValue(Expression<Func<TClass, IEnumerable<ulong?>>> expression, Action<NamelessMultiArgConfig<TClass, ulong?, IEnumerable<ulong?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong?, IEnumerable<ulong?>>(false, ToNullableULong, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;ulong?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong?, HashSet<ulong?>> AddMultiValue(Expression<Func<TClass, HashSet<ulong?>>> expression, Action<NamelessMultiArgConfig<TClass, ulong?, HashSet<ulong?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong?, HashSet<ulong?>>(false, ToNullableULong, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;ulong?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong?, Stack<ulong?>> AddMultiValue(Expression<Func<TClass, Stack<ulong?>>> expression, Action<NamelessMultiArgConfig<TClass, ulong?, Stack<ulong?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong?, Stack<ulong?>>(false, ToNullableULong, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;ulong?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to ulong? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, ulong?, Queue<ulong?>> AddMultiValue(Expression<Func<TClass, Queue<ulong?>>> expression, Action<NamelessMultiArgConfig<TClass, ulong?, Queue<ulong?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, ulong?, Queue<ulong?>>(false, ToNullableULong, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the float?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float?, float?[]> AddMultiValue(Expression<Func<TClass, float?[]>> expression, Action<NamelessMultiArgConfig<TClass, float?, float?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float?, float?[]>(false, ToNullableFloat, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;float?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float?, List<float?>> AddMultiValue(Expression<Func<TClass, List<float?>>> expression, Action<NamelessMultiArgConfig<TClass, float?, List<float?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float?, List<float?>>(false, ToNullableFloat, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;float?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float?, IList<float?>> AddMultiValue(Expression<Func<TClass, IList<float?>>> expression, Action<NamelessMultiArgConfig<TClass, float?, IList<float?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float?, IList<float?>>(false, ToNullableFloat, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;float?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float?, IReadOnlyList<float?>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<float?>>> expression, Action<NamelessMultiArgConfig<TClass, float?, IReadOnlyList<float?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float?, IReadOnlyList<float?>>(false, ToNullableFloat, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;float?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float?, ICollection<float?>> AddMultiValue(Expression<Func<TClass, ICollection<float?>>> expression, Action<NamelessMultiArgConfig<TClass, float?, ICollection<float?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float?, ICollection<float?>>(false, ToNullableFloat, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;float?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float?, IReadOnlyCollection<float?>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<float?>>> expression, Action<NamelessMultiArgConfig<TClass, float?, IReadOnlyCollection<float?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float?, IReadOnlyCollection<float?>>(false, ToNullableFloat, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;float?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float?, IEnumerable<float?>> AddMultiValue(Expression<Func<TClass, IEnumerable<float?>>> expression, Action<NamelessMultiArgConfig<TClass, float?, IEnumerable<float?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float?, IEnumerable<float?>>(false, ToNullableFloat, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;float?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float?, HashSet<float?>> AddMultiValue(Expression<Func<TClass, HashSet<float?>>> expression, Action<NamelessMultiArgConfig<TClass, float?, HashSet<float?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float?, HashSet<float?>>(false, ToNullableFloat, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;float?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float?, Stack<float?>> AddMultiValue(Expression<Func<TClass, Stack<float?>>> expression, Action<NamelessMultiArgConfig<TClass, float?, Stack<float?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float?, Stack<float?>>(false, ToNullableFloat, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;float?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to float? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, float?, Queue<float?>> AddMultiValue(Expression<Func<TClass, Queue<float?>>> expression, Action<NamelessMultiArgConfig<TClass, float?, Queue<float?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, float?, Queue<float?>>(false, ToNullableFloat, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the double?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double?, double?[]> AddMultiValue(Expression<Func<TClass, double?[]>> expression, Action<NamelessMultiArgConfig<TClass, double?, double?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double?, double?[]>(false, ToNullableDouble, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;double?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double?, List<double?>> AddMultiValue(Expression<Func<TClass, List<double?>>> expression, Action<NamelessMultiArgConfig<TClass, double?, List<double?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double?, List<double?>>(false, ToNullableDouble, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;double?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double?, IList<double?>> AddMultiValue(Expression<Func<TClass, IList<double?>>> expression, Action<NamelessMultiArgConfig<TClass, double?, IList<double?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double?, IList<double?>>(false, ToNullableDouble, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;double?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double?, IReadOnlyList<double?>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<double?>>> expression, Action<NamelessMultiArgConfig<TClass, double?, IReadOnlyList<double?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double?, IReadOnlyList<double?>>(false, ToNullableDouble, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;double?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double?, ICollection<double?>> AddMultiValue(Expression<Func<TClass, ICollection<double?>>> expression, Action<NamelessMultiArgConfig<TClass, double?, ICollection<double?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double?, ICollection<double?>>(false, ToNullableDouble, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;double?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double?, IReadOnlyCollection<double?>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<double?>>> expression, Action<NamelessMultiArgConfig<TClass, double?, IReadOnlyCollection<double?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double?, IReadOnlyCollection<double?>>(false, ToNullableDouble, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;double?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double?, IEnumerable<double?>> AddMultiValue(Expression<Func<TClass, IEnumerable<double?>>> expression, Action<NamelessMultiArgConfig<TClass, double?, IEnumerable<double?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double?, IEnumerable<double?>>(false, ToNullableDouble, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;double?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double?, HashSet<double?>> AddMultiValue(Expression<Func<TClass, HashSet<double?>>> expression, Action<NamelessMultiArgConfig<TClass, double?, HashSet<double?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double?, HashSet<double?>>(false, ToNullableDouble, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;double?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double?, Stack<double?>> AddMultiValue(Expression<Func<TClass, Stack<double?>>> expression, Action<NamelessMultiArgConfig<TClass, double?, Stack<double?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double?, Stack<double?>>(false, ToNullableDouble, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;double?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to double? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, double?, Queue<double?>> AddMultiValue(Expression<Func<TClass, Queue<double?>>> expression, Action<NamelessMultiArgConfig<TClass, double?, Queue<double?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, double?, Queue<double?>>(false, ToNullableDouble, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the decimal?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal?, decimal?[]> AddMultiValue(Expression<Func<TClass, decimal?[]>> expression, Action<NamelessMultiArgConfig<TClass, decimal?, decimal?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal?, decimal?[]>(false, ToNullableDecimal, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;decimal?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal?, List<decimal?>> AddMultiValue(Expression<Func<TClass, List<decimal?>>> expression, Action<NamelessMultiArgConfig<TClass, decimal?, List<decimal?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal?, List<decimal?>>(false, ToNullableDecimal, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;decimal?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal?, IList<decimal?>> AddMultiValue(Expression<Func<TClass, IList<decimal?>>> expression, Action<NamelessMultiArgConfig<TClass, decimal?, IList<decimal?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal?, IList<decimal?>>(false, ToNullableDecimal, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;decimal?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal?, IReadOnlyList<decimal?>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<decimal?>>> expression, Action<NamelessMultiArgConfig<TClass, decimal?, IReadOnlyList<decimal?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal?, IReadOnlyList<decimal?>>(false, ToNullableDecimal, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;decimal?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal?, ICollection<decimal?>> AddMultiValue(Expression<Func<TClass, ICollection<decimal?>>> expression, Action<NamelessMultiArgConfig<TClass, decimal?, ICollection<decimal?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal?, ICollection<decimal?>>(false, ToNullableDecimal, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;decimal?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal?, IReadOnlyCollection<decimal?>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<decimal?>>> expression, Action<NamelessMultiArgConfig<TClass, decimal?, IReadOnlyCollection<decimal?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal?, IReadOnlyCollection<decimal?>>(false, ToNullableDecimal, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;decimal?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal?, IEnumerable<decimal?>> AddMultiValue(Expression<Func<TClass, IEnumerable<decimal?>>> expression, Action<NamelessMultiArgConfig<TClass, decimal?, IEnumerable<decimal?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal?, IEnumerable<decimal?>>(false, ToNullableDecimal, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;decimal?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal?, HashSet<decimal?>> AddMultiValue(Expression<Func<TClass, HashSet<decimal?>>> expression, Action<NamelessMultiArgConfig<TClass, decimal?, HashSet<decimal?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal?, HashSet<decimal?>>(false, ToNullableDecimal, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;decimal?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal?, Stack<decimal?>> AddMultiValue(Expression<Func<TClass, Stack<decimal?>>> expression, Action<NamelessMultiArgConfig<TClass, decimal?, Stack<decimal?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal?, Stack<decimal?>>(false, ToNullableDecimal, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;decimal?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to decimal? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, decimal?, Queue<decimal?>> AddMultiValue(Expression<Func<TClass, Queue<decimal?>>> expression, Action<NamelessMultiArgConfig<TClass, decimal?, Queue<decimal?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, decimal?, Queue<decimal?>>(false, ToNullableDecimal, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the TEnum?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum?, TEnum?[]> AddMultiValue<TEnum>(Expression<Func<TClass, TEnum?[]>> expression, Action<NamelessMultiArgConfig<TClass, TEnum?, TEnum?[]>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum?, TEnum?[]>(false, ToNullableEnum<TEnum>, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;TEnum?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum?, List<TEnum?>> AddMultiValue<TEnum>(Expression<Func<TClass, List<TEnum?>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum?, List<TEnum?>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum?, List<TEnum?>>(false, ToNullableEnum<TEnum>, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;TEnum?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum?, IList<TEnum?>> AddMultiValue<TEnum>(Expression<Func<TClass, IList<TEnum?>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum?, IList<TEnum?>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum?, IList<TEnum?>>(false, ToNullableEnum<TEnum>, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;TEnum?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum?, IReadOnlyList<TEnum?>> AddMultiValue<TEnum>(Expression<Func<TClass, IReadOnlyList<TEnum?>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum?, IReadOnlyList<TEnum?>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum?, IReadOnlyList<TEnum?>>(false, ToNullableEnum<TEnum>, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;TEnum?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum?, ICollection<TEnum?>> AddMultiValue<TEnum>(Expression<Func<TClass, ICollection<TEnum?>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum?, ICollection<TEnum?>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum?, ICollection<TEnum?>>(false, ToNullableEnum<TEnum>, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;TEnum?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum?, IReadOnlyCollection<TEnum?>> AddMultiValue<TEnum>(Expression<Func<TClass, IReadOnlyCollection<TEnum?>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum?, IReadOnlyCollection<TEnum?>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum?, IReadOnlyCollection<TEnum?>>(false, ToNullableEnum<TEnum>, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;TEnum?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum?, IEnumerable<TEnum?>> AddMultiValue<TEnum>(Expression<Func<TClass, IEnumerable<TEnum?>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum?, IEnumerable<TEnum?>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum?, IEnumerable<TEnum?>>(false, ToNullableEnum<TEnum>, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;TEnum?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum?, HashSet<TEnum?>> AddMultiValue<TEnum>(Expression<Func<TClass, HashSet<TEnum?>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum?, HashSet<TEnum?>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum?, HashSet<TEnum?>>(false, ToNullableEnum<TEnum>, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;TEnum?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum?, Stack<TEnum?>> AddMultiValue<TEnum>(Expression<Func<TClass, Stack<TEnum?>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum?, Stack<TEnum?>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum?, Stack<TEnum?>>(false, ToNullableEnum<TEnum>, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;TEnum?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TEnum? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TEnum?, Queue<TEnum?>> AddMultiValue<TEnum>(Expression<Func<TClass, Queue<TEnum?>>> expression, Action<NamelessMultiArgConfig<TClass, TEnum?, Queue<TEnum?>>> config) where TEnum : struct, Enum
		{
			var obj = new NamelessMultiArgConfig<TClass, TEnum?, Queue<TEnum?>>(false, ToNullableEnum<TEnum>, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the DateTime?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime?, DateTime?[]> AddMultiValue(Expression<Func<TClass, DateTime?[]>> expression, Action<NamelessMultiArgConfig<TClass, DateTime?, DateTime?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime?, DateTime?[]>(false, ToNullableDateTime, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;DateTime?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime?, List<DateTime?>> AddMultiValue(Expression<Func<TClass, List<DateTime?>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime?, List<DateTime?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime?, List<DateTime?>>(false, ToNullableDateTime, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;DateTime?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime?, IList<DateTime?>> AddMultiValue(Expression<Func<TClass, IList<DateTime?>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime?, IList<DateTime?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime?, IList<DateTime?>>(false, ToNullableDateTime, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;DateTime?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime?, IReadOnlyList<DateTime?>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<DateTime?>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime?, IReadOnlyList<DateTime?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime?, IReadOnlyList<DateTime?>>(false, ToNullableDateTime, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;DateTime?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime?, ICollection<DateTime?>> AddMultiValue(Expression<Func<TClass, ICollection<DateTime?>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime?, ICollection<DateTime?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime?, ICollection<DateTime?>>(false, ToNullableDateTime, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;DateTime?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime?, IReadOnlyCollection<DateTime?>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<DateTime?>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime?, IReadOnlyCollection<DateTime?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime?, IReadOnlyCollection<DateTime?>>(false, ToNullableDateTime, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;DateTime?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime?, IEnumerable<DateTime?>> AddMultiValue(Expression<Func<TClass, IEnumerable<DateTime?>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime?, IEnumerable<DateTime?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime?, IEnumerable<DateTime?>>(false, ToNullableDateTime, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;DateTime?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime?, HashSet<DateTime?>> AddMultiValue(Expression<Func<TClass, HashSet<DateTime?>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime?, HashSet<DateTime?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime?, HashSet<DateTime?>>(false, ToNullableDateTime, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;DateTime?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime?, Stack<DateTime?>> AddMultiValue(Expression<Func<TClass, Stack<DateTime?>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime?, Stack<DateTime?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime?, Stack<DateTime?>>(false, ToNullableDateTime, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;DateTime?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to DateTime? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, DateTime?, Queue<DateTime?>> AddMultiValue(Expression<Func<TClass, Queue<DateTime?>>> expression, Action<NamelessMultiArgConfig<TClass, DateTime?, Queue<DateTime?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, DateTime?, Queue<DateTime?>>(false, ToNullableDateTime, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the TimeSpan?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan?, TimeSpan?[]> AddMultiValue(Expression<Func<TClass, TimeSpan?[]>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan?, TimeSpan?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan?, TimeSpan?[]>(false, ToNullableTimeSpan, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;TimeSpan?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan?, List<TimeSpan?>> AddMultiValue(Expression<Func<TClass, List<TimeSpan?>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan?, List<TimeSpan?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan?, List<TimeSpan?>>(false, ToNullableTimeSpan, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;TimeSpan?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan?, IList<TimeSpan?>> AddMultiValue(Expression<Func<TClass, IList<TimeSpan?>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan?, IList<TimeSpan?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan?, IList<TimeSpan?>>(false, ToNullableTimeSpan, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;TimeSpan?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan?, IReadOnlyList<TimeSpan?>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<TimeSpan?>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan?, IReadOnlyList<TimeSpan?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan?, IReadOnlyList<TimeSpan?>>(false, ToNullableTimeSpan, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;TimeSpan?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan?, ICollection<TimeSpan?>> AddMultiValue(Expression<Func<TClass, ICollection<TimeSpan?>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan?, ICollection<TimeSpan?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan?, ICollection<TimeSpan?>>(false, ToNullableTimeSpan, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;TimeSpan?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan?, IReadOnlyCollection<TimeSpan?>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<TimeSpan?>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan?, IReadOnlyCollection<TimeSpan?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan?, IReadOnlyCollection<TimeSpan?>>(false, ToNullableTimeSpan, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;TimeSpan?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan?, IEnumerable<TimeSpan?>> AddMultiValue(Expression<Func<TClass, IEnumerable<TimeSpan?>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan?, IEnumerable<TimeSpan?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan?, IEnumerable<TimeSpan?>>(false, ToNullableTimeSpan, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;TimeSpan?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan?, HashSet<TimeSpan?>> AddMultiValue(Expression<Func<TClass, HashSet<TimeSpan?>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan?, HashSet<TimeSpan?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan?, HashSet<TimeSpan?>>(false, ToNullableTimeSpan, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;TimeSpan?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan?, Stack<TimeSpan?>> AddMultiValue(Expression<Func<TClass, Stack<TimeSpan?>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan?, Stack<TimeSpan?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan?, Stack<TimeSpan?>>(false, ToNullableTimeSpan, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;TimeSpan?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to TimeSpan? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, TimeSpan?, Queue<TimeSpan?>> AddMultiValue(Expression<Func<TClass, Queue<TimeSpan?>>> expression, Action<NamelessMultiArgConfig<TClass, TimeSpan?, Queue<TimeSpan?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, TimeSpan?, Queue<TimeSpan?>>(false, ToNullableTimeSpan, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Guid?[] specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid? and stored in a collection of type Array.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid?, Guid?[]> AddMultiValue(Expression<Func<TClass, Guid?[]>> expression, Action<NamelessMultiArgConfig<TClass, Guid?, Guid?[]>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid?, Guid?[]>(false, ToNullableGuid, Array);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the List&lt;Guid?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid?, List<Guid?>> AddMultiValue(Expression<Func<TClass, List<Guid?>>> expression, Action<NamelessMultiArgConfig<TClass, Guid?, List<Guid?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid?, List<Guid?>>(false, ToNullableGuid, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IList&lt;Guid?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid?, IList<Guid?>> AddMultiValue(Expression<Func<TClass, IList<Guid?>>> expression, Action<NamelessMultiArgConfig<TClass, Guid?, IList<Guid?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid?, IList<Guid?>>(false, ToNullableGuid, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyList&lt;Guid?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid?, IReadOnlyList<Guid?>> AddMultiValue(Expression<Func<TClass, IReadOnlyList<Guid?>>> expression, Action<NamelessMultiArgConfig<TClass, Guid?, IReadOnlyList<Guid?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid?, IReadOnlyList<Guid?>>(false, ToNullableGuid, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the ICollection&lt;Guid?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid?, ICollection<Guid?>> AddMultiValue(Expression<Func<TClass, ICollection<Guid?>>> expression, Action<NamelessMultiArgConfig<TClass, Guid?, ICollection<Guid?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid?, ICollection<Guid?>>(false, ToNullableGuid, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IReadOnlyCollection&lt;Guid?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid? and stored in a collection of type List.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid?, IReadOnlyCollection<Guid?>> AddMultiValue(Expression<Func<TClass, IReadOnlyCollection<Guid?>>> expression, Action<NamelessMultiArgConfig<TClass, Guid?, IReadOnlyCollection<Guid?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid?, IReadOnlyCollection<Guid?>>(false, ToNullableGuid, List);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the IEnumerable&lt;Guid?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid? and stored in a collection of type Enumerable.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid?, IEnumerable<Guid?>> AddMultiValue(Expression<Func<TClass, IEnumerable<Guid?>>> expression, Action<NamelessMultiArgConfig<TClass, Guid?, IEnumerable<Guid?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid?, IEnumerable<Guid?>>(false, ToNullableGuid, Enumerable);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the HashSet&lt;Guid?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid? and stored in a collection of type HashSet.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid?, HashSet<Guid?>> AddMultiValue(Expression<Func<TClass, HashSet<Guid?>>> expression, Action<NamelessMultiArgConfig<TClass, Guid?, HashSet<Guid?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid?, HashSet<Guid?>>(false, ToNullableGuid, HashSet);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Stack&lt;Guid?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid? and stored in a collection of type Stack.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid?, Stack<Guid?>> AddMultiValue(Expression<Func<TClass, Stack<Guid?>>> expression, Action<NamelessMultiArgConfig<TClass, Guid?, Stack<Guid?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid?, Stack<Guid?>>(false, ToNullableGuid, Stack);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
		/// <summary>
		/// Adds a new MultiValue to set the Queue&lt;Guid?&gt; specified by <paramref name="expression"/>. By default this MultiValue is not required.
		/// Adds a new MultiValue, by default it is not required. The elements will be converted to Guid? and stored in a collection of type Queue.
		/// </summary>
		/// <param name="expression">The property.</param>
		/// <param name="config">The action to configure the MultiValue.</param>
		/// <returns>A configured MultiValue.</returns>
		public MultiValue<TClass, Guid?, Queue<Guid?>> AddMultiValue(Expression<Func<TClass, Queue<Guid?>>> expression, Action<NamelessMultiArgConfig<TClass, Guid?, Queue<Guid?>>> config)
		{
			var obj = new NamelessMultiArgConfig<TClass, Guid?, Queue<Guid?>>(false, ToNullableGuid, Queue);
			config(obj);
			return AddMultiValueCore(expression, obj);
		}
	}
}
