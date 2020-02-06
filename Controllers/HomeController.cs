using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ProductosManager productoManager = new ProductosManager();
            ViewBag.lista= productoManager.ListaOfertas();
            return View();
        }

        public ActionResult Indumentaria()
        {
            ProductosManager manager = new ProductosManager();
            ViewBag.lista = manager.ListaIndumentaria();


            return View();
        }

        public ActionResult Detalle(int id)
        {
            ProductosManager productosManager = new ProductosManager();
            ViewBag.detalle= productosManager.MostrarPorDetalle(id);
            return View();
        }

        public ActionResult Quienes()
        {
            

            return View();
        }

        public ActionResult Contactos()

        {
            return View();
        }

       
    }
}