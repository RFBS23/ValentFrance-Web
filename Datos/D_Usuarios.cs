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
                    query.AppendLine("select idusuarioweb, rutaimagen, nombreimagen, documento, nombres, apellidos, nombreusuario, correo, clave, reestablecer, estado, CONVERT(VARCHAR(10), fecharegistro, 120)AS fecharegistro_producto from usuariosweb");
                    query.AppendLine("go");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuarios()
                            {
                                idusuarioweb = Convert.ToInt32(dr["idusuarioweb"]),
                                rutaimagen = dr["rutaimagen"].ToString(),
                                nombreimagen = dr["nombreimagen"].ToString(),
                                documento = dr["documento"].ToString(),
                                nombres = dr["nombres"].ToString(),
                                apellidos = dr["apellidos"].ToString(),
                                nombreusuario = dr["nombreusuario"].ToString(),
                                correo = dr["correo"].ToString(),
                                reestablecer = Convert.ToBoolean(dr["reestablecer"]),
                                estado = Convert.ToBoolean(dr["estado"]),
                                fecharegistro = dr["fecharegistro_producto"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
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
                    SqlCommand cmd = new SqlCommand("spu_registrar_usuarioweb", oconexion);
                    cmd.Parameters.AddWithValue("documento", obj.documento);
                    cmd.Parameters.AddWithValue("nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("nombreusuario", obj.nombreusuario);
                    cmd.Parameters.AddWithValue("correo", obj.correo);
                    cmd.Parameters.AddWithValue("clave", obj.clave);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
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

        public bool Editar(Usuarios obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("spu_editarusuario_web", oconexion);
                    cmd.Parameters.AddWithValue("idusuarioweb", obj.idusuarioweb);
                    cmd.Parameters.AddWithValue("documento", obj.documento);
                    cmd.Parameters.AddWithValue("nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("nombreusuario", obj.nombreusuario);
                    cmd.Parameters.AddWithValue("correo", obj.correo);
                    cmd.Parameters.AddWithValue("estado", obj.estado);

                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
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

        public bool GuardarimagenUsuario(Usuarios obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "update usuariosweb set rutaimagen = @rutaimagen, nombreimagen = @nombreimagen where idusuarioweb = @idusuarioweb";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@rutaimagen", obj.rutaimagen);
                    cmd.Parameters.AddWithValue("@nombreimagen", obj.nombreimagen);
                    cmd.Parameters.AddWithValue("@idusuarioweb", obj.idusuarioweb);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo actualiza la imagen 🖼️";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
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
                    SqlCommand cmd = new SqlCommand("update usuariosweb set clave = @nuevaclave, restablecer = 0 where idusuarioweb = @id", oconexion);
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
                    SqlCommand cmd = new SqlCommand("update usuariosweb set clave = @clave, restablecer = 1 where idusuarioweb = @id", oconexion);
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

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from usuariosweb where idusuarioweb = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            } catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
