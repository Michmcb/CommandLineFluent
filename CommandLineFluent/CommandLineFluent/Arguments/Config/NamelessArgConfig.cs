﻿namespace CommandLineFluent.Arguments.Config
{
	using System;

	public sealed class NamelessArgConfig<TClass, TProp, TRaw> where TClass : class, new()
	{
		public ArgumentRequired ArgumentRequired { get; set; }
		public TProp DefaultValue { get; set; }
		public Dependencies<TClass>? Dependencies { get; set; }
		public string HelpText { get; set; }
		public string? DescriptiveName { get; set; }
		public Func<TRaw, Converted<TProp, string>>? Converter { get; set; }
	}
}
