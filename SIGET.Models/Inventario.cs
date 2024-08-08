using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGET.Models
{
    public class Inventario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public int PuntoReabastecimiento { get; set; }

        [Required]
        public int FechaExpiracion { get; set; }

        [Required]
        public string Descripcion { get; set; }
        public DateTime? FechaRenovacion { get; set; }
        public string ImageUrl { get; set; }
    }
}