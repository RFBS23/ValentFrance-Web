﻿using Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Productos
    {
        public List<Productos> Listar()
        {
            List<Productos> lista = new List<Productos>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                try
                {
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select idproducto, rutaimagen, rutaimagendos, rutaimagen3, rutaimagen4, nombreimagen, nombreimagendos, nombreimagen3, nombreimagen4, codigo, nombre, descripcion, c.idcategoria, c.nombrecategoria, tr.idtallaropa, tr.nombretalla, m.idmarca, m.nombremarca, stock, colores, numcaja, precioventa, temporada, descuento, total, ubicacion, CONVERT(VARCHAR(10), p.fecharegistro, 120)AS fecharegistro_producto from productos p");
                    query.AppendLine("inner join categorias c on c.idcategoria = p.idcategoria");
                    query.AppendLine("inner join tallasropa tr on tr.idtallaropa = p.idtallaropa");
                    query.AppendLine("inner join marca m on m.idmarca = p.idmarca");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Productos()
                            {
                                idproducto = Convert.ToInt32(dr["idproducto"]),
                                rutaimagen = dr["rutaimagen"].ToString(),
                                rutaimagendos = dr["rutaimagendos"].ToString(),
                                rutaimagen3 = dr["rutaimagen3"].ToString(),
                                rutaimagen4 = dr["rutaimagen4"].ToString(),
                                nombreimagen = dr["nombreimagen"].ToString(),
                                nombreimagendos = dr["nombreimagendos"].ToString(),
                                nombreimagen3 = dr["nombreimagen3"].ToString(),
                                nombreimagen4 = dr["nombreimagen4"].ToString(),
                                codigo = dr["codigo"].ToString(),
                                nombre = dr["nombre"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                oCategorias = new Categoria() { idcategoria = Convert.ToInt32(dr["idcategoria"]), nombrecategoria = dr["nombrecategoria"].ToString() },
                                oTallasropa = new Tallas() { idtallaropa = Convert.ToInt32(dr["idtallaropa"]), nombretalla = dr["nombretalla"].ToString() },
                                oMarca = new Marca() { idmarca = Convert.ToInt32(dr["idmarca"]), nombremarca = dr["nombremarca"].ToString() },
                                colores = dr["colores"].ToString(),
                                stock = Convert.ToInt32(dr["stock"]),
                                numcaja = dr["numcaja"].ToString(),
                                precioventa = Convert.ToDecimal(dr["precioventa"]),
                                temporada = dr["temporada"].ToString(),
                                descuento = Convert.ToInt32(dr["descuento"]),
                                total = Convert.ToDecimal(dr["total"]),
                                ubicacion = dr["ubicacion"].ToString(),
                                fecharegistro = dr["fecharegistro_producto"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Productos>();
                }
            }
            return lista;
        }

        public int Registrar(Productos obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("spu_registrar_productoropa", oconexion);
                    cmd.Parameters.AddWithValue("codigo", obj.codigo);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("idcategoria", obj.oCategorias.idcategoria);
                    cmd.Parameters.AddWithValue("idtallaropa", obj.oTallasropa.idtallaropa);
                    cmd.Parameters.AddWithValue("idmarca", obj.oMarca.idmarca);
                    cmd.Parameters.AddWithValue("colores", obj.colores);
                    cmd.Parameters.AddWithValue("stock", obj.stock);
                    cmd.Parameters.AddWithValue("numcaja", obj.numcaja);
                    cmd.Parameters.AddWithValue("precioventa", obj.precioventa);
                    cmd.Parameters.AddWithValue("temporada", obj.temporada);
                    cmd.Parameters.AddWithValue("descuento", obj.descuento);
                    cmd.Parameters.AddWithValue("ubicacion", obj.ubicacion);
                    cmd.Parameters.AddWithValue("total", obj.total);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
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

        public bool Editar(Productos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("spu_editar_productoropa", oconexion);
                    cmd.Parameters.AddWithValue("idproducto", obj.idproducto);
                    cmd.Parameters.AddWithValue("codigo", obj.codigo);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("idcategoria", obj.oCategorias.idcategoria);
                    cmd.Parameters.AddWithValue("idtallaropa", obj.oTallasropa.idtallaropa);
                    cmd.Parameters.AddWithValue("idmarca", obj.oMarca.idmarca);
                    cmd.Parameters.AddWithValue("colores", obj.colores);
                    cmd.Parameters.AddWithValue("stock", obj.stock);
                    cmd.Parameters.AddWithValue("numcaja", obj.numcaja);
                    cmd.Parameters.AddWithValue("precioventa", obj.precioventa);
                    cmd.Parameters.AddWithValue("temporada", obj.temporada);
                    cmd.Parameters.AddWithValue("descuento", obj.descuento);
                    cmd.Parameters.AddWithValue("total", obj.total);
                    cmd.Parameters.AddWithValue("ubicacion", obj.ubicacion);

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

        public bool GuardarImg(Productos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "update productos set rutaimagen = @rutaimagen, nombreimagen = @nombreimagen where idproducto = @idproducto";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@rutaimagen", obj.rutaimagen);
                    cmd.Parameters.AddWithValue("@nombreimagen", obj.nombreimagen);
                    cmd.Parameters.AddWithValue("@idproducto", obj.idproducto);
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

        public bool GuardarImg2(Productos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "update productos set rutaimagendos = @rutaimagendos, nombreimagendos = @nombreimagendos where idproducto = @idproducto";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@rutaimagendos", obj.rutaimagendos);
                    cmd.Parameters.AddWithValue("@nombreimagendos", obj.nombreimagendos);
                    cmd.Parameters.AddWithValue("@idproducto", obj.idproducto);
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

        public bool GuardarImg3(Productos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "UPDATE productos SET rutaimagen3 = @rutaimagen3, nombreimagen3 = @nombreimagen3 WHERE idproducto = @idproducto";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@rutaimagen3", obj.rutaimagen3);
                    cmd.Parameters.AddWithValue("@nombreimagen3", obj.nombreimagen3);
                    cmd.Parameters.AddWithValue("@idproducto", obj.idproducto);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = $"No se actualizó la imagen 3. Filas afectadas: {filasAfectadas}";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = "Error al guardar la imagen 3: " + ex.Message;
            }
            return resultado;
        }

        public bool GuardarImg4(Productos obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "UPDATE productos SET rutaimagen4 = @rutaimagen4, nombreimagen4 = @nombreimagen4 WHERE idproducto = @idproducto";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@rutaimagen4", obj.rutaimagen4);
                    cmd.Parameters.AddWithValue("@nombreimagen4", obj.nombreimagen4);
                    cmd.Parameters.AddWithValue("@idproducto", obj.idproducto);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = $"No se actualizó la imagen 4. Filas afectadas: {filasAfectadas}";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = "Error al guardar la imagen 4: " + ex.Message;
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
                    SqlCommand cmd = new SqlCommand("spu_eliminar_productoropa", oconexion);
                    cmd.Parameters.AddWithValue("idproducto", id);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["respuesta"].Value);
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

    }
}
