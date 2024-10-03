using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Productos
    {
        public int idproducto { get; set; }
        public string rutaimagen { get; set; }
        public string rutaimagendos { get; set; }
        public string rutaimagen3 { get; set; }
        public string rutaimagen4 { get; set; }
        public string nombreimagen { get; set; }
        public string nombreimagendos { get; set; }
        public string nombreimagen3 { get; set; }
        public string nombreimagen4 { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Marca oMarca { get; set; }
        public Categoria oCategorias { get; set; }
        public Tallas oTallasropa { get; set; }
        public int stock { get; set; }
        public string colores { get; set; }
        public decimal precioventa { get; set; }
        public decimal preciocompra { get; set; }
        public string numcaja { get; set; }
        public string temporada { get; set; }
        public int descuento { get; set; }
        public decimal total { get; set; }
        public string ubicacion { get; set; }
        public string fecharegistro { get; set; }
        public string precioTexto { get; set; }
        public string base64 { get; set; }
        public string base2 { get; set; }
        public string base3 { get; set; }
        public string base4 { get; set; }
        public string Extension { get; set; }
        public string Extension2 { get; set; }
        public string Extension3 { get; set; }
        public string Extension4 { get; set; }
    }
}
