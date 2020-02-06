using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
       public ActionResult Index()
        {
            return View();
        }
        public ActionResult Producto()
        {
           ProductosManager productomanager = new ProductosManager();
           ViewBag.lista = productomanager.ConsultarTodos();
            return View();
        }

        //------BUSCAR PRODUCTO---------
        public ActionResult BuscarPorNombre( string nombre)
        {
            ProductosManager productoManager = new ProductosManager();
            ViewBag.lista= productoManager.BuscarProducto(nombre);
            return View();
        }

        // ----------EGREGAR PRODUCTO--------
        public ActionResult AgregarProducto(FormCollection formulario)
        {
            Producto producto = new Producto();
            producto.id = Convert.ToInt32( formulario["id"]);
            producto.precio = Convert.ToDecimal(formulario["precio"]);
            producto.tipo = Convert.ToString(formulario["tipo"]);
            producto.talla = Convert.ToString(formulario["talla"]);
            producto.stock = Convert.ToInt32(formulario["stock"]);
            producto.rutaimg = Convert.ToString(formulario["rutaimg"]);

            ProductosManager productoManager = new ProductosManager();
            productoManager.insertarProducto(producto);
            return View();
        }

        // ----------EDITAR PRODUCTO--------
        public ActionResult EditarProducto(int ID, FormCollection formulario)
        {
            Producto producto = new Producto();
            producto.id = ID;
            producto.precio = Convert.ToDecimal(formulario["precio"]);
            producto.tipo = Convert.ToString(formulario["tipo"]);
            producto.talla = Convert.ToString(formulario["talla"]);
            producto.stock = Convert.ToInt32(formulario["stock"]);
            producto.rutaimg = Convert.ToString(formulario["rutaimg"]);
            ProductosManager productoManager = new ProductosManager();
            productoManager.EditarProducto(producto);

            if (Request.Files.Count > 0 &&
               Request.Files[0].ContentLength > 0) //para validar que vino el archivo
            {
                string rutaFinal = Server.MapPath("/Content/imagenes" + producto.rutaimg+ ".jpg");
                Request.Files[0].SaveAs(rutaFinal);
            }

            return RedirectToAction("Producto", "Admin");
        }
          

        //-------ELIMINAR PRODUCTO-------------
        public ActionResult BorrarProducto(int id)
        {
            ProductosManager productos = new ProductosManager();
            productos.EliminarProducto(id);
            return RedirectToAction("Producto","Admin");
        }

    

        //----------USUARIOS-----------

        public ActionResult Usuario()
             {
            UsuarioManager usuarioManager = new UsuarioManager();
            ViewBag.lista = usuarioManager.ConsultarTodos();

                 return View();
             }

        public ActionResult BuscarUsuario(string nombre)
        {
            UsuarioManager usuarioManager =new UsuarioManager();
            ViewBag.lista= usuarioManager.BuscaUsuario(nombre);

            return View();
        }
        public ActionResult AgregarUsuario(FormCollection formulario)
        {
            UsuarioManager usuarioManager = new UsuarioManager();
           Usuario usuario = new Usuario();
           
            usuario.nombre = Convert.ToString(formulario["nombre"]);
            usuario.mail = Convert.ToString(formulario["mail"]);
            usuario.telefono = Convert.ToString(formulario["telefono"]);
            usuario.contraseña = Convert.ToString( formulario["contraseña"]);
            usuario.idrol = Convert.ToInt16( formulario["idrol_c1"]);
           
        
            usuarioManager.AgregarUsuario(usuario);
            return RedirectToAction("Usuario", "Admin");
        }



        public ActionResult BorrarUsuario(string id)
        {
            UsuarioManager usuarioManager = new UsuarioManager();
            usuarioManager.EliminarUsuario(id);

            return RedirectToAction("Usuario", "Admin");
        }
       
    }
}