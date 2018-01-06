using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient.Filters;

namespace WebClient
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new InitializeSimpleMembershipAttribute());
            filters.Add(new RequireHttpsAttribute());
        }
    }
}