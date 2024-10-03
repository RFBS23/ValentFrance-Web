using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Newtonsoft.Json;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace presentacionAdmin.Controllers
{
    [Authorize]
    public class ComponentesController : Controller
    {
        // GET: Componentes
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
        public ActionResult Tallas()
        {
            return View();
        }

        #region Categorias
        [HttpGet]
        public JsonResult listarCategoria()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = new N_Categoria().Listar();
            return Json(new {data = oLista}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategorias(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.idcategoria == 0)
            {
                resultado = new N_Categoria().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new N_Categoria().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategorias(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new N_Categoria().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region tallas
        [HttpGet]
        public JsonResult listarTallas()
        {
            List<Tallas> oLista = new List<Tallas>();
            oLista = new N_Tallas().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTallas(Tallas objeto)
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
            List<Categoria> listaCompleta = new N_Categoria().Listar();
            List<Categoria> listaFiltrada = listaCompleta.Where(c => c.estado).ToList();
            var opciones = listaFiltrada.Select(categoria => new { id = categoria.idcategoria, nombre = categoria.nombrecategoria });
            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult listarTalla()
        {
            List<Tallas> listaCompleta = new N_Tallas().Listar();
            List<Tallas> listaFiltrada = listaCompleta.ToList();
            var opciones = listaFiltrada.Select(categoria => new { id = categoria.idtallaropa, nombre = categoria.nombretalla });
            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult listarMarca()
        {
            List<Marca> listaCompleta = new N_Marca().Listar();
            List<Marca> listaFiltrada = listaCompleta.Where(c => c.estado).ToList();
            var opciones = listaFiltrada.Select(categoria => new { id = categoria.idmarca, nombre = categoria.nombremarca });
            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Marca
        [HttpGet]
        public JsonResult listarMarcas()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = new N_Marca().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMarcas(Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.idmarca == 0)
            {
                resultado = new N_Marca().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new N_Marca().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarcas(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_Marca().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Productos
        [HttpGet]
        public JsonResult listarProductos()
        {
            List<Productos> oLista = new List<Productos>();
            oLista = new N_Productos().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listarCatProductos()
        {
            List<Categoria> listaCompleta = new N_Categoria().Listar();
            List<Categoria> listaFiltrada = listaCompleta.Where(c => c.estado).ToList();
            var opciones = listaFiltrada.Select(categoria => new { id = categoria.idcategoria, nombre = categoria.nombrecategoria });
            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult listartallasProductos()
        {
            List<Tallas> listaCompleta = new N_Tallas().Listar();
            var opciones = listaCompleta.Select(tallas => new { id = tallas.idtallaropa, nombre = tallas.nombretalla });
            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listarmarcasProductos()
        {
            List<Marca> listaCompleta = new N_Marca().Listar();
            var opciones = listaCompleta.Select(marca => new { id = marca.idmarca, nombre = marca.nombremarca });
            return Json(new { data = opciones }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProductos(string objeto, HttpPostedFileBase archivoimg, HttpPostedFileBase archivoimg2, HttpPostedFileBase archivoimg3, HttpPostedFileBase archivoimg4)
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
                return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe ser 00.00" }, JsonRequestBehavior.AllowGet);
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

                if (archivoimg2 != null)
                {
                    string rutaguardar = ConfigurationManager.AppSettings["ServidorFotosdos"];
                    string extension = Path.GetExtension(archivoimg2.FileName);
                    string nombreimg2 = string.Concat(oProducto.idproducto.ToString(), extension);
                    try
                    {
                        archivoimg2.SaveAs(Path.Combine(rutaguardar, nombreimg2));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardarimg = false;
                    }
                    if (guardarimg)
                    {
                        oProducto.rutaimagendos = rutaguardar;
                        oProducto.nombreimagendos = nombreimg2;
                        bool rpta = new N_Productos().GuardarImg2(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto pero se encontraron problemas en la imagen 🌄";
                    }
                }

                if (archivoimg3 != null)
                {
                    string rutaguardar = ConfigurationManager.AppSettings["ServidorFotos3"];
                    string extension = Path.GetExtension(archivoimg3.FileName);
                    string nombreimg3 = string.Concat(oProducto.idproducto.ToString(), extension);
                    try
                    {
                        archivoimg3.SaveAs(Path.Combine(rutaguardar, nombreimg3));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardarimg = false;
                    }
                    if (guardarimg)
                    {
                        oProducto.rutaimagen3 = rutaguardar;
                        oProducto.nombreimagen3 = nombreimg3;
                        bool rpta = new N_Productos().GuardarImg3(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto pero se encontraron problemas en la imagen 🌄";
                    }
                }

                if (archivoimg4 != null)
                {
                    string rutaguardar = ConfigurationManager.AppSettings["ServidorFotos4"];
                    string extension = Path.GetExtension(archivoimg4.FileName);
                    string nombreimg4 = string.Concat(oProducto.idproducto.ToString(), extension);
                    try
                    {
                        archivoimg4.SaveAs(Path.Combine(rutaguardar, nombreimg4));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardarimg = false;
                    }
                    if (guardarimg)
                    {
                        oProducto.rutaimagen4 = rutaguardar;
                        oProducto.nombreimagen4 = nombreimg4;
                        bool rpta = new N_Productos().GuardarImg4(oProducto, out mensaje);
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
        public JsonResult imagenProducto(int id)
        {
            bool conversion;
            Productos oproducto = new N_Productos().Listar().Where(p => p.idproducto == id).FirstOrDefault();

            string textoBase64 = N_Recursos.ConvertirBase64(Path.Combine(oproducto.rutaimagen, oproducto.nombreimagen), out conversion);
            return Json(new
            {
                conversion = conversion,
                textobase64 = textoBase64,
                extension = Path.GetExtension(oproducto.nombreimagen)
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult imagenProductodos(int id)
        {
            bool conversion;
            Productos oproducto = new N_Productos().Listar().Where(p => p.idproducto == id).FirstOrDefault();

            string textoBase64 = N_Recursos.ConvertirBase2(Path.Combine(oproducto.rutaimagendos, oproducto.nombreimagendos), out conversion);
            return Json(new
            {
                conversion = conversion,
                textobase64 = textoBase64,
                extension = Path.GetExtension(oproducto.nombreimagendos)
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult imagenProductotres(int id)
        {
            bool conversion;
            Productos oproducto = new N_Productos().Listar().Where(p => p.idproducto == id).FirstOrDefault();

            string textoBase64 = N_Recursos.ConvertirBase3(Path.Combine(oproducto.rutaimagen3, oproducto.nombreimagen3), out conversion);
            return Json(new
            {
                conversion = conversion,
                textobase64 = textoBase64,
                extension = Path.GetExtension(oproducto.nombreimagen3)
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult imagenProductocuatro(int id)
        {
            bool conversion;
            Productos oproducto = new N_Productos().Listar().Where(p => p.idproducto == id).FirstOrDefault();

            string textoBase64 = N_Recursos.ConvertirBase4(Path.Combine(oproducto.rutaimagen4, oproducto.nombreimagen4), out conversion);
            return Json(new
            {
                conversion = conversion,
                textobase64 = textoBase64,
                extension = Path.GetExtension(oproducto.nombreimagen4)
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarProductos(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_Productos().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}