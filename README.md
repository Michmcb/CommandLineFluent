# CommandLineFluent
A .NET Command Line Parsing library which is set up and parsed using fluent syntax. It parses command line arguments into strongly-typed classes which you define. Supports conversion, validation, default values, and automatic help/usage text. It also supports invoking awaitable or asynchronous actions with the classes you define.

# Migrating from Version 1.x
- Change FluentParser and FluentParserBuilder to CliParser and CliParserBuilder.
- AddVerbless no longer exists. Instead, use a single descriptive verb.
- HelpText is now required for all arguments. Missing help text will cause a CliParserBuilderException to be thrown.
- Remove calls to FluentParserBuilder.Configure(config => config.ConfigureWithDefaults());, as defaults are automatically used. If you are using non-default config options, pass a CliParserConfig object.
- AddValue, AddOption, AddSwitch, and AddMultiValue must accept a new parameter, which is an action that accepts an object which lets you configure the argument.
- When calling AddValue, AddOption, AddSwitch, and AddMultiValue, you can append the type name (e.g. AddOptionInt) for the default converter to be used automatically. By doing this, you can also remove WithConverter() calls if they are setting up the default converter.
- Validators no longer exist. Instead, use a converter that performs validation.
- Replace the call to parser.Parse().OnSuccess<Type1>() etc., with a call to parser.Handle(parser.Parse()).


## Terminology
An Option is a piece of unique text, followed by another. For example: foo.exe -o option
A Switch is a piece of unique text, whose presence dictates on/off. For example: foo.exe -s
A Value is a lone piece of text. For example: foo.exe value
A MultiValue is many lone pieces of text. For example: foo.exe value1 value2 value3

## Examples
### Basic Parsing
Create a class with public getters/setters and a public parameterless constructor. This class will hold the parsed arguments.

Then, CliParser has to be configured using the CliParserBuilder to map to that class' properties. Below is a simple example, which parses arguments into a ProcessFile instance.

```csharp
public class ProcessFile
{
	public string OutputFile { get; set; }
	public bool Frobulate { get; set; }
	public string InputFile { get; set; }
}
	// Default configuration is used by not providing a ctor argument
CliParser parser = new CliParserBuilder()
	.AddVerb<ProcessFile>("process", verb =>
	{
		verb.HelpText = "Does something to the input file";
		// You don't need both but this is just an example
		// But note if you try and call one that is not set, it will throw an exception
		verb.Invoke = ProcessMethod;
		verb.InvokeAsync = ProcessMethodAsync;

		// ValueProperty is a string
		verb.AddValueString(v => v
			.ForProperty(theClass => theClass.InputFile)
			.WithName("Input File")
			.WithHelpText("The file which has to be processed"));
		
		// Frobulate is a bool
		verb.AddSwitchBool("-f", "--frobulate", s => s
			.ForProperty(theClass => theClass.Frobulate)
			.WithName("Frobulation Specifier")
			.WithHelpText("If provided, the file will be frobulated"));
		
		// OutputFile is a string
		verb.AddOptionString("-o", "--output", o => o
			.ForProperty(theClass => theClass.OutputFile)
			.WithName("Output file")
			.WithHelpText("The output file"));
	})
	.Build();

	// parser.Handle(IParseResult) is a convenience method that, if parsing failed (or help was requested), writes error messages and overall usage information, or information for a specific verb
	// And if parsing worked, it calls the appropriate Invoke method, which as configured above, is ProcessMethod()
	IParseResult result = parser.Parse(args);

	parser.Handle(result); // calls what we set the Invoke property to
	parser.HandleAsync(result); // calls what we set the InvokeAsync property to

	// And if we want to provide a shell-like interface, all you need to do is call this.
	// You can set the colour of the prompt and the colour of the commands (i.e. what the user enters) like this too. It will loop until the user enters "exit".
	parser.Shell(prompt: "FileProcessor> ", exitKeyword: "exit", promptColor: ConsoleColor.White, commandColor: ConsoleColor.Gray);
```

