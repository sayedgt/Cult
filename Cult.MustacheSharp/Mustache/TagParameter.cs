using System;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public sealed class TagParameter
    {
        public TagParameter(string parameterName)
        {
            if (!RegexHelper.IsValidIdentifier(parameterName))
            {
                throw new ArgumentException(Resources.BlankParameterName, "parameterName");
            }
            Name = parameterName;
        }

        public string Name { get; }

        public bool IsRequired { get; set; }

        public object DefaultValue { get; set; }
    }
}
