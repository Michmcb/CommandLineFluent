# CommandLineFluent
A .NET Command Line Parsing library which is set up and parsed using fluent syntax. It parses command line arguments into strongly-typed classes which you define. Supports conversion, validation, default values, and automatic help/usage text.

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
verbless.AddValue<FileInfo>()
	.ForProperty(theClass => theClass.InputFileInfo)
	.WithName("Input File")
	.WithConverter(rawValue =>
	{
		if(File.Exists(rawValue))
		{
			return new Converted<FileInfo>(new FileInfo(rawValue));
		}
		else
		{
			return new Converted<FileInfo>(null, $"The file {rawValue} doesn't exist");
		}
	})
	.WithHelpText("The file which has to be processed")
	.IsOptional(new FileInfo()); // Optional values will also be typed as FileInfo objects
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