namespace CommandLineFluent.Arguments.Config
{
	using System;

	public sealed class NamedArgConfig<TClass, TProp, TRaw> where TClass : class, new()
	{
		public string? ShortName { get; set; }
		public string? LongName { get; set; }
		public ArgumentRequired ArgumentRequired { get; set; }
		public TProp DefaultValue { get; set; }
		public Dependencies<TClass>? Dependencies { get; set; }
		public string HelpText { get; set; }
		public string? DescriptiveName { get; set; }
		public Func<TRaw, Converted<TProp, string>>? Converter { get; set; }
	}
}
