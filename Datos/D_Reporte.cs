using Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Reporte
    {
        public Dashboard VerTotales()
        {
            Dashboard objeto = new Dashboard();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("spu_reportetotales", oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new Dashboard()
                            {
                                totalcliente = Convert.ToInt32(dr["totalcliente"]),
                                totalproducto = Convert.ToInt32(dr["totalproducto"]),
                                totalventa = Convert.ToInt32(dr["totalventa"])
                            };
                        }
                    }
                }
            }
            catch
            {
                objeto = new Dashboard();
            }
            return objeto;
        }

        /*entidad reportes*/
        public List<Reportes> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reportes> lista = new List<Reportes>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("spu_reporte_ventasweb", oconexion);
                    cmd.Parameters.AddWithValue("idtransaccion", idtransaccion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Reportes()
                            {
                                idtransaccion = dr["idtransaccion"].ToString(),
                                NombresApellidos = dr["NombresApellidos"].ToString(),
                                nombre = dr["nombre"].ToString(),
                                precioventa = Convert.ToDecimal(dr["precioventa"], new CultureInfo("es-PE")),
                                cantidad = Convert.ToInt32(dr["cantidad"].ToString()),
                                total = Convert.ToDecimal(dr["total"], new CultureInfo("es-PE")),
                                FechaVenta = dr["FechaVenta"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Reportes>();
            }
            return lista;
        }
    }
}
