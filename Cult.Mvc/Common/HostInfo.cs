// ReSharper disable once CheckNamespace
namespace Cult.Mvc
{
    public class HostInfo
    {
        public string ConnectionId { get; set; }
        public string RemoteIp { get; set; }
        public string LocalIp { get; set; }
        public int? RemotePort { get; set; }
        public int? LocalPort { get; set; }
    }
}