using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

namespace Cult.Mvc.Middlewares
{
    public static class CorrelationIdMiddlewareExtensions
    {
        public static IServiceCollection AddCorrelationId(this IServiceCollection service, Action<CorrelationIdOptions> options = default)
        {
            options ??= _ => { };

            service.Configure(options);
            return service;
        }

        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}