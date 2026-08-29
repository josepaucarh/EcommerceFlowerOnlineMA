using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowerOnlineMA_ENTITIES;
using System.Data;

namespace FlowerOnlineMA_DAL
{
    public class ProductosDAL
    {
        //lista de productos por nombre
        public List<Producto> Listar(string nombre="") 
        {
            List<Producto> listaProductos = new List<Producto>();
            using (SqlConnection cn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ListarProductos", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre?? "");

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaProductos.Add(new Producto {
                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            Stock= Convert.ToInt32(dr["Stock"]),
                            RutaImagen = dr["RutaImagen"].ToString(),
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                            NombreCategoria = dr["NombreCategoria"].ToString(),
                            TipoLuz = dr["TipoLuz"] != DBNull.Value ? dr["TipoLuz"].ToString() : null,
                            FrecuenciaRiego = dr["FrecuenciaRiego"] != DBNull.Value ? dr["FrecuenciaRiego"].ToString() : null,
                            NivelCuidado = dr["NivelCuidado"] != DBNull.Value ? dr["NivelCuidado"].ToString() : null
                        });
                    }
                }
            }
            return listaProductos;
        }

        //lista de productos por ID
        public Producto ObtenerPorId(int IdProducto)
        {
            Producto producto = null;

            using (SqlConnection cn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ObtenerProductoID", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProducto", IdProducto);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        producto = new Producto
                        {
                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            Stock = Convert.ToInt32(dr["Stock"]),
                            RutaImagen = dr["RutaImagen"].ToString(),
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                            NombreCategoria = dr["NombreCategoria"].ToString(),
                            TipoLuz = dr["TipoLuz"] != DBNull.Value ? dr["TipoLuz"].ToString() : null,
                            FrecuenciaRiego = dr["FrecuenciaRiego"] != DBNull.Value ? dr["FrecuenciaRiego"].ToString() : null,
                            NivelCuidado = dr["NivelCuidado"] != DBNull.Value ? dr["NivelCuidado"].ToString() : null
                        };
                    }
                }
            }
            return producto;
        }

        //Insertar Productos
        public bool Insertar(Producto producto, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection cn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_InsertarProducto", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                cmd.Parameters.AddWithValue("@RutaImagen", producto.RutaImagen?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);
                cmd.Parameters.AddWithValue("@TipoLuz", producto.TipoLuz ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FrecuenciaRiego", producto.FrecuenciaRiego ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NivelCuidado", producto.NivelCuidado ?? (object)DBNull.Value);

                cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                cn.Open();
                cmd.ExecuteNonQuery();

                respuesta = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
            }
            return respuesta;
        }

        //Editar Producto
        public bool Editar(Producto producto, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection cn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_EditarProducto", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                cmd.Parameters.AddWithValue("@RutaImagen", producto.RutaImagen ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);
                cmd.Parameters.AddWithValue("@TipoLuz", producto.TipoLuz ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FrecuenciaRiego", producto.FrecuenciaRiego ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NivelCuidado", producto.NivelCuidado ?? (object)DBNull.Value);

                cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                cn.Open();
                cmd.ExecuteNonQuery();

                respuesta = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
            }
            return respuesta;
        }

        //Eliminar Producto
        public bool Eliminar(int idProducto, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection cn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_EliminarProducto", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                cn.Open();
                cmd.ExecuteNonQuery();

                respuesta = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
            }
            return respuesta;
        }

    }
}
