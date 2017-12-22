using Finan.WEB.Classes;
using System.Web;
using System.Web.Optimization;

namespace Finan.WEB
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {



            // Vendor scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.1.min.js"));

            // jQuery Validation
            var bundle = new ScriptBundle("~/bundles/jqueryval") { Orderer = new AsIsBundleOrderer() };

            bundle
                .Include("~/Scripts/jquery.unobtrusive-ajax.js")
                .Include("~/Scripts/jquery.validate-vsdoc.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/globalize/globalize.js")
                .Include("~/Scripts/jquery.validate.globalize.js");
            bundles.Add(bundle);


            // Validações no padrão brasileiro
            bundles.Add(new ScriptBundle("~/bundles/validations_pt-br").Include(
                      "~/Scripts/jquery.validate.custom.pt-br*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Scripts/app/inspinia.js"));

            // Jquery UI
            bundles.Add(new ScriptBundle("~/plugins/jquery-ui").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/plugins/jqueryui").Include(
                        "~/Content/plugins/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform()));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimScroll/jquery.slimscroll.min.js"));

            // jQuery plugins
            bundles.Add(new ScriptBundle("~/plugins/metsiMenu").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/pace").Include(
                      "~/Scripts/plugins/pace/pace.min.js"));

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/style.css"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));


            //Jquery sweetAlert
            bundles.Add(new ScriptBundle("~/plugins/sweetalert").Include(
                 "~/Scripts/plugins/sweetalert/sweetalert.min.js"));
            bundles.Add(new StyleBundle("~/plugins/sweetalert-css").Include(
                    "~/Content/plugins/sweetalert/sweetalert.css", new CssRewriteUrlTransform()));

            //Jquery jTable
            bundles.Add(new ScriptBundle("~/plugins/jtable").Include(
                 "~/Scripts/plugins/jtable.2.4.0/jquery.jtable.js"));

            bundles.Add(new StyleBundle("~/plugins/jtable-css").Include(
                    "~/Scripts/plugins/jtable.2.4.0/themes/metro/lightgray/jtable.css", new CssRewriteUrlTransform()));

            // switchery
            bundles.Add(new ScriptBundle("~/plugins/switchery").Include(
                      "~/Scripts/plugins/switchery/switchery.js"));
            bundles.Add(new StyleBundle("~/plugins/switcheryStyles").Include(
                      "~/Content/plugins/switchery/switchery.css"));



            // Footable
            bundles.Add(new ScriptBundle("~/plugins/footable").Include(
                      "~/Scripts/plugins/footable/footable.all.min.js"));

            bundles.Add(new StyleBundle("~/plugins/footableStyles").Include(
                      "~/Content/plugins/footable/footable.core.css", new CssRewriteUrlTransform()));


            // Flot chart
            bundles.Add(new ScriptBundle("~/plugins/flot").Include(
                      "~/Scripts/plugins/flot/jquery.flot.js",
                      "~/Scripts/plugins/flot/jquery.flot.tooltip.min.js",
                      "~/Scripts/plugins/flot/jquery.flot.resize.js",
                      "~/Scripts/plugins/flot/jquery.flot.pie.js",
                      "~/Scripts/plugins/flot/jquery.flot.time.js",
                      "~/Scripts/plugins/flot/jquery.flot.spline.js"));

            // fullCalendar styles
            bundles.Add(new StyleBundle("~/plugins/fullCalendarStyles").Include(
                      "~/Content/plugins/fullcalendar/fullcalendar.css"));

            // fullCalendar 
            bundles.Add(new ScriptBundle("~/plugins/fullCalendar").Include(
                      "~/Scripts/plugins/fullcalendar/moment.min.js",
                      "~/Scripts/plugins/fullcalendar/fullcalendar.min.js"));



            //Script para site em geral
            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/Scripts/site-funcoes.js"));

            BundleTable.EnableOptimizations = false;

        }
    }
}
