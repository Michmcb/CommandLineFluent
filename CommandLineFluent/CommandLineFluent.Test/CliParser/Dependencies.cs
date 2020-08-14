namespace CommandLineFluent.Test.CliParser
{
	using CommandLineFluent;
	using CommandLineFluent.Test.Options;
	using Xunit;
	using static CommandLineFluent.Converters;
	public class Dependencies
	{
		[Fact]
		public void DependencyEqualTo()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.ForProperty(e => e.Value)
						.WithHelpText("h")
						.WithDependencies("", value =>
						{
							value.RequiredIf(e => e.Option).IsEqualTo(25).WithErrorMessage("If Option is equal to 25, you must specify a value");
						}));

					verb.AddOptionInt("o", "option", o => o
						.ForProperty(e => e.Option)
						.WithHelpText("h")
						.WithConverter(ToInt));
				})
				.Build();

			Maybe<IParseResult, System.Collections.Generic.IReadOnlyCollection<Error>> res = fp.Parse(new string[] { "default", "MyValue", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "MyValue", "-o", "25" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "25" });
			Assert.False(res.Ok);

			fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.ForProperty(e => e.Value)
						.WithHelpText("h")
						.WithDependencies("", value =>
						{
							value.MustNotAppearIf(e => e.Option).IsEqualTo(25).WithErrorMessage("If Option is equal to 25, you must NOT specify a value");
						}));

					verb.AddOptionInt("o", "option", o => o
						.ForProperty(e => e.Option)
						.WithHelpText("h")
						.WithConverter(ToInt));
				})
				.Build();

			res = fp.Parse(new string[] { "default", "MyValue", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "MyValue", "-o", "25" });
			Assert.False(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "25" });
			Assert.True(res.Ok);
		}
		[Fact]
		public void DependencyNotEqualTo()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.ForProperty(e => e.Value)
						.WithHelpText("h")
						.WithDependencies("", value =>
						{
							value.RequiredIf(e => e.Option).IsNotEqualTo(25).WithErrorMessage("If Option is not equal to 25, you must specify a value");
						}));

					verb.AddOptionInt("o", "option", o => o
						.ForProperty(e => e.Option)
						.WithHelpText("h")
						.WithConverter(ToInt));
				})
				.Build();

			Maybe<IParseResult, System.Collections.Generic.IReadOnlyCollection<Error>> res = fp.Parse(new string[] { "default", "MyValue", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "30" });
			Assert.False(res.Ok);
			res = fp.Parse(new string[] { "default", "MyValue", "-o", "25" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "25" });
			Assert.True(res.Ok);

			fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.ForProperty(e => e.Value)
						.WithHelpText("h")
						.WithDependencies("", value =>
						{
							value.MustNotAppearIf(e => e.Option).IsNotEqualTo(25).WithErrorMessage("If Option is not equal to 25, you must NOT specify a value");
						}));

					verb.AddOptionInt("o", "option", o => o
						.ForProperty(e => e.Option)
						.WithHelpText("h"));
				})
				.Build();

			res = fp.Parse(new string[] { "default", "MyValue", "-o", "30" });
			Assert.False(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "MyValue", "-o", "25" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "25" });
			Assert.True(res.Ok);
		}
		[Fact]
		public void DependencyNull()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.ForProperty(e => e.Value)
						.WithHelpText("h")
						.WithDependencies("", value =>
						{
							value.RequiredIf(e => e.OptionNullable).IsNull().WithErrorMessage("If OptionNullable is null, you must specify a value");
						}));

					verb.AddOptionNullableInt("o", "option", o => o
						.ForProperty(e => e.OptionNullable)
						.WithHelpText("h")
						.IsOptional(null));
				})
				.Build();

			Maybe<IParseResult, System.Collections.Generic.IReadOnlyCollection<Error>> res = fp.Parse(new string[] { "default", "MyValue", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "MyValue" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default" });
			Assert.False(res.Ok);


			fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.ForProperty(e => e.Value)
						.WithHelpText("h")
						.WithDependencies("", value =>
						{
							value.MustNotAppearIf(e => e.OptionNullable).IsNull().WithErrorMessage("If OptionNullable is null, you must NOT specify a value");
						}));

					verb.AddOptionNullableInt("o", "option", o => o
						.ForProperty(e => e.OptionNullable)
						.WithConverter(ToNullableInt)
						.WithHelpText("h")
						.IsOptional(null));
				})
				.Build();

			res = fp.Parse(new string[] { "default", "MyValue", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "MyValue" });
			Assert.False(res.Ok);
			res = fp.Parse(new string[] { "default" });
			Assert.True(res.Ok);
		}
		[Fact]
		public void DependencyNotNull()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.ForProperty(e => e.Value)
						.WithHelpText("h")
						.WithDependencies("", value =>
						{
							value.RequiredIf(e => e.OptionNullable).IsNotNull().WithErrorMessage("If OptionNullable is not null, you must specify a value");
						}));

					verb.AddOptionNullableInt("o", "option", o => o
						.ForProperty(e => e.OptionNullable)
						.WithHelpText("h")
						.WithConverter(ToNullableInt)
						.IsOptional(null));
				})
				.Build();

			Maybe<IParseResult, System.Collections.Generic.IReadOnlyCollection<Error>> res = fp.Parse(new string[] { "default", "MyValue", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "30" });
			Assert.False(res.Ok);
			res = fp.Parse(new string[] { "default", "MyValue" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default" });
			Assert.True(res.Ok);


			fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.WithHelpText("h")
						.ForProperty(e => e.Value)
						.WithDependencies("", value =>
						{
							value.MustNotAppearIf(e => e.OptionNullable).IsNotNull().WithErrorMessage("If OptionNullable is not null, you must NOT specify a value");
						}));

					verb.AddOptionNullableInt("o", "option", o => o
						.ForProperty(e => e.OptionNullable)
						.WithConverter(ToNullableInt)
						.WithHelpText("h")
						.IsOptional(null));
				})
				.Build();

			res = fp.Parse(new string[] { "default", "MyValue", "-o", "30" });
			Assert.False(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "MyValue" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default" });
			Assert.True(res.Ok);
		}
		[Fact]
		public void DependencyWhen()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.ForProperty(e => e.Value)
						.WithHelpText("h")
						.WithDependencies("", value =>
						{
							value.RequiredIf(e => e.OptionNullable).When(v => v > 50).WithErrorMessage("Only required if OptionNullable is larger than 50");
						}));

					verb.AddOptionNullableInt("o", "option", o => o
						.ForProperty(e => e.OptionNullable)
						.WithConverter(ToNullableInt)
						.WithHelpText("h")
						.IsOptional(null));
				})
				.Build();

			// We're basically saying first: MyValue is required
			Maybe<IParseResult, System.Collections.Generic.IReadOnlyCollection<Error>> res = fp.Parse(new string[] { "default", "MyValue", "-o", "55" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "55" });
			Assert.False(res.Ok);
			// And now we're saying MyValue is not required (So it should work either way)
			res = fp.Parse(new string[] { "default", "MyValue", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "30" });
			Assert.True(res.Ok);


			fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.ForProperty(e => e.Value)
						.WithHelpText("h")
						.WithDependencies("", value =>
						{
							value.MustNotAppearIf(e => e.OptionNullable).When(v => v > 50).WithErrorMessage("Shouldn't appear if OptionNullable is larger than 50");
						}));

					verb.AddOptionNullableInt("o", "option", o => o
						.ForProperty(e => e.OptionNullable)
						.WithHelpText("h")
						.WithConverter(ToNullableInt)
						.IsOptional(null));
				})
				.Build();

			// We're basically saying first: MyValue is required
			res = fp.Parse(new string[] { "default", "MyValue", "-o", "55" });
			Assert.False(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "55" });
			Assert.True(res.Ok);
			// And now we're saying MyValue is not required (So it should work either way)
			res = fp.Parse(new string[] { "default", "MyValue", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "30" });
			Assert.True(res.Ok);
		}
		[Fact]
		public void DependencyWhenNot()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.ForProperty(e => e.Value)
						.WithHelpText("h")
						.WithDependencies("", value =>
						{
							value.RequiredIf(e => e.OptionNullable).WhenNot(v => v > 50).WithErrorMessage("Only required if OptionNullable is not larger than 50");
						}));

					verb.AddOptionNullableInt("o", "option", o => o
						.ForProperty(e => e.OptionNullable)
						.WithHelpText("h")
						.WithConverter(ToNullableInt)
						.IsOptional(null));
				})
				.Build();

			// We're basically saying first: MyValue is required
			Maybe<IParseResult, System.Collections.Generic.IReadOnlyCollection<Error>> res = fp.Parse(new string[] { "default", "MyValue", "-o", "55" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "55" });
			Assert.True(res.Ok);
			// And now we're saying MyValue is not required (So it should work either way)
			res = fp.Parse(new string[] { "default", "MyValue", "-o", "30" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "30" });
			Assert.False(res.Ok);


			fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.ForProperty(e => e.Value)
						.WithHelpText("h")
						.WithDependencies("", value =>
						{
							value.MustNotAppearIf(e => e.OptionNullable).WhenNot(v => v > 50).WithErrorMessage("Must not appear if OptionNullable is smaller than 51");
						}));

					verb.AddOptionNullableInt("o", "option", o => o
						.ForProperty(e => e.OptionNullable)
						.WithHelpText("h")
						.WithConverter(ToNullableInt)
						.IsOptional(null));
				})
				.Build();

			// We're basically saying first: MyValue is required
			res = fp.Parse(new string[] { "default", "MyValue", "-o", "55" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "55" });
			Assert.True(res.Ok);
			// And now we're saying MyValue is not required (So it should work either way)
			res = fp.Parse(new string[] { "default", "MyValue", "-o", "30" });
			Assert.False(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "30" });
			Assert.True(res.Ok);
		}
		[Fact]
		public void DependencyMultipleRules()
		{
			CliParser fp = new CliParserBuilder()
				.AddVerb<VerbVariety>("default", verb =>
				{
					verb.AddValueString(v => v
						.ForProperty(e => e.Value)
						.WithHelpText("h")
						.WithDependencies("", value =>
						{
							value.MustNotAppearIf(e => e.OptionNullable).When(v => v > 50).WithErrorMessage("Must not be provided if OptionNullable is > 50");
							value.RequiredIf(e => e.OptionNullable).When(v => v < 45).WithErrorMessage("Must be provided if OptionNullable is < 45");
							// Optional if it's between 45 to 50, inclusive
						}));

					verb.AddOptionNullableInt("o", "option", o => o
						.ForProperty(e => e.OptionNullable)
						.WithHelpText("h")
						.WithConverter(ToNullableInt)
						.IsOptional(null));
				})
				.Build();

			Maybe<IParseResult, System.Collections.Generic.IReadOnlyCollection<Error>> res = fp.Parse(new string[] { "default", "MyValue", "-o", "55" });
			Assert.False(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "55" });
			Assert.True(res.Ok);

			res = fp.Parse(new string[] { "default", "MyValue", "-o", "47" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "47" });
			Assert.True(res.Ok);

			res = fp.Parse(new string[] { "default", "MyValue", "-o", "40" });
			Assert.True(res.Ok);
			res = fp.Parse(new string[] { "default", "-o", "40" });
			Assert.False(res.Ok);
		}
	}
}
