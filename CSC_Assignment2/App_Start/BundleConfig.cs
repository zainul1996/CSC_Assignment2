using System.Web;
using System.Web.Optimization;

namespace CSC_Assignment2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/semantic/css").Include(
                "~/Content/Semantic-UI-CSS-master/semantic.min.css"));

            bundles.Add(new ScriptBundle("~/Content/semantic/script").Include(
                "~/Content/Semantic-UI-CSS-master/semantic.min.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
