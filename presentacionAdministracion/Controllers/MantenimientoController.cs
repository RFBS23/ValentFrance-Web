using Entidad;
using Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace presentacionAdministracion.Controllers
{
    public class MantenimientoController : Controller
    {
        // GET: Mantenimiento
        public ActionResult Categoria()
        {
            return View();
        }

        public ActionResult Marca()
        {
            return View();
        }

        public ActionResult Producto()
        {
            return View();
        }

        public ActionResult Talla()
        {
            return View();
        }


        #region categoria
        [HttpGet]
        public JsonResult listarCategoria()
        {
            List<Categorias> oLista = new List<Categorias>();
            oLista = new N_Categorias().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategorias(Categorias objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.idcategoria == 0)
            {
                resultado = new N_Categorias().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new N_Categorias().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategorias(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_Categorias().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region tallas
        [HttpGet]
        public JsonResult listarTallas()
        {
            List<Tallasropa> oLista = new List<Tallasropa>();
            oLista = new N_Tallas().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTallas(Tallasropa objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.idtallaropa == 0)
            {
                resultado = new N_Tallas().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new N_Tallas().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTallas(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_Tallas().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        
        #region listar
        [HttpGet]
        public JsonResult listarCategorias()
        {
            List<Categorias> listaCompleta = new N_Categorias().Listar();
            List<Categorias> listaFiltrada = listaCompleta.Where(c => c.estado).ToList();
            var opciones = listaFiltrada.Select(categoria => new { id = categoria.idcategoria, nombre = categoria.nombrecategoria });
            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult listarTalla()
        {
            List<Tallasropa> listaCompleta = new N_Tallas().Listar();
            List<Tallasropa> listaFiltrada = listaCompleta.ToList();
            var opciones = listaFiltrada.Select(categoria => new { id = categoria.idtallaropa, nombre = categoria.nombretalla });
            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult listarMarca()
        {
            List<Marca> listaCompleta = new N_Marcas().Listar();
            List<Marca> listaFiltrada = listaCompleta.Where(c => c.estado).ToList();
            var opciones = listaFiltrada.Select(categoria => new { id = categoria.idmarca, nombre = categoria.nombremarca });
            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region marcas
        [HttpGet]
        public JsonResult listarMarcas()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = new N_Marcas().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMarcas(Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.idmarca == 0)
            {
                resultado = new N_Marcas().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new N_Marcas().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarcas(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_Marcas().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region productos
        [HttpGet]
        public JsonResult listarProductos()
        {
            List<Productos> oLista = new List<Productos>();
            oLista = new N_Productos().Listar(); // Asegúrate de que tu método Listar está configurado para utilizar el procedimiento almacenado
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listarCatProductos()
        {
            List<Categorias> listaCompleta = new N_Categorias().Listar();
            List<Categorias> listaFiltrada = listaCompleta.Where(c => c.estado).ToList();
            var opciones = listaFiltrada.Select(categoria => new { id = categoria.idcategoria, nombre = categoria.nombrecategoria });
            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult listartallasProductos()
        {
            List<Tallasropa> listaCompleta = new N_Tallas().Listar();
            var opciones = listaCompleta.Select(tallas => new { id = tallas.idtallaropa, nombre = tallas.nombretalla });
            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listarmarcasProductos()
        {
            List<Marca> listaCompleta = new N_Marcas().Listar();
            var opciones = listaCompleta.Select(marca => new { id = marca.idmarca, nombre = marca.nombremarca });
            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProductos(string objeto, HttpPostedFileBase archivoimg)
        {
            string mensaje = string.Empty;
            bool operacionexitosa = true;
            bool guardarimg = true;

            Productos oProducto = new Productos();
            oProducto = JsonConvert.DeserializeObject<Productos>(objeto);
            decimal precio;
            if (decimal.TryParse(oProducto.precioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out precio))
            {
                oProducto.precioventa = precio;
            }
            else
            {
                return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);
            }
            if (oProducto.idproducto == 0)
            {
                int idprodgenerado = new N_Productos().Registrar(oProducto, out mensaje);
                if (idprodgenerado != 0)
                {
                    oProducto.idproducto = idprodgenerado;
                }
                else
                {
                    operacionexitosa = false;
                }
            }
            else
            {
                operacionexitosa = new N_Productos().Editar(oProducto, out mensaje);
            }
            if (operacionexitosa)
            {
                if (archivoimg != null)
                {
                    string rutaguardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoimg.FileName);
                    string nombreimg = string.Concat(oProducto.idproducto.ToString(), extension);
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
                        oProducto.rutaimagen = rutaguardar;
                        oProducto.nombreimagen = nombreimg;
                        bool rpta = new N_Productos().GuardarImg(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto pero se encontraron problemas en la imagen 🌄";
                    }
                }
            }

            return Json(new { operacionExitosa = operacionexitosa, idgenerado = oProducto.idproducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult imgProductos(int id)
        {
            bool conversion;
            Productos oProducto = new N_Productos().Listar().Where(p => p.idproducto == id).FirstOrDefault();
            string textoBase64 = N_Recursos.ConvertirBase64(Path.Combine(oProducto.rutaimagen, oProducto.nombreimagen), out conversion);
            return Json(new
            {
                conversion = conversion,
                textobase64 = textoBase64,
                extension = Path.GetExtension(oProducto.nombreimagen)
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}