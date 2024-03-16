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
using System.IO;
using Newtonsoft.Json;
using System.Configuration;
using System.Globalization;
using System.Data;
using System.Xml;
using ClosedXML.Excel;

namespace presentacionAdministracion.Controllers
{
    [Authorize]
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

        [HttpGet]
        public JsonResult listarUsuarios()
        {
            List<Usuarios> oLista = new List<Usuarios>();
            oLista = new N_Usuarios().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult GuardarUsuarios(string objeto, HttpPostedFileBase archivoimg)
        {
            string mensaje = string.Empty;
            bool operacionexitosa = true;
            bool guardarimg = true;

            Usuarios oUsuarios = new Usuarios();
            oUsuarios = JsonConvert.DeserializeObject<Usuarios>(objeto);
            if (oUsuarios.idusuarioweb == 0)
            {
                int idusergenerado = new N_Usuarios().Registrar(oUsuarios, out mensaje);
                if (idusergenerado != 0)
                {
                    oUsuarios.idusuarioweb = idusergenerado;
                }
                else
                {
                    operacionexitosa = false;
                }
            }
            else
            {
                operacionexitosa = new N_Usuarios().Editar(oUsuarios, out mensaje);
            }
            if (operacionexitosa)
            {
                if (archivoimg != null)
                {
                    string rutaguardar = ConfigurationManager.AppSettings["ServidorFotosUsuarios"];
                    string extension = Path.GetExtension(archivoimg.FileName);
                    string nombreimg = string.Concat(oUsuarios.idusuarioweb.ToString(), extension);
                    try
                    {
                        archivoimg.SaveAs(Path.Combine(rutaguardar, nombreimg));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardarimg = false;
                    }
                    if (guardarimg)
                    {
                        oUsuarios.rutaimagen = rutaguardar;
                        oUsuarios.nombreimagen = nombreimg;
                        bool rpta = new N_Usuarios().GuardarimagenUsuario(oUsuarios, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardó el usuario, pero se encontraron problemas en la imagen 🌄";
                    }
                }
            }
            return Json(new { operacionExitosa = operacionexitosa, idgenerado = oUsuarios.idusuarioweb, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult imagenUsuario(int id)
        {
            bool conversion;
            Usuarios ousuario = new N_Usuarios().Listar().Where(u => u.idusuarioweb == id).FirstOrDefault();
            string textoBase64 = N_Recursos.ConvertirBase64(Path.Combine(ousuario.rutaimagen, ousuario.nombreimagen), out conversion);
            return Json(new
            {
                conversion = conversion,
                textobase64 = textoBase64,
                extension = Path.GetExtension(ousuario.nombreimagen)
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarUsuarios(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new N_Usuarios().Eliminar(id, out mensaje);
            return Json(new
            {
                resultado = respuesta,
                mensaje = mensaje,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult VistaTotalCards()
        {
            Dashboard objeto = new N_Reportes().verTotales();
            return Json(new
            {
                resultado = objeto
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Reportes(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reportes> oLista = new List<Reportes>();
            oLista = new N_Reportes().Ventas(fechainicio, fechafin, idtransaccion);
            return Json(new {data = oLista}, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public FileResult ExportarVentaExcel(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reportes> oLista = new List<Reportes>();
            oLista = new N_Reportes().Ventas(fechainicio, fechafin, idtransaccion);
            DataTable dt = new DataTable();
            dt.Columns.Add("Comprobante", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("Fecha de Venta", typeof(string));

            foreach (Reportes r in oLista)
            {
                dt.Rows.Add(new object[] { r.idtransaccion, r.NombresApellidos, r.nombre, r.precioventa, r.cantidad, r.total, r.FechaVenta });
            }
            var columnWidths = new int[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                columnWidths[i] = dt.AsEnumerable().Select(row => row.Field<string>(i)).Concat(new[] { dt.Columns[i].ColumnName }).Max(s => s.Length) + 3;
            }
            dt.TableName = "Ventas Realizadas";
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ws.Column(i + 1).Width = columnWidths[i];
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta" + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }


    }
}