using System.Web.Optimization;

namespace IdentitySample
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/vendor/bootstrap/css/bootstrap.min.css",
                "~/Content/vendor/fontawesome-free/css/all.min.css",
                "~/Content/css/clean-blog.min.css"
                ));
            bundles.Add(new ScriptBundle("~/Content/js").Include(
                "~/Content/vendor/jquery/jquery.min.js",
                "~/Content/vendor/bootstrap/js/bootstrap.bundle.min.js",
                "~/Content/js/clean-blog.min.js"
                ));
        }
    }
}
