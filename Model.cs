using MySql.Data.MySqlClient; // Librería que nos permitirá conectarnos a las bases de datos de "MySQL".
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    /*
       Esta clase nos ayudará a realizar todas las insersiones a "MySql".
       En otras palabras, esta clase nos va a permitir guardar todos los métodos para las validaciones.
    */
    class Model{
        // <---------------------------------------> //
        // <---------- MÉTODOS / METHODS ----------> //
        // <---------------------------------------> //

        // <--- Método #1: Validación de datos y registro de usuarios ---> //
        public int register(registro_usuarios user){ // Recibe como parámetro una variable del tipo "registro_usuarios".
                                       // Referencia a la clase de nombre "SQLConnection".
            MySqlConnection conexion = SQLConnection.getConnection();

            conexion.Open(); // Esta función permite abrir la conexión.

            // Inserción a "SQL".
            /*
              "INSERT INTO" se utiliza para "seleccionar" los campos de la tabla "registro_usuarios"
              donde se insertarán los datos que registre el usuario.
              "VALUES" son los valores que se van a registrar en la tabla de "MySql".
            */
            string sql = "INSERT INTO registro_usuarios (Name, Last_name, Username, Email, Password, id_tipo) VALUES (@Name, @Last_name, @Username, @Email, @Password, @id_tipo)";

            // Se crea un objeto de la clase "MySqlCommand", enviandole como parámetros "sql" y "conexion".
            MySqlCommand command = new MySqlCommand(sql, conexion);

            /*
               Se crean "comandos" que realizan la acción de enviar los datos que el ususario registre
               a "MySql", permitiendo así, guardar su información.

               ".AddWithValue() recibe dos parámetros, el primero es el valor que recibirá el comando
               (Valores con "@") y el segundo es el dato que el usuario haya ingresado (un dato de tipo "usuario"
               con la propiedad de la clase "registro_usuario").
            */
            command.Parameters.AddWithValue("@Name", user.Name1); // Comando para el campo "Name".
            command.Parameters.AddWithValue("@Last_name", user.Last_name1); // Comando para el campo "Last_name".
            command.Parameters.AddWithValue("@Username", user.Username1); // Comando para el campo "Username".
            command.Parameters.AddWithValue("@Email", user.Email1); // Comando para el campo "Email".
            command.Parameters.AddWithValue("@Password", user.Password1); // Comando para el campo "Password".
            command.Parameters.AddWithValue("@id_tipo", user.Id_tipo); // Comando para el campo "id_tipo".

            // Declaración de variables.
            int resultado = command.ExecuteNonQuery(); // Te devuelve el número de "filas" que se hayan insertado.

            return resultado; // Retorno de valor.
        }

        // <--- Método #2: Validación de nombres de usuario repetidos al momento de registrar un nuevo usuario. ---> //
        public bool existUser(string username){ // Recibe como parámetro una variable de tipo "string" de nombre "username".
            // Este objeto permite leer todos los datos que se encuentren en la base de datos.
            MySqlDataReader reader;

            MySqlConnection conexion = SQLConnection.getConnection();
            conexion.Open(); // Esta función permite abrir la conexión.

            // Declaración de variable.
            /*
               Esta variable selecciona de la tabla "registro_usuarios" el registro de numero "id"
               cuando en el campo de la base de datos "username" sea igual al parámetros
               que se está registrando.
            */
            string sql = "SELECT id FROM registro_usuarios WHERE Username LIKE @Username";

            // Se crea un objeto de la clase "MySqlCommand", enviandole como parámetros "sql" y "conexion".
            MySqlCommand command = new MySqlCommand(sql, conexion);
            command.Parameters.AddWithValue("@Username", username); // Comando para el campo "Username".

            reader = command.ExecuteReader(); // ".ExecuteReader() es el método que permite realizar la lectura de datos.

            /*
               Condición que solo es válida sí y sólo sí el "lector" (reader) encuentra un "username" igual al que
               el actual usuario quiere registrar.
            */
            if (reader.HasRows) return true; // Retorno de valor "true" si un usuario ya está registrado.
            else return false; // Retorno de valor "false" si un usuario no está registrado.
        }

        // <--- Método #3: Validación de nombre de usuario ya existente para el "login" y acceso a la aplicación. ---> //
        /*
          Este método es muy parecido al método "existeUsuario". Las diferencias que tiene este método con el mencionado
          son las siguientes:
          1.- El nombre de este método es "LoginUsuario".
          2.- Ya no es de tipo "bool", sino de tipo "registro_usuarios" (clase donde se encuentran las propiedades
              de los campos de la tabla "registro_usuarios" de "MySql".
          3.- Nueva condición "While()".
        */
        public registro_usuarios LoginUser(string user){
            // Este objeto permite leer todos los datos que se encuentren en la base de datos.
            MySqlDataReader reader;

            MySqlConnection conexion = SQLConnection.getConnection();
            conexion.Open(); // Esta función permite abrir la conexión.

            // Declaración de variable.
            /*
               Esta variable selecciona de la tabla "registro_usuarios" los siguientes datos de los campos correspondientes:
               1. id.    3. Last_name.  5. Email.     7. id_tipo.
               2. Name.  4. Username.   6. Password.
               Los selecciona cuando en el campo de la base de datos "username" sea igual al parámetros que está
               iniciando sesión.
            */
            string sql = "SELECT id, Name, Last_name, Username, Email, Password, id_tipo FROM registro_usuarios WHERE Username LIKE @Username";

            // Se crea un objeto de la clase "MySqlCommand", enviandole como parámetros "sql" y "conexion".
            MySqlCommand command = new MySqlCommand(sql, conexion);
            command.Parameters.AddWithValue("@Username", user); // Comando para el campo "Username".

            reader = command.ExecuteReader(); // ".ExecuteReader() es el método que permite realizar la lectura de datos.
            registro_usuarios usr = null; // Declaración de una variable de tipo "registro_usuarios".

            while (reader.Read()){
                usr = new registro_usuarios(); // Se crea una instancia de la clase "registro_usuarios".

                // Asignación de propiedades.
                usr.Id = Convert.ToInt32(reader["id"].ToString());
                usr.Name1 = reader["Name"].ToString();
                usr.Last_name1 = reader["Last_name"].ToString();
                usr.Email1 = reader["Email"].ToString();
                usr.Password1 = reader["Password"].ToString();
                usr.Id_tipo = Convert.ToInt32(reader["id_tipo"].ToString());
            }
            return usr; // Retorno de los datos acumulados en la variable, correspondientes a la consulta.
        }

        // <--- Método #4: Recuperación de contraseña. ---> //
        /*
           Este método permite recuperar la contraseña de usuario.
        */
        public registro_usuarios RecoverPass(string usuario){
            // Este objeto permite leer todos los datos que se encuentren en la base de datos.
            MySqlDataReader reader;

            MySqlConnection conexion = SQLConnection.getConnection();
            conexion.Open(); // Esta función permite abrir la conexión.

            // Declaración de variable.
            /*
               Esta variable selecciona de la tabla "registro_usuarios" los siguientes datos de los campos correspondientes:
               1. Username.
               Los selecciona cuando en el campo de la base de datos "username" sea igual al parámetros que está
               tratando de recuperar su contraseña.
            */
            string sql = "SELECT id, username, password FROM registro_usuarios WHERE Username LIKE @Username"; ;

            // Se crea un objeto de la clase "MySqlCommand", enviandole como parámetros "sql" y "conexion".
            MySqlCommand command = new MySqlCommand(sql, conexion);
            command.Parameters.AddWithValue("@Username", usuario); // Comando para el campo "Username".

            reader = command.ExecuteReader(); // ".ExecuteReader() es el método que permite realizar la lectura de datos.
            registro_usuarios usr = null; // Declaración de una variable de tipo "registro_usuarios".

            while (reader.Read()){
                usr = new registro_usuarios(); // Se crea una instancia de la clase "registro_usuarios".

                // Asignación de propiedades.
                usr.Username1 = reader["Username"].ToString();
                usr.Password1 = reader["Password"].ToString();
            }

            return usr; // Retorno de los datos acumulados en la variable, correspondientes a la consulta.
        }

        // <--- Método #5: Validación de datos y registro de actividades. ---> //
        public int newAct(actividades activity){ // Recibe como parámetro una variable del tipo "registro_usuarios".
                                       // Referencia a la clase de nombre "SQLConnection".
            MySqlConnection conexion = SQLConnection.getConnection();

            conexion.Open(); // Esta función permite abrir la conexión.

            // Inserción a "SQL".
            /*
              "INSERT INTO" se utiliza para "seleccionar" los campos de la tabla "actividades"
              donde se insertarán los datos que registre el usuario.
              "VALUES" son los valores que se van a registrar en la tabla de "MySql".
            */
            string sql = "INSERT INTO actividades (Name, Type, Start, End, id_Username) VALUES (@Name, @Type, @Start, @End, @id_Username)";

            // Se crea un objeto de la clase "MySqlCommand", enviandole como parámetros "sql" y "conexion".
            MySqlCommand command = new MySqlCommand(sql, conexion);

            /*
               Se crean "comandos" que realizan la acción de enviar los datos que el ususario registre
               a "MySql", permitiendo así, guardar su información.

               ".AddWithValue() recibe dos parámetros, el primero es el valor que recibirá el comando
               (Valores con "@") y el segundo es el dato que el usuario haya ingresado (un dato de tipo "actividad"
               con la propiedad de la clase "actividades").
            */
            command.Parameters.AddWithValue("@Name", activity.Name1); // Comando para el campo "Name".
            command.Parameters.AddWithValue("@Type", activity.Type1); // Comando para el campo "Type".
            command.Parameters.AddWithValue("@Start", activity.Start1); // Comando para el campo "Start".
            command.Parameters.AddWithValue("@End", activity.End1); // Comando para el campo "End".
            command.Parameters.AddWithValue("@id_Username", activity.Id_Username); // Comando para el campo "Id_Username".

            // Declaración de variables.
            int resultado = command.ExecuteNonQuery(); // Te devuelve el número de "filas" que se hayan insertado.

            return resultado; // Retorno de valor.
        }

        // <--- Método #6: Asignación de los usuarios registrados a una lista ---> //
        public List<Object> userQuery(string data){
            // Este objeto permite leer todos los datos que se encuentren en la base de datos.
            MySqlDataReader reader;
            // Creación d eun objeto de la clase "Lista<Object>".
            List<Object> List = new List<object>();
            // Inserción a Sql.
            string sql;

            // Condición que sólo es válida sí y sólo la tabla se carga por primera vez.
            if (data == null)
                // Inserción.
                sql = "SELECT id, Name, Last_name, Username, Email, Password, id_tipo FROM registro_usuarios ORDER BY Username ASC";
            // Condición que permitirá visualizar los datos registrados.
            else
                // Inserción.
                sql = "SELECT id, Name, Last_name, Username, Email, Password, id_tipo FROM registro_usuarios WHERE id LIKE '%" + data + "%' OR Name LIKE '%" + data + "%' OR Last_name LIKE '%" + data + "%' OR Username LIKE '%" + data + "%' OR Email LIKE '%" + data + "%' OR Password LIKE '%" + data + "%' OR id_tipo LIKE '%" + data + "%' ORDER BY Username ASC";

            try{
                MySqlConnection connection = SQLConnection.getConnection();
                connection.Open(); // Esta función permite abrir la conexión.

                // Se crea un objeto de la clase "MySqlCommand", enviandole como parámetros "sql" y "conexion".
                MySqlCommand command = new MySqlCommand(sql, connection);

                reader = command.ExecuteReader(); // ".ExecuteReader() es el método que permite realizar la lectura de datos.

                while (reader.Read()){
                    // Creación de un objeto de la clase "registro_usuarios".
                    registro_usuarios User = new registro_usuarios();

                    // Asignación de propiedades.
                    User.Id = Convert.ToInt32(reader.GetString(0));
                    User.Name1 = reader[1].ToString();
                    User.Last_name1 = reader[2].ToString();
                    User.Username1 = reader[3].ToString();
                    User.Email1 = reader[4].ToString();
                    User.Password1 = reader[5].ToString();
                    User.Id_tipo = Convert.ToInt32(reader[6].ToString());

                    List.Add(User); // Los datos que se leyeron se agregarán a la lista.
                }
            }
            // "catch" que sólo se activará si se presenta algún tipo de excepción de MySql.
            catch (MySqlException ex){
                Console.WriteLine(ex.Message.ToString()); // Mensaje de error.
            }

            return List; // Retorno de la lista.
        }

        // <--- Método #9: Asignación de las actividades registradas en la base de datos a una lista ---> //
        public List<object> ActQuery(string act){
            // Este objeto permite leer todos los datos que se encuentren en la base de datos.
            MySqlDataReader reader;
            // Creación d eun objeto de la clase "Lista<Object>".
            List<object> List = new List<object>();
            // Variable para inserción a Sql.
            string sql;

            /*
               La inserción a "MySQL" es la siguiente:
               De la tabla "actividades" se seleccionan dichos campos, en donde el "id" sea igual al "id" 
               del usuario que tiene la sesión activa.
            */
            if (act == null) sql = "SELECT id, Name, Type, Start, End, id_Username FROM actividades WHERE id_Username LIKE '" + Session.id + "'";
            else sql = "SELECT id, Name, Type, Start, End, id_Username FROM actividades WHERE id_Username LIKE '" + Session.id + "'";

            try{
                MySqlConnection connection = SQLConnection.getConnection();
                connection.Open(); // Esta función permite abrir la conexión.

                // Se crea un objeto de la clase "MySqlCommand", enviandole como parámetros "sql" y "conexion".
                MySqlCommand command = new MySqlCommand(sql, connection);

                reader = command.ExecuteReader(); // ".ExecuteReader() es el método que permite realizar la lectura de datos.

                while (reader.Read()){
                    // Creación de un objeto de la clase "actividades".
                    actividades Activity = new actividades();

                    // Asignación de propiedades.
                    Activity.Id = Convert.ToInt32(reader.GetString(0));
                    Activity.Name1 = reader[1].ToString();
                    Activity.Type1 = reader[2].ToString();
                    Activity.Start1 = Convert.ToDateTime(reader.GetString(3));
                    Activity.End1 = Convert.ToDateTime(reader.GetString(4));
                    Activity.Id_Username = Convert.ToInt32(reader.GetString(5));

                    List.Add(Activity); // Los datos que se leyeron se agregarán a la lista.
                }
            }
            // "catch" que sólo se activará si se presenta algún tipo de excepción de MySql.
            catch (MySqlException ex){
                Console.WriteLine(ex.Message.ToString()); // Mensaje de error.
            }

            return List; // Retorno de la lista.
        }
    }
}