### Configuring the Parser
Most of the time, you can use the defaults. They are:
- A default short and long prefix (- and --, respectively)
- Help switches (-? and --help)
- Matches verbs and option/switch names using a case-insensitive comparison
Because the defaults involve setting a default short and long name prefix, you don't need to include these prefixes when adding Options and Switches.
To change configuration, you only need to pass a CliParserConfig object.

You can also set a custom message formatter, a custom tokenizer (which splits a string into an IEnumerable<string>, to parse strings, not just string arrays), or a custom Console (can be useful for unit tests)

```csharp
new CliParserBuilder(new CliParserConfig()
	{
		// Use a single dash for both
		DefaultShortPrefix = "-",
		DefaultLongPrefix = "-",
		
		// The user has to be REALLY desperate for some help
		ShortHelpSwitch = "-h",
		LongHelpSwitch = "-heeeeeeelp",
		
		// Case insensitive just to make life difficult
		StringComparer = StringComparer.Ordinal
	})
	.UseTokenizer(new MyTokenizer()) // This is, by default, QuotedStringTokenizer
	.UseConsole(new MyConsole()) // This is, by default, StandardConsole, which just calls Console methods
	.UseMessageFormatter(new MyMessageFormatter()) // This is, by default, StandardMessageFormatter, which provides default usage, help, and error formatting.
	.AddVerb<ProcessFile>(verb =>
	{
		// This automatically prefixes these, so they become -f and -frobulate
		verb.AddSwitchBool("f", "frobulate", s => s
			.ForProperty(x => x.Frobulate)
			.WithName("Frobulate")
			.WithHelpText("Frobulates stuff"));
		
		// This doesn't prefix them, because they already have the default prefix, so they remain as -b and -bojangle
		verb.AddSwitchBool("-b", "-bojangle", s => s
			.ForProperty(x => x.Bojangle)
			.WithName("Bojangle")
			.WithHelpText("Bojangles stuff"));
	};
```

### Required/Optional arguments
By using IsOptional(defaultValue), we denote that an Option or Value is not required. If it does not appear, it will be assigned defaultValue.
And IsRequired() means they're required, although this is normally the default.

All Options, Values, and MultiValues are required by default. Switches are optional by default.

```csharp
verb.AddOptionString("-p", "--parameters", s => s
	.ForProperty(theClass => theClass.ParametersFile)
	.WithName("Parameters file")
	.WithHelpText("A file which contains extra parameters defining how to frobulate the file")
	.IsOptional("defaultFile.frob")); // If not provided, property will be assigned this string
```

### Conditional Dependencies
By using WithDependencies(config), we can define some arguments are only required under certain conditions.
For example, we can allow somebody to log on with either a username and password, or denote that they want to use a single sign on method, for their current account. In this case it doesn't make sense to provide a username/password AND use single sign on, so we can configure the CliParser to only accept one or the other.

```csharp
verb.AddOptionString("-u", "--username", o => o
	.ForProperty(theClass => theClass.Username)
	.WithName("Username")
	.WithHelpText("The username to use to log in")
	.WithDependencies(opt =>
	{
		// We have to specify when it's required AND when it mustn't appear. There are no implicit rules when you use dependencies.

		opt.RequiredIf(theClass => theClass.UseSingleSignOn)
			.IsEqualTo(false) // We can compare the property value against a specific value like this
			.WithErrorMessage("If you don't want to use Single Sign On, you must provide a username");

		opt.MustNotAppearIf(theClass => theClass.UseSingleSignOn)
			.When(UseSingleSignOnValue => UseSingleSignOnValue == true) // Or we can use a predicate for more complex comparisons
			.WithErrorMessage("If you want to use Single Sign On, you cannot provide a username");
	}));

verb.AddOptionString("-p", "--password", o => o
	.ForProperty(theClass => theClass.Password)
	.WithName("Password")
	.WithHelpText("The password to use to log in")
	.WithDependencies(config =>
	{
		// Note you don't HAVE to specify a property. You can specify the class itself if you need to check multiple properties at once
		config.RequiredIf(theClass => theClass)
			.When(theClass => theClass.UseSingleSignOn == false)
			.WithErrorMessage("If you don't want to use Single Sign On, you must provide a password");

		config.MustNotAppearIf(theClass => theClass.UseSingleSignOn)
			.IsEqualTo(true)
			.WithErrorMessage("If you don't want to use Single Sign On, you must provide a password");
	}));

verb.AddSwitchBool("-s", "--singlesignon", s => s
	.ForProperty(theClass => theClass.UseSingleSignOn)
	.WithName("Single Sign-On")
	.WithHelpText("If specified, single sign on is used, and no username/password is required"));
```

