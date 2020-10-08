using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        public int CuentaId { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; }
        public decimal? Amount { get; set; }

    }
}
