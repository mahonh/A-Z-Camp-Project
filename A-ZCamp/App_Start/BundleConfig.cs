using System.Web;
using System.Web.Optimization;

namespace A_ZCamp
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
                      "~/Scripts/respond.js"));

            // Bundle for original site's javascript at begining
            bundles.Add(new ScriptBundle("~/bundles/MainSiteBeginJS").Include(
                      "~/Content/JavaScript/common.js",
                      "~/Content/JavaScript/util.js",
                      "~/Content/JavaScript/stats.js"));

            // Bundle for original site's javascript at end
            bundles.Add(new ScriptBundle("~/bundles/MainSiteJS").Include(
                      "~/Content/JavaScript/jquery-1.js",
                      "~/Content/JavaScript/bootstrap.js",
                      "~/Content/JavaScript/jquery_003.js",
                      "~/Content/JavaScript/waypoints.js",
                      "~/Content/JavaScript/js",
                      "~/Content/JavaScript/gmaps.js",
                      "~/Content/JavaScript/masonry.js",
                      "~/Content/JavaScript/owl.js",
                      "~/Content/JavaScript/jquery_004.js",
                      "~/Content/JavaScript/jquery.js",
                      "~/Content/JavaScript/jquery_002.js",
                      "~/Content/JavaScript/font.js"));

            // Bundle for original site's css
            bundles.Add(new StyleBundle("~/bundles/MainSiteCSS").Include(
                      "~/Content/Style/css.css",
                      "~/Content/Style/font-awesome.css",
                      "~/Content/bootstrap.css",
                      "~/Content/Style/style.css",
                      "~/Content/Style/custom.css",
                      "~/Content/Style/owl.css",
                      "~/Content/Style/owl_002.css",
                      "~/Content/Style/animate.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
