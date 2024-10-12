using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_ProductosCategoria
    {
        private D_ProductosCategoria objdatos = new D_ProductosCategoria();
        public List<ProductosCategoria> ProductosDamas()
        {
            return objdatos.ProductosDamas();
        }

        public List<ProductosCategoria> ProductosDamasDescuento()
        {
            return objdatos.ProductosDamasDescuento();
        }

        public List<ProductosCategoria> ProductosDamasNuevos()
        {
            return objdatos.ProductosDamasNuevos();
        }

        /*seccion inicio*/
        public List<ProductosCategoria> ProductosInicioDescuento()
        {
            return objdatos.ProductosDamasDescuento();
        }

        public List<ProductosCategoria> ProductosInicioNuevos()
        {
            return objdatos.ProductosDamasNuevos();
        }

    }
}
