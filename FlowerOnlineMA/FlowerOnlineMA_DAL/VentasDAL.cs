using FlowerOnlineMA_ENTITIES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerOnlineMA_DAL
{
    public class VentasDAL
    {
        public bool RegistrarVenta(Venta venta, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    string queryVenta = @"INSERT INTO Ventas (IdUsuario, Total, TipoEntrega, CostoEnvio, DireccionEnvio, MensajeDedicatoria) 
                                          VALUES (@IdUsuario, @Total, @TipoEntrega, @CostoEnvio, @DireccionEnvio, @MensajeDedicatoria);
                                          SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdVenta = new SqlCommand(queryVenta, cn, transaction);
                    cmdVenta.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario);
                    cmdVenta.Parameters.AddWithValue("@Total", venta.Total);
                    cmdVenta.Parameters.AddWithValue("@TipoEntrega", venta.TipoEntrega ?? "EnvioDomicilio");
                    cmdVenta.Parameters.AddWithValue("@CostoEnvio", venta.CostoEnvio);
                    cmdVenta.Parameters.AddWithValue("@DireccionEnvio", (object)venta.DireccionEnvio ?? DBNull.Value);
                    cmdVenta.Parameters.AddWithValue("@MensajeDedicatoria", (object)venta.MensajeDedicatoria ?? DBNull.Value);

                    int idVentaGenerado = Convert.ToInt32(cmdVenta.ExecuteScalar());

                    foreach (var detalle in venta.DetalleVenta)
                    {
                        string queryDetalle = "INSERT INTO DetalleVenta (IdVenta, IdProducto, Cantidad, Precio) VALUES (@IdVenta, @IdProducto, @Cantidad, @Precio)";
                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle, cn, transaction);
                        cmdDetalle.Parameters.AddWithValue("@IdVenta", idVentaGenerado);
                        cmdDetalle.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        cmdDetalle.Parameters.AddWithValue("@Precio", detalle.Precio);
                        cmdDetalle.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    respuesta = false;
                    mensaje = "Error al registrar la venta: " + ex.Message;
                }
            }
            return respuesta;
        }
    }
}
