using System.Diagnostics;
using Microsoft.AspNetCore.Http;

// ReSharper disable All

namespace Cult.Mvc.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetRequestId(this HttpContext httpContext)
        {
            return Activity.Current?.Id ?? httpContext.TraceIdentifier;
        }
    }
}