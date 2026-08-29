using FlowerOnlineMA_ENTITIES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerOnlineMA_DAL
{
    public class FavoritosDAL
    {
        public List<Producto> ListarPorUsuario(int idUsuario)
        {
            List<Producto> lista = new List<Producto>();
            string query = @"SELECT p.*, c.Nombre as NombreCategoria 
                             FROM Favoritos f 
                             INNER JOIN Productos p ON f.IdProducto = p.IdProducto 
                             INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria
                             WHERE f.IdUsuario = @IdUsuario";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Producto
                        {
                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
                            Nombre = dr["Nombre"].ToString(),
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            RutaImagen = dr["RutaImagen"].ToString(),
                            TipoLuz = dr["TipoLuz"] != DBNull.Value ? dr["TipoLuz"].ToString() : null
                        });
                    }
                }
            }
            return lista;
        }
    }
}
