// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public sealed class Context
    {
        internal Context(string tagName, ContextParameter[] parameters)
        {
            TagName = tagName;
            Parameters = parameters;
        }

        public string TagName { get; }

        public ContextParameter[] Parameters { get; }
    }
}
