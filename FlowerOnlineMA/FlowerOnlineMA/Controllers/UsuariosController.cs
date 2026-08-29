using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using FlowerOnlineMA_BLL;
using FlowerOnlineMA_ENTITIES;

namespace FlowerOnlineMA.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        private readonly UsuariosBLL usuariosBll = new UsuariosBLL();

        public class LoginRequest
        {
            public string Correo { get; set; }
            public string Clave { get; set; }
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                if (request == null) return BadRequest("Datos no proporcionados.");

                string mensaje = string.Empty;
                Usuario usuario = usuariosBll.Login(request.Correo, request.Clave, out mensaje);

                if (usuario == null)
                {
                    return Ok(new { resultado = false, mensaje = mensaje });
                }

                return Ok(new { resultado = true, usuario = usuario, mensaje = "Inicio de sesión exitoso." });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}