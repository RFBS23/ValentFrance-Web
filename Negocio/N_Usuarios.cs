using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Usuarios
    {
        private D_Usuarios objdatos = new D_Usuarios();
        public List<Usuarios> Listar()
        {
            return objdatos.Listar();
        }

        public int Registrar(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if(string.IsNullOrEmpty(obj.documento) || string.IsNullOrWhiteSpace(obj.documento))
            {
                Mensaje = "Debe ingresar el documento de identidad";
            }
            if (string.IsNullOrEmpty(obj.nombreusuario) || string.IsNullOrWhiteSpace(obj.nombreusuario))
            {
                Mensaje = "Debe ingresar un nombre de usuario";
            }
            if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                Mensaje = "Debe ingresar un correo";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                string claveAcceso = N_Recursos.GenerarClave(); // Clave generada
                string asunto = "Cuenta creada exitosamente";
                string mensajeEmail = $"<h3>Su cuenta fue creada correctamente</h3><br><p>Su contraseña para acceder al sistema es: {claveAcceso}</p>";

                bool respuesta = N_Recursos.EnviarEmail(obj.correo, asunto, mensajeEmail);
                if (respuesta)
                {
                    obj.clave = claveAcceso;
                    return objdatos.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se ha podido enviar el correo 📧";
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.documento) || string.IsNullOrWhiteSpace(obj.documento))
            {
                Mensaje = "Debe ingresar el documento de identidad";
            }
            if (string.IsNullOrEmpty(obj.nombreusuario) || string.IsNullOrWhiteSpace(obj.nombreusuario))
            {
                Mensaje = "Debe ingresar un nombre de usuario";
            }
            if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                Mensaje = "Debe ingresar un correo";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objdatos.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool GuardarimagenUsuario(Usuarios obj, out string Mensaje)
        {
            return objdatos.GuardarimagenUsuario(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objdatos.Eliminar(id, out Mensaje);
        }

        public bool CambiarClave(int idusuarioweb, string nuevaclave, out string Mensaje)
        {
            return objdatos.Cambiarclave(idusuarioweb, nuevaclave, out Mensaje);
        }

        public bool RestablecerClave(int idusuarioweb, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaclave = N_Recursos.GenerarClave();
            bool resultado = objdatos.RestablecerClave(idusuarioweb, nuevaclave, out Mensaje);

            if (resultado)
            {
                string asunto = "VALENT FRANCE - Restablecer contraseña";
                string mensajeEmail = "<h3>Su contraseña fue restablecida Correctamente</h3><br><p>Para Acceder al sistema Debe utilizar la siguiente contraseña 🔑: ! contraseña ¡ </p>";
                mensajeEmail = mensajeEmail.Replace("! contraseña ¡", nuevaclave);
                bool respuesta = N_Recursos.EnviarEmail(correo, asunto, mensajeEmail);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se ha enviado el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se ha podido restablecer la contraseña :( ";
                return false;
            }
        }

    }
}
