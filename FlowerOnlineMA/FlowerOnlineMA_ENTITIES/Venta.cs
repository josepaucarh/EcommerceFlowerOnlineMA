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
        public string MensajeDedicatorio { get; set; }
        public DateTime FechaVenta { get; set; }

        public List<DetalleVenta> DetalleVenta { get; set; }
    }
}
