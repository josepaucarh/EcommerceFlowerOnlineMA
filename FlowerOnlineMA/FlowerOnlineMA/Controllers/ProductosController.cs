using FlowerOnlineMA_BLL;
using FlowerOnlineMA_ENTITIES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerOnlineMA.Controllers
{
    public class ProductosController : Controller
    {
        private readonly CategoriasBLL categoriasBll = new CategoriasBLL();
        private readonly ProductosBLL productosBll = new ProductosBLL();

        //Devuelve los productos en Json para llenar DataTables
        public JsonResult ListarProductos()
        {
            var lista = productosBll.Listar();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //Devuelve las categorias para llenar el Modal
        public JsonResult ListarCategorias()
        {
            var lista = categoriasBll.Listar();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //Guarda o Edita manejando la subida de imagen
        [HttpPost]
        public JsonResult GuardarProducto(Producto producto, HttpPostedFileBase imagenFile)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            try
            {
                //Procesar la imagen si el usuario subió un nuevo archivo
                if (imagenFile != null && imagenFile.ContentLength > 0)
                {
                    string extension = Path.GetExtension(imagenFile.FileName);
                    string nombreImagen = Guid.NewGuid().ToString() + extension;
                    string carpetaDestino = Server.MapPath("~/Uploads/Productos/");
                    
                    //Crear la carpeta si no existe
                    if (!Directory.Exists(carpetaDestino))
                    {
                        Directory.CreateDirectory(carpetaDestino);
                    }

                    string rutaCompleta = Path.Combine(carpetaDestino, nombreImagen);
                    imagenFile.SaveAs(rutaCompleta);

                    producto.RutaImagen = "/Uploads/Productos/" + nombreImagen;
                }

                //Si es un producto nuevo
                if (producto.IdProducto == 0)
                {
                    resultado = productosBll.Insertar(producto, out mensaje);
                }
                else {
                    resultado = productosBll.Editar(producto, out mensaje);
                }

            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error en el servidor: " + ex.Message;
            }

            return Json(new { resultado= resultado, mensaje=mensaje});
        }

        [HttpPost]
        public JsonResult EliminarProducto(int IdProducto)
        {
            string mensaje = string.Empty;
            bool resultado = productosBll.Eliminar(IdProducto, out mensaje);

            return Json(new { resultado = resultado, mensaje=mensaje});
        }

    }
}