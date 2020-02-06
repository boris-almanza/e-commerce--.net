using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class ManagerConulta
    {
        public void InsertarDatos(Consulta consulta)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]);
            conexion.Open();
            
            SqlCommand comando = conexion.CreateCommand();
            comando.CommandText = "insert into Productos(id,tipo,precio,talla,stock)"
                                    + "values()";
        }
    }
}