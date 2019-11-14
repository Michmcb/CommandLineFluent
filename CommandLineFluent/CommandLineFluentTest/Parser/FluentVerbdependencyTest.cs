using CommandLineFluent;
using Xunit;
using static CommandLineFluent.Conversion.Converters;

namespace CommandLineFluentTest.Parser
{
	public class FluentVerbdependencyTest
	{
		[Fact]
		public void DependencyEqualTo()
		{
			FluentParser fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verb =>
				{
					verb.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.RequiredIf(e => e.Option).IsEqualTo(25).WithErrorMessage("If Option is equal to 25, you must specify a value");
						});

					verb.AddOption<int>("o", "option")
						.ForProperty(e => e.Option)
						.WithConverter(ToInt);
				})
				.Build();

			FluentParserResult res = fp.Parse(new string[] { "MyValue", "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "MyValue", "-o", "25" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "25" });
			Assert.False(res.Success);


			fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verb =>
				{
					verb.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.MustNotAppearIf(e => e.Option).IsEqualTo(25).WithErrorMessage("If Option is equal to 25, you must NOT specify a value");
						});

					verb.AddOption<int>("o", "option")
						.ForProperty(e => e.Option)
						.WithConverter(ToInt);
				})
				.Build();

			res = fp.Parse(new string[] { "MyValue", "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "MyValue", "-o", "25" });
			Assert.False(res.Success);
			res = fp.Parse(new string[] { "-o", "25" });
			Assert.True(res.Success);
		}
		[Fact]
		public void DependencyNotEqualTo()
		{
			FluentParser fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verb =>
				{
					verb.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.RequiredIf(e => e.Option).IsNotEqualTo(25).WithErrorMessage("If Option is not equal to 25, you must specify a value");
						});

					verb.AddOption<int>("o", "option")
						.ForProperty(e => e.Option)
						.WithConverter(ToInt);
				})
				.Build();

			FluentParserResult res = fp.Parse(new string[] { "MyValue", "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "30" });
			Assert.False(res.Success);
			res = fp.Parse(new string[] { "MyValue", "-o", "25" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "25" });
			Assert.True(res.Success);


			fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verb =>
				{
					verb.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.MustNotAppearIf(e => e.Option).IsNotEqualTo(25).WithErrorMessage("If Option is not equal to 25, you must NOT specify a value");
						});

					verb.AddOption<int>("o", "option")
						.ForProperty(e => e.Option)
						.WithConverter(ToInt);
				})
				.Build();

			res = fp.Parse(new string[] { "MyValue", "-o", "30" });
			Assert.False(res.Success);
			res = fp.Parse(new string[] { "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "MyValue", "-o", "25" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "25" });
			Assert.True(res.Success);
		}
		[Fact]
		public void DependencyNull()
		{
			FluentParser fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verb =>
				{
					verb.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.RequiredIf(e => e.OptionNullable).IsNull().WithErrorMessage("If OptionNullable is null, you must specify a value");
						});

					verb.AddOption<int?>("o", "option")
						.ForProperty(e => e.OptionNullable)
						.WithConverter(ToNullableInt)
						.IsOptional(null);
				})
				.Build();

			FluentParserResult res = fp.Parse(new string[] { "MyValue", "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "MyValue" });
			Assert.True(res.Success);
			res = fp.Parse(System.Array.Empty<string>());
			Assert.False(res.Success);


			fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verb =>
				{
					verb.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.MustNotAppearIf(e => e.OptionNullable).IsNull().WithErrorMessage("If OptionNullable is null, you must NOT specify a value");
						});

					verb.AddOption<int?>("o", "option")
						.ForProperty(e => e.OptionNullable)
						.WithConverter(ToNullableInt)
						.IsOptional(null);
				})
				.Build();

			res = fp.Parse(new string[] { "MyValue", "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "MyValue" });
			Assert.False(res.Success);
			res = fp.Parse(System.Array.Empty<string>());
			Assert.True(res.Success);
		}
		[Fact]
		public void DependencyNotNull()
		{
			FluentParser fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verb =>
				{
					verb.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.RequiredIf(e => e.OptionNullable).IsNotNull().WithErrorMessage("If OptionNullable is not null, you must specify a value");
						});

					verb.AddOption<int?>("o", "option")
						.ForProperty(e => e.OptionNullable)
						.WithConverter(ToNullableInt)
						.IsOptional(null);
				})
				.Build();

			FluentParserResult res = fp.Parse(new string[] { "MyValue", "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "30" });
			Assert.False(res.Success);
			res = fp.Parse(new string[] { "MyValue" });
			Assert.True(res.Success);
			res = fp.Parse(System.Array.Empty<string>());
			Assert.True(res.Success);


			fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verb =>
				{
					verb.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.MustNotAppearIf(e => e.OptionNullable).IsNotNull().WithErrorMessage("If OptionNullable is not null, you must NOT specify a value");
						});

					verb.AddOption<int?>("o", "option")
						.ForProperty(e => e.OptionNullable)
						.WithConverter(ToNullableInt)
						.IsOptional(null);
				})
				.Build();

			res = fp.Parse(new string[] { "MyValue", "-o", "30" });
			Assert.False(res.Success);
			res = fp.Parse(new string[] { "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "MyValue" });
			Assert.True(res.Success);
			res = fp.Parse(System.Array.Empty<string>());
			Assert.True(res.Success);
		}
		[Fact]
		public void DependencyWhen()
		{
			FluentParser fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verb =>
				{
					verb.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.RequiredIf(e => e.OptionNullable).When(v => v > 50).WithErrorMessage("Only required if OptionNullable is larger than 50");
						});

					verb.AddOption<int?>("o", "option")
						.ForProperty(e => e.OptionNullable)
						.WithConverter(ToNullableInt)
						.IsOptional(null);
				})
				.Build();

			// We're basically saying first: MyValue is required
			FluentParserResult res = fp.Parse(new string[] { "MyValue", "-o", "55" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "55" });
			Assert.False(res.Success);
			// And now we're saying MyValue is not required (So it should work either way)
			res = fp.Parse(new string[] { "MyValue", "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "30" });
			Assert.True(res.Success);


			fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verb =>
				{
					verb.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.MustNotAppearIf(e => e.OptionNullable).When(v => v > 50).WithErrorMessage("Shouldn't appear if OptionNullable is larger than 50");
						});

					verb.AddOption<int?>("o", "option")
						.ForProperty(e => e.OptionNullable)
						.WithConverter(ToNullableInt)
						.IsOptional(null);
				})
				.Build();

			// We're basically saying first: MyValue is required
			res = fp.Parse(new string[] { "MyValue", "-o", "55" });
			Assert.False(res.Success);
			res = fp.Parse(new string[] { "-o", "55" });
			Assert.True(res.Success);
			// And now we're saying MyValue is not required (So it should work either way)
			res = fp.Parse(new string[] { "MyValue", "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "30" });
			Assert.True(res.Success);
		}
		[Fact]
		public void DependencyWhenNot()
		{
			FluentParser fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verb =>
				{
					verb.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.RequiredIf(e => e.OptionNullable).WhenNot(v => v > 50).WithErrorMessage("Only required if OptionNullable is not larger than 50");
						});

					verb.AddOption<int?>("o", "option")
						.ForProperty(e => e.OptionNullable)
						.WithConverter(ToNullableInt)
						.IsOptional(null);
				})
				.Build();

			// We're basically saying first: MyValue is required
			FluentParserResult res = fp.Parse(new string[] { "MyValue", "-o", "55" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "55" });
			Assert.True(res.Success);
			// And now we're saying MyValue is not required (So it should work either way)
			res = fp.Parse(new string[] { "MyValue", "-o", "30" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "30" });
			Assert.False(res.Success);


			fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verb =>
				{
					verb.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.MustNotAppearIf(e => e.OptionNullable).WhenNot(v => v > 50).WithErrorMessage("Must not appear if OptionNullable is smaller than 51");
						});

					verb.AddOption<int?>("o", "option")
						.ForProperty(e => e.OptionNullable)
						.WithConverter(ToNullableInt)
						.IsOptional(null);
				})
				.Build();

			// We're basically saying first: MyValue is required
			res = fp.Parse(new string[] { "MyValue", "-o", "55" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "55" });
			Assert.True(res.Success);
			// And now we're saying MyValue is not required (So it should work either way)
			res = fp.Parse(new string[] { "MyValue", "-o", "30" });
			Assert.False(res.Success);
			res = fp.Parse(new string[] { "-o", "30" });
			Assert.True(res.Success);
		}
		[Fact]
		public void DependencyMultipleRules()
		{
			FluentParser fp = new FluentParserBuilder()
				.Configure(config => config.ConfigureWithDefaults())
				.WithoutVerbs<VerbVariety>(verbless =>
				{
					verbless.AddValue<string>()
						.ForProperty(e => e.Value)
						.WithDependencies(value =>
						{
							value.MustNotAppearIf(e => e.OptionNullable).When(v => v > 50).WithErrorMessage("Must not be provided if OptionNullable is > 50");
							value.RequiredIf(e => e.OptionNullable).When(v => v < 45).WithErrorMessage("Must be provided if OptionNullable is less than 45");
							// Optional if it's between 45 to 50, inclusive
						});

					verbless.AddOption<int?>("o", "option")
						.ForProperty(e => e.OptionNullable)
						.WithConverter(ToNullableInt)
						.IsOptional(null);
				})
				.Build();

			FluentParserResult res = fp.Parse(new string[] { "MyValue", "-o", "55" });
			Assert.False(res.Success);
			res = fp.Parse(new string[] { "-o", "55" });
			Assert.True(res.Success);

			res = fp.Parse(new string[] { "MyValue", "-o", "47" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "47" });
			Assert.True(res.Success);

			res = fp.Parse(new string[] { "MyValue", "-o", "40" });
			Assert.True(res.Success);
			res = fp.Parse(new string[] { "-o", "40" });
			Assert.False(res.Success);
		}
	}
}
