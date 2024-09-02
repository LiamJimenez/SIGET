using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGET.Models
{
    public class Pedidos
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Elija colaborador")]
        public int? ColaboradoresId { get; set; } = null;
        [ValidateNever]
        public Colaboradores Colaboradores { get; set; }

        [Display(Name = "Elija el servicio")]
        public int? ServiciosId { get; set; } = null;
        [ValidateNever]
        public Servicios Servicios { get; set; }

        [Required]
        public int Cantidad { get; set; }

    }
}
