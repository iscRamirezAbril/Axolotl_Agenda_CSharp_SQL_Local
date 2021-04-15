using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    /*
       Clase que corresponde a la tabla de "MySQL" de nombre "activities".
    */
    public class actividades{
        // Se declaran las variables que corresponden a los campos de la tabla "activities" de "MySQL".
        // Se declararon de acuerdo al tipo de dato que corresponden en "MySql".
        int id, id_Username;
        public string Name, Type;
        DateTime Start, End;

        // Referencias para retornar cada una de las propiedades.
        public int Id { get => id; set => id = value; }
        public int Id_Username { get => id_Username; set => id_Username = value; }
        public string Name1 { get => Name; set => Name = value; }
        public string Type1 { get => Type; set => Type = value; }
        public DateTime Start1 { get => Start; set => Start = value; }
        public DateTime End1 { get => End; set => End = value; }
    }
}
