using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace presentacionAdmin.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Perfil()
        {
            return View();
        }

        private const string ApiBaseUrl = "https://api.apis.net.pe/v1/ruc";

        [HttpGet]
        public async Task<ActionResult> ObtenerInformacionRuc(string numeroRuc)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string apiUrl = $"{ApiBaseUrl}?numero={numeroRuc}";
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Json(responseBody, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Error al obtener la información del RUC", JsonRequestBehavior.AllowGet);
                }
            }
        }

    }
}