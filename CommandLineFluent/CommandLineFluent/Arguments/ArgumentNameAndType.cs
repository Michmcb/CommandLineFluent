namespace CommandLineFluent.Arguments
{
	internal class ArgumentNameAndType
	{
		internal string Name { get; set; }
		internal FluentArgumentType Type { get; set; }
		internal ArgumentNameAndType(string name, FluentArgumentType type)
		{
			Name = name;
			Type = type;
		}
	}
}
