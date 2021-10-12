namespace Cult.Toolkit
{
    public class DynamicFilter
    {
        public string PropertyName { get; set; }
        public ComparisonFilter ComparisonMethod { get; set; }
        public object PropertyValue { get; set; }
    }
}
