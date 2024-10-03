using Entidad;
using Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Globalization;
using ClosedXML.Excel;

namespace presentacionAdmin.Controllers
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
        public JsonResult ListarUsuarios()
        {
            List<Usuarios> oLista = new List<Usuarios>();
            oLista = new N_Usuarios().Listar();
            return Json(new { data= oLista }, JsonRequestBehavior.AllowGet);
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
        public JsonResult GuardarUsuarios(Usuarios objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if(objeto.idusuarioweb == 0)
            {
                resultado = new N_Usuarios().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new N_Usuarios().Editar(objeto, out mensaje);
            }
            
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
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
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta - " + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }

    }
}