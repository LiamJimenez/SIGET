using System.ComponentModel.DataAnnotations;

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
    }
}
