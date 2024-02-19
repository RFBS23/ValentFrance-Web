using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Nivelacceso
    {
        private D_Nivelacceso objd_nivelesacceso = new D_Nivelacceso();
        public List<NivelAcceso> Listar()
        {
            return objd_nivelesacceso.Listar();
        }
    }
}
