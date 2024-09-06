using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGET.Models
{
    public class Servicios
    {   
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        [Display(Name = "Elija componente")]
        public int? ComponentesFisicosId { get; set; } = null;
        [ValidateNever]
        public ComponentesFisicos ComponentesFisicos { get; set; }

        [Display(Name = "Elija licencia")]
        public int? LicenciasId { get; set; } = null;
        [ValidateNever]
        public Licencias Licencias { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int Precio { get; set; }
        [ValidateNever]
        [Display(Name = "Imagen")]
        public string ImageUrl { get; set; }
    }
}