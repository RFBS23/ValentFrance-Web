using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace presentacionAdministracion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        public JsonResult listarUsuarios()
        {
            List<Usuarios> oLista = new List<Usuarios>();
            oLista = new N_Usuarios().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listarNivelAcceso()
        {
            List<NivelAcceso> oLista = new List<NivelAcceso>();
            oLista = new N_Nivelacceso().Listar();

            var opciones = oLista.Select(nivel => new { id = nivel.idrol, nombre = nivel.nombrerol });

            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerInformacionDNI(string numeroDNI)
        {
            var apiUrl = $"https://api.apis.net.pe/v1/dni?numero={numeroDNI}";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(apiUrl);
                    // Aquí puedes procesar la respuesta y devolver los datos necesarios a la vista
                    return Content(response, "application/json");
                }
            }
            catch (HttpRequestException ex)
            {
                return Content($"Error al obtener la información del DNI: {ex.Message}", "text/plain");
            }
        }

    }
}