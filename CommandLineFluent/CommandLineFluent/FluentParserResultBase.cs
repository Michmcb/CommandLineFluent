using System.Collections.Generic;

namespace CommandLineFluent
{
	/// <summary>
	/// The result of parsing some command line arguments
	/// </summary>
	public abstract class FluentParserResultBase
	{
		/// <summary>
		/// Any errors encountered when parsing
		/// </summary>
		public IReadOnlyCollection<Error> Errors { get; }
		/// <summary>
		/// The verb that was parsed
		/// </summary>
		public IFluentVerb ParsedVerb { get; }
		/// <summary>
		/// Whether or not parsing was successful
		/// </summary>
		public bool Success { get; }
		internal FluentParserResultBase()
		{
			Success = false;
			Errors = null;
		}
		internal FluentParserResultBase(IReadOnlyCollection<Error> errors, IFluentVerb parsedVerb)
		{
			Success = (errors?.Count ?? 0) == 0;
			Errors = errors;
			ParsedVerb = parsedVerb;
		}
		/// <summary>
		/// If parsing was successful and the type that was parsed matches the type specified, returns the object that was parsed.
		/// Otherwise, returns the default value for type <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">The type of the parsed class</typeparam>
		public T GetParsedObject<T>() where T : class, new()
		{
			if (Success)
			{
				if (typeof(T) == ParsedVerb.TargetType)
				{
					if (ParsedVerb.Successful == true)
					{
						return ((FluentVerb<T>)ParsedVerb).ParsedObject;
					}
				}
				else
				{
					return default;
				}
			}
			return default;
		}
	}
}
