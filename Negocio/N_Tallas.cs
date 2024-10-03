using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Tallas
    {
        private D_Tallas objDatos = new D_Tallas();
        public List<Tallas> Listar()
        {
            return objDatos.Listar();
        }

        public int Registrar(Tallas obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombretalla) || string.IsNullOrWhiteSpace(obj.nombretalla))
            {
                Mensaje = "Debes colocar una talla";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objDatos.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Tallas obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombretalla) || string.IsNullOrWhiteSpace(obj.nombretalla))
            {
                Mensaje = "Debes ingresar una talla";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objDatos.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        //eliminar
        public bool Eliminar(int id, out string Mensaje)
        {
            return objDatos.Eliminar(id, out Mensaje);
        }

        public List<Tallas> FiltrosTallasCategorias(int idcategoria)
        {
            return objDatos.FiltrosTallasCategorias(idcategoria);
        }

    }
}
