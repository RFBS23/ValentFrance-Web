using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Carrito
    {
        private D_Carritos objDatos = new D_Carritos();

        public bool AgregarCarrito(int idcliente, int idproducto)
        {
            return objDatos.AgregarCarrito(idcliente, idproducto);
        }

        public bool OperacionCarrito(int idcliente, int idproducto, bool sumar, out string mensaje)
        {
            return objDatos.OperacionCarrito(idcliente, idproducto, sumar, out mensaje);
        }

        public int CantidadCarrito(int idcliente)
        {
            return objDatos.CantidadCarrito(idcliente);
        }

        public List<Carrito> ListarProducto(int idcliente)
        {
            return objDatos.ListarProducto(idcliente);
        }

        public bool EliminarCarrito(int idcliente, int idproducto)
        {
            return objDatos.EliminarCarrito(idcliente, idproducto);
        }

    }
}
