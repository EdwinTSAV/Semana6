using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Name { get; set; }
        public string Currency { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        public string Image { get; set; }
        public int UserId { get; set; }
        public decimal? Limite { get; set; }
        //relaciones
        public Tipos Tipo { get; set; }
        public List<Transaccion> Transaccions { get; set; }
    }
}
