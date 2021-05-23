namespace CommandLineFluent
{
	using System;

	/// <summary>
	/// A basic implementation of <see cref="ILoopCondition"/>.
	/// Its <see cref="ShouldGo(string?)"/> returns false if the input is equal to a keyword, or if <see cref="CancelRequested"/> is set to true.
	/// </summary>
	public sealed class StopOnKeyword : ILoopCondition
	{
		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="keyword">Stops once this keyword is found.</param>
		/// <param name="stringComparer">Compares against <paramref name="keyword"/> using this.</param>
		public StopOnKeyword(string keyword, StringComparer stringComparer)
		{
			Keyword = keyword;
			Comparer = stringComparer;
		}
		/// <summary>
		/// Stops once this keyword is found
		/// </summary>
		public string Keyword { get; set; }
		/// <summary>
		/// Compares against <see cref="Keyword"/> using this.
		/// </summary>
		public StringComparer Comparer { get; set; }
		/// <summary>
		/// Set this to true to cause <see cref="ShouldGo(string?)"/> to return false, regardless of the input.
		/// </summary>
		public bool CancelRequested { get; set; }
		/// <summary>
		/// Returns false if <see cref="CancelRequested"/> is true, if <paramref name="rawInput"/> is null, or <paramref name="rawInput"/> equals <see cref="Keyword"/>.
		/// Otherwise returns true.
		/// </summary>
		/// <param name="rawInput">The raw input.</param>
		public bool ShouldGo(string? rawInput)
		{
			// Go so long as they didn't request cancellation, 
			return !CancelRequested && rawInput != null && !Comparer.Equals(Keyword, rawInput);
		}
	}
}
