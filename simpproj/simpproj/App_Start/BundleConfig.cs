using System.Web;
using System.Web.Optimization;

namespace simpproj
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*
            bundles.Add(new StyleBundle("~/Content/css/Pinegrow").Include(
                // Put in all the pinegrow style files
                "~/Content/materialize/materialize.css",
                "~/Content/PG-Blocks/blocks.css",
                "~/Content/PG-Blocks/font-awesome.css",
                "~/Content/PG-Blocks/plugins.css",
                "~/Content/PG-Blocks/style-library-1.css"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/js/Pinegrow").Include(
                // Put in all the JS files for PG
                "~/Scripts/materialize/materialize.js",
                "~/Scripts/PG-Blocks/bskit-scripts.js",
                "~/Scripts/PG-Blocks/html5shiv.js",
                "~/Scripts/PG-Blocks/plugins.js",
                "~/Scripts/PG-Blocks/respond.min.js"
                ));
                */

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/bootstrap").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-theme.css",
                        "~/Content/PG-Blocks/font-awesome.css"
                ));
            bundles.Add(new StyleBundle("~/styles/site").Include(
                    "~/Content/Site.css",
                    "~/Content/bootstrap.css",
                    "~/Content/bootstrap-theme.css"
                ));

            bundles.Add(new StyleBundle("~/styles/admin").Include(
                    "~/Content/Admin.css",
                    "~/Content/bootstrap.css",
                    "~/Content/bootstrap-theme.css"
                ));

            /* bundles.Add(new ScriptBundle("~/scripts/admin").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery.validate.js",
                    "~/Scripts/jquery.validate.unobtrusive.js",
                    "~/Scripts/bootstrap.js"
                )); */

            bundles.Add(new ScriptBundle("~/scripts").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery.validate.js",
                    "~/Scripts/jquery.validate.unobtrusive.js",
                    "~/Scripts/bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/modernizer").Include(
                    "~/Scripts/modernizr-{version}.js"
                ));
        }
    }
}