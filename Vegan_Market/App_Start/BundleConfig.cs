using System.Web;
using System.Web.Optimization;

namespace Vegan_Market
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/chat.js",
                       "~/Scripts/widgets.js",
                        "~/Scripts/vis-timeline.bundle.js",
                         "~/Scripts/users-search.js",
                         "~/Scripts/upgrade-plan.js",
                         "~/Scripts/widgets.bundle.js",
                         "~/Scripts/plugins.bundle.js",
                         "~/Scripts/datatables.bundle.js",
                         "~/Scripts/scripts.bundle.js",
                       "~/Scripts/bottom-72c95545.js",
                       "~/Scripts/bottom-9037d745.js",
                       "~/Scripts/bottom-c5e8c845.js",
                       "~/Scripts/bottom-d90a5145.js",
                       "~/Scripts/rocket-loader.min.js",
                       "~/Scripts/jquery.rateyo.min.js",
                       "~/Scripts/jquery.min.js",
                             "~/Scripts/general.js",
                                "~/Scripts/generals.js"





                       ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/vis-timeline.bundle.css",
                      "~/Content/style.bundle.css",
                      "~/Content/plugins.bundle.css",
                      "~/Content/datatables.bundle.css",
                      "~/Content/theme-4bc04146.css",
                      "~/Content/theme-532f6046.css",
                      "~/Content/theme-8d25c846.css",
                      "~/Content/theme-a8aa3946.css",
                      "~/Content/theme-0d240746.css",
                      "~/Content/theme-00bm.css",
                        "~/Content/jquery.rateyo.min.css"));
        }
    }
}
