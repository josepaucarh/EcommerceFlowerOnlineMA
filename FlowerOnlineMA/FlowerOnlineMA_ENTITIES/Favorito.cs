using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerOnlineMA_ENTITIES
{
    public class Favorito
    {
        public int IdFavorito { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public DateTime FechaAgregado { get; set; }
        public Producto Producto { get; set; }
    }
}
