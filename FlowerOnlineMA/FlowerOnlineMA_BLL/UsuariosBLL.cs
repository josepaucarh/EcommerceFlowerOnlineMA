using FlowerOnlineMA_DAL;
using FlowerOnlineMA_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerOnlineMA_BLL
{
    public class UsuariosBLL
    {
        private readonly UsuariosDAL usuarioDal = new UsuariosDAL();

        public Usuario Login(string correo, string clave, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(correo))
            {
                mensaje = "Ingrese un correo electrónico válido.";
                return null;
            }

            if (string.IsNullOrWhiteSpace(clave))
            {
                mensaje = "Ingrese su contraseña.";
                return null;
            }

            correo = correo.Trim();
            Usuario usuario = usuarioDal.Login(correo, clave);

            if (usuario == null)
            {
                mensaje = "Correo o contraseña incorrectos.";
            }

            return usuario;
        }
    }
}
