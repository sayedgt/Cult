using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

// ReSharper disable All

namespace Cult.Mvc.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetRequestId(this HttpContext httpContext)
        {
            return Activity.Current?.Id ?? httpContext.TraceIdentifier;
        }
        public static IHttpConnectionFeature GetHttpConnectionFeature(this HttpContext context)
        {
            return context.Features.Get<IHttpConnectionFeature>();
        }
    }
}