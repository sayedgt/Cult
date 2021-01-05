using System.IO;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public sealed class NestedContext
    {
        public NestedContext()
        {
        }

        public TextWriter Writer { get; set; }

        public bool WriterNeedsConsidated { get; set; }

        public Scope KeyScope { get; set; }

        public Scope ContextScope { get; set; }
    }
}
