namespace CommandLineFluent
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq.Expressions;
	using System.Reflection;

	/// <summary>
	/// Builds up verbs and their arguments, and once done can create a <see cref="CliParser"/>.
	/// </summary>
	public sealed class CliParserBuilder
	{
		private readonly Dictionary<string, IVerb> verbsByName;
		private readonly List<IVerb> verbs;
		private readonly CliParserConfig config;
		private IConsole? console;
		private ITokenizer? tokenizer;
		private IMessageFormatter? msgFormatter;
		/// <summary>
		/// Creates a new instance, using a default <see cref="CliParserConfig"/>.
		/// </summary>
		public CliParserBuilder()
		{
			verbsByName = new Dictionary<string, IVerb>(StringComparer.OrdinalIgnoreCase);
			verbs = new List<IVerb>();
			config = new CliParserConfig();
			console = null;
			tokenizer = null;
			msgFormatter = null;
		}
		/// <summary>
		/// Creates a new instance.
		/// </summary>
		public CliParserBuilder(CliParserConfig config, IConsole? console = null, ITokenizer? tokenizer = null, IMessageFormatter? msgFormatter = null)
		{
			this.config = config ?? new CliParserConfig();
			verbsByName = new Dictionary<string, IVerb>(this.config.StringComparer);
			verbs = new List<IVerb>();
			this.console = console;
			this.tokenizer = tokenizer;
			this.msgFormatter = msgFormatter;
		}
		/// <summary>
		/// Specifies the <see cref="IConsole"/> to use.
		/// By default, this is <see cref="StandardConsole"/>, which just uses the static class <see cref="Console"/>.
		/// </summary>
		/// <param name="console">The console to use.</param>
		public CliParserBuilder UseConsole(IConsole console)
		{
			this.console = console;
			return this;
		}
		/// <summary>
		/// Specifies the <see cref="ITokenizer"/> to use.
		/// By default, this is <see cref="QuotedStringTokenizer"/>, which splits strings into tokens based on single or double quotes, or spaces.
		/// </summary>
		/// <param name="tokenizer">The tokenizer to use.</param>
		public CliParserBuilder UseTokenizer(ITokenizer tokenizer)
		{
			this.tokenizer = tokenizer;
			return this;
		}
		/// <summary>
		/// Specifies the <see cref="IMessageFormatter"/> to use.
		/// By default, this is <see cref="StandardMessageFormatter"/>.
		/// </summary>
		/// <param name="msgFormatter">The tokenizer to use.</param>
		public CliParserBuilder UseMessageFormatter(IMessageFormatter msgFormatter)
		{
			this.msgFormatter = msgFormatter;
			return this;
		}
		/// <summary>
		/// Adds a verb for this parser. To invoke it, the user has to enter <paramref name="longName"/> on the command line.
		/// e.g. "foo.exe add" invokes the verb with the name "add".
		/// </summary>
		/// <param name="longName">The long name of the verb</param>
		/// <param name="config">The action to configure the verb</param>
		public CliParserBuilder AddVerb(string longName, Action<Verb> config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), "You cannot pass a null configuration action");
			}
			if (string.IsNullOrEmpty(longName))
			{
				throw new ArgumentNullException(nameof(longName), "Verb Long Name cannot be null or an empty string");
			}
			if (verbsByName.ContainsKey(longName))
			{
				throw new CliParserBuilderException(string.Concat("The verb name ", longName, " has already been used, you may only use unique verb names"));
			}
			Verb v = new Verb(null, longName);
			config.Invoke(v);
			verbsByName.Add(longName, v);
			verbs.Add(v);
			return this;
		}
		/// <summary>
		/// Adds a verb for this parser. To invoke it, the user has to enter <paramref name="longName"/> or <paramref name="shortName"/> on the command line.
		/// e.g. "foo.exe add" invokes the verb with the name "add".
		/// </summary>
		/// <param name="longName">The long name of the verb</param>
		/// <param name="shortName">The short name of the verb</param>
		/// <param name="config">The action to configure the verb</param>
		public CliParserBuilder AddVerb(string longName, string shortName, Action<Verb> config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), "You cannot pass a null configuration action");
			}
			if (string.IsNullOrEmpty(longName))
			{
				throw new ArgumentNullException(nameof(longName), "Verb Long Name cannot be null or an empty string");
			}
			if (string.IsNullOrEmpty(shortName))
			{
				throw new ArgumentNullException(nameof(longName), "Verb Short Name cannot be null or an empty string");
			}
			if (verbsByName.ContainsKey(longName))
			{
				throw new CliParserBuilderException(string.Concat("The verb name ", longName, " has already been used, you may only use unique verb names"));
			}
			if (verbsByName.ContainsKey(shortName))
			{
				throw new CliParserBuilderException(string.Concat("The verb name ", shortName, " has already been used, you may only use unique verb names"));
			}
			Verb v = new Verb(shortName, longName);
			config.Invoke(v);
			verbsByName.Add(shortName, v);
			verbsByName.Add(longName, v);
			verbs.Add(v);
			return this;
		}
		/// <summary>
		/// Adds a verb for this parser. To invoke it, the user has to enter <paramref name="longName"/> on the command line.
		/// e.g. "foo.exe add" invokes the verb with the name "add".
		/// </summary>
		/// <typeparam name="TClass">The type of the class which will be created when arguments for that verb are parsed successfully</typeparam>
		/// <param name="longName">The long name of the verb</param>
		/// <param name="config">The action to configure the verb</param>
		public CliParserBuilder AddVerb<TClass>(string longName, Action<Verb<TClass>> config) where TClass : class, new()
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), "You cannot pass a null configuration action");
			}
			if (string.IsNullOrEmpty(longName))
			{
				throw new ArgumentNullException(nameof(longName), "Verb Long Name cannot be null or an empty string");
			}
			if (verbsByName.ContainsKey(longName))
			{
				throw new CliParserBuilderException(string.Concat("The verb name ", longName, " has already been used, you may only use unique verb names"));
			}
			Verb<TClass> v = new Verb<TClass>(null, longName, this.config);
			config.Invoke(v);
			verbsByName.Add(longName, v);
			verbs.Add(v);
			return this;
		}
		/// <summary>
		/// Adds a verb for this parser. To invoke it, the user has to enter <paramref name="longName"/> or <paramref name="shortName"/> on the command line.
		/// e.g. "foo.exe add" invokes the verb with the name "add".
		/// </summary>
		/// <typeparam name="TClass">The type of the class which will be created when arguments for that verb are parsed successfully</typeparam>
		/// <param name="longName">The long name of the verb</param>
		/// <param name="shortName">The short name of the verb</param>
		/// <param name="config">The action to configure the verb</param>
		public CliParserBuilder AddVerb<TClass>(string longName, string shortName, Action<Verb<TClass>> config) where TClass : class, new()
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), "You cannot pass a null configuration action");
			}
			if (string.IsNullOrEmpty(longName))
			{
				throw new ArgumentNullException(nameof(longName), "Verb Long Name cannot be null or an empty string");
			}
			if (string.IsNullOrEmpty(shortName))
			{
				throw new ArgumentNullException(nameof(longName), "Verb Short Name cannot be null or an empty string");
			}
			if (verbsByName.ContainsKey(longName))
			{
				throw new CliParserBuilderException(string.Concat("The verb name ", longName, " has already been used, you may only use unique verb names"));
			}
			if (verbsByName.ContainsKey(shortName))
			{
				throw new CliParserBuilderException(string.Concat("The verb name ", shortName, " has already been used, you may only use unique verb names"));
			}
			Verb<TClass> v = new Verb<TClass>(shortName, longName, this.config);
			config.Invoke(v);
			verbsByName.Add(shortName, v);
			verbsByName.Add(longName, v);
			verbs.Add(v);
			return this;
		}
		/// <summary>
		/// Creates a <see cref="CliParser"/>.
		/// Throws a <see cref="CliParserBuilderException"/> if anything is improperly configured.
		/// </summary>
		public CliParser Build()
		{
			List<Error> errors = new List<Error>();
			if (verbsByName.Values.Count <= 0)
			{
				throw new CliParserBuilderException("The parser has no verbs, use AddVerb<TClass> to add some verbs");
			}
			if (errors.Count == 0)
			{
				return new CliParser(console ?? new StandardConsole(), tokenizer ?? new QuotedStringTokenizer(), msgFormatter ?? new StandardMessageFormatter(ConsoleColor.Cyan), verbsByName, verbs, config);
			}
			else
			{
				throw new CliParserBuilderException("CliParserBuilder has not been configured correctly", errors);
			}
		}
		/// <summary>
		/// Given <paramref name="expression"/>, returns the corresponding <see cref="PropertyInfo"/>. The <paramref name="expression"/> must be a <see cref="MemberExpression"/>, whose Member is a
		/// <see cref="PropertyInfo"/>. If not, throws <see cref="CliParserBuilderException"/>.
		/// </summary>
		/// <typeparam name="TClass">Argument which is passed to the expression.</typeparam>
		/// <typeparam name="TProp">What the expression returns.</typeparam>
		/// <param name="expression">The expression which must be a property.</param>
		/// <returns>A PropertyInfo from <paramref name="expression"/>.</returns>
		public static PropertyInfo PropertyInfoFromExpression<TClass, TProp>(Expression<Func<TClass, TProp>> expression)
		{
			if (!(expression.Body is MemberExpression me))
			{
				throw new CliParserBuilderException($"Expression has to be a property of type {typeof(TProp)} of class {typeof(TClass)}");
			}
			if (!(me.Member is PropertyInfo prop))
			{
				throw new CliParserBuilderException($"Expression has to be a property of type {typeof(TProp)} of class {typeof(TClass)}");
			}
			return prop;
		}
		/// <summary>
		/// First, calls <see cref="PropertyInfoFromExpression{TClass, TProp}(Expression{Func{TClass, TProp}})"/>.
		/// Then it creates a strongly-typed delegate which, when passed an instace of <typeparamref name="TClass"/>, sets the property
		/// specified by <paramref name="propertyInfo"/>.
		/// If there is no accessible getter, throws <see cref="CliParserBuilderException"/>.
		/// </summary>
		/// <typeparam name="TClass">Argument which is passed to the expression.</typeparam>
		/// <typeparam name="TProp">What the expression returns.</typeparam>
		/// <param name="expression">The expression which must be a property.</param>
		/// <returns>A delegate returns the value of the property when passed a <typeparamref name="TClass"/>.</returns>
		public static Action<TClass, TProp> GetSetMethodDelegateFromExpression<TClass, TProp>(Expression<Func<TClass, TProp>> expression, out PropertyInfo propertyInfo)
		{
			propertyInfo = PropertyInfoFromExpression(expression);
			MethodInfo? m = propertyInfo.GetSetMethod(true);
			if (m == null)
			{
				throw new CliParserBuilderException(string.Concat("There is no accessible setter for the property ", propertyInfo.Name, " on the class ", typeof(TClass).Name));
			}
			return (Action<TClass, TProp>)Delegate.CreateDelegate(typeof(Action<TClass, TProp>), m);
		}
		// This stuff is useless and just adds clutter, so hide it
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}
	}
}