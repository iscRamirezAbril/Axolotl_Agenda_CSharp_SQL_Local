using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{ 
    /*
       Clase que corresponde a la tabla de "MySQL" de nombre "registro_usuarios".
    */
    public class registro_usuarios{
        // Se declaran las variables que corresponden a los campos de la tabla "registro_usuarios" de "MySQL".
        // Se declararon de acuerdo al tipo de dato que corresponden en "MySql".
        int id, id_tipo;
        public string Name, Last_name, Username, Email, Password, ConPassword;

        // Referencias para retornar cada una de las propiedades.
        public string Name1 { get => Name; set => Name = value; }
        public string Last_name1 { get => Last_name; set => Last_name = value; }
        public string Username1 { get => Username; set => Username = value; }
        public string Email1 { get => Email; set => Email = value; }
        public string Password1 { get => Password; set => Password = value; }
        public string ConPassword1 { get => ConPassword; set => ConPassword = value; }
        public int Id { get => id; set => id = value; }
        public int Id_tipo { get => id_tipo; set => id_tipo = value; }
    }
}
