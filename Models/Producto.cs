using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Producto
    {
        public int id { get; set; }
        public string tipo { get; set; }
        public decimal precio { get; set; }
        public string talla { get; set; }
        public int stock { get; set; }
        public string rutaimg { get; set; }
    }
}