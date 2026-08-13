using FlowerOnlineMA_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerOnlineMA.Controllers
{
    public class ProductosController : Controller
    {
        private readonly CategoriasBLL categoriaBll = new CategoriasBLL();

        public ActionResult Index()
        {
            return View();
        }
    }
}