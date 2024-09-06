using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGET.Models
{
    public class Colaboradores
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Area { get; set; }
        [ValidateNever]
        [Display(Name = "Imagen")]
        public string? ImageUrl { get; set; }

        public int ComputadoresId { get; set; }
        [ForeignKey("ComputadoresId")]
        [ValidateNever]
        public Computadores Computadores { get; set; }
    }
}