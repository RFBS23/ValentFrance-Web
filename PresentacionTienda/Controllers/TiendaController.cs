using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace PresentacionTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Masproductos()
        {
            return View();
        }

        public ActionResult DetalleProducto(int idproducto = 0)
        {
            Productos oProducto = new Productos();
            bool conversion;
            oProducto = new N_Productos().Listar().Where(p => p.idproducto == idproducto).FirstOrDefault();
            if (oProducto != null)
            {
                oProducto.base64 = N_Recursos.ConvertirBase64(Path.Combine(oProducto.rutaimagen, oProducto.nombreimagen), out conversion);
                oProducto.Extension = Path.GetExtension(oProducto.nombreimagen);
            }
            return View(oProducto);
        }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<Categorias> lista = new List<Categorias>();
            lista = new N_Categorias().FiltrosCategorias();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
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
            }).Where(p => p.oCategorias.idcategoria == (idcategoria == 0 ? p.oCategorias.idcategoria : idcategoria) &&
            p.oMarca.idmarca == (idmarca == 0 ? p.oMarca.idmarca : idmarca) &&
            p.oTallasropa.idtallaropa == (idtallaropa == 0 ? p.oTallasropa.idtallaropa : idtallaropa) &&
            p.stock > 0).ToList();

            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        [HttpPost]
        public JsonResult AgregarCarrito(int idproducto)
        {
            int idcliente = ((Clientes)Session["cliente"]).idcliente;
            bool existe = new N_Carrito().AgregarCarrito(idcliente, idproducto);
            bool respuesta = false;
            string mensaje = string.Empty;
            if(existe)
            {
                mensaje = "El Producto ya existe en el carrito";
            } else
            {
                respuesta = new N_Carrito().OperacionCarrito(idcliente, idproducto, true, out mensaje);
            }
            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CantidadCarrito()
        {
            int idcliente = ((Clientes)Session["cliente"]).idcliente;
            int cantidad = new N_Carrito().CantidadCarrito(idcliente);
            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerDepartamento()
        {
            List<departamento> oLista = new List<departamento> ();
            oLista = new N_Ubicacion().ObtenerDepartamento();
            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerProvincia(string iddepartamento)
        {
            List<provincia> oLista = new List<provincia>();
            oLista = new N_Ubicacion().ObtenerProvincia(iddepartamento);
            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerDistrito(string iddepartamento, string idprovincia)
        {
            List<distrito> oLista = new List<distrito>();
            oLista = new N_Ubicacion().ObtenerDistrito(iddepartamento, idprovincia);
            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Carrito()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListarProductosCarrito()
        {
            int idcliente = ((Clientes)Session["cliente"]).idcliente;
            List<Carrito> oLista = new List<Carrito>();
            bool conversion;
            oLista = new N_Carrito().ListarProducto(idcliente).Select(oc => new Carrito()
            {
                oProductos = new Productos()
                {
                    idproducto = oc.oProductos.idproducto,
                    nombre = oc.oProductos.nombre,
                    descripcion = oc.oProductos.descripcion,
                    oMarca = oc.oProductos.oMarca,
                    descuento = oc.oProductos.descuento,
                    precioventa = oc.oProductos.precioventa,
                    colores = oc.oProductos.colores,
                    rutaimagen = oc.oProductos.rutaimagen,
                    base64 = N_Recursos.ConvertirBase64(Path.Combine(oc.oProductos.rutaimagen, oc.oProductos.nombreimagen), out conversion),
                    Extension = Path.GetExtension(oc.oProductos.nombreimagen)
                },
                cantidad = oc.cantidad
            }).ToList();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OperacionCarrito(int idproducto, bool sumar)
        {
            int idcliente = ((Clientes)Session["cliente"]).idcliente;
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new N_Carrito().OperacionCarrito(idcliente, idproducto, true, out mensaje);
            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCarrito(int idproducto)
        {
            int idcliente = ((Clientes)Session["cliente"]).idcliente;
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new N_Carrito().EliminarCarrito(idcliente, idproducto);
            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}