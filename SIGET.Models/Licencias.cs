using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGET.Models
{
    public class Licencias
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaExpiracion { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaRenovacion { get; set; }
        // [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
