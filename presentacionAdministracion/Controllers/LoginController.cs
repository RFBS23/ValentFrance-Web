using DocumentFormat.OpenXml.Wordprocessing;
using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;

namespace presentacionAdministracion.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
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
        public ActionResult Index(string correo, string clave)
        {
            Usuarios ousuario = new N_Usuarios().Listar().Where(u => u.correo == correo && u.clave == clave).FirstOrDefault();
            if (ousuario == null)
            {
                ViewBag.Error = "Correo o contraseña no son validos";
                return View();
            }
            else
            {
                if (ousuario.reestablecer)
                {
                    TempData["idusuarioweb"] = ousuario.idusuarioweb;
                    ViewData["nombreapellidos"] = ousuario.nombres + ' ' + ousuario.apellidos;
                    return RedirectToAction("CambiarClave");
                }

                FormsAuthentication.SetAuthCookie(ousuario.correo, false);
                Session["nombreUsuario"] = ousuario.nombreusuario;
                Session["nombreapellidos"] = ousuario.nombres + ' ' + ousuario.apellidos;
                Session["correoUsuario"] = ousuario.correo;
                Session["documento"] = ousuario.documento;
                ViewBag.Error = null;                
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string idusuarioweb, string claveactual, string nuevaclave, string confirmarclave)
        {
            Usuarios ousuario = new N_Usuarios().Listar().Where(u => u.idusuarioweb == int.Parse(idusuarioweb)).FirstOrDefault();
            if(ousuario.clave != claveactual)
            {
                TempData["idusuarioweb"] = idusuarioweb;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña no es correcta";
                return View();
            }
            else if (nuevaclave != confirmarclave)
            {
                TempData["idusuarioweb"] = idusuarioweb;
                ViewData["vclave"] = claveactual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            ViewData["vclave"] = "";
            nuevaclave = nuevaclave;
            string mensaje = string.Empty;
            bool respuesta = new N_Usuarios().CambiarClave(int.Parse(idusuarioweb), nuevaclave, out mensaje);
            if(respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["idusuarioweb"] = idusuarioweb;
                ViewBag.Error = mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Usuarios ousuario = new N_Usuarios().Listar().Where(item => item.correo == correo).FirstOrDefault();
            if(ousuario == null)
            {
                ViewBag.Error = "No se encontro al usuario con el correo ingresado";
                return View();
            }
            string mensaje = string.Empty;
            bool respuesta = new N_Usuarios().RestablecerClave(ousuario.idusuarioweb, correo, out mensaje);
            if(respuesta)
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

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}