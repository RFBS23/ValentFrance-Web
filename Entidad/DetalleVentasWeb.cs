using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class DetalleVentasWeb
    {
        public int iddetalleventaweb { get; set; }
        public int idventaweb { get; set; }
        public Productos oProductos { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public string idtransaccion { get; set; }
        public List<DetalleVentasWeb> oDetalle_Ventaweb { get; set; }
    }
}
