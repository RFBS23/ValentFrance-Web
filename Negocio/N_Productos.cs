using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Productos
    {
        private D_Productos objdatos = new D_Productos();
        public List<Productos> Listar()
        {
            return objdatos.Listar();
        }

        public int Registrar(Productos obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.codigo) || string.IsNullOrWhiteSpace(obj.codigo))
            {
                Mensaje = "Debes colocar un codigo";
            }
            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                Mensaje = "Debes colocar un nombre";
            }
            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                Mensaje = "Debes colocar una descripcion";
            }
            if (string.IsNullOrEmpty(obj.colores) || string.IsNullOrWhiteSpace(obj.colores))
            {
                Mensaje = "Debes colocar un color";
            }
            if (string.IsNullOrEmpty(obj.numcaja) || string.IsNullOrWhiteSpace(obj.numcaja))
            {
                Mensaje = "Debes colocar un numero de caja";
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

        public bool Editar(Productos obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.codigo) || string.IsNullOrWhiteSpace(obj.codigo))
            {
                Mensaje = "Debes colocar un codigo";
            }
            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                Mensaje = "Debes colocar un nombre";
            }
            if (string.IsNullOrEmpty(obj.descripcion) || string.IsNullOrWhiteSpace(obj.descripcion))
            {
                Mensaje = "Debes colocar una descripcion";
            }
            if (string.IsNullOrEmpty(obj.colores) || string.IsNullOrWhiteSpace(obj.colores))
            {
                Mensaje = "Debes colocar un color";
            }
            if (string.IsNullOrEmpty(obj.numcaja) || string.IsNullOrWhiteSpace(obj.numcaja))
            {
                Mensaje = "Debes colocar un numero de caja";
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

        public bool GuardarImg(Productos obj, out string Mensaje)
        {
            return objdatos.GuardarImg(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objdatos.Eliminar(id, out Mensaje);
        }
    }
}
