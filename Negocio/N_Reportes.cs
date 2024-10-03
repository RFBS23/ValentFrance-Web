using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Reportes
    {
        private D_Reporte objCapaDato = new D_Reporte();

        public Dashboard verTotales()
        {
            return objCapaDato.VerTotales();
        }

        public List<Reportes> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            return objCapaDato.Ventas(fechainicio, fechafin, idtransaccion);
        }
    }
}
