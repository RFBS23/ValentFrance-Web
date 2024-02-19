using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Usuarios
    {
        private D_Usuarios objdatos = new D_Usuarios();
        public List<Usuarios> Listar()
        {
            return objdatos.Listar();
        }

    }
}
