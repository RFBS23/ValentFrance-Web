using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Marcas
    {
        private D_Marcas objdatos = new D_Marcas();
        public List<Marca> Listar()
        {
            return objdatos.Listar();
        }

        public int Registrar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombremarca) || string.IsNullOrWhiteSpace(obj.nombremarca))
            {
                Mensaje = "Debes colocar una categoria";
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

        public bool Editar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombremarca) || string.IsNullOrWhiteSpace(obj.nombremarca))
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

    }
}
