using FlowerOnlineMA_BLL;
using FlowerOnlineMA_ENTITIES;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FlowerOnlineMA.Controllers
{
    public class ProductosController : Controller
    {
        private readonly CategoriasBLL categoriasBll = new CategoriasBLL();
        private readonly ProductosBLL productosBll = new ProductosBLL();

        // Helper para responder siempre en camelCase
        private ContentResult JsonCamelCase(object data)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string json = JsonConvert.SerializeObject(data, settings);
            return Content(json, "application/json", Encoding.UTF8);
        }

        // Devuelve los productos en Json para llenar DataTables / Angular
        public ActionResult ListarProductos()
        {
            var lista = productosBll.Listar();
            return JsonCamelCase(lista);
        }

        // GET: ListarActivos
        [HttpGet]
        public ActionResult ListarActivos()
        {
            var listaActivos = productosBll.Listar();
            return JsonCamelCase(listaActivos);
        }

        // Devuelve las categorias para llenar el Modal
        public ActionResult ListarCategorias()
        {
            var lista = categoriasBll.Listar();
            return JsonCamelCase(lista);
        }

        // Guarda o Edita manejando la subida de imagen
        [HttpPost]
        public ActionResult GuardarProducto(Producto producto, HttpPostedFileBase imagenFile)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            try
            {
                if (imagenFile != null && imagenFile.ContentLength > 0)
                {
                    string extension = Path.GetExtension(imagenFile.FileName);
                    string nombreImagen = Guid.NewGuid().ToString() + extension;
                    string carpetaDestino = Server.MapPath("~/Uploads/Productos/");

                    if (!Directory.Exists(carpetaDestino))
                    {
                        Directory.CreateDirectory(carpetaDestino);
                    }

                    string rutaCompleta = Path.Combine(carpetaDestino, nombreImagen);
                    imagenFile.SaveAs(rutaCompleta);

                    producto.RutaImagen = "/Uploads/Productos/" + nombreImagen;
                }

                if (producto.IdProducto == 0)
                {
                    resultado = productosBll.Insertar(producto, out mensaje);
                }
                else
                {
                    resultado = productosBll.Editar(producto, out mensaje);
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = "Error en el servidor: " + ex.Message;
            }

            return JsonCamelCase(new { resultado = resultado, mensaje = mensaje });
        }

        [HttpPost]
        public ActionResult EliminarProducto(int IdProducto)
        {
            string mensaje = string.Empty;
            bool resultado = productosBll.Eliminar(IdProducto, out mensaje);

            return JsonCamelCase(new { resultado = resultado, mensaje = mensaje });
        }
    }
}