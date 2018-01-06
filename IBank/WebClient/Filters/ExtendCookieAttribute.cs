using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace WebClient.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExtendCookieAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string language = (string)filterContext.RouteData.Values["language"] ?? "ru";
            string culture = (string)filterContext.RouteData.Values["culture"] ?? "RU";

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", language, culture));
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", language, culture));

            if (filterContext.RequestContext.HttpContext.Request.IsAuthenticated)
            {
                FormsAuthentication.SetAuthCookie(filterContext.RequestContext.HttpContext.User.Identity.Name, false);
            }
        }
    }
}