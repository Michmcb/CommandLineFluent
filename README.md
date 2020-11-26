# CommandLineFluent
A .NET Command Line Parsing library which is set up and parsed using fluent syntax. It parses command line arguments into strongly-typed classes which you define. Supports conversion, validation, default values, and automatic help/usage text. It also supports invoking awaitable or asynchronous actions with the classes you define.


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
		verb.AddValue(theClass => theClass.InputFile, x => {
				x.DescriptiveName = "Input File";
				x.HelpText = "The file which has to be processed";
			});
		
		// Frobulate is a bool
		verb.AddSwitch(theClass => theClass.Frobulate, x => {
				x.ShortName = "-f";
				x.LongName = "--frobulate";
				x.DescriptiveName = "Frobulation Specifier";
				x.HelpText = "If provided, the file will be frobulated";
			});
		
		// OutputFile is a string
		verb.AddOption(theClass => theClass.OutputFile, x => {
				x.ShortName = "-o";
				x.LongName = "--output";
				x.DescriptiveName = "Output file";
				x.HelpText = "The output file";
			});
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
To change configuration, you need to pass a CliParserConfig object to the CliParserBuilder's constructor.

You can also set a custom message formatter, a custom tokenizer (which splits a string into an IEnumerable<string> to parse arguments), or a custom Console (can be useful for unit tests)

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
	.AddVerb<ProcessFile>("process", verb =>
	{
		// This automatically prefixes these, so they become -f and -frobulate
		verb.AddSwitch(x => x.Frobulate, x => {
				x.ShortName = "f";
				x.LongName = "frobulate";
				x.DescriptiveName = "Frobulate";
				x.HelpText = "Frobulates stuff";
			});
		
		// This doesn't prefix them again, because they already have the default prefix, so they remain as -b and -bojangle
		verb.AddSwitch(x => x.Bojangle, x => {
				x.ShortName = "-b";
				x.LongName = "-bojangle";
				x.DescriptiveName = "Bojangle";
				x.HelpText = "Bojangles stuff";
			});
	};
```

### Required/Optional arguments
By setting DefaultValue, we denote that an Argument is not required, and has a default value when it does not appear. If it does not appear, it will be assigned DefaultValue. Setting DefaultValue also sets IsRequired to false.
Setting IsRequired won't configure a default value.
The default value for IsRequired depends on the kind of argument and the nullability of the target property type.
- Options and Values for non-nullable properties are required, whereas they are optional for nullable properties.
- Switches and MultiValues are always optional.

All Options, Values, and MultiValues are required by default. Switches are optional by default.

```csharp
verb.AddOption(x => x.ParametersFile, x => {
		x.ShortName = "-p";
		x.LongName = "--parameters";
		x.DescriptiveName = "Parameters file";
		x.HelpText = "A file which contains extra parameters defining how to frobulate the file";
		x.DefaultValue = "defaultFile.frob"; // If not provided, property will be assigned this string
		x.IsRequired = false; // Not required, because setting DefaultValue sets this to false automatically
	});
```

### Conditional Dependencies
By using the Dependencies property, we can define some arguments are only required under certain conditions.
For example, we can allow somebody to log on with either a username and password, or denote that they want to use a single sign on method, for their current account. In this case it doesn't make sense to provide a username/password AND use single sign on, so we can configure the CliParser to only accept one or the other.

```csharp
verb.AddOption(x => x.Username, x => {
		x.ShortName = "-u";
		x.LongName = "--username";
		x.DescriptiveName = "Username";
		x.HelpText = "The username to use to log in";
		
		// We have to specify when it's required AND when it mustn't appear. There are no implicit rules when you use dependencies.
		
		x.HasDependency.RequiredIf(theClass => theClass.UseSingleSignOn)
			.IsEqualTo(false) // We can compare the property value against a specific value like this
			.WithErrorMessage("If you don't want to use Single Sign On, you must provide a username");

		x.HasDependency.MustNotAppearIf(theClass => theClass.UseSingleSignOn)
			.When(UseSingleSignOnValue => UseSingleSignOnValue == true) // Or we can use a predicate for more complex comparisons
			.WithErrorMessage("If you want to use Single Sign On, you cannot provide a username");
	});

verb.AddOption(x => x.Password, x => {
		x.ShortName = "-p";
		x.LongName = "--password";
		x.DescriptiveName = "Password";
		x.HelpText = "The password to use to log in";
		
		// Note you don't HAVE to specify a property. You can specify the class itself if you need to check multiple properties at once
		
		x.HasDependency.RequiredIf(theClass => theClass)
			.When(theClass => theClass.UseSingleSignOn == false)
			.WithErrorMessage("If you don't want to use Single Sign On, you must provide a password");

		x.HasDependency.MustNotAppearIf(theClass => theClass.UseSingleSignOn)
			.IsEqualTo(true)
			.WithErrorMessage("If you don't want to use Single Sign On, you must provide a password");
	});

verb.AddSwitch(x => x.UseSingleSignOn, x => {
		x.ShortName = "-s";
		x.LongName = "--singlesignon";
		x.DescriptiveName = "Single Sign-On";
		x.HelpText = "If specified, single sign on is used, and no username/password is required";
	});
```

### Converters
Converters take the raw string value, and validate/convert it to something else. They are automatically invoked when parsing.
There are converters built in for most primitive types, which are automatically set for you.
The primitive types that are supported by default are: string, short, int, uint, long, ulong, float, double, decimal, enum types, DateTime, TimeSpan, Guid, and nullables for all of those types.

```csharp
verb.AddOption(x => x.MyInt, x => {
	x.ShortName = "-i";
	x.LongName = "--integer";
	x.DescriptiveName = "Some number";
	x.HelpText = "Number of things";
});
```

To use a custom converter, you can create an extension method or just call AddOptionCore. The same goes for AddValueCore, AddSwitchCore, and AddMultiValueCore.
This is how you can call AddOptionCore.

```csharp
verb.AddOptionCore<MyType>(x => x.InputFileInfo, x => {
		x.ShortName = "-f";
		x.LongName = "--frobulationIntensity";
		x.DescriptiveName = "Frobulation Intensity";
		x.HelpText = "frobulation intensifies";
		x.DefaultValue = MyType.Default;
		
		// Returning a string is the error message, whereas returning a type of TProp is success.
		// This is just for demonstration
		x.Converter = (rawString) => {
		if (MyType.TryParse(rawString, out MyType value) {
			// Converted<TProp, string> can be implicitly constructed as successful from MyType
			return value;
		}
		else {
			// It can also be implicitly constructed as failed from string, which is the error message.
			return "Cannot parse string as MyType";
		}
	}
});
```

You can also create an extension method to set some defaults on the object. This way you don't need to provide certain properties every time.
Note, if you need to return Converted<string, string>, you need to use Converted<string, string>.Value() and Converted<string, string>.Error() to clarify when it's successful and when it's failed.

```csharp
	public static Option<TClass, Intensity> AddOption<TClass>(this Verb<TClass> verb, Expression<Func<TClass, Intensity>> expression, Action<NamedArgConfig<TClass, Intensity, string>> config) where TClass : class, new()
	{
		// We set a default converter on the object
		var obj = new NamedArgConfig<TClass, Intensity, string>();
		obj.Converter = IntensityConverter;
		
		// There's also a constructor for the common scenario of requiredness/converter
		obj = new NamedArgConfig<TClass, Intensity, string>(
			isRequired: true,
			converter: IntensityConverter);
		
		return verb.AddOptionCore(expression, obj);
	}
	public static Converted<Intensity, string> IntensityConverter(string rawString)
	{
		if (Intensity.TryParse(rawString, out Intensity value) {
			return value;
		}
		else {
			return "Cannot parse string as Intensity";
		}
	}
```

### Post-Parsing Validation
If you need to validate an object as a whole after it's done parsing, set the ValidateObject `Func<TClass, string?>` to something. Returning null/empty indicates success, and returning a string indicates failure, with the string being the error message itself.

```csharp
CliParser parser = new CliParserBuilder()
	.AddVerb<FrobulateFile>(FrobulateFile.verbName, verb =>
	{
		verb.AddOption(x => x.InputFile, x => {
			x.ShortName = "-i";
			x.LongName = "--inputFile";
			x.DescriptiveName = "Input File";
			x.HelpText = "The input file which will get Frobulated";
		});

		// Just an aside; not the best idea. The file could be deleted by the time you get to using it!
		verb.ValidateObject = (obj) => File.Exists(obj.InputFile) ? null : "The file doesn't exist: " + obj.InputFile;
	}).Build();
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
		verb.AddOption(x => x.InputFile, x => {
			x.ShortName = "-i";
			x.LongName = "--inputFile";
			x.DescriptiveName = "Input File";
			x.HelpText = "The input file which will get Frobulated";
		});
	})
	.AddVerb<BojangleFile>(BojangleFile.verbName, verb =>
	{
		verb.AddOption(x => x.InputFile, x => {
			x.ShortName = "-i";
			x.LongName = "--inputFile";
			x.DescriptiveName = "Input File";
			x.HelpText = "The input file which will get Bojangled";
		});
	}).Build();

	parser.Handle(parser.Parse(args));
```

### Multi-Arguments
A Multivalue picks up any lone arguments, like a Value. However it will collect some kind of collection, instead.
They're almost identical to set up. The only difference is that they also have an accumulator, which is a function that accepts `IEnumerable<TProp>` and returns a `TPropCollection` to create the correct collection type. Not that it has to be a collection, it can be any sort of accumulator you want.

There are default accumulators for various collections. These are: TProp[], List<TProp>, IList<TProp>, IReadOnlyList<TProp>, IReadOnlyCollection<TProp>, IEnumerable<TProp>, HashSet<TProp>, Stack<TProp>, Queue<TProp>.

```csharp
verb.AddMultiValue(x => x.MyCollectionOfIntegers, x => {
		x.DescriptiveName = "ManyIntegers";
		x.HelpText = "A bunch of numbers";

		// Say the property MyCollectionOfIntegers is an interface, but we want a specific concrete type, such as an array. So, we can define a custom accumulator.
		x.Accumulator = enumerable => new Stack<int>(enumerable);
});

verb.AddMultiValueCore<int, int>(x => x.SummedInteger, x => {
		x.DescriptiveName = "ManyIntegers";
		x.HelpText = "A bunch of numbers which will be summed";

		// Say the property SummedInteger is just an integer. So, we can just do this to accumulate them all.
		x.Accumulator = enumerable => 
		{
			int total = 0;
			foreach (int number in enumerable)
			{
				total += number;
			}
			return total;
		};
});
```