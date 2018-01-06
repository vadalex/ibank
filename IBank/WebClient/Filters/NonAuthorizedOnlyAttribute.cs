using System;
using System.Web.Mvc;

namespace WebClient.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NonAuthorizedOnlyAttribute : ActionFilterAttribute
    {
        private readonly string _returnUrl;

        public NonAuthorizedOnlyAttribute(string returnUrl = "/Home/Index")
        {
            _returnUrl = returnUrl;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult(_returnUrl);
            }
        }
    }
}