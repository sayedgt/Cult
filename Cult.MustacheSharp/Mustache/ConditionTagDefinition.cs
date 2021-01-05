using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal abstract class ConditionTagDefinition : ContentTagDefinition
    {
        private const string conditionParameter = "condition";

        protected ConditionTagDefinition(string tagName)
            : base(tagName, true)
        {
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            return new TagParameter[] { new TagParameter(conditionParameter) { IsRequired = true } };
        }

        protected override IEnumerable<string> GetChildTags()
        {
            return new string[] { "elif", "else" };
        }

        public override bool ShouldCreateSecondaryGroup(TagDefinition definition)
        {
            return new string[] { "elif", "else" }.Contains(definition.Name);
        }

        public override bool ShouldGeneratePrimaryGroup(Dictionary<string, object> arguments)
        {
            object condition = arguments[conditionParameter];
            return isConditionSatisfied(condition);
        }

        private bool isConditionSatisfied(object condition)
        {
            if (condition == null || condition == DBNull.Value)
            {
                return false;
            }
            if (condition is IEnumerable enumerable)
            {
                return enumerable.Cast<object>().Any();
            }
            if (condition is char)
            {
                return (char)condition != '\0';
            }
            try
            {
                decimal number = (decimal)Convert.ChangeType(condition, typeof(decimal));
                return number != 0.0m;
            }
            catch
            {
                return true;
            }
        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new TagParameter[0];
        }
    }
}
