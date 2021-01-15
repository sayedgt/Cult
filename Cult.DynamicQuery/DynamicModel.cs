namespace Cult.DynamicQuery
{
    public class DynamicFilter
    {
        public string PropertyName { get; set; }
        public ComparisonMethod ComparisonMethod { get; set; }
        public object PropertyValue { get; set; }
    }
}
