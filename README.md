# CommandLineFluent
A .NET Command Line Parsing library which is set up and parsed using fluent syntax. It parses command line arguments into strongly-typed classes which you define. Supports conversion, validation, default values, and automatic help/usage text.

## Terminology

An Option is a piece of unique text, followed by another. For example: foo.exe -o option
A Value is a lone piece of text. For example: foo.exe value
A Switch is a piece of unique text, whose presence dictates on/off. For example: foo.exe -s


## Examples
### Basic setup

First, you need to create a class with public getters/setters and a public parameterless constructor. This class will hold the parsed arguments.
Then, FluentParser has to be configured to map to that class' properties. Below is a simple example, which parses arguments into a ProcessFile instance.

```csharp
public class ProcessFile
{
	public string OutputFile { get; set; }
	public bool Frobulate { get; set; }
	public string InputFile { get; set; }
}
FluentParser fp = new FluentParser()
	.Configure(config =>
	{
		config.ShowHelpAndUsageOnFailure(); // Goes to the console by default
		config.UseHelpSwitch("-?", "--help"); // If we encounter this, we'll immediately stop and write out some help
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
	});
	
	fp.Parse(args)
		.OnSuccess<ProcessFile>(processFileInstance => MyFrobulationMethod(processFileInstance));
```

### Configuring the Parser

The Parser itself can be configured. Most of the time, you can use defaults, which will be sufficient. The defaults include a default short and long prefix (- and --, respectively), help switches (-? and --help), and will write Help and Usage text to the console.
Note that because the defaults involve setting a default short and long name, you don't need to include these prefixes when adding Options and Switches; they will automatically be prepended to what is provided.

```csharp
new FluentParser().Configure(config => config.ConfigureWithDefaults());
```


### Optional arguments
We also may decide that, we might need to have an optional parameters file.
By using IsOptional(defaultValue), we denote that an Option or Value is not required. If it does not appear, it will be assigned defaultValue. Switches can be given a default by calling WithDefaultValue(defaultValue).
Unless you call IsOptional(), all Options and Values are, by default, required. Switches are always optional.
To set up an Option that is not required, we can use the below...

```csharp
verbless.AddOption("-p", "--parameters")
	.ForProperty(theClass => theClass.ParametersFile)
	.WithName("Parameters file")
	.WithHelpText("A file which contains extra parameters defining how to frobulate the file")
	.IsOptional("defaultFile.frob");
```

### Validators

You can also add some validation. CommandLineFluent comes with some basic validators. For example, Validators.FileExists is a method that takes a string and makes sure that it is a file which exists. If we needed to make sure our file existed first, we can do this.

```csharp
verbless.AddValue()
	.ForProperty(theClass => theClass.InputFile)
	.WithName("Input File")
	.WithValidator(Validators.FileExists) // <-- Added a validator here
	.WithHelpText("The file which has to be processed")
	.IsRequired();
```

### Converters

Validators only need to take a string and return null if valid, or a string (which is the error message) if it is invalid. It's easy to add your own.
Sometimes, you may want to convert the raw string to something else. By adding a converter, the raw string value will be automatically converted to the specified type before setting your class property.
Converters need to return an instance of Converted<T>, because it has a Successful and ErrorMessage property. This way, you can do conversion and validation in one function.
If we wanted to convert the input file to a FileInfo, we can do this...

```csharp
verbless.AddValue<FileInfo>()
	.ForProperty(theClass => theClass.InputFileInfo)
	.WithName("Input File")
	.WithConverter(rawValue => new Converted(new FileInfo(rawValue))) // <-- Added a converter here
	.WithHelpText("The file which has to be processed")
	.IsRequired();
```

### Multiple Verbs

We've only been using a single class for now. But it's possible to set up multiple different verbs, e.g. git add and git pull. Adding Verbs entails the exact same setup as above, except you use need one .AddVerb<VerbClass>(verbName, verbConfig) call per verb. The verb names have to be unique.
You don't strictly need the Verb Names defines as static const strings, but it's easier to manage that way.

```csharp
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
FluentParser fp = new FluentParser()
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
	});
	
	fp.Parse(args)
		.OnSuccess<FrobulateFile>(frob => MyFrobulationMethod(frob))
		.OnSuccess<BojangleFile>(boj => MyBojanglingMethod(boj));
```