using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerOnlineMA_ENTITIES
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string RutaImagen { get; set; }
        public int IdCategoria { get; set; }
        public bool Estado { get; set; }
        public string NombreCategoria { get; set; }
        public string TipoLuz { get; set; }
        public string FrecuenciaRiego { get; set; }
        public string NivelCuidado { get; set; }
    }
}
