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
            if (obj.oNivelAcceso == null || string.IsNullOrEmpty(obj.oNivelAcceso.nombrerol) || string.IsNullOrWhiteSpace(obj.oNivelAcceso.nombrerol))
            {
                Mensaje = "Debe seleccionar un nivel de acceso";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                string claveAcceso = N_Recursos.GenerarClave(); //clave generada
                string asunto = "Cuenta creada exitosamente";
                string mensajeEmail = "<h3>Su cuenta fue Creada Correctamente</h3><br><p>Su Contraseña 🔑 para Acceder al sistema es: ! contraseña ¡ </p>";
                mensajeEmail = mensajeEmail.Replace("! contraseña ¡", claveAcceso);
                bool respuesta = N_Recursos.EnviarEmail(obj.correo, asunto, mensajeEmail);
                if (respuesta)
                {
                    obj.clave = N_Recursos.ConvertitSha256(claveAcceso);
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
            if (obj.oNivelAcceso == null || string.IsNullOrEmpty(obj.oNivelAcceso.nombrerol) || string.IsNullOrWhiteSpace(obj.oNivelAcceso.nombrerol))
            {
                Mensaje = "Debe seleccionar un nivel de acceso";
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

        //cambiar y restablecer contraseña
        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {
            return objdatos.Cambiarclave(idusuario, nuevaclave, out Mensaje);
        }

        public bool RestablecerClave(int idusuario, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaclave = N_Recursos.GenerarClave(); //clave generada
            bool resultado = objdatos.RestablecerClave(idusuario, N_Recursos.ConvertitSha256(nuevaclave), out Mensaje);

            if (resultado)
            {
                string asunto = "FarmaDev - Restablecer contraseña";
                string mensajeEmail = "<h3>Su contraseña fue restablecida Correctamente</h3><br><p>Para Acceder al sistema Debe utilizar la siguiente contraseña 🔑: ! contraseña ¡ </p>";
                mensajeEmail = mensajeEmail.Replace("! contraseña ¡", nuevaclave);
                bool respuesta = N_Recursos.EnviarEmail(correo, asunto, mensajeEmail);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se puedo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se puedo restablecer la contraseña :( ";
                return false;
            }
        }

    }
}
