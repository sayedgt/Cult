using System.Collections.Generic;
using Cult.MustacheSharp.Mustache;

// ReSharper disable All 
namespace Cult.MustacheSharp.Tags
{
    public class IsNullOrEmptyTagDefinition : ContentTagDefinition
    {
        private const string ConditionParameter = "condition";

        public IsNullOrEmptyTagDefinition()
                            : base("IsNullOrEmpty")
        { }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new TagParameter[0];
        }

        protected override bool GetIsContextSensitive()
        {
            return false;
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            return new[] { new TagParameter(ConditionParameter) { IsRequired = true } };
        }

        private bool IsConditionSatisfied(object condition)
        {
            if (condition == null)
            {
                return true;
            }

            return condition is string ? string.IsNullOrEmpty(condition as string) : false;
        }

        public override bool ShouldGeneratePrimaryGroup(Dictionary<string, object> arguments)
        {
            object condition = arguments[ConditionParameter];
            return IsConditionSatisfied(condition);
        }
    }
}
