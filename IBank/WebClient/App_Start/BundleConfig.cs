using System.Web.Optimization;

namespace WebClient
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/timer").Include("~/Scripts/moment.min.js",
                "~/Scripts/jquery.tinytimer.min.js",
                "~/Scripts/timer.js"));

            bundles.Add(new ScriptBundle("~/bundles/ddl").Include("~/Scripts/ddl.js"));

            bundles.Add(new ScriptBundle("~/bundles/history").Include("~/Scripts/history.js"));
            bundles.Add(new ScriptBundle("~/bundles/historyList").Include("~/Scripts/historyList.js"));

            bundles.Add(new ScriptBundle("~/bundles/cardAccountsButtonsRefresh").Include("~/Scripts/cardAccountsButtonsRefresh.js", "~/Scripts/blockCard.js"));

            bundles.Add(new ScriptBundle("~/bundles/ownPayments").Include("~/Scripts/ownPayments.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/mobileAutopayCreate").Include(
                        "~/Scripts/mobile-autopay-create.js"));

            bundles.Add(new ScriptBundle("~/bundles/mobileAutopayList").Include(
                        "~/Scripts/mobile-autopay-list.js"));

            bundles.Add(new StyleBundle("~/Home/css").Include("~/Content/styles/main.css",
                "~/Content/styles/shared.css",
                "~/Content/styles/header.css",
                "~/Content/styles/menu.css",
                "~/Content/styles/footer.css",
                "~/Content/styles/content.css",
                "~/Content/styles/home.css"));

            bundles.Add(new StyleBundle("~/Login/css").Include("~/Content/styles/shared.css", 
                "~/Content/styles/login.css",
                "~/Content/styles/header.css"));

            bundles.Add(new StyleBundle("~/Login/SecretCode/css").Include("~/Content/styles/secret.css"));

            bundles.Add(new StyleBundle("~/Home/Payments/css").Include("~/Content/styles/payments.css"));

            bundles.Add(new StyleBundle("~/CardAccountsTable/css").Include("~/Content/styles/cardtable.css"));

            bundles.Add(new StyleBundle("~/CardAccounts/css").Include("~/Content/styles/cardAccounts.css"));

            bundles.Add(new StyleBundle("~/OwnPayments/css").Include("~/Content/styles/ownPayments.css"));

            bundles.Add(new StyleBundle("~/SSISList/css").Include("~/Content/styles/ssisList.css"));

            bundles.Add(new StyleBundle("~/MobileAutopay/css").Include("~/Content/styles/mobile-autopay.css"));

            bundles.Add(new StyleBundle("~/History/css").Include("~/Content/styles/history.css",
                "~/Content/styles/jquery-ui.css"));
        
        }
    }
}