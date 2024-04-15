using System.Web;
using System.Web.Optimization;

namespace PresentacionTienda
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/complementos").Include(
                        "~/Scripts/loadingoverlay/loadingoverlay.min.js",
                        "~/Scripts/sweetalert.min.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/tiny-slider.js",
                        "~/Scripts/glightbox.min.js",
                        "~/Scripts/jstienda.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js",
                      "~/Scripts/jquery.easing.min.js",
                      "~/Scripts/jquery.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-icons/bootstrap-icons.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/sweetalert.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/LineIcons.3.0.css",
                      "~/Content/tiny-slider.css",
                      "~/Content/glightbox.min.css",
                      "~/Content/main.css"));
        }
    }
}
