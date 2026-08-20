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

        [HttpGet]
        [Route("Listar")]
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
    }
}