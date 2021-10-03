using System;
using System.Linq;
using System.Net;
using Cult.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Versioning;
// ReSharper disable UnusedMember.Global

namespace Cult.Mvc.Attributes
{
    // [UnavailableApiVersions("1.0,1.1")]
    [AttributeUsage(AttributeTargets.Method)]
    public class UnavailableApiVersionsAttribute : ActionFilterAttribute
    {
        private static string FixVersion(string version)
        {
            var v = version.Contains('.') ? version : $"{version}.0";
            return v.Trim();
        }
        private readonly string _commaSeparatedVersions;
        public UnavailableApiVersionsAttribute(string commaSeparatedVersions)
        {
            _commaSeparatedVersions = commaSeparatedVersions;
        }

        public string Header { get; set; } = "x-api-version";
        public string QueryString { get; set; } = "v";
        public string UrlSegment { get; set; } = "version";
        public bool IsADeprecatedVersionValid { get; set; } = true;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(_commaSeparatedVersions)) return;

            var props = context.ActionDescriptor.Properties;
            var url = context.HttpContext.Request.GetUrl();

            var headerVersion = context.HttpContext.Request.Headers.Count(x => string.Equals(x.Key, Header.Trim(), StringComparison.InvariantCultureIgnoreCase));
            var routeVersion = context.RouteData.Values[UrlSegment.Trim()];
            var queryVersion = context.HttpContext.Request.QueryString.Value?.Trim();
            var matchedQuery = queryVersion?.Replace("?", "").Split('&').FirstOrDefault(x => x.StartsWith($"{QueryString}="));

            var isSkippable = routeVersion == null && headerVersion == 0 && string.IsNullOrEmpty(matchedQuery);
            if (isSkippable) return;

            var version = "";

            if (routeVersion != null)
            {
                version = routeVersion.ToString();
            }

            if (headerVersion > 0)
            {
                version = context.HttpContext.Request.Headers["x-api-version"].ToString();
            }

            if (!string.IsNullOrEmpty(matchedQuery))
            {
                version = matchedQuery.Replace($"{QueryString}=", "");
            }

            version = FixVersion(version);
            var unavailableVersions = _commaSeparatedVersions.Split(',').Select(x => FixVersion(x.Trim()));
            var isUnavailableVersion = unavailableVersions.Contains(version);

            foreach (var prop in props)
            {
                if (prop.Value is ApiVersionModel apiVersionModel)
                {
                    if (apiVersionModel.IsApiVersionNeutral) return;

                    var deprecated = apiVersionModel.DeprecatedApiVersions.Select(x => x.ToString());
                    var supported = apiVersionModel.SupportedApiVersions.Select(x => x.ToString());

                    if (isUnavailableVersion)
                    {
                        context.Result = new JsonResult(new UnavailableApiVersion
                        {
                            Code = "UnavailableApiVersion",
                            AvailableVersions = supported,
                            DeprecatedVersions = deprecated,
                            Message = $"The HTTP resource that matches the request URI '{url}' does not available via the API version '{version}'."
                        });
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                }
            }
        }
    }
}