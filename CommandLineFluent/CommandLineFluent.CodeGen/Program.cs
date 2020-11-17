namespace CommandLineFluent.CodeGen
{
	using System;
	using System.IO;
	using System.Text;
	internal class Program
	{
		private static void Main()
		{
			// TODO fix this up at least a bit, put the types into dictionaries for example and stuff, maybe use CodeDOM instead of plain text?
			// Pretty bogan code generation here mate
			Directory.SetCurrentDirectory(@"../../../../CommandLineFluent");
			GenerateValues("Verb.Values.cs");
			GenerateOptions("Verb.Options.cs");
			//GenerateSwitches(args[2]);
			GenerateMultiValues("Verb.MultiValues.cs");
			//GenerateMultiOptions(args[4]);

		}
		private static void GenerateValues(string name)
		{
			(bool isRequired, string tProp, string converter, string methodName, string genericConstraint)[] typesAndConverters =
				new (bool isRequired, string tProp, string converter, string genericParameter, string genericConstraint)[]
			{
				( true, "string", "NoConversion", "AddValue", string.Empty ),
				( true, "short", "ToShort", "AddValue", string.Empty ),
				( true, "ushort", "ToUShort", "AddValue", string.Empty ),
				( true, "int", "ToInt", "AddValue", string.Empty ),
				( true, "uint", "ToUInt", "AddValue", string.Empty ),
				( true, "long", "ToLong", "AddValue", string.Empty ),
				( true, "ulong", "ToULong", "AddValue", string.Empty ),
				( true, "float", "ToFloat", "AddValue", string.Empty ),
				( true, "double", "ToDouble", "AddValue", string.Empty ),
				( true, "decimal", "ToDecimal", "AddValue", string.Empty ),
				( true, "TEnum", "(x) => ToEnum<TEnum>(x, this.config.IsCaseSensitive)", "AddValue<TEnum>", " where TEnum : struct, Enum"),
				( true, "DateTime", "ToDateTime", "AddValue", string.Empty ),
				( true, "TimeSpan", "ToTimeSpan", "AddValue", string.Empty ),
				( true, "Guid", "ToGuid", "AddValue", string.Empty ),

				( false, "string?", "NoConversionNullable", "AddValueNullable", string.Empty ),
				( false, "short?", "ToNullableShort", "AddValue", string.Empty ),
				( false, "ushort?", "ToNullableUShort", "AddValue", string.Empty ),
				( false, "int?", "ToNullableInt", "AddValue", string.Empty ),
				( false, "uint?", "ToNullableUInt", "AddValue", string.Empty ),
				( false, "long?", "ToNullableLong", "AddValue", string.Empty ),
				( false, "ulong?", "ToNullableULong", "AddValue", string.Empty ),
				( false, "float?", "ToNullableFloat", "AddValue", string.Empty ),
				( false, "double?", "ToNullableDouble", "AddValue", string.Empty ),
				( false, "decimal?", "ToNullableDecimal", "AddValue", string.Empty ),
				( false, "TEnum?", "(x) => ToNullableEnum<TEnum>(x, this.config.IsCaseSensitive)", "AddValue<TEnum>", " where TEnum : struct, Enum"),
				( false, "DateTime?", "ToNullableDateTime", "AddValue", string.Empty ),
				( false, "TimeSpan?", "ToNullableTimeSpan", "AddValue", string.Empty ),
				( false, "Guid?", "ToNullableGuid" , "AddValue", string.Empty )
			};

			using StreamWriter csOut = new StreamWriter(new FileStream(name, FileMode.Create, FileAccess.Write), Encoding.UTF8);
			csOut.Write("namespace CommandLineFluent\n");
			csOut.Write("{\n");
			csOut.Write("\tusing CommandLineFluent.Arguments;\n");
			csOut.Write("\tusing CommandLineFluent.Arguments.Config;\n");
			csOut.Write("\tusing System;\n");
			csOut.Write("\tusing System.Linq.Expressions;\n");
			csOut.Write("\tusing static Converters;\n");
			csOut.Write("\tpublic sealed partial class Verb<TClass> : IVerb where TClass : class, new()\n");
			csOut.Write("\t{\n");

			foreach ((bool isRequired, string tProp, string converter, string methodName, string genericConstraint) in typesAndConverters)
			{
				csOut.Write("\t\t/// <summary>\n");
				csOut.Write($"\t\t/// Adds a new Value to set the {tProp} specified by <paramref name=\"expression\"/>. By default this Value is {(isRequired ? "required" : "not required")}.\n");
				csOut.Write("\t\t/// </summary>\n\t\t/// <param name=\"expression\">The property.</param>\n\t\t/// <param name=\"config\">The action to configure the Value.</param>\n\t\t/// <returns>A configured Value.</returns>\n");

				csOut.Write($"\t\tpublic Value<TClass, {tProp}> {methodName}(Expression<Func<TClass, {tProp}>> expression, Action<NamelessArgConfig<TClass, {tProp}>> config){genericConstraint}\n");
				csOut.Write("\t\t{\n");
				csOut.Write($"\t\t\tvar obj = new NamelessArgConfig<TClass, {tProp}>({(isRequired ? "true" : "false")}, {converter});\n");
				csOut.Write($"\t\t\tconfig(obj);\n");
				csOut.Write("\t\t\treturn AddValueCore(expression, obj);\n");
				csOut.Write("\t\t}\n");
			}
			csOut.Write("\t}\n");
			csOut.Write("}\n");
		}
		private static void GenerateOptions(string name)
		{
			(bool isRequired, string tProp, string converter, string methodName, string genericConstraint)[] typesAndConverters =
				new (bool isRequired, string tProp, string converter, string genericParameter, string genericConstraint)[]
			{
				( true, "string", "NoConversion", "AddOption", string.Empty ),
				( true, "short", "ToShort", "AddOption", string.Empty ),
				( true, "ushort", "ToUShort", "AddOption", string.Empty ),
				( true, "int", "ToInt", "AddOption", string.Empty ),
				( true, "uint", "ToUInt", "AddOption", string.Empty ),
				( true, "long", "ToLong", "AddOption", string.Empty ),
				( true, "ulong", "ToULong", "AddOption", string.Empty ),
				( true, "float", "ToFloat", "AddOption", string.Empty ),
				( true, "double", "ToDouble", "AddOption", string.Empty ),
				( true, "decimal", "ToDecimal", "AddOption", string.Empty ),
				( true, "TEnum", "(x) => ToEnum<TEnum>(x, this.config.IsCaseSensitive)", "AddOption<TEnum>", " where TEnum : struct, Enum"),
				( true, "DateTime", "ToDateTime", "AddOption", string.Empty ),
				( true, "TimeSpan", "ToTimeSpan", "AddOption", string.Empty ),
				( true, "Guid", "ToGuid", "AddOption", string.Empty ),

				( false, "string?", "NoConversionNullable", "AddOptionNullable", string.Empty ),
				( false, "short?", "ToNullableShort", "AddOption", string.Empty ),
				( false, "ushort?", "ToNullableUShort", "AddOption", string.Empty ),
				( false, "int?", "ToNullableInt", "AddOption", string.Empty ),
				( false, "uint?", "ToNullableUInt", "AddOption", string.Empty ),
				( false, "long?", "ToNullableLong", "AddOption", string.Empty ),
				( false, "ulong?", "ToNullableULong", "AddOption", string.Empty ),
				( false, "float?", "ToNullableFloat", "AddOption", string.Empty ),
				( false, "double?", "ToNullableDouble", "AddOption", string.Empty ),
				( false, "decimal?", "ToNullableDecimal", "AddOption", string.Empty ),
				( false, "TEnum?", "(x) => ToNullableEnum<TEnum>(x, this.config.IsCaseSensitive)", "AddOption<TEnum>", " where TEnum : struct, Enum"),
				( false, "DateTime?", "ToNullableDateTime", "AddOption", string.Empty ),
				( false, "TimeSpan?", "ToNullableTimeSpan", "AddOption", string.Empty ),
				( false, "Guid?", "ToNullableGuid" , "AddOption", string.Empty )
			};

			using StreamWriter csOut = new StreamWriter(new FileStream(name, FileMode.Create, FileAccess.Write), Encoding.UTF8);
			csOut.Write("namespace CommandLineFluent\n");
			csOut.Write("{\n");
			csOut.Write("\tusing CommandLineFluent.Arguments;\n");
			csOut.Write("\tusing CommandLineFluent.Arguments.Config;\n");
			csOut.Write("\tusing System;\n");
			csOut.Write("\tusing System.Linq.Expressions;\n");
			csOut.Write("\tusing static Converters;\n");
			csOut.Write("\tpublic sealed partial class Verb<TClass> : IVerb where TClass : class, new()\n");
			csOut.Write("\t{\n");

			foreach ((bool isRequired, string tProp, string converter, string methodName, string genericConstraint) in typesAndConverters)
			{
				csOut.Write("\t\t/// <summary>\n");
				csOut.Write($"\t\t/// Adds a new Option to set the {tProp} specified by <paramref name=\"expression\"/>. By default this Option is {(isRequired ? "required" : "not required")}.\n");
				csOut.Write("\t\t/// </summary>\n\t\t/// <param name=\"expression\">The property.</param>\n\t\t/// <param name=\"config\">The action to configure the Option.</param>\n\t\t/// <returns>A configured Option.</returns>\n");

				csOut.Write($"\t\tpublic Option<TClass, {tProp}> {methodName}(Expression<Func<TClass, {tProp}>> expression, Action<NamedArgConfig<TClass, {tProp}, string>> config){genericConstraint}\n");
				csOut.Write("\t\t{\n");
				csOut.Write($"\t\t\tvar obj = new NamedArgConfig<TClass, {tProp}, string>({(isRequired ? "true" : "false")}, {converter});\n");
				csOut.Write($"\t\t\tconfig(obj);\n");
				csOut.Write("\t\t\treturn AddOptionCore(expression, obj);\n");
				csOut.Write("\t\t}\n");
			}
			csOut.Write("\t}\n");
			csOut.Write("}\n");
		}
		private static void GenerateMultiValues(string name)
		{
			(string tPropCollectionFormat, string accumulator)[] collectionTypes = new (string, string)[] {
				("{TProp}[]", "Array"),
				("List<{TProp}>", "List"),
				("IList<{TProp}>", "List"),
				("IReadOnlyList<{TProp}>", "List"),
				("ICollection<{TProp}>", "List"),
				("IReadOnlyCollection<{TProp}>", "List"),
				("IEnumerable<{TProp}>", "Enumerable"),
				("HashSet<{TProp}>", "HashSet"),
				("Stack<{TProp}>", "Stack"),
				("Queue<{TProp}>", "Queue") };

			(string tProp, string converter, string methodName, string genericConstraint)[] typesAndConverters =
				new (string tProp, string converter, string genericParameter, string genericConstraint)[]
			{
				( "string", "NoConversion", "AddMultiValue", string.Empty ),
				( "short", "ToShort", "AddMultiValue", string.Empty ),
				( "ushort", "ToUShort", "AddMultiValue", string.Empty ),
				( "int", "ToInt", "AddMultiValue", string.Empty ),
				( "uint", "ToUInt", "AddMultiValue", string.Empty ),
				( "long", "ToLong", "AddMultiValue", string.Empty ),
				( "ulong", "ToULong", "AddMultiValue", string.Empty ),
				( "float", "ToFloat", "AddMultiValue", string.Empty ),
				( "double", "ToDouble", "AddMultiValue", string.Empty ),
				( "decimal", "ToDecimal", "AddMultiValue", string.Empty ),
				( "TEnum", "(x) => ToEnum<TEnum>(x, this.config.IsCaseSensitive)", "AddMultiValue<TEnum>", " where TEnum : struct, Enum"),
				( "DateTime", "ToDateTime", "AddMultiValue", string.Empty ),
				( "TimeSpan", "ToTimeSpan", "AddMultiValue", string.Empty ),
				( "Guid", "ToGuid", "AddMultiValue", string.Empty ),

				( "string?", "NoConversionNullable", "AddMultiValueNullable", string.Empty ),
				( "short?", "ToNullableShort", "AddMultiValue", string.Empty ),
				( "ushort?", "ToNullableUShort", "AddMultiValue", string.Empty ),
				( "int?", "ToNullableInt", "AddMultiValue", string.Empty ),
				( "uint?", "ToNullableUInt", "AddMultiValue", string.Empty ),
				( "long?", "ToNullableLong", "AddMultiValue", string.Empty ),
				( "ulong?", "ToNullableULong", "AddMultiValue", string.Empty ),
				( "float?", "ToNullableFloat", "AddMultiValue", string.Empty ),
				( "double?", "ToNullableDouble", "AddMultiValue", string.Empty ),
				( "decimal?", "ToNullableDecimal", "AddMultiValue", string.Empty ),
				( "TEnum?", "(x) => ToNullableEnum<TEnum>(x, this.config.IsCaseSensitive)", "AddMultiValue<TEnum>", " where TEnum : struct, Enum"),
				( "DateTime?", "ToNullableDateTime", "AddMultiValue", string.Empty ),
				( "TimeSpan?", "ToNullableTimeSpan", "AddMultiValue", string.Empty ),
				( "Guid?", "ToNullableGuid" , "AddMultiValue", string.Empty )
			};

			using StreamWriter csOut = new StreamWriter(new FileStream(name, FileMode.Create, FileAccess.Write), Encoding.UTF8);
			csOut.Write("namespace CommandLineFluent\n");
			csOut.Write("{\n");
			csOut.Write("\tusing CommandLineFluent.Arguments;\n");
			csOut.Write("\tusing CommandLineFluent.Arguments.Config;\n");
			csOut.Write("\tusing System;\n");
			csOut.Write("\tusing System.Collections.Generic;\n");
			csOut.Write("\tusing System.Linq.Expressions;\n");
			csOut.Write("\tusing static Converters;\n");
			csOut.Write("\tusing static Accumulators;\n");
			csOut.Write("\tpublic sealed partial class Verb<TClass> : IVerb where TClass : class, new()\n");
			csOut.Write("\t{\n");
			foreach ((string tProp, string converter, string methodName, string genericConstraint) in typesAndConverters)
			{
				foreach ((string tPropCollectionFormat, string accumulator) in collectionTypes)
				{
					string tPropCollection = tPropCollectionFormat.Replace("{TProp}", tProp);
					string tPropCollectionXmlEscaped = tPropCollection.Replace(">", "&gt;").Replace("<", "&lt;");
					csOut.Write("\t\t/// <summary>\n");
					csOut.Write($"\t\t/// Adds a new MultiValue to set the {tPropCollectionXmlEscaped} specified by <paramref name=\"expression\"/>. By default this MultiValue is not required.\n");
					csOut.Write($"\t\t/// Adds a new MultiValue, by default it is not required. The elements will be converted to {tProp} and stored in a collection of type {accumulator}.\n");
					csOut.Write("\t\t/// </summary>\n\t\t/// <param name=\"expression\">The property.</param>\n\t\t/// <param name=\"config\">The action to configure the MultiValue.</param>\n\t\t/// <returns>A configured MultiValue.</returns>\n");

					csOut.Write($"\t\tpublic MultiValue<TClass, {tProp}, {tPropCollection}> {methodName}(Expression<Func<TClass, {tPropCollection}>> expression, Action<NamelessMultiArgConfig<TClass, {tProp}, {tPropCollection}>> config){genericConstraint}\n");
					csOut.Write("\t\t{\n");
					csOut.Write($"\t\t\tvar obj = new NamelessMultiArgConfig<TClass, {tProp}, {tPropCollection}>(false, {converter}, {accumulator});\n");
					csOut.Write("\t\t\tconfig(obj);\n\t\t\treturn AddMultiValueCore(expression, obj);\n\t\t}\n");
				}
			}
			csOut.Write("\t}\n");
			csOut.Write("}\n");
		}
	}
}
