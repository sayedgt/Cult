using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
// ReSharper disable All

namespace Cult.Mvc.Attributes
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        private readonly string _sessionKey;
        private readonly string _action;
        private readonly string _controller;

        public SessionTimeoutAttribute(string sessionKey, string action, string controller)
        {
            _sessionKey = sessionKey;
            _action = action;
            _controller = controller;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context != null && (context.HttpContext.Session?.TryGetValue(_sessionKey, out _) != true))
            {
                context.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = _controller,
                        action = _action
                    }));
            }
            base.OnActionExecuting(context);
        }
    }
}
