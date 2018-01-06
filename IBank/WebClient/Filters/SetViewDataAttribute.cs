using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebClient.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SetViewDataAttribute : ActionFilterAttribute
    {
        private string _prop;
        private object _value;

        public SetViewDataAttribute(string prop, object value)
        {
            this._prop = prop;
            this._value = value;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData[this._prop] = this._value;
        }
    }
}