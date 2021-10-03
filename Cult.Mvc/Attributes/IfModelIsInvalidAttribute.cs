/*
[HttpGet]
[IfModelIsInvalid(RedirectToAction = "Index", RedirectToController = "Account")]
public IActionResult ConfirmEmail(CodeViewModel model)
{
    // Logic here
    return View();
}
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
// ReSharper disable UnusedMember.Global

namespace Cult.Mvc.Attributes
{
    public class IfModelIsInvalidAttribute : ActionFilterAttribute
    {


        public string RedirectToController { get; set; }

        public string RedirectToAction { get; set; }

        public string RedirectToPage { get; set; }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new RedirectToRouteResult(ConstructRouteValueDictionary());
            }
        }

        private RouteValueDictionary ConstructRouteValueDictionary()
        {
            var dict = new RouteValueDictionary();

            if (!string.IsNullOrWhiteSpace(RedirectToPage))
            {
                dict.Add("page", RedirectToPage);
            }
            // Assuming RedirectToController & RedirectToAction are set
            else
            {
                dict.Add("controller", RedirectToController);
                dict.Add("action", RedirectToAction);
            }

            return dict;
        }
    }
}