using FlowerOnlineMA_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using FlowerOnlineMA_ENTITIES;

namespace FlowerOnlineMA.Controllers
{
    [RoutePrefix("api/categorias")]
    public class CategoriasController : ApiController
    {
        private readonly CategoriasBLL categoriasBll = new CategoriasBLL();

        //GET: api/categorias/Listar
        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                var lista = categoriasBll.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //POST: api/categorias/Agregar
        [HttpPost]
        [Route("insertar")]
        public IHttpActionResult Insertar([FromBody] Categoria categoria)
        {
            string mensaje = string.Empty;
            bool respuesta = categoriasBll.Insertar(categoria, out mensaje);
            return Ok(new { resultado = respuesta, mensaje = mensaje });
        }

        //POST: api/categorias/editar
        [HttpPost]
        [Route("editar")]
        public IHttpActionResult Editar([FromBody] Categoria categoria)
        {
            string mensaje = string.Empty;
            bool respuesta = categoriasBll.Editar(categoria, out mensaje);
            return Ok(new { resultado = respuesta, mensaje = mensaje});
        }

        //POST: api/categorias/eliminar
        [HttpPost]
        [Route("eliminar")]
        public IHttpActionResult Eliminar([FromBody] Categoria categoria)
        {
            string mensaje = string.Empty;
            bool respuesta = categoriasBll.Eliminar(categoria.IdCategoria, out mensaje);
            return Ok(new {resultado = respuesta, mensaje = mensaje });
        }


    }
}