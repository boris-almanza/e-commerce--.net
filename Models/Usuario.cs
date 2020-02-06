using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Usuario
    {
   

        public string nombre { get; set;}      
        public string mail { get; set; }
        public string telefono { get; set; }
        public string contraseña { get; set; }
        public int idrol { get; set; }
    }
}