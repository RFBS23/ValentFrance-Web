using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Ubicacion
    {
        private D_Ubicacion objDatos = new D_Ubicacion();
        public List<departamento> ObtenerDepartamento()
        {
            return objDatos.ObtenerDepartamento();
        }

        public List<provincia> ObtenerProvincia(string iddepartamento)
        {
            return objDatos.ObtenerProvincia(iddepartamento);
        }

        public List<distrito> ObtenerDistrito(string iddepartamento, string idprovincia)
        {
            return objDatos.ObtenerDistrito(iddepartamento, idprovincia);
        }
    }
}
