using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class UsuarioManager
    {
        //--------  consultar usuarios------
        public List<Usuario> ConsultarTodos()
        {
            List<Usuario> lista = new List<Usuario>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]);
            conexion.Open();
            /* string cmd= "select * Producto";*/
            SqlCommand comando = conexion.CreateCommand();

            comando.CommandText = "select * from usuario";
            // instanmciamos la clase data table para guardar los datos
            DataTable tablaDatos = new DataTable();
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            adaptador.Fill(tablaDatos);

            foreach (DataRow fila in tablaDatos.Rows)
            {
                Usuario usuario = new Usuario();
                usuario.nombre = (string)fila["nombre"];
                usuario.mail= (string)fila["mail"];
                usuario.telefono = Convert.ToString(fila["telefono"]);
                usuario.contraseña = (string)fila["contraseña"];
                usuario.idrol= (int)fila["idrol_c1"];
            
                lista.Add(usuario);
            }

            return lista;
        }

        //---------------- AGREGAR USUARIO/ usuario--------------
        public void AgregarUsuario(Usuario usuario)
        {
           
            //CONECTAMOS CON LA BASE DE DATOS
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]);
            conexion.Open();
            //CREAMOS EL COMANDO(ES COMO ABRIR UNA NUEVA QUERY
            SqlCommand comando = conexion.CreateCommand();
            //setamos la consulta
            comando.CommandText = "insert into usuario(nombre,mail,telefono,contraseña,idrol_c1)" +
                                    "values(@nombre,@mail,@telefono,@contraseña,@idrol)";
            //pasamos los parametros con el valor que tomara c/u
            comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value=usuario.nombre;
            comando.Parameters.Add("@mail", SqlDbType.VarChar).Value = usuario.mail;
            comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = usuario.telefono;
            comando.Parameters.Add("@contraseña", SqlDbType.VarChar).Value = usuario.contraseña;
            comando.Parameters.Add("@idrol", SqlDbType.Int).Value = usuario.idrol;
             /*tambien se puede hacer de sta forma o definiendo cada una con la instancia de sqlparameter
            comando.Parameters.Add(new SqlParameter("@nombre",SqlDbType.VarChar));
            comando.Parameters["@nombre"].Value = usuario.nombre;*/

            //ejecutamos la query
            comando.ExecuteNonQuery();
            conexion.Close();
        }
  

        public Usuario ConsultarLogin(string contraseña)
        {
            Usuario usuario = null;
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]))
            {
                string consulta = "select * from usuario where contraseña=@contraseña";
                    conexion.Open();

                //2) Creamos el comando (como abrir una nueva query)
                SqlCommand comando = new SqlCommand(consulta,conexion);

               
                comando.Parameters.AddWithValue("@Contraseña", contraseña);


                //Preparamos la tabla donde va a qudar el resultado
                DataTable tablaResultado = new DataTable();
                //Creamos el que va a llenarnos la tabla
                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                //Ejecuta la consulta y llena la tabla
                adaptador.Fill(tablaResultado);

                //pregunto si el resultado tiene filas
                //para saber si existe o no el usuario
                if (tablaResultado.Rows.Count > 0)
                {
                    //el usuario existe
                    usuario = new Usuario();

                    DataRow fila = tablaResultado.Rows[0]; //solo trae una fila.

                   
                    usuario.nombre = (string)fila["Nombre"];
                    usuario.mail = (string)fila["mail"];
                    usuario.telefono = (string)fila["telefono"];
                    usuario.contraseña = (string)fila["contraseña"];
                    usuario.idrol = (int)fila["idrol_c1"];
                    //..
                }

                
            }
            return usuario;
          }













    //----------BUSCAR USARIO/admin---------
        
  public List<Usuario> BuscaUsuario(string nombre)
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]))
            {
               
               
                string consulta= "select * from Usuario where tipo like '%@nombre%'";
                SqlCommand cmd = new SqlCommand(consulta,conexion);
                cmd.Parameters.AddWithValue(@nombre, nombre);
                using (SqlDataReader leer = cmd.ExecuteReader())
                {
                    while (leer.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.nombre = Convert.ToString(leer["nombre"]);
                        usuario.mail = Convert.ToString(leer["mail"]);
                        usuario.telefono = Convert.ToString(leer["telefono"]);
                        usuario.contraseña = Convert.ToString(leer["contrasena"]);
                        usuario.idrol= Convert.ToInt16(leer["idrol"]);
                   
                        lista.Add(usuario);
                    }
                }
                conexion.Close();
                
            }
            return lista; 
    }



  //------ELIMINAR USUARIO---------

  public void EliminarUsuario(string id)
  {

      using (SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]))
      {
          string cmd = "delete from Usuario where mail = @id";

          SqlCommand comando = new SqlCommand(cmd, conexion);
          conexion.Open();
          //definimos los parametros 
          SqlParameter paran = new SqlParameter();
          paran.ParameterName = "@id";
          paran.Value = id;
          //comando.parameter.add recibe una coleccionde sqlparameter
          comando.Parameters.Add(paran);

          comando.ExecuteNonQuery();
      }
  }

       // ----------- AGRAGAR USUARIO/admin----------
  public void InsertarUsuario(Usuario usuario)
  {
      SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]);
      string cmd = "insert into values(@nombre,,@mail,@telefono,@contrasena,@idrol)";
      SqlCommand comando = new SqlCommand(cmd, conexion);



      conexion.Open();
      comando.Parameters.AddWithValue("@nombre", usuario.nombre);
      comando.Parameters.AddWithValue("@mail", usuario.mail);
      comando.Parameters.AddWithValue("@telefono", usuario.telefono);
      comando.Parameters.AddWithValue("@contrasena", usuario.contraseña);
      comando.Parameters.AddWithValue("@idrol", usuario.idrol);
     
      comando.ExecuteNonQuery();

      conexion.Close();
  }
         

}
}