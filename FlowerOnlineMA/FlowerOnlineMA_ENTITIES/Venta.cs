using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerOnlineMA_ENTITIES
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdUsuario { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaVenta { get; set; }

        public List<DetalleVenta> DetalleVenta { get; set; }

        public string TipoEntrega { get; set; } 
        public decimal CostoEnvio { get; set; }
        public string DireccionEnvio { get; set; }
        public string MensajeDedicatoria { get; set; }
    }
}
