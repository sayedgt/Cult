using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
// ReSharper disable CheckNamespace

namespace Cult.Mvc.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CorrelationIdOptions _options;

        public CorrelationIdMiddleware(RequestDelegate next, IOptions<CorrelationIdOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _next = next ?? throw new ArgumentNullException(nameof(next));

            _options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(_options.Key, out StringValues correlationId))
            {
                context.TraceIdentifier = correlationId;
            }
            else
            {
                context.TraceIdentifier = context.TraceIdentifier.Replace(":", "").ToLowerInvariant();
            }

            context.Items[_options.Key] = context.TraceIdentifier;

            if (_options.IncludeInResponseHeader)
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add(_options.Key, new[] { context.TraceIdentifier });
                    return Task.CompletedTask;
                });
            }

            if (_options.IncludeInUserClaim)
            {
                if (context.User.Identity is ClaimsIdentity user && !user.HasClaim(x => x.Type == _options.Key))
                    user.AddClaim(new Claim(_options.Key, context.TraceIdentifier, ClaimValueTypes.String));
            }

            return _next(context);
        }
    }
}