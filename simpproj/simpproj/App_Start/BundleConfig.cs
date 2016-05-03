using System.Web;
using System.Web.Optimization;

namespace simpproj
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                ));

            bundles.Add(new StyleBundle("~/styles/site").Include(
                    "~/Content/Site.css",
                    "~/Content/bootstrap.css",
                    "~/Content/bootstrap-theme.css",
                    "~/Content/PG-Blocks/font-awesome.css",
                    "~/Content/PG-Blocks/blocks.css",
                    "~/Content/PG-Blocks/plugins.css",
                    "~/Content/PG-Blocks/style-library-1.css"
                ));

            bundles.Add(new StyleBundle("~/styles/admin").Include(
                    "~/Content/Admin.css",
                    "~/Content/bootstrap.css",
                    "~/Content/bootstrap-theme.css",
                    "~/Content/PG-Blocks/font-awesome.css",
                    "~/Content/PG-Blocks/blocks.css",
                    "~/Content/PG-Blocks/plugins.css",
                    "~/Content/PG-Blocks/style-library-1.css"
                ));

             bundles.Add(new ScriptBundle("~/scripts/admin").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery.validate.js",
                    "~/Scripts/jquery-{version}.intellisense.js",
                    "~/Scripts/jquery.validate.unobtrusive.js",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/PG-Blocks/bskit-scripts.js",
                    "~/Scripts/PG-Blocks/html5shiv.js",
                    "~/Scripts/PG-Blocks/plugins.js",
                    "~/Scripts/PG-Blocks/respond.min.js",
                    "~/Areas/Admin/Scripts/Forms.js"
                ));

            bundles.Add(new ScriptBundle("~/scripts").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery-{version}.intellisense.js",
                    "~/Scripts/jquery.validate.js",
                    "~/Scripts/jquery.validate.unobtrusive.js",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/PG-Blocks/bskit-scripts.js",
                    "~/Scripts/PG-Blocks/html5shiv.js",
                    "~/Scripts/PG-Blocks/plugins.js",
                    "~/Scripts/PG-Blocks/respond.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/modernizer").Include(
                    "~/Scripts/modernizr-{version}.js"
                ));
        }
    }
}