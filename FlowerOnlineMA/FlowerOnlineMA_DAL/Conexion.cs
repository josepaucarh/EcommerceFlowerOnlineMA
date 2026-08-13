using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerOnlineMA_DAL
{
    public class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["flowerDB"].ConnectionString;
            return new SqlConnection(cadenaConexion);
        }
    }
}
