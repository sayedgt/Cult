// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class IfTagDefinition : ConditionTagDefinition
    {
        public IfTagDefinition()
            : base("if")
        {
        }

        protected override bool GetIsContextSensitive()
        {
            return false;
        }
    }
}
