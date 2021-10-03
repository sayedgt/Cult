using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Http.Features;

// ReSharper disable All 
namespace Cult.Mvc.Extensions
{
    public static class HttpRequestExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
        public static string GetUrl(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
        }

        public static HostInfo GetCallerHost(this HttpContext context)
        {
            var callerFeatures = context.Features.Get<IHttpConnectionFeature>();
            var callerHostRemoteIp = callerFeatures?.RemoteIpAddress?.ToString();
            var callerHostRemotePort = callerFeatures?.RemotePort;
            var callerHostConnectionId = callerFeatures?.ConnectionId;
            var callerHostLocalIp = callerFeatures?.LocalIpAddress.ToString();
            var callerHostLocalPort = callerFeatures?.LocalPort;

            return new HostInfo()
            {
                ConnectionId = callerHostConnectionId,
                RemoteIp = callerHostRemoteIp,
                LocalIp = callerHostLocalIp,
                RemotePort = callerHostRemotePort,
                LocalPort = callerHostLocalPort
            };
        }
    }
}
