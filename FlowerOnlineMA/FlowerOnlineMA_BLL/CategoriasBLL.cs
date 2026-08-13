using FlowerOnlineMA_DAL;
using FlowerOnlineMA_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerOnlineMA_BLL
{
    public class CategoriasBLL
    {
        private readonly CategoriasDAL categoriaDal = new CategoriasDAL();

        //Validacion de Listar
        public List<Categoria> Listar()
        {
            return categoriaDal.Listar();
        }

        //Validacion de Insertar
        public bool Insertar(Categoria categoria, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                mensaje = "El nombre de la categoría no puede estar vacío.";
                return false;
            }

            categoria.Nombre = categoria.Nombre.Trim();

            return categoriaDal.Insertar(categoria, out mensaje);
        }

        //Validacion de Editar
        public bool Editar(Categoria categoria, out string mensaje)
        {
            mensaje = string.Empty;

            if (categoria.IdCategoria <= 0)
            {
                mensaje = "Debe seleccionar una categoría válida.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                mensaje = "El nombre de la categoria no puede estar vacío.";
                return false;
            }

            categoria.Nombre = categoria.Nombre.Trim();

            return categoriaDal.Editar(categoria, out mensaje);
        }

        //Validacion de Eliminar
        public bool Eliminar(int IdCategoria, out string mensaje)
        {
            mensaje = string.Empty;

            if (IdCategoria <= 0)
            {
                mensaje = "Seleccione una categoría válida para eliminar";
                return false;
            }

            return categoriaDal.Eliminar(IdCategoria, out mensaje);
        }

    }
}
