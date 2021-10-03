// ReSharper disable CheckNamespace
namespace Cult.Mvc.Middlewares
{
    public class CorrelationIdOptions
    {
        public string Key { get; set; } = "X-Correlation-Id";
        public bool IncludeInResponseHeader { get; set; } = true;
        public bool IncludeInUserClaim { get; set; } = true;

    }
}