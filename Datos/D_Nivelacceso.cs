using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;

namespace Datos
{
    public class D_Nivelacceso
    {
        public List<NivelAcceso> Listar()
        {
            List<NivelAcceso> lista = new List<NivelAcceso>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select idrol, nombrerol from rol");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new NivelAcceso()
                            {
                                idrol = Convert.ToInt32(dr["idrol"]),
                                nombrerol = dr["nombrerol"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<NivelAcceso>();
                }
            }
            return lista;
        }
    }
}
