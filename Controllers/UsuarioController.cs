using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpPost]
        public ActionResult Recibir(FormCollection formulario)
        {
            Usuario usuario = new Usuario();
            usuario.nombre = formulario["nombre"];
            usuario.mail = formulario["mail"];
            usuario.telefono = Convert.ToString(formulario["telefono"]);
            usuario.contraseña = formulario["contrasena"];
            usuario.idrol = 1;
            //INSTANCIAMOS EL MANAGER USUARIO
            UsuarioManager managerUsuario = new UsuarioManager();
            managerUsuario.AgregarUsuario(usuario);

            return View("Registrado");
        }


        [HttpPost]
        public ActionResult DoLogin(FormCollection formulario)
        {
           
            string mail = Convert.ToString(formulario["mail"]);
            string contraseñad = Convert.ToString(formulario["contrasena"]);

            UsuarioManager usuarioManager = new UsuarioManager();
            Usuario usuario=usuarioManager.ConsultarLogin(contraseñad);
            if (usuario != null)
            {
                //existe el usuario
                if (usuario.contraseña.Equals(contraseñad))
                {
                    //EL LOGIN ES CORRECTO
                    Session["Usuario"] = usuario;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //LA contraseña es incorrecta
                    return RedirectToAction("Login");
                }

            }
            else
            {
                //EL usuario no existe
                return RedirectToAction("Login");
            }

        }



        public ActionResult Logout()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
        
    
