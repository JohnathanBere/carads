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
                "~/Content/materialize/css/materialize.min.css"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/js/Pinegrow").Include(
                // Put in all the JS files for PG
                "~/Scripts/materialize/materialize.js",
                "~/Scripts/materialize/materialize.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                ));
        }
    }
}