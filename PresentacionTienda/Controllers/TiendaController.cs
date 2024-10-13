using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace presentacionTienda.Controllers
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

        public ActionResult Contacto()
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
                oProducto.base2 = N_Recursos.ConvertirBase2(Path.Combine(oProducto.rutaimagendos, oProducto.nombreimagendos), out conversion);
                oProducto.Extension2 = Path.GetExtension(oProducto.nombreimagendos);
                oProducto.base3 = N_Recursos.ConvertirBase3(Path.Combine(oProducto.rutaimagen3, oProducto.nombreimagen3), out conversion);
                oProducto.Extension3 = Path.GetExtension(oProducto.nombreimagen3);
                oProducto.base4 = N_Recursos.ConvertirBase4(Path.Combine(oProducto.rutaimagen4, oProducto.nombreimagen4), out conversion);
                oProducto.Extension4 = Path.GetExtension(oProducto.nombreimagen4);
            }
            return View(oProducto);
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            lista = new N_Categoria().FiltrosCategorias();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FiltroMarca()
        {
            List<Marca> lista = new List<Marca>();
            lista = new N_Marca().FiltrosMarcas();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FiltroTallas(int idcategoria)
        {
            List<Tallas> lista = new List<Tallas>();
            lista = new N_Tallas().FiltrosTallasCategorias(idcategoria);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult listarProductos(int idcategoria, int idmarca, int idtallaropa, int page = 1, int pageSize = 12)
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
                rutaimagendos = p.rutaimagendos,
                rutaimagen3 = p.rutaimagen3,
                rutaimagen4 = p.rutaimagen4,
                base64 = N_Recursos.ConvertirBase64(Path.Combine(p.rutaimagen, p.nombreimagen), out conversion),
                Extension = Path.GetExtension(p.nombreimagen),
                base2 = N_Recursos.ConvertirBase2(Path.Combine(p.rutaimagendos, p.nombreimagendos), out conversion),
                Extension2 = Path.GetExtension(p.nombreimagendos),
                base3 = N_Recursos.ConvertirBase3(Path.Combine(p.rutaimagen3, p.nombreimagen3), out conversion),
                Extension3 = Path.GetExtension(p.nombreimagen3),
                base4 = N_Recursos.ConvertirBase4(Path.Combine(p.rutaimagen4, p.nombreimagen4), out conversion),
                Extension4 = Path.GetExtension(p.nombreimagen4),
            }).Where(p =>
                p.oCategorias.idcategoria == (idcategoria == 0 ? p.oCategorias.idcategoria : idcategoria) &&
                p.oMarca.idmarca == (idmarca == 0 ? p.oMarca.idmarca : idmarca) &&
                p.oTallasropa.idtallaropa == (idtallaropa == 0 ? p.oTallasropa.idtallaropa : idtallaropa) &&
                p.stock > 0
            ).ToList();
            int totalItems = lista.Count();
            var productosPaginados = lista.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var jsonresult = Json(new
            {
                data = productosPaginados,
                totalItems = totalItems,
                totalPages = totalPages
            }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        [HttpPost]
        public JsonResult listarInicioProductos(int idcategoria, int idmarca, int idtallaropa, int page = 1, int pageSize = 8)
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
                rutaimagendos = p.rutaimagendos,
                rutaimagen3 = p.rutaimagen3,
                rutaimagen4 = p.rutaimagen4,
                base64 = N_Recursos.ConvertirBase64(Path.Combine(p.rutaimagen, p.nombreimagen), out conversion),
                Extension = Path.GetExtension(p.nombreimagen),
                base2 = N_Recursos.ConvertirBase2(Path.Combine(p.rutaimagendos, p.nombreimagendos), out conversion),
                Extension2 = Path.GetExtension(p.nombreimagendos),
                base3 = N_Recursos.ConvertirBase3(Path.Combine(p.rutaimagen3, p.nombreimagen3), out conversion),
                Extension3 = Path.GetExtension(p.nombreimagen3),
                base4 = N_Recursos.ConvertirBase4(Path.Combine(p.rutaimagen4, p.nombreimagen4), out conversion),
                Extension4 = Path.GetExtension(p.nombreimagen4),
            }).Where(p =>
                p.oCategorias.idcategoria == (idcategoria == 0 ? p.oCategorias.idcategoria : idcategoria) &&
                p.oMarca.idmarca == (idmarca == 0 ? p.oMarca.idmarca : idmarca) &&
                p.oTallasropa.idtallaropa == (idtallaropa == 0 ? p.oTallasropa.idtallaropa : idtallaropa) &&
                p.stock > 0
            ).ToList();
            int totalItems = lista.Count();
            var productosPaginados = lista.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var jsonresult = Json(new
            {
                data = productosPaginados,
                totalItems = totalItems,
                totalPages = totalPages
            }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        [HttpPost]
        public JsonResult listarProductosDescuento(int idcategoria, int idmarca, int idtallaropa, int page = 1, int pageSize = 8)
        {
            List<ProductosCategoria> lista = new List<ProductosCategoria>();
            bool conversion;

            lista = new N_ProductosCategoria().ProductosInicioDescuento().Select(p => new ProductosCategoria()
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
                rutaimagendos = p.rutaimagendos,
                rutaimagen3 = p.rutaimagen3,
                rutaimagen4 = p.rutaimagen4,
                base64 = N_Recursos.ConvertirBase64(Path.Combine(p.rutaimagen, p.nombreimagen), out conversion),
                Extension = Path.GetExtension(p.nombreimagen),
                base2 = N_Recursos.ConvertirBase2(Path.Combine(p.rutaimagendos, p.nombreimagendos), out conversion),
                Extension2 = Path.GetExtension(p.nombreimagendos),
                base3 = N_Recursos.ConvertirBase3(Path.Combine(p.rutaimagen3, p.nombreimagen3), out conversion),
                Extension3 = Path.GetExtension(p.nombreimagen3),
                base4 = N_Recursos.ConvertirBase4(Path.Combine(p.rutaimagen4, p.nombreimagen4), out conversion),
                Extension4 = Path.GetExtension(p.nombreimagen4),
            }).Where(p =>
                p.oCategorias.idcategoria == (idcategoria == 0 ? p.oCategorias.idcategoria : idcategoria) &&
                p.oMarca.idmarca == (idmarca == 0 ? p.oMarca.idmarca : idmarca) &&
                p.oTallasropa.idtallaropa == (idtallaropa == 0 ? p.oTallasropa.idtallaropa : idtallaropa) &&
                p.stock > 0
            ).ToList();
            int totalItems = lista.Count();
            var productosPaginados = lista.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var jsonresult = Json(new
            {
                data = productosPaginados,
                totalItems = totalItems,
                totalPages = totalPages
            }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

        [HttpPost]
        public JsonResult listarProductosNuevos(int idcategoria, int idmarca, int idtallaropa, int page = 1, int pageSize = 8)
        {
            List<ProductosCategoria> lista = new List<ProductosCategoria>();
            bool conversion;

            lista = new N_ProductosCategoria().ProductosInicioNuevos().Select(p => new ProductosCategoria()
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
                rutaimagendos = p.rutaimagendos,
                rutaimagen3 = p.rutaimagen3,
                rutaimagen4 = p.rutaimagen4,
                base64 = N_Recursos.ConvertirBase64(Path.Combine(p.rutaimagen, p.nombreimagen), out conversion),
                Extension = Path.GetExtension(p.nombreimagen),
                base2 = N_Recursos.ConvertirBase2(Path.Combine(p.rutaimagendos, p.nombreimagendos), out conversion),
                Extension2 = Path.GetExtension(p.nombreimagendos),
                base3 = N_Recursos.ConvertirBase3(Path.Combine(p.rutaimagen3, p.nombreimagen3), out conversion),
                Extension3 = Path.GetExtension(p.nombreimagen3),
                base4 = N_Recursos.ConvertirBase4(Path.Combine(p.rutaimagen4, p.nombreimagen4), out conversion),
                Extension4 = Path.GetExtension(p.nombreimagen4),
                fecharegistro = p.fecharegistro
            }).Where(p =>
                p.oCategorias.idcategoria == (idcategoria == 0 ? p.oCategorias.idcategoria : idcategoria) &&
                p.oMarca.idmarca == (idmarca == 0 ? p.oMarca.idmarca : idmarca) &&
                p.oTallasropa.idtallaropa == (idtallaropa == 0 ? p.oTallasropa.idtallaropa : idtallaropa) &&
                p.stock > 0
            ).ToList();
            int totalItems = lista.Count();
            var productosPaginados = lista.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var jsonresult = Json(new
            {
                data = productosPaginados,
                totalItems = totalItems,
                totalPages = totalPages
            }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;
            return jsonresult;
        }

    }
}