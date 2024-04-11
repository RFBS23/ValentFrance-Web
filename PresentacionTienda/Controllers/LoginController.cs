using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PresentacionTienda.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }
        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Clientes objeto)
        {
            int resultado;
            string mensaje = string.Empty;
            ViewData["Documento"] = string.IsNullOrEmpty(objeto.documento) ? "" : objeto.documento;
            ViewData["Nombres"] = string.IsNullOrEmpty(objeto.nombres) ? "" : objeto.nombres;
            ViewData["Apellidos"] = string.IsNullOrEmpty(objeto.apellidos) ? "" : objeto.apellidos;
            ViewData["Telefono"] = string.IsNullOrEmpty(objeto.telefono) ? "" : objeto.telefono;
            ViewData["Correo"] = string.IsNullOrEmpty(objeto.correo) ? "" : objeto.correo;

            if(objeto.clave != objeto.confirmarclave)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            resultado = new N_Clientes().Registrar(objeto, out mensaje);
            if(resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Clientes oCliente = null;
            oCliente = new N_Clientes().Listar().Where(item => item.correo == correo && item.clave == clave).FirstOrDefault();
            if(oCliente == null)
            {
                ViewBag.Error = "Las credenciales son incorrectas";
                return View();
            }else
            {
                if (oCliente.reestablecer)
                {
                    TempData["idcliente"] = oCliente.idcliente;
                    ViewData["nombreapellidos"] = oCliente.nombres + ' ' + oCliente.apellidos;
                    return RedirectToAction("CambiarClave", "Login");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(oCliente.correo, false);
                    Session["cliente"] = oCliente;
                    Session["nombreapellidos"] = oCliente.nombres + ' ' + oCliente.apellidos;
                    Session["correoCliente"] = oCliente.correo;
                    Session["documento"] = oCliente.documento;
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Tienda");
                }
            }
        }

        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Clientes cliente = new N_Clientes().Listar().Where(item => item.correo == correo).FirstOrDefault();
            if (cliente == null)
            {
                ViewBag.Error = "No se encontro al cliente con el correo ingresado";
                return View();
            }
            string mensaje = string.Empty;
            bool respuesta = new N_Clientes().RestablecerClave(cliente.idcliente, correo, out mensaje);
            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string idcliente, string claveactual, string nuevaclave, string confirmarclave)
        {
            Clientes oclientes = new N_Clientes().Listar().Where(u => u.idcliente == int.Parse(idcliente)).FirstOrDefault();
            if (oclientes.clave != claveactual)
            {
                TempData["idcliente"] = idcliente;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña no es correcta";
                return View();
            }
            else if (nuevaclave != confirmarclave)
            {
                TempData["idcliente"] = idcliente;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            ViewData["vclave"] = "";
            nuevaclave = nuevaclave;
            string mensaje = string.Empty;
            bool respuesta = new N_Clientes().CambiarClave(int.Parse(idcliente), nuevaclave, out mensaje);
            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["idcliente"] = idcliente;
                ViewBag.Error = mensaje;
                return View();
            }
        }

        public ActionResult CerrarSesion()
        {
            Session["cliente"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

    }
}