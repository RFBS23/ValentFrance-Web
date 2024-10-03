using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Categoria
    {
        private D_Categoria objdatos = new D_Categoria();
        public List<Categoria> Listar()
        {
            return objdatos.Listar();
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if(string.IsNullOrEmpty(obj.nombrecategoria) || string.IsNullOrWhiteSpace(obj.nombrecategoria))
            {
                Mensaje = "Desbes ingresar el nombre de la categoria";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objdatos.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombrecategoria) || string.IsNullOrWhiteSpace(obj.nombrecategoria))
            {
                Mensaje = "Debes colocar una categoria";
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

        public bool Eliminar(int id, out string Mensaje)
        {
            return objdatos.Eliminar(id, out Mensaje);
        }

        public List<Categoria> FiltrosCategorias()
        {
            return objdatos.FiltrosCategorias();
        }

    }
}
