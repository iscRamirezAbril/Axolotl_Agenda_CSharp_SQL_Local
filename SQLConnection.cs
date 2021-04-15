using MySql.Data.MySqlClient; // Librería que nos permitirá conectarnos a las bases de datos de "MySQL".
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    /*
       Esta clase se creó específicamente para crear la connexión a la base de datos de
       MySQL. La clase contiene un método de nombre "getConnection", que se encarga de
       realizar dicha conexión.
    */
    class SQLConnection{
        // <--- Método #1: Conexión a "MySQL". ---> //
        public static MySqlConnection getConnection(){
            // Declaración de variables.
            string servidor = "localhost"; // Ruta de "MySQL" donde se encuentra nuestro servidor.
            string puerto = "3306"; // Puerto que tiene "MySQL".
            string usuario = "root"; // Usuario de "MySQL".
            string password = "5107_ARF!0103"; // Contraseña del host.
            string bd = "sistema_usuarios"; // Nombre de la Base de Datos.

            // Cadena de conexión a "MySQL".
            string stringConnection = "server=" + servidor + "; port=" + puerto + "; user id=" + usuario + "; password=" + password + "; database=" + bd;

            /*
               Se genera un objeto de nombre "connection" de la clase "MySqlConnection".
            */
            MySqlConnection connection = new MySqlConnection(stringConnection);

            return connection; // Retorno de valor.
        }
    }
}
