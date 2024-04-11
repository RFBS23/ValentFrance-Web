using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Categorias
    {
        private D_Categorias objDatos = new D_Categorias();
        public List<Categorias> Listar()
        {
            return objDatos.Listar();
        }

        public int Registrar(Categorias obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombrecategoria) || string.IsNullOrWhiteSpace(obj.nombrecategoria))
            {
                Mensaje = "Debes colocar una categoria";
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

        public bool Editar(Categorias obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombrecategoria) || string.IsNullOrWhiteSpace(obj.nombrecategoria))
            {
                Mensaje = "Debes colocar una categoria";
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

        public List<Categorias> FiltrosCategorias()
        {
            return objDatos.FiltrosCategorias();
        }

    }
}
