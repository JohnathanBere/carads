using System.Web;
using System.Web.Optimization;

namespace simpproj
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css/Pinegrow").Include(
                // Put in all the pinegrow style files
                "~/Content/materialize/css/materialize.css",
                "~/Content/materialize/css/materialize.min.css",
                "~/Content/PG-Blocks/blocks.css",
                "~/Content/PG-Blocks/font-awesome.min.css",
                "~/Content/PG-Blocks/plugins.css",
                "~/Content/PG-Blocks/style-library-1.css"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/js/Pinegrow").Include(
                // Put in all the JS files for PG
                "~/Scripts/materialize/materialize.js",
                "~/Scripts/materialize/materialize.min.js",
                "~/Scripts/PG-Blocks/bskit-scripts.js",
                "~/Scripts/PG-Blocks/html5shiv.js",
                "~/Scripts/PG-Blocks/plugins.js",
                "~/Scripts/PG-Blocks/respond.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                ));
        }
    }
}