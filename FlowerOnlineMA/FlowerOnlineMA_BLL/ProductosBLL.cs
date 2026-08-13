using FlowerOnlineMA_DAL;
using FlowerOnlineMA_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerOnlineMA_BLL
{
    public class ProductosBLL
    {
        private readonly ProductosDAL productoDal = new ProductosDAL();

        public List<Producto> Listar(string nombre = "")
        {
            nombre = nombre?.Trim() ?? "";

            return productoDal.Listar(nombre);
        }

        public Producto ObtenerPorId(int IdProducto)
        {
            if (IdProducto <= 0) return null;
            return productoDal.ObtenerPorId(IdProducto);
        }

        public bool Insertar(Producto producto, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                mensaje = "El nombre del producto no puede estar vacío.";
                return false;
            }

            if (producto.IdCategoria <= 0) 
            {
                mensaje = "Debe seleccionar una categoría válida.";
                return false;
            }


            if (producto.Precio <= 0)
            {
                mensaje = "El precio debe ser un valor mayor a cero.";
                return false;
            }

            if (producto.Stock <0)
            {
                mensaje = "El stock no puede ser un número negativo.";
                return false;
            }
            return productoDal.Insertar(producto, out mensaje);
        }

        public bool Editar(Producto producto, out string mensaje)
        {
            mensaje = string.Empty;

            if (producto.IdProducto <= 0)
            {
                mensaje = "Identificador de producto no válido.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(producto.Nombre))
            {
                mensaje = "El nombre del producto no puede estar vacío.";
                return false;
            }

            if (producto.IdCategoria <= 0)
            {
                mensaje = "Debe seleccionar una categoría válida.";
                return false;
            }

            if (producto.Precio <= 0)
            {
                mensaje = "El precio debe ser un valor mayor a cero";
                return false;
            }

            if (producto.Stock < 0)
            {
                mensaje = "El s tock no puede ser un número negativo.";
                return false;
            }

            return productoDal.Editar(producto, out mensaje);

        }

        public bool Eliminar(int idProducto, out string mensaje)
        {
            mensaje = string.Empty;

            if (idProducto <= 0)
            {
                mensaje = "Seleccione un producto válido para eliminar";
                return false;
            }
            return productoDal.Eliminar(idProducto, out mensaje);
        }

    }
}
