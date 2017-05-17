using System.Web;
using System.Web.Optimization;

namespace ShopFull
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/defcss")
           .Include("~/Content/css/flexslider.css")
           .Include("~/Content/css/menu.css"));

            bundles.Add(new StyleBundle("~/bundles/stylecss").Include("~/Content/css/style.css"));

            bundles.Add(new StyleBundle("~/bundles/bootstrapcss").Include("~/Content/bootstrap.min.css", "~/Content/bootstrap.css", "~/Content/bootstrap-theme.css", "~/Content/bootstrap-theme.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include("~/Content/js/jquery-1.7.2.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/sliderjs")
               .Include("~/Content/js/jquery.flexslider.js"));


            bundles.Add(new ScriptBundle("~/bundles/scriptjqm")
                .Include("~/Content/js/jquerymain.js"));
            bundles.Add(new ScriptBundle("~/bundles/script")
                .Include("~/Content/js/script.js"));
            bundles.Add(new ScriptBundle("~/bundles/nav")
                .Include("~/Content/js/nav.js"));
            bundles.Add(new ScriptBundle("~/bundles/move-top")
                .Include("~/Content/js/move-top.js"));
            bundles.Add(new ScriptBundle("~/bundles/easing")
                .Include("~/Content/js/easing.js"));
        }
    }
}