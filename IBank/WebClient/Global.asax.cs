using System.Globalization;
using System.Threading;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebClient.BusinessLogic;
using System.Data.Entity;
using DAL;

namespace WebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<IBankContext>(null);
            Database.SetInitializer(new IBankInitializer());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            Thread thread = new Thread(new ThreadStart((new AutopayExecuter()).Execute));
            thread.CurrentCulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", "ru", "RU"));
            thread.CurrentUICulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", "ru", "RU"));
            thread.Start();
        }
    }
}
