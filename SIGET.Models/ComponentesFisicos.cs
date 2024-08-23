using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGET.Models
{
    public class ComponentesFisicos
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
        public string ImageUrl { get; set; }
        [Required]
        public string Descripcion { get; set; }
    }
}
