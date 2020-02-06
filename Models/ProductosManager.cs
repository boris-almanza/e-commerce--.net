using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace ProyectoFinal.Models
{
    public class ProductosManager
    {
        //-------------- CONSULTA INDEX / OFERTAS-----------
        public List<Producto> ListaOfertas()
        {
            List<Producto> lista = new List<Producto>();
            string consulta = "select top(6) tipo, precio,rutaimg  from producto";

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]))
            {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.tipo = Convert.ToString(reader["tipo"]);
                    producto.precio = Convert.ToDecimal(reader["precio"]);
                    producto.rutaimg = Convert.ToString(reader["rutaimg"]);
                    lista.Add(producto);

                }
                reader.Close();
            }
            return lista;
        }

        //--------------CONSULTA DE INDUMENRTARIA/usuario--------------

        public List<Producto> ListaIndumentaria()
        {
            List<Producto> lista = new List<Producto>();
            string consulta = "select top(9) tipo, precio,rutaimg  from producto";

            using(SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]))
            {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
              
                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.tipo = Convert.ToString( reader["tipo"]);
                    producto.precio = Convert.ToDecimal(reader["precio"]);
                    producto.rutaimg = Convert.ToString(reader["rutaimg"]);
                    lista.Add(producto);
                
                }
                reader.Close();
            }
            return lista;
        }

        public Producto MostrarPorDetalle( int id)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]);
            Producto producto = new Producto();
            string consulta = "select rutaimg,tipo,precio, from Producto where idproducto=@id";
            conexion.Open();
            SqlCommand cmd = new SqlCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader leer = cmd.ExecuteReader();
                while (leer.Read())
                {

                producto.rutaimg = Convert.ToString(leer["rutaimg"]);
                producto.tipo = Convert.ToString(leer["tipo"]);
                producto.precio = Convert.ToDecimal(leer["precio"]);
                  
                }

                leer.Close();
                conexion.Close();
           
            return producto;
        }






        //-------------- CONSULTAS DEL ADMINISTRADOR.----------------


        public List<Producto> ConsultarTodos()
        {
            List<Producto> listaProductos = new List<Producto>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]);
            conexion.Open();
            /* string cmd= "select * Producto";*/
            SqlCommand comando = conexion.CreateCommand();

            comando.CommandText ="select * from Producto";
            // instanmciamos la clase data table para guardar los datos
            DataTable tablaDatos = new DataTable();
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            adaptador.Fill(tablaDatos);
         
            foreach (DataRow fila in tablaDatos.Rows)
            {
                Producto producto = new Producto();
                producto.id = (int)fila["idproducto"];
                producto.tipo = (string) fila["tipo"];
                producto.precio = Convert.ToDecimal( fila["precio"]);
                producto.talla = (string) fila["talla"];
                producto.stock = (int)fila["stock"];
                producto.rutaimg = (string)fila["rutaimg"];
                listaProductos.Add(producto);
            }


            return listaProductos;
        }


        //----------------INSERTAR PRODUCTOS EN BBDD--------------

        public void insertarProducto(Producto producto)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]);
            string cmd = "insert into values(@id,@tipo,@precio,@talla,@stock,@rutaimg)";
            SqlCommand comando = new SqlCommand(cmd, conexion);
            

           
                conexion.Open();
                comando.Parameters.AddWithValue("@id", producto.id);
                comando.Parameters.AddWithValue("@tipo", producto.tipo);
                comando.Parameters.AddWithValue("@precio", producto.precio);
                comando.Parameters.AddWithValue("@talla", producto.talla);
                comando.Parameters.AddWithValue("@stock", producto.stock);
                comando.Parameters.AddWithValue("@rutaimg", producto.rutaimg);
                comando.ExecuteNonQuery();

            conexion.Close();
           }

        //--------- ACTUALIZAR PRODUCTOS-----------

        public Producto EditarProducto(Producto producto)
        {
           using (SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"])) 
            { string cmd = "update producto set precio=@precio, tipo=@tipo , talla=@talla, stock=@stock, rutaimg=@rutaimg  where idproducto=@id";
                SqlCommand comando = new SqlCommand(cmd, conexion);
               

                conexion.Open();
                comando.Parameters.AddWithValue("@id", producto.id);
                comando.Parameters.AddWithValue("@precio", producto.precio);
                comando.Parameters.AddWithValue("@tipo", producto.tipo);
                comando.Parameters.AddWithValue("@talla", producto.talla);
                comando.Parameters.AddWithValue("@stock", producto.stock);
                comando.Parameters.AddWithValue("@rutaimg", producto.rutaimg);
                comando.ExecuteNonQuery();
            }
            return producto;
        }


        //------ELIMINAR PRODUCTOS---------

        public void EliminarProducto(int id)
        {

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]))
            {
                string cmd = "delete from Producto where idproducto = @id";
               
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


        //---------------BUSCAR PRODUCTOS EN BBDD----------

        public List<Producto> BuscarProducto(string nombre)
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["bdconexion"]))
            {
               
               
                string consulta= "select * from Producto where tipo like '%@nombre%'";
                SqlCommand cmd = new SqlCommand(consulta,conexion);
                cmd.Parameters.AddWithValue(@nombre, nombre);
                using (SqlDataReader leer = cmd.ExecuteReader())
                {
                    while (leer.Read())
                    {
                        Producto producto = new Producto();
                        producto.id = Convert.ToInt16(leer["id"]);
                        producto.tipo = Convert.ToString(leer["tipo"]);
                        producto.precio = Convert.ToDecimal(leer["precio"]);
                        producto.talla = Convert.ToString(leer["talla"]);
                        producto.stock = Convert.ToInt16(leer["stock"]);
                        producto.rutaimg = Convert.ToString(leer["rutaimg"]);
                        lista.Add(producto);
                    }
                }
                conexion.Close();
            }
                return lista;
        }
    }
}