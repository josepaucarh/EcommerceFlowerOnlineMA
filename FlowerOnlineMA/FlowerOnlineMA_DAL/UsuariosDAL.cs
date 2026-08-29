using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using FlowerOnlineMA_ENTITIES;

namespace FlowerOnlineMA_DAL
{
    public class UsuariosDAL
    {
        public Usuario Login(string correo, string clave)
        {
            Usuario usuario = null;
            string query = "SELECT IdUsuario, Nombre, Apellido, Correo, EsAdmin, Estado, RutaAvatar FROM Usuarios WHERE Correo=@Correo AND Clave=@Clave AND Estado=1";

            using (SqlConnection cn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Clave", clave);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        usuario = new Usuario
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            EsAdmin = Convert.ToBoolean(dr["EsAdmin"]),
                            Estado = Convert.ToBoolean(dr["Estado"]),
                            RutaAvatar = dr["RutaAvatar"] != DBNull.Value ? dr["RutaAvatar"].ToString() : null
                        };
                    }
                }
            }
            return usuario;
        }
    }
}
