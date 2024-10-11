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
    public class D_ProductosCategoria
    {
        public List<ProductosCategoria> ProductosDamas()
        {
            List<ProductosCategoria> lista = new List<ProductosCategoria>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                try
                {
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select idproducto, rutaimagen, rutaimagendos, rutaimagen3, rutaimagen4, nombreimagen, nombreimagendos, nombreimagen3, nombreimagen4, codigo, nombre, descripcion, c.idcategoria, c.nombrecategoria, tr.idtallaropa, tr.nombretalla, m.idmarca, m.nombremarca, stock, colores, numcaja, precioventa, temporada, descuento, total, ubicacion, CONVERT(VARCHAR(10), p.fecharegistro, 120) AS fecharegistro_producto FROM productos p");
                    query.AppendLine("inner join categorias c on c.idcategoria = p.idcategoria");
                    query.AppendLine("inner join tallasropa tr on tr.idtallaropa = p.idtallaropa");
                    query.AppendLine("inner join marca m on m.idmarca = p.idmarca");
                    query.AppendLine("where c.nombrecategoria = 'Damas'");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ProductosCategoria()
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
                    lista = new List<ProductosCategoria>();
                }
            }
            return lista;
        }

        public List<ProductosCategoria> ProductosDamasDescuento()
        {
            List<ProductosCategoria> lista = new List<ProductosCategoria>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                try
                {
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("SELECT idproducto, rutaimagen, rutaimagendos, rutaimagen3, rutaimagen4, nombreimagen, nombreimagendos, nombreimagen3, nombreimagen4, codigo, nombre, descripcion, c.idcategoria, c.nombrecategoria, tr.idtallaropa, tr.nombretalla, m.idmarca, m.nombremarca, stock, colores, numcaja, precioventa, temporada, descuento, total, ubicacion, CONVERT(VARCHAR(10), p.fecharegistro, 120) AS fecharegistro_producto FROM productos p");
                    query.AppendLine("inner join categorias c on c.idcategoria = p.idcategoria");
                    query.AppendLine("inner join tallasropa tr on tr.idtallaropa = p.idtallaropa");
                    query.AppendLine("inner join marca m on m.idmarca = p.idmarca");
                    query.AppendLine("WHERE c.nombrecategoria = 'damas' and p.descuento > 0");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ProductosCategoria()
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
                    lista = new List<ProductosCategoria>();
                }
            }
            return lista;
        }

        public List<ProductosCategoria> ProductosDamasNuevos()
        {
            List<ProductosCategoria> lista = new List<ProductosCategoria>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT idproducto, rutaimagen, rutaimagendos, rutaimagen3, rutaimagen4, nombreimagen, nombreimagendos, nombreimagen3, nombreimagen4, codigo, nombre, descripcion, c.idcategoria, c.nombrecategoria, tr.idtallaropa, tr.nombretalla, m.idmarca, m.nombremarca, stock, colores, numcaja, precioventa, temporada, descuento, total, ubicacion, CONVERT(VARCHAR(10), p.fecharegistro, 120) AS fecharegistro_producto FROM productos p");
                    query.AppendLine("inner join categorias c on c.idcategoria = p.idcategoria");
                    query.AppendLine("inner join tallasropa tr on tr.idtallaropa = p.idtallaropa");
                    query.AppendLine("inner join marca m on m.idmarca = p.idmarca");
                    query.AppendLine("WHERE c.nombrecategoria = 'damas' and p.fecharegistro >= CONVERT(DATE, GETDATE()) AND p.fecharegistro < DATEADD(MONTH, 2, CONVERT(DATE, GETDATE()))");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ProductosCategoria()
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
                    lista = new List<ProductosCategoria>();
                }
            }
            return lista;
        }

    }
}
