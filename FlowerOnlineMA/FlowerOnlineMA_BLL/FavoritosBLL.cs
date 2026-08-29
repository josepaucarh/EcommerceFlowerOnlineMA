using FlowerOnlineMA_DAL;
using FlowerOnlineMA_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerOnlineMA_BLL
{
    public class FavoritosBLL
    {
        private readonly FavoritosDAL favoritoDal = new FavoritosDAL();

        public List<Producto> ListarPorUsuario(int idUsuario)
        {
            if (idUsuario <= 0) return new List<Producto>();
            return favoritoDal.ListarPorUsuario(idUsuario);
        }
    }
}
