﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public sealed class FormatCompiler
    {
        private readonly Dictionary<string, TagDefinition> _tagLookup = new Dictionary<string, TagDefinition>();
        private readonly Dictionary<string, Regex> _regexLookup = new Dictionary<string, Regex>();
        private readonly MasterTagDefinition _masterDefinition = new MasterTagDefinition();

        public FormatCompiler()
        {
            IfTagDefinition ifDefinition = new IfTagDefinition();
            _tagLookup.Add(ifDefinition.Name, ifDefinition);
            ElifTagDefinition elifDefinition = new ElifTagDefinition();
            _tagLookup.Add(elifDefinition.Name, elifDefinition);
            ElseTagDefinition elseDefinition = new ElseTagDefinition();
            _tagLookup.Add(elseDefinition.Name, elseDefinition);
            EachTagDefinition eachDefinition = new EachTagDefinition();
            _tagLookup.Add(eachDefinition.Name, eachDefinition);
            IndexTagDefinition indexDefinition = new IndexTagDefinition();
            _tagLookup.Add(indexDefinition.Name, indexDefinition);
            WithTagDefinition withDefinition = new WithTagDefinition();
            _tagLookup.Add(withDefinition.Name, withDefinition);
            NewlineTagDefinition newlineDefinition = new NewlineTagDefinition();
            _tagLookup.Add(newlineDefinition.Name, newlineDefinition);
            SetTagDefinition setDefinition = new SetTagDefinition();
            _tagLookup.Add(setDefinition.Name, setDefinition);

            RemoveNewLines = true;
        }

        public event EventHandler<PlaceholderFoundEventArgs> PlaceholderFound;

        public event EventHandler<VariableFoundEventArgs> VariableFound;

        public bool RemoveNewLines { get; set; }

        public bool AreExtensionTagsAllowed { get; set; }

        public void RegisterTag(TagDefinition definition, bool isTopLevel)
        {
            if (definition == null)
            {
                throw new ArgumentNullException(nameof(definition));
            }
            if (_tagLookup.ContainsKey(definition.Name))
            {
                string message = string.Format(Resources.DuplicateTagDefinition, definition.Name);
                throw new ArgumentException(message, nameof(definition));
            }
            _tagLookup.Add(definition.Name, definition);
        }

        public Generator Compile(string format)
        {
            if (format == null)
            {
                throw new ArgumentNullException(nameof(format));
            }
            CompoundGenerator generator = new CompoundGenerator(_masterDefinition, new ArgumentCollection());
            List<Context> context = new List<Context>() { new Context(_masterDefinition.Name, new ContextParameter[0]) };
            int formatIndex = buildCompoundGenerator(_masterDefinition, context, generator, format, 0);
            string trailing = format.Substring(formatIndex);
            generator.AddGenerator(new StaticGenerator(trailing, RemoveNewLines));
            return new Generator(generator);
        }

        private Match findNextTag(TagDefinition definition, string format, int formatIndex)
        {
            Regex regex = prepareRegex(definition);
            return regex.Match(format, formatIndex);
        }

        private Regex prepareRegex(TagDefinition definition)
        {
            if (!_regexLookup.TryGetValue(definition.Name, out Regex regex))
            {
                List<string> matches = new List<string>()
                {
                    getKeyRegex(),
                    getCommentTagRegex()
                };
                foreach (string closingTag in definition.ClosingTags)
                {
                    matches.Add(getClosingTagRegex(closingTag));
                }
                foreach (TagDefinition globalDefinition in _tagLookup.Values)
                {
                    if (!globalDefinition.IsContextSensitive)
                    {
                        matches.Add(getTagRegex(globalDefinition));
                    }
                }
                foreach (string childTag in definition.ChildTags)
                {
                    TagDefinition childDefinition = _tagLookup[childTag];
                    matches.Add(getTagRegex(childDefinition));
                }
                matches.Add(getUnknownTagRegex());
                string combined = string.Join("|", matches);
                string match = "{{(?<match>" + combined + ")}}";
                if (AreExtensionTagsAllowed)
                {
                    string tripleMatch = "{{{(?<extension>" + combined + ")}}}";
                    match = "(?:" + match + ")|(?:" + tripleMatch + ")";
                }
                regex = new Regex(match);
                _regexLookup.Add(definition.Name, regex);
            }
            return regex;
        }

        private static string getClosingTagRegex(string tagName)
        {
            StringBuilder regexBuilder = new StringBuilder();
            regexBuilder.Append(@"(?<close>(/(?<name>");
            regexBuilder.Append(tagName);
            regexBuilder.Append(@")\s*?))");
            return regexBuilder.ToString();
        }

        private static string getCommentTagRegex()
        {
            return @"(?<comment>#!.*?)";
        }

        private static string getKeyRegex()
        {
            return @"((?<key>" + RegexHelper.CompoundKey + @")(,(?<alignment>(\+|-)?[\d]+))?(:(?<format>.*?))?)";
        }

        private static string getTagRegex(TagDefinition definition)
        {
            StringBuilder regexBuilder = new StringBuilder();
            regexBuilder.Append(@"(?<open>(#(?<name>");
            regexBuilder.Append(definition.Name);
            regexBuilder.Append(@")");
            foreach (TagParameter parameter in definition.Parameters)
            {
                regexBuilder.Append(@"(\s+?");
                regexBuilder.Append(@"(?<argument>(");
                regexBuilder.Append(RegexHelper.Argument);
                regexBuilder.Append(@")))");
                if (!parameter.IsRequired)
                {
                    regexBuilder.Append("?");
                }
            }
            regexBuilder.Append(@"\s*?))");
            return regexBuilder.ToString();
        }

        private static string getUnknownTagRegex()
        {
            return @"(?<unknown>(#.*?))";
        }

        private int buildCompoundGenerator(
            TagDefinition tagDefinition,
            List<Context> context,
            CompoundGenerator generator,
            string format, int formatIndex)
        {
            while (true)
            {
                Match match = findNextTag(tagDefinition, format, formatIndex);

                if (!match.Success)
                {
                    if (tagDefinition.ClosingTags.Any())
                    {
                        string message = string.Format(Resources.MissingClosingTag, tagDefinition.Name);
                        throw new FormatException(message);
                    }
                    break;
                }

                string leading = format.Substring(formatIndex, match.Index - formatIndex);

                if (match.Groups["key"].Success)
                {
                    generator.AddGenerator(new StaticGenerator(leading, RemoveNewLines));
                    formatIndex = match.Index + match.Length;
                    bool isExtension = match.Groups["extension"].Success;
                    string key = match.Groups["key"].Value;
                    string alignment = match.Groups["alignment"].Value;
                    string formatting = match.Groups["format"].Value;
                    if (key.StartsWith("@"))
                    {
                        VariableFoundEventArgs args = new VariableFoundEventArgs(key.Substring(1), alignment, formatting, isExtension, context.ToArray());
                        if (VariableFound != null)
                        {
                            VariableFound(this, args);
                            key = "@" + args.Name;
                            alignment = args.Alignment;
                            formatting = args.Formatting;
                            isExtension = args.IsExtension;
                        }
                    }
                    else
                    {
                        PlaceholderFoundEventArgs args = new PlaceholderFoundEventArgs(key, alignment, formatting, isExtension, context.ToArray());
                        if (PlaceholderFound != null)
                        {
                            PlaceholderFound(this, args);
                            key = args.Key;
                            alignment = args.Alignment;
                            formatting = args.Formatting;
                            isExtension = args.IsExtension;
                        }
                    }
                    KeyGenerator keyGenerator = new KeyGenerator(key, alignment, formatting, isExtension);
                    generator.AddGenerator(keyGenerator);
                }
                else if (match.Groups["open"].Success)
                {
                    formatIndex = match.Index + match.Length;
                    string tagName = match.Groups["name"].Value;
                    TagDefinition nextDefinition = _tagLookup[tagName];
                    if (nextDefinition == null)
                    {
                        string message = string.Format(Resources.UnknownTag, tagName);
                        throw new FormatException(message);
                    }

                    generator.AddGenerator(new StaticGenerator(leading, RemoveNewLines));
                    ArgumentCollection arguments = getArguments(nextDefinition, match, context);

                    if (nextDefinition.HasContent)
                    {
                        CompoundGenerator compoundGenerator = new CompoundGenerator(nextDefinition, arguments);
                        IEnumerable<TagParameter> contextParameters = nextDefinition.GetChildContextParameters();
                        bool hasContext = contextParameters.Any();
                        if (hasContext)
                        {
                            ContextParameter[] parameters = contextParameters.Select(p => new ContextParameter(p.Name, arguments.GetKey(p))).ToArray();
                            context.Add(new Context(nextDefinition.Name, parameters));
                        }
                        formatIndex = buildCompoundGenerator(nextDefinition, context, compoundGenerator, format, formatIndex);
                        generator.AddGenerator(nextDefinition, compoundGenerator);
                        if (hasContext)
                        {
                            context.RemoveAt(context.Count - 1);
                        }
                    }
                    else
                    {
                        InlineGenerator inlineGenerator = new InlineGenerator(nextDefinition, arguments);
                        generator.AddGenerator(inlineGenerator);
                    }
                }
                else if (match.Groups["close"].Success)
                {
                    generator.AddGenerator(new StaticGenerator(leading, RemoveNewLines));
                    string tagName = match.Groups["name"].Value;
                    TagDefinition nextDefinition = _tagLookup[tagName];
                    formatIndex = match.Index;
                    if (tagName == tagDefinition.Name)
                    {
                        formatIndex += match.Length;
                    }
                    break;
                }
                else if (match.Groups["comment"].Success)
                {
                    generator.AddGenerator(new StaticGenerator(leading, RemoveNewLines));
                    formatIndex = match.Index + match.Length;
                }
                else if (match.Groups["unknown"].Success)
                {
                    string tagName = match.Value;
                    string message = string.Format(Resources.UnknownTag, tagName);
                    throw new FormatException(message);
                }
            }
            return formatIndex;
        }

        private ArgumentCollection getArguments(TagDefinition definition, Match match, List<Context> context)
        {
            // make sure we don't have too many arguments
            List<Capture> captures = match.Groups["argument"].Captures.Cast<Capture>().ToList();
            List<TagParameter> parameters = definition.Parameters.ToList();
            if (captures.Count > parameters.Count)
            {
                string message = string.Format(Resources.WrongNumberOfArguments, definition.Name);
                throw new FormatException(message);
            }

            // provide default values for missing arguments
            if (captures.Count < parameters.Count)
            {
                captures.AddRange(Enumerable.Repeat((Capture)null, parameters.Count - captures.Count));
            }

            // pair up the parameters to the given arguments
            // provide default for parameters with missing arguments
            // throw an error if a missing argument is for a required parameter
            Dictionary<TagParameter, string> arguments = new Dictionary<TagParameter, string>();
            foreach (var pair in parameters.Zip(captures, (p, c) => new { Capture = c, Parameter = p }))
            {
                string value = null;
                if (pair.Capture != null)
                {
                    value = pair.Capture.Value;                    
                }
                else if (pair.Parameter.IsRequired)
                {
                    string message = string.Format(Resources.WrongNumberOfArguments, definition.Name);
                    throw new FormatException(message);
                }
                arguments.Add(pair.Parameter, value);
            }

            // indicate that a key/variable has been encountered
            // update the key/variable name
            ArgumentCollection collection = new ArgumentCollection();
            foreach (var pair in arguments)
            {
                string placeholder = pair.Value;
                IArgument argument = null;
                if (placeholder != null)
                {
                    if (placeholder.StartsWith("@"))
                    {
                        string variableName = placeholder.Substring(1);
                        VariableFoundEventArgs args = new VariableFoundEventArgs(placeholder.Substring(1), string.Empty, string.Empty, false, context.ToArray());
                        if (VariableFound != null)
                        {
                            VariableFound(this, args);
                            variableName = args.Name;
                        }
                        argument = new VariableArgument(variableName);
                    }
                    else if (RegexHelper.IsString(placeholder))
                    {
                        string value = placeholder.Trim('\'');
                        argument = new StringArgument(value);
                    }
                    else if (RegexHelper.IsNumber(placeholder))
                    {
                        if (decimal.TryParse(placeholder, out decimal number))
                        {
                            argument = new NumberArgument(number);
                        }
                    }
                    else
                    {
                        string placeholderName = placeholder;
                        PlaceholderFoundEventArgs args = new PlaceholderFoundEventArgs(placeholder, string.Empty, string.Empty, false, context.ToArray());
                        if (PlaceholderFound != null)
                        {
                            PlaceholderFound(this, args);
                            placeholderName = args.Key;
                        }
                        argument = new PlaceholderArgument(placeholderName);
                    }
                }
                collection.AddArgument(pair.Key, argument);
            }
            return collection;
        }
    }
}
