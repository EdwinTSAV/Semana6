using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Models
{
    public class Tipos
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // si es que lo vamos a llamar, es decir para ver las cuentas de este tipo
        //public List<Cuenta> Cuentas { get; set; } // hay momento en donde esto es o no necesario
    }
}
