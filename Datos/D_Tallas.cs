using Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Tallas
    {
        public List<Tallas> Listar()
        {
            List<Tallas> lista = new List<Tallas>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select idtallaropa, c.idcategoria, c.nombrecategoria, nombretalla, tr.estado from tallasropa tr");
                    query.AppendLine("inner join categorias c on c.idcategoria = tr.idcategoria");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Tallas()
                            {
                                idtallaropa = Convert.ToInt32(dr["idtallaropa"]),
                                nombretalla = dr["nombretalla"].ToString(),
                                oCategoria = new Categoria() { idcategoria = Convert.ToInt32(dr["idcategoria"]), nombrecategoria = dr["nombrecategoria"].ToString() }
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Tallas>();
                }
            }
            return lista;
        }

        public int Registrar(Tallas obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("spu_registrar_tallasropa", oconexion);
                    cmd.Parameters.AddWithValue("nombretalla", obj.nombretalla);
                    cmd.Parameters.AddWithValue("idcategoria", obj.oCategoria.idcategoria);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idautogenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

        public bool Editar(Tallas obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("spu_editar_tallasropa", oconexion);
                    cmd.Parameters.AddWithValue("idcategoria", obj.oCategoria.idcategoria);
                    cmd.Parameters.AddWithValue("idtallaropa", obj.idtallaropa);
                    cmd.Parameters.AddWithValue("nombretalla", obj.nombretalla);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("spu_eliminar_tallasropa", oconexion);
                    cmd.Parameters.AddWithValue("idtallaropa", id);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public List<Tallas> FiltrosTallasCategorias(int idcategoria)
        {
            List<Tallas> lista = new List<Tallas>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select distinct t.idtallaropa, t.nombretalla from productos p");
                    sb.AppendLine("inner join categorias c on c.idcategoria = p.idcategoria");
                    sb.AppendLine("inner join tallasropa t on t.idtallaropa = p.idtallaropa and t.estado = 1");
                    sb.AppendLine("where c.idcategoria = iif(@idcategoria = 0, c.idcategoria, @idcategoria)");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idcategoria", idcategoria);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Tallas()
                            {
                                idtallaropa = Convert.ToInt32(dr["idtallaropa"]),
                                nombretalla = dr["nombretalla"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Tallas>();
            }
            return lista;
        }
    }
}
