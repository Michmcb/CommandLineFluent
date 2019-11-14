# CommandLineFluent
A .NET Command Line Parsing library which is set up and parsed using fluent syntax. It parses command line arguments into strongly-typed classes which you define. Supports conversion, validation, default values, and automatic help/usage text. It also supports invoking awaitable or asynchronous actions with the classes you define.

## Terminology

An Option is a piece of unique text, followed by another. For example: foo.exe -o option

A Value is a lone piece of text. For example: foo.exe value

A Switch is a piece of unique text, whose presence dictates on/off. For example: foo.exe -s

## Examples
### Basic Parsing

Create a class with public getters/setters and a public parameterless constructor. This class will hold the parsed arguments.

Then, FluentParser has to be configured using the FluentParserBuilder to map to that class' properties. Below is a simple example, which parses arguments into a ProcessFile instance.

```csharp
public class ProcessFile
{
	public string OutputFile { get; set; }
	public bool Frobulate { get; set; }
	public string InputFile { get; set; }
}
FluentParser fp = new FluentParserBuilder()
	.Configure(config =>
	{
		config.ShowHelpAndUsageOnFailure(); // Goes to the console by default
		config.UseHelpSwitch("-?", "--help"); // If encountered, the Parser immediately stops and writes help/usage
	})
	.WithoutVerbs<ProcessFile>(verbless =>
	{
		verbless.WithHelpText("Does something to the input file");

		// ValueProperty is a string
		verbless.AddValue()
			.ForProperty(theClass => theClass.InputFile)
			.WithName("Input File")
			.WithHelpText("The file which has to be processed")
			.IsRequired();
		
		// Frobulate is a bool
		verbless.AddSwitch("-f", "--frobulate")
			.ForProperty(theClass => theClass.Frobulate)
			.WithHelpText("If provided, the file will be frobulated");
		
		// OutputFile is a string
		verbless.AddOption("-o", "--output")
			.ForProperty(theClass => theClass.OutputFile)
			.WithName("Output file")
			.WithHelpText("The output file")
			.IsRequired();
	}).Build();
	
	fp.Parse(args)
		.OnFailure(errors => MyFailureMethod(errors))
		.OnSuccess<ProcessFile>(processFileInstance => MyFrobulationMethod(processFileInstance));
```

### Configuring the Parser

Most of the time, you can use defaults. They are: A default short and long prefix (- and --, respectively), help switches (-? and --help), and writes Help and Usage text to the console on any parsing errors (either verb-specific or overall).

Because the defaults involve setting a default short and long name prefix, you don't need to include these prefixes when adding Options and Switches.

```csharp
new FluentParserBuilder().Configure(config => config.ConfigureWithDefaults())
	.WithoutVerbs<ProcessFile>(verbless =>
	{
		verbless.AddSwitch("f", "frobulate"); // Defaults automatically prefix these, so they become -f and --frobulate
	};
```

### Optional arguments
By using IsOptional(defaultValue), we denote that an Option or Value is not required. If it does not appear, it will be assigned defaultValue. Switches can be given a default by calling WithDefaultValue(defaultValue).

All Options and Values are, by default, required. Switches are always optional.

```csharp
verbless.AddOption("-p", "--parameters")
	.ForProperty(theClass => theClass.ParametersFile)
	.WithName("Parameters file")
	.WithHelpText("A file which contains extra parameters defining how to frobulate the file")
	.IsOptional("defaultFile.frob"); // If not provided, property will be assigned this string
```

### Conditional Dependencies
By using WithDependencies(config), we can define some arguments are only required under certain conditions.
For example, we can allow somebody to log on with either a username and password, or denote that they want to use a single sign on method, for their current account. In this case it doesn't make sense to provide a username/password AND use single sign on, so we can configure the FluentParser to only accept one or the other.

