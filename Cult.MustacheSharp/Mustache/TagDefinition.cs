using System;
using System.Collections.Generic;
using System.IO;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public abstract class TagDefinition
    {
        protected TagDefinition(string tagName)
            : this(tagName, false)
        {
        }

        internal TagDefinition(string tagName, bool isBuiltIn)
        {
            if (!isBuiltIn && !RegexHelper.IsValidIdentifier(tagName))
            {
                throw new ArgumentException(Resources.BlankTagName, "tagName");
            }
            Name = tagName;
        }

        public string Name { get; private set; }

        internal bool IsSetter
        {
            get { return GetIsSetter(); }
        }

        protected virtual bool GetIsSetter()
        {
            return false;
        }

        internal bool IsContextSensitive
        {
            get { return GetIsContextSensitive(); }
        }

        protected virtual bool GetIsContextSensitive()
        {
            return false;
        }

        internal IEnumerable<TagParameter> Parameters
        {
            get { return GetParameters(); }
        }

        protected virtual IEnumerable<TagParameter> GetParameters()
        {
            return new TagParameter[] { };
        }

        internal bool HasContent
        {
            get { return GetHasContent(); }
        }

        protected abstract bool GetHasContent();

        internal IEnumerable<string> ClosingTags
        {
            get  { return GetClosingTags(); }
        }

        protected virtual IEnumerable<string> GetClosingTags()
        {
            if (HasContent)
            {
                return new string[] { Name };
            }
            else
            {
                return new string[] { };
            }
        }

        internal IEnumerable<string> ChildTags
        {
            get { return GetChildTags(); }
        }

        protected virtual IEnumerable<string> GetChildTags()
        {
            return new string[] { };
        }

        public abstract IEnumerable<TagParameter> GetChildContextParameters();

        public virtual IEnumerable<NestedContext> GetChildContext(
            TextWriter writer, 
            Scope keyScope, 
            Dictionary<string, object> arguments,
            Scope contextScope)
        {
            yield return new NestedContext() 
            { 
                KeyScope = keyScope, 
                Writer = writer,
                ContextScope = contextScope.CreateChildScope()
            };
        }

        public virtual void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
        }

        public virtual string ConsolidateWriter(TextWriter writer, Dictionary<string, object> arguments)
        {
            return writer.ToString();
        }

        public virtual bool ShouldCreateSecondaryGroup(TagDefinition definition)
        {
            return false;
        }

        public virtual bool ShouldGeneratePrimaryGroup(Dictionary<string, object> arguments)
        {
            return true;
        }
    }
}
