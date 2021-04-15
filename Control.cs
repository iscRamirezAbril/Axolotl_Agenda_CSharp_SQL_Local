﻿using MySql.Data.MySqlClient; // Librería que nos permitirá conectarnos a las bases de dato
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    /*
       Esta clase nos ayudará a validar todos aquellos errores posibles que se puedan presentar en los formularios
       que se conectan a la base de datos de "Sql".
    */
    class Control{
        // <---------------------------------------> //
        // <---------- MÉTODOS / METHODS ----------> //
        // <---------------------------------------> //

        // <--- Método #1: Retornar mensajes de error para el formulario de registro. ---> //
        public string ctrlRegister(registro_usuarios user){ // Recibe como parámetro una variable de tipo "registro_usuarios".
            Model model = new Model(); // Se crea una instancia de la clase "Model".

            // Declaración de variable de tipo string que almacenará los mensajes de error que correspondan.
            string errorMessage = "";

            // <--- Validación #1: Llenar todos los campos de formulario de registro. ---> //
            // Condición que se activará sí y sólo sí alguno de los campos están vacíos.
            /*
               ".IsNullOrEmpty() verifica que el campo esté vacío o sea "nulo". de ser así, la condición se cumple.
            */
            if (string.IsNullOrEmpty(user.Name1) || string.IsNullOrEmpty(user.Last_name1) ||
                string.IsNullOrEmpty(user.Username1) || string.IsNullOrEmpty(user.Password1) ||
                string.IsNullOrEmpty(user.ConPassword1) || string.IsNullOrEmpty(user.Email1))
                errorMessage = "Debe llenar todos los campos"; // Mensaje de error.

            // <--- Validación #2: Revisión de coincidencia en contraseñas. ---> //
            else{
                // Condición que hace una revisión de las contraseñas, confirmando que coincidan.
                if (user.Password1 == user.ConPassword1){
                    // Mensaje de error que se mostrará sólo si el nombre de usuario insertado ya existe.
                    if (model.existUser(user.Username1)) errorMessage = "El nombre de usuario ya existe. \nFavor de elegir otro nombre...";
                    else{
                        /*
                          La contraseña insertada por el usuario se encriptará (para eso se llama al método
                          de nombre "Encrypt") y lo asignará a la variable "Password1" del objeto "user".
                        */
                        user.Password1 = Encrypt(user.Password1);
                        /*
                           Los usuarios que se registren serán solamente "Usuarios", ya que:
                           id 1 = "Administrador".
                           id 2 = "Usuario".
                        */
                        user.Id_tipo = 2;

                        /*
                           Se llama al método "registro" mediante un objeto correspondiante a la clase "Model", 
                           al cual se le enviará el objeto "user", ya que, ese objeto contiene todos los datos registrados por el usuario.
                        */
                        model.register(user);
                    }
                }
                else
                    // Mensaje de error que se mostrará si las contraseñas no coinciden.
                    errorMessage = "Las contraseñas no coinciden. \nFavor de interntarlo nuevamente =).";
            }
            return errorMessage; // Retorno del mensaje de error.
        }

        // <--- Método #2: Retornar mensajes de error para el formulario de Control de Usuarios (Sólo para administradores). ---> //
        public string ctrlregisterAdmins(registro_usuarios userAdmin){ // Recibe como parámetro una variable de tipo "registro_usuarios".
            Model model = new Model(); // Se crea una instancia de la clase "Model".

            // Declaración de variable. Esta almacenará los mensajes de error que correspondan.
            string errorMessage = "";

            // <--- Validación #1: Llenar todos los campos de formulario de registro. ---> //
            // Condición que se activará sí y sólo sí alguno de los campos están vacíos.
            /*
               ".IsNullOrEmpty() verifica que el campo esté vacío o sea "nulo". de ser así, la condición se cumple.
            */
            if (string.IsNullOrEmpty(userAdmin.Name1) || string.IsNullOrEmpty(userAdmin.Last_name1) ||
                string.IsNullOrEmpty(userAdmin.Username1) || string.IsNullOrEmpty(userAdmin.Password1)
                || string.IsNullOrEmpty(userAdmin.Email1))
                errorMessage = "Debe llenar todos los campos"; // Mensaje de error.

            // <--- Validación #2: Revisión de usuario existente. ---> //
            else{
                // Mensaje de error que se mostrará sólo si el nombre de usuario insertado ya existe.
                if (model.existUser(userAdmin.Username1)) errorMessage = "El nombre de usuario ya existe. \nFavor de elegir otro nombre...";
                else{
                    /*
                        La contraseña insertada por el usuario se encriptará (para eso se llama al método
                        de nombre "Encypt") y lo asignará a la variable "Password1" del objeto "userAdmin".
                    */
                    userAdmin.Password1 = Encrypt(userAdmin.Password1);
                    /*
                        Los usuarios que se registren serán solamente "Administradores".
                    */
                    userAdmin.Id_tipo = 1;

                    /*
                        Se llama al método "registro" de la clase "model" que se le enviará el objeto "userAdmin",
                        ya que, ese objeto contiene todos los datos registrados por el usuario.
                    */
                    model.register(userAdmin);
                }
            }
            return errorMessage; // Retorno del mensaje de error.
        }

        // <--- Método #3: Comprobación de usuario existente para el inicio de sesión. ---> //
        public string crtlLogin(string username, string password){ // Recibe como parámetros 2 variables de tipo "string": "usuario" y "password".
            Model model = new Model(); // Se crea una instancia de la clase "Model".

            // Declaración de variable. Esta almacenará los mensajes de error que correspondan.
            string errorMessage = "";
            registro_usuarios userData = null;

            // <--- Validación #1: Verificar que los campos estén vacíos. ---> //
            // Condición que solo es válida sí y sólo sí el usuario no ha insertado su "username" y "password".
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) errorMessage = "Debe llenar todos los campos..."; // Mensaje de error.

            // <--- Validación #2: Traer datos de Sql a mi consulta. ---> //
            else{
                // El dato que se recupere de la base de datos se le asignará a la variable "datosUsuario".
                /*
                   Para eso se hace la llamada al método "LoginUsuario()", que se encarga de hacer las consultas a la
                   base de datos "registro_usuarios", enviandole como parámetro la variable "username".
                */
                userData = model.LoginUser(username);

                // <--- Validación #3: Usuario no registrado.. ---> //
                // Condición que sólo sea válida sí y sólo sí el usuario no se encuentra o no existe.
                if (userData == null) errorMessage = "El usuario no existe."; // Mensaje de error.

                // <--- Validación #3: Usuario registrado. ---> //
                else{
                    /* 
                       Condición que sólo sea válida sí y sólo sí la contraseña registrada en la base de datos (Base 64)
                       es diferente a la contraseña ingresada por el usuario. 
                       Como la contraseña que ingresa el usuario no está en formato "Base 64", se llamará al método "Encrypt"
                       para realizar dicha conversión, permitiendo que el método funcione correctamente.
                    */
                    if (userData.Password1 != Encrypt(password)) errorMessage = "El usuario y/o contraseñas no coinciden..."; // Mensaje de error.

                    // <--- Validación #4: Datos de usuario que está iniciando sesión. ---> //
                    else{
                        /*
                           Asignación de la propiedad de la clase "registro_usuarios" al objeto "datosUsuario"
                           a las variables de la clase "Session".
                        */
                        Session.id = userData.Id;
                        Session.name = userData.Name1;
                        Session.last_name = userData.Last_name1;
                        Session.email = userData.Email1;
                        Session.password = userData.Password1;
                        Session.id_tipo = userData.Id_tipo;

                        /*
                           A este objeto se le asigna la variable "username" directamente ya que se envía como
                           parámetro al método "ctrlLogin()".
                        */
                        Session.username = username;
                    }
                }
            }
            return errorMessage; // Retorno del mensaje de error.
        }

        // <--- Método #4: Recuperación de contraseña. ---> //
        public string ctrlRecoverPassword(string username, string email, string emailpass){ // Recibe como parámetros 3 variables de tipo "string", que son "username", "email", "emailpass".
            Model model = new Model(); // Se crea una instancia de la clase "Model".

            // Declaración de variable. Esta almacenará los mensajes de error que correspondan.
            string errorMessage = "";
            registro_usuarios userData = null;

            // <--- Validación #1: Verificar que los campos estén vacíos. ---> //
            // Condición que solo es válida sí y sólo sí el usuario no ha insertado su "username" y "password".
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(emailpass))
                errorMessage = "Debe llenar todos los campos..."; // Mensaje de error.

            // <--- Validación #2: Traer datos de Sql a mi consulta. ---> //
            else{
                // El dato que se recupere de la base de datos se le asignará a la variable "userData".
                /*
                   Para eso se hace la llamada al método "RecoverPass()", que se encarga de recuperar la contraseña que
                   se encuentre registrada en la base de datos.
                */
                userData = model.RecoverPass(username);

                // <--- Validación #3: Usuario no registrado.. ---> //
                // Condición que sólo sea válida sí y sólo sí el usuario no se encuentra o no existe.
                if (userData == null) errorMessage = "Lo sentimos, no tenemos a algún usuario registrado con ese nombre."; // Mensaje de error.

                else{
                    // A las propiedades "username" y "password" se les asigna los datos correspondientes.
                    Session.username = username;
                    /*
                       Para la contraseña, se llama al método "Desencypt", debido a que la contraseña que se encuentra registrada
                       en la base de datos, está encriptada.
                    */
                    Session.password = Desencrypt(userData.Password1);
                }
            }
            return errorMessage; // Retorno del mensaje de error.
        }

        // <--- Método #5: Retornar mensajes de error en el formulario de registro de actividades. ---> //
        public string ctrlActLog(actividades activity){ // Recibe como parámetro una variable del tipo "actividades".
            Model model = new Model(); // Objeto de la clase "Model".

            // Declaración de variable. Esta almacenará los mensajes de error que correspondan.
            string errorMessage = "";

            // <--- Validación #1: Llenar todos los campos de formulario de registro. ---> //
            // Condición que se activará sí y sólo sí alguno de los campos están vacíos.
            /*
               ".IsNullOrEmpty() verifica que el campo esté vacío o sea "nulo". de ser así, la condición se cumple.
            */
            if (string.IsNullOrEmpty(activity.Name1) || string.IsNullOrEmpty(activity.Type1) || string.IsNullOrEmpty(Convert.ToString(activity.Start1))
                || string.IsNullOrEmpty(Convert.ToString(activity.End1)))
                errorMessage = "Debe llenar todos los campos."; // Mensaje de error.
            else{
                activity.Id_Username = Session.id; // A la propiedad "Id_Username" se le asignará el "Id" del usuario que tiene su sesión iniciada.
                /*
                  Se llama al método "registro" de la clase "model" que se le enviará el objeto "usuario",
                  ya que, ese objeto contiene todos los datos registrados por el usuario.
                */
                model.newAct(activity);
            }

            return errorMessage; // Retorno del mensaje de error.
        }

        // <--- Método #6: Encriptar contraseñas. ---> //
        public string Encrypt(string userPassword){ // Recibe como parámetro una variable de tipo "string" de nombre "userPassword".
            byte[] encryted = Encoding.Unicode.GetBytes(userPassword); // Obtiene una matriz de tipo bytes que representa la cadena unicode, dos por cada carácter.
            userPassword = Convert.ToBase64String(encryted); // A la variable cadena se le asigna la matriz enriptada a "Base 64".
            return userPassword; // Retorno de la contraseña encriptada.
        }

        // <--- Método #7: Desencriptar contraseñas. ---> //
        /*
           Este método es muy parecido al "Método #4", lo único distinto es que realiza el proceso inverso de dicho método.
        */
        public string Desencrypt(string encyptPass){ // Recibe como parámetro una variable de tipo "string" de nombre "encyptPass".
            byte[] decryted = Convert.FromBase64String(encyptPass);
            encyptPass = Encoding.Unicode.GetString(decryted);
            return encyptPass; // Retorno de la contraseña desencriptada.
        }
    }
}
