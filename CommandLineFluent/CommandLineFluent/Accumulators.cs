namespace CommandLineFluent
{
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Built-in accumulators, which turn an <see cref="IEnumerable{T}"/> into a concrete collection type.
	/// </summary>
	public static class Accumulators
	{
		/// <summary>
		/// Uses <see cref="Enumerable.ToArray{TSource}(IEnumerable{TSource})"/>
		/// </summary>
		/// <typeparam name="T">The type of the values</typeparam>
		/// <param name="vals">The values</param>
		/// <returns>An array</returns>
		public static T[] Array<T>(IEnumerable<T> vals) => vals.ToArray();
		/// <summary>
		/// Creates a new List, passing <paramref name="vals"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values</typeparam>
		/// <param name="vals">The values</param>
		/// <returns>A List</returns>
		public static List<T> List<T>(IEnumerable<T> vals) => new(vals);
		/// <summary>
		/// Creates a new Stack, passing <paramref name="vals"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values</typeparam>
		/// <param name="vals">The values</param>
		/// <returns>A Stack</returns>
		public static Stack<T> Stack<T>(IEnumerable<T> vals) => new(vals);
		/// <summary>
		/// Creates a new Queue, passing <paramref name="vals"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values</typeparam>
		/// <param name="vals">The values</param>
		/// <returns>A Queue</returns>
		public static Queue<T> Queue<T>(IEnumerable<T> vals) => new(vals);
		/// <summary>
		/// Creates a new HashSet, passing <paramref name="vals"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values</typeparam>
		/// <param name="vals">The values</param>
		/// <returns>A HashSet</returns>
		public static HashSet<T> HashSet<T>(IEnumerable<T> vals) => new(vals);
		/// <summary>
		/// Returns <paramref name="vals"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values</typeparam>
		/// <param name="vals">The values</param>
		/// <returns><paramref name="vals"/></returns>
		public static IEnumerable<T> Enumerable<T>(IEnumerable<T> vals) => vals;
	}
}