```csharp
verbless.AddOption("-u", "--username")
	.ForProperty(theClass => theClass.Username)
	.WithDependencies(config =>
	{
		// We have to specify when it's required AND when it mustn't appear. There are no implicit rules when you use dependencies.

		config.RequiredIf(theClass => theClass.UseSingleSignOn)
			.IsEqualTo(false) // We can compare the property value against a specific value like this
			.WithErrorMessage("If you don't want to use Single Sign On, you must provide a username");

		config.MustNotAppearIf(theClass => theClass.UseSingleSignOn)
			.When(UseSingleSignOnValue => UseSingleSignOnValue == true) // Or we can use a predicate for more complex comparisons
			.WithErrorMessage("If you want to use Single Sign On, you cannot provide a username");
	});

verbless.AddOption("-p", "--password")
	.ForProperty(theClass => theClass.Password)
	.WithDependencies(config =>
	{
		// Note you don't HAVE to specify a property. You can specify the class itself if you need to check multiple properties at once
		config.RequiredIf(theClass => theClass)
			.When(theClass => theClass.UseSingleSignOn == false)
			.WithErrorMessage("If you don't want to use Single Sign On, you must provide a password");

		config.MustNotAppearIf(theClass => theClass.UseSingleSignOn)
			.IsEqualTo(true)
			.WithErrorMessage("If you don't want to use Single Sign On, you must provide a password");
	});

verbless.AddSwitch("-s", "--singlesignon")
	.ForProperty(theClass => theClass.UseSingleSignOn);
```

### Validators

You can also add some validation. CommandLineFluent comes with some basic validators. For example, Validators.FileExists is a method that takes a string and makes sure that it is a file which exists.

Validators return a string, which is the error message. Return null to indicate validation was successful.

```csharp
verbless.AddValue()
	.ForProperty(theClass => theClass.InputFile)
	.WithName("Input File")
	.WithValidator(Validators.FileExists) // <-- Added a validator here
	.WithHelpText("The file which has to be processed")
	.IsRequired();
```

### Converters

Converters take the raw string value and convert it to something else. They are automatically invoked when parsing.

Constructing a Converted<T> instance like the below indicates success. An optional second parameter denotes the error message; if provided, conversion is considered to have failed.

```csharp
verbless.AddOption<bool>("f", "frobulateTheFile")
	.ForProperty(theClass => theClass.InputFileInfo)
	.WithName("Frobulation")
	.WithConverter(Converters.ToBool)
	.WithHelpText("Whether or not to frobulate the file")
	.IsOptional(false); // Optional values will also be typed as bools
```

### Awaitable/Asynchronous

If your target methods are awaitble (i.e. They return a Task object) then you are able to invoke them and await the Task or invoke them asynchronously. The Invoke() method will return the Task your method returns, and the InvokeAsync() method will await the Task your method returns.

```csharp
FluentParser fp = new FluentParserBuilder()
	.Configure(config =>
	{
		config.ConfigureWithDefaults();
	})
	.WithoutVerbs<ProcessFile>(verbless =>
	{
		// ValueProperty is a string
		verbless.AddValue()
			.ForProperty(theClass => theClass.InputFile)
			.WithName("Input File")
			.WithHelpText("The file which has to be processed")
			.IsRequired();
	}).Build();

	// Await the task that MyFrobulationMethodAsync returns, .Invoke() will just return the Task without awaiting it
	await fp.ParseAwaitable(args)
		.OnSuccess<ProcessFile>(processFileInstance => MyFrobulationMethodAsync(processFileInstance))
		.Invoke();

	// Await the task that MyFrobulationMethodAsync returns, .InvokeAsync() will await the Task
	await fp.ParseAwaitable(args)
		.OnSuccess<ProcessFile>(processFileInstance => MyFrobulationMethodAsync(processFileInstance))
		.InvokeAsync();
```


### Multiple Verbs

It's possible to set up multiple different verbs, e.g. git add and git pull.

Adding Verbs entails the exact same setup as above, except you use need one .AddVerb<VerbClass>(verbName, verbConfig) call per verb. The verb names have to be unique.

```csharp
// Verb Names don't have to be const fields, but it's easier to manage
public class FrobulateFile
{
	public const string verbName = "frobulate";
	public string InputFile { get; set; }
}
public class BojangleFile
{
	public const string verbName = "bojangle";
	public string InputFile { get; set; }
}
FluentParser fp = new FluentParserBuilder()
	.Configure(config =>
	{
		config.ShowHelpAndUsageOnFailure(); // Goes to the console by default
		config.UseHelpSwitch("-?", "--help"); // If we encounter this, we'll immediately stop and write out some help
	})
	.AddVerb<FrobulateFile>(FrobulateFile.verbName, verb =>
	{
		verb.AddOption("-i", "--inputFile")
			.ForProperty(o => o.InputFile);
	})
	.AddVerb<BojangleFile>(BojangleFile.verbName, verb =>
	{
		verb.AddOption("-i", "--inputFile")
			.ForProperty(o => o.InputFile);
	}).Build();
	
	fp.Parse(args)
		.OnSuccess<FrobulateFile>(frob => MyFrobulationMethod(frob))
		.OnSuccess<BojangleFile>(boj => MyBojanglingMethod(boj));
```