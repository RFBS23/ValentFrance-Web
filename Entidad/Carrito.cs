using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Carrito
    {
        public int idcarrito { get; set; }
        public Clientes oClientes { get; set; }
        public Productos oProductos { get; set; }
        public int cantidad { get; set; }

    }
}
