using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentacionTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetalleProducto(int idproducto = 0)
        {
            Productos oProducto = new Productos();
            bool conversion;
            oProducto = new N_Productos().Listar().Where(p => p.idproducto == idproducto).FirstOrDefault();
            if(oProducto != null)
            {
                oProducto.base64 = N_Recursos.ConvertirBase64(Path.Combine(oProducto.rutaimagen, oProducto.nombreimagen), out conversion);
                oProducto.Extension = Path.GetExtension(oProducto.nombreimagen);
            }
            return View(oProducto);
        }

        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<Categorias> lista = new List<Categorias>();
            lista = new N_Categorias().FiltrosCategorias();
            return Json(new {data = lista}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FiltroMarca()
        {
            List<Marca> lista = new List<Marca>();
            lista = new N_Marcas().FiltrosMarcas();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FiltroTallas(int idcategoria)
        {
            List<Tallasropa> lista = new List<Tallasropa>();
            lista = new N_Tallas().FiltrosTallasCategorias(idcategoria);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult listarProductos(int idcategoria, int idmarca, int idtallaropa)
        {
            List<Productos> lista = new List<Productos>();
            bool conversion;
            lista = new N_Productos().Listar().Select(p => new Productos()
            {
                idproducto = p.idproducto,
                codigo = p.codigo,
                nombre = p.nombre,
                descripcion = p.descripcion,
                colores = p.colores,
                temporada = p.temporada,
                descuento = p.descuento,
                oMarca = p.oMarca,
                oCategorias = p.oCategorias,
                oTallasropa = p.oTallasropa,
                precioventa = p.precioventa,
                stock = p.stock,
                rutaimagen = p.rutaimagen,
                base64 = N_Recursos.ConvertirBase64(Path.Combine(p.rutaimagen, p.nombreimagen), out conversion),
                Extension = Path.GetExtension(p.nombreimagen)
            }).Where (p => p.oCategorias.idcategoria == (idcategoria == 0 ? p.oCategorias.idcategoria : idcategoria) && 
            p.oMarca.idmarca == (idmarca == 0 ? p.oMarca.idmarca : idmarca ) && 
            p.oTallasropa.idtallaropa == (idtallaropa == 0 ? p.oTallasropa.idtallaropa : idtallaropa) && 
            p.stock > 0 ).ToList();

            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

    }
}