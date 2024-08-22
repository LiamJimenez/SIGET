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
        public int ComponentesFisicosId { get; set; }

        [ValidateNever]
        public ComponentesFisicos ComponentesFisicos { get; set; }

        public int LicenciasId { get; set; }
        [ValidateNever]
        public Licencias Licencias { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int Precio { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}