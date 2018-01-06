using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Modules
{
    public class BlockCacheModule : IHttpModule
    {

        public BlockCacheModule()
        {
        }

        public string ModuleName
        {
            get { return "BlockCacheModule"; }
        }

        public void Init(HttpApplication application)
        {
            application.EndRequest +=
                (new EventHandler(this.Application_EndRequest));
        }

        private void Application_EndRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            context.Response.Expires = 0;
            context.Response.ExpiresAbsolute = DateTime.Now;
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "private");
            context.Response.CacheControl = "no-cache";
        }

        public void Dispose() { }

    }
}