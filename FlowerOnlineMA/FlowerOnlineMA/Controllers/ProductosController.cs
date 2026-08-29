using FlowerOnlineMA_BLL;
using FlowerOnlineMA_ENTITIES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Linq;
using System.Text;


namespace FlowerOnlineMA.Controllers
{
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {
        private readonly ProductosBLL productosBll = new ProductosBLL();

        // GET: Listar los productos
        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                var lista = productosBll.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //GET: listar productos activos
        [HttpGet]
        [Route("listaractivos")]
        public IHttpActionResult ListaActivo()
        {
            try
            {
                var listaActivos = productosBll.Listar();
                return Ok(listaActivos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        // Guarda o Edita manejando la subida de imagen
        [HttpPost]
        [Route("guardar")]
        public async Task<IHttpActionResult> GuardarProducto()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("Formato de petición no soportado. Se esperaba multipart/form-data.");
            }

            string mensaje = string.Empty;
            bool resultado = false;

            try
            {
                string root = HttpContext.Current.Server.MapPath("~/Uploads/Productos/");

                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                var provider = new MultipartFormDataStreamProvider(root);
                await Request.Content.ReadAsMultipartAsync(provider);

                //Instancia del objeto Producto desde los datos del FormData
                Producto producto = new Producto
                {
                    IdProducto = Convert.ToInt32(provider.FormData["IdProducto"] ?? "0"),
                    Nombre = provider.FormData["Nombre"],
                    Descripcion = provider.FormData["Descripcion"],
                    Precio = Convert.ToDecimal(provider.FormData["Precio"] ?? "0"),
                    Stock = Convert.ToInt32(provider.FormData["Stock"] ?? "0"),
                    IdCategoria = Convert.ToInt32(provider.FormData["IdCategoria"] ?? "0"),
                    TipoLuz = provider.FormData["TipoLuz"],
                    FrecuenciaRiego = provider.FormData["FrecuenciaRiego"],
                    NivelCuidado = provider.FormData["NivelCuidado"]
                };

                //Procesar la imagen enviada
                if (provider.FileData.Count > 0)
                {
                    var fileData = provider.FileData[0];
                    string originalFileName = fileData.Headers.ContentDisposition.FileName.Trim('"');
                    string extension = Path.GetExtension(originalFileName);
                    string nuevoNombre = Guid.NewGuid().ToString() + extension;
                    string rutaFinal = Path.Combine(root, nuevoNombre);

                    File.Move(fileData.LocalFileName, rutaFinal);
                    producto.RutaImagen = "/Uploads/Productos/" + nuevoNombre;
                }
                else 
                { 
                    producto.RutaImagen = provider.FormData["RutaImagen"];
                }

                if (producto.IdProducto == 0)
                {
                    resultado = productosBll.Insertar(producto, out mensaje);
                }
                else {
                    resultado = productosBll.Editar(producto, out mensaje);
                }

                return Ok(new {resultado = resultado, mensaje = mensaje });
            }
            catch (Exception ex)
            {
                return Ok(new {resultado = false, mensaje="Error en el servidor: " + ex.Message });
            }

            
        }

        //Eliminar Productos
        [HttpPost]
        [Route("eliminar")]
        public IHttpActionResult EliminarProducto([FromBody] Producto request)
        {
            try
            {
                string mensaje = string.Empty;
                bool resultado = productosBll.Eliminar(request.IdProducto, out mensaje);
                return Ok(new { resultado = resultado, mensaje = mensaje });
            }
            catch (Exception ex) 
            {
                return InternalServerError(ex);
            }
        }

    }
}