namespace CommandLineFluent
{
	using System;

	/// <summary>
	/// A basic implementation of <see cref="ILoopCondition"/>.
	/// </summary>
	public sealed class StopOnKeywordOrCancel : ILoopCondition
	{
		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="keyword">Stops once this keyword is found.</param>
		/// <param name="stringComparer">Compares against <paramref name="keyword"/> using this.</param>
		/// <param name="stopOnCancel">If true, calls <see cref="RegisterKeyCancel"/> in the constructor.</param>
		public StopOnKeywordOrCancel(string keyword, StringComparer stringComparer, bool stopOnCancel = true)
		{
			Keyword = keyword;
			Comparer = stringComparer;
			if (stopOnCancel)
			{
				RegisterKeyCancel();
			}
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
		/// Adds <see cref="Console_CancelKeyPress(object?, ConsoleCancelEventArgs)"/> as an event handler to <see cref="Console.CancelKeyPress"/>.
		/// </summary>
		public void RegisterKeyCancel()
		{
			Console.CancelKeyPress += Console_CancelKeyPress;
		}
		/// <summary>
		/// Removes <see cref="Console_CancelKeyPress(object?, ConsoleCancelEventArgs)"/> as an event handler from <see cref="Console.CancelKeyPress"/>.
		/// </summary>
		public void UnregisterKeyCancel()
		{
			Console.CancelKeyPress -= Console_CancelKeyPress;
		}
		/// <summary>
		/// Sets <see cref="ConsoleCancelEventArgs.Cancel"/> to true, and <see cref="CancelRequested"/> to true.
		/// </summary>
		public void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
		{
			e.Cancel = true;
			CancelRequested = true;
		}
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
