using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_cliente
    {
        private D_cliente objdatos = new D_cliente();
        public List<Clientes> Listar()
        {
            return objdatos.Listar();
        }

        public int Registrar(Clientes obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.documento) || string.IsNullOrWhiteSpace(obj.documento))
            {
                Mensaje = "Debe ingresar el documento de identidad";
            }
            if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                Mensaje = "Debe ingresar un correo";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                obj.clave = obj.clave;
                return objdatos.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool CambiarClave(int idcliente, string nuevaclave, out string Mensaje)
        {
            return objdatos.Cambiarclave(idcliente, nuevaclave, out Mensaje);
        }

        public bool RestablecerClave(int idcliente, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaclave = N_Recursos.GenerarClave();
            bool resultado = objdatos.RestablecerClave(idcliente, nuevaclave, out Mensaje);
            if (resultado)
            {
                string asunto = "VALENT FRANCE - Restablecer contraseña";
                string mensajeEmail = "<h3>Su contraseña fue restablecida Correctamente</h3><br><p>Para acceder a nuestra web debe utilizar la siguiente contraseña 🔑: ! contraseña ¡ </p>";
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
