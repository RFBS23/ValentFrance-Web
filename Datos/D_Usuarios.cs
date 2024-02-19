using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;

namespace Datos
{
    public class D_Usuarios
    {
        public List<Usuarios> Listar()
        {
            List<Usuarios> lista = new List<Usuarios>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.idusuario, u.documento, u.nombres, u.apellidos, u.nombreusuario, u.correo, u.clave, u.estado, r.idrol, r.nombrerol, CONVERT(VARCHAR(10), u.fecharegistro, 120)AS fecharegistro_producto from usuarios u");
                    query.AppendLine("inner join rol r on r.idrol = u.idrol");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuarios()
                            {
                                idusuario = Convert.ToInt32(dr["idusuario"]),
                                documento = dr["documento"].ToString(),
                                nombres = dr["nombres"].ToString(),
                                apellidos = dr["apellidos"].ToString(),
                                nombreusuario = dr["nombreusuario"].ToString(),
                                correo = dr["correo"].ToString(),
                                clave = dr["clave"].ToString(),
                                oNivelAcceso = new NivelAcceso() { idrol = Convert.ToInt32(dr["idrol"]), nombrerol = dr["nombrerol"].ToString() },
                                estado = Convert.ToBoolean(dr["estado"]),
                                fecharegistro = dr["fecharegistro_producto"].ToString()
                            });
                        }
                    }
                } catch (Exception ex)
                {
                    lista = new List<Usuarios>();
                }
            }
            return lista;
        }

    }
}
