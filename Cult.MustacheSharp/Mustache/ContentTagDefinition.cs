// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public abstract class ContentTagDefinition : TagDefinition
    {
        protected ContentTagDefinition(string tagName)
            : base(tagName)
        {
        }

        internal ContentTagDefinition(string tagName, bool isBuiltin)
            : base(tagName, isBuiltin)
        {
        }

        protected override bool GetHasContent()
        {
            return true;
        }
    }
}