### Converters
Converters take the raw string value, and validate/convert it to something else. They are automatically invoked when parsing.
There are converters built in for most primitive types, which are automatically set by using the appropriate "AddOptionType" overload.

```csharp
verb.AddOptionInt("f", "frobulationIntensity", o => o
	.ForProperty(theClass => theClass.InputFileInfo)
	.WithName("Frobulation Intensity")
	.WithHelpText("frobulation intensifies")
	.IsOptional(5)); // Optional values will also be typed as ints
```

To use a custom converter, you can do the below, and return a Converted<Type, string> instance in the WithConverter delegate, where string is the error message.
Converted<Type, string> instances are implicitly cast from either of their generic types, automatically denoting success or failure.
However if you need to return Converted<string, string>, you need to use Converted<string, string>.Value() and Converted<string, string>.Error().

```csharp
verb.AddOption<MyType>("f", "frobulationIntensity", o => o
	.ForProperty(theClass => theClass.InputFileInfo)
	.WithName("Frobulation Intensity")
	.WithHelpText("frobulation intensifies")
	.IsOptional(5) // Optional values will also be typed as ints, as we can see
	.WithConverter(rawString =>
	{
		if (MyType.TryParse(rawString, out MyType instance)
		{
			// Converted<MyType, string> can be implicitly constructed as successful from MyType
			return instance;
		}
		else
		{
			// Likewise, it can also be implicitly constructed as failed from string, which is the error message.
			return "Failed to parse as a MyType instance";
		}
	})));
```

### Multiple Verbs
It's possible to set up multiple different verbs, e.g. git add and git pull.

Adding Verbs entails the exact same setup as above, except you use need one .AddVerb<VerbClass>(verbName, verbConfig) call per verb. The verb names have to be unique.

```csharp
// Verb Names don't have to be const fields, but it may be easier to manage
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
CliParser parser = new CliParserBuilder()
	.AddVerb<FrobulateFile>(FrobulateFile.verbName, verb =>
	{
		verb.AddOption("-i", "--inputFile", o => o
			.WithName("Input File")
			.WithHelpText("The input file which will get Frobulated")
			.ForProperty(o => o.InputFile));
	})
	.AddVerb<BojangleFile>(BojangleFile.verbName, verb =>
	{
		verb.AddOption("-i", "--inputFile", o => o
			.WithName("Input File")
			.WithHelpText("The input file which will get Bojangled")
			.ForProperty(o => o.InputFile));
	}).Build();

	parser.Handle(parser.Parse(args));
```

### Multi-Arguments
A Multivalue picks up any lone arguments, like a Value. However it will collect a list, instead.
They're almost identical to set up. The only difference is that when calling `.ForProperty(x => x.MyProperty)` for a recognized collection type, it will also set up a callback that creates that collection.
For example if you have a Stack<string>, then `.ForProperty(x => x.MyStack)` will set up a callback that turns the IReadOnlyCollection<string> into a Stack<string>. You can define your own collection creator as the second argument passed to ForProperty.