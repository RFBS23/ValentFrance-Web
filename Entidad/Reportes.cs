using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Reportes
    {
        public string idtransaccion { get; set; }        
        public string NombresApellidos { get; set; }
        public string nombre { get; set; }
        public decimal precioventa {  get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public string FechaVenta {  get; set; }
    }
}
