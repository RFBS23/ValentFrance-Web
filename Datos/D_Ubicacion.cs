using Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Ubicacion
    {
        public List<departamento> ObtenerDepartamento()
        {
            List<departamento> lista = new List<departamento>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "SELECT * FROM departamento";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add( new departamento()
                                {
                                    iddepartamento = dr["iddepartamento"].ToString(),
                                    descripcion = dr["descripcion"].ToString()
                                });
                        }
                    }

                }
            } catch
            {
                lista = new List<departamento>();
            }
            return lista;
        }

        public List<provincia> ObtenerProvincia(string iddepartamento)
        {
            List<provincia> lista = new List<provincia>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "select * from provincia WHERE iddepartamento = @iddepartamento";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add( new provincia()
                            {
                                idprovincia = dr["idprovincia"].ToString(),
                                descripcion = dr["descripcion"].ToString()
                            });
                        }
                    }

                }
            }
            catch
            {
                lista = new List<provincia>();
            }
            return lista;
        }

        public List<distrito> ObtenerDistrito(string iddepartamento, string idprovincia)
        {
            List<distrito> lista = new List<distrito>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "SELECT * from distrito WHERE idprovincia = @idprovincia and iddepartamento = @iddepartamento";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.Parameters.AddWithValue("@iddepartamento", iddepartamento);

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new distrito()
                            {
                                iddistrito = dr["iddistrito"].ToString(),
                                nombredistrito = dr["nombredistrito"].ToString()
                            });
                        }
                    }

                }
            }
            catch
            {
                lista = new List<distrito>();
            }
            return lista;
        }

    }
}
