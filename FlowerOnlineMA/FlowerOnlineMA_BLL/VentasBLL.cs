using FlowerOnlineMA_DAL;
using FlowerOnlineMA_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerOnlineMA_BLL
{
    public class VentasBLL
    {
        private readonly VentasDAL ventaDal = new VentasDAL();

        public bool RegistrarVenta(Venta venta, out string mensaje)
        {
            mensaje = string.Empty;

            if (venta == null)
            {
                mensaje = "Los datos de la compra no son válidos.";
                return false;
            }

            if (venta.IdUsuario <= 0)
            {
                mensaje = "Debe iniciar sesión para realizar una compra.";
                return false;
            }

            if (venta.DetalleVenta == null || venta.DetalleVenta.Count == 0)
            {
                mensaje = "El carrito de compras está vacío.";
                return false;
            }

            if (venta.TipoEntrega == "EnvioDomicilio" && string.IsNullOrWhiteSpace(venta.DireccionEnvio))
            {
                mensaje = "Debe ingresar una dirección para el envío a domicilio.";
                return false;
            }

            return ventaDal.RegistrarVenta(venta, out mensaje);
        }
    }
}
