using System.Web;
using System.Web.Optimization;

namespace presentacionAdmin
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/complementos").Include(
                "~/Scripts/fontawesome/all.min.js",
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/quill.min.js",
                "~/Scripts/sweetalert.min.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery-ui.js",
                "~/Scripts/loadingoverlay/loadingoverlay.min.js",
                "~/Scripts/DataTables/dataTables.responsive.js",
                "~/Scripts/tinymce/tinymce.min.js",
                "~/Scripts/main.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap-icons/bootstrap-icons.css",
                "~/Content/bootstrap.min.css",
                "~/Content/quill.snow.css",
                "~/Content/sweetalert.css",
                "~/Content/quill.bubble.css",
                "~/Content/jquery-ui.css",
                "~/Content/DataTables/css/jquery.dataTables.css",
                "~/Content/DataTables/css/responsive.dataTables.css",
                "~/Content/site.css"
             ));
        }
    }
}
