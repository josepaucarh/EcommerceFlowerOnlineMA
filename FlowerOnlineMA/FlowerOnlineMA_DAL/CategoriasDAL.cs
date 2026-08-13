using FlowerOnlineMA_ENTITIES;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerOnlineMA_DAL
{
    public class CategoriasDAL
    {
        public List<Categoria> Listar()
        {
            List<Categoria> listaCategorias = new List<Categoria>();
            using (SqlConnection cn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("SELECT IdCategoria, Nombre, Estado FROM CATEGORIA", cn))
            {
                cmd.CommandType = CommandType.Text;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaCategorias.Add(new Categoria
                        {
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                            Nombre= dr["Nombre"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }
            return listaCategorias;
        }

        public bool Insertar(Categoria categoria, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;


            string query = "INSERT INTO Categoria (Nombre,Estado) VALUES (@Nombre, @Estado)";
            try
            {
                using (SqlConnection cn = Conexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@Estado", categoria.Estado);

                    cn.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
            } catch (Exception ex)
            {
                respuesta = false;
                mensaje = "Error al registrar la categoria: " + ex.Message;
            }
            return respuesta;
        }

        public bool Editar(Categoria categoria, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            string query = "UPDATE Categoria SET Nombre=@Nombre, Estado=@Estado WHERE IdCategoria=@IdCategoria";

            try
            {
                using (SqlConnection cn = Conexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@Estado", categoria.Estado);

                    cn.Open();

                    respuesta = cmd.ExecuteNonQuery() >0;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = "Error al actualizar la categoría: " + ex.Message;
            }
            return respuesta;
        }

        public bool Eliminar(int IdCategoria, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            string query = "DELETE FROM Categoria WHERE IdCategoria=@IdCategoria";
            
            try
            {
                using (SqlConnection cn = Conexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@IdCategoria", IdCategoria);
                    cn.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }

            } catch (SqlException ex)
            {
                respuesta = false;
                if (ex.Number == 547)
                {
                    mensaje = "No se puede eliminar la categoría porque tiene productos asignados.";
                }
                else
                {
                    mensaje = "Error al eliminar la categoría: " + ex.Message;
                }
            }
            return respuesta;
        }

    }
}
