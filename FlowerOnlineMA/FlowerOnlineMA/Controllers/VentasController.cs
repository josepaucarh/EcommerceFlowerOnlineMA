using FlowerOnlineMA_BLL;
using FlowerOnlineMA_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FlowerOnlineMA.Controllers
{
    [RoutePrefix("api/ventas")]
    public class VentasController : ApiController
    {
        private readonly VentasBLL ventasBll = new VentasBLL();

        [HttpPost]
        [Route("registrar")]
        public IHttpActionResult Registrar([FromBody] Venta venta)
        {
            try
            {
                string mensaje = string.Empty;
                bool resultado = ventasBll.RegistrarVenta(venta, out mensaje);

                return Ok(new { resultado = resultado, mensaje = mensaje });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}