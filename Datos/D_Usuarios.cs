using System;
using System.Collections.Generic;
using System.Data;
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
                    query.AppendLine("select u.idusuario, u.documento, u.nombres, u.apellidos, u.nombreusuario, u.correo, u.estado, u.reestablecer, r.idrol, r.nombrerol, CONVERT(VARCHAR(10), u.fecharegistro, 120)AS fecharegistro_producto from usuarios u");
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
                                reestablecer = Convert.ToBoolean(dr["reestablecer"]),
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

        public int Registrar(Usuarios obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("spu_registrar_usuario", oconexion);
                    cmd.Parameters.AddWithValue("documento", obj.documento);
                    cmd.Parameters.AddWithValue("nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("nombreusuario", obj.nombreusuario);
                    cmd.Parameters.AddWithValue("correo", obj.correo);
                    cmd.Parameters.AddWithValue("clave", obj.clave);
                    cmd.Parameters.AddWithValue("idrol", obj.oNivelAcceso.idrol);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("idusuarioresultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idautogenerado = Convert.ToInt32(cmd.Parameters["idusuarioresultado"].Value);
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

        public bool Editar(Usuarios obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("spu_editar_usuarioweb", oconexion);
                    cmd.Parameters.AddWithValue("idusuario", obj.idusuario);
                    cmd.Parameters.AddWithValue("documento", obj.documento);
                    cmd.Parameters.AddWithValue("nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("nombreusuario", obj.nombreusuario);
                    cmd.Parameters.AddWithValue("correo", obj.correo);
                    cmd.Parameters.AddWithValue("idrol", obj.oNivelAcceso.idrol);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("respuesta", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["respuesta"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }

        //cambiar contraseña
        public bool Cambiarclave(int idusuario, string nuevaclave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("update usuarios set clave = @nuevaclave, restablecer = 0 where idusuario = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idusuario);
                    cmd.Parameters.AddWithValue("@nuevaclave", nuevaclave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool RestablecerClave(int idusuario, string clave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("update usuarios set clave = @clave, restablecer = 1 where idusuario = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", idusuario);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

    }
}
