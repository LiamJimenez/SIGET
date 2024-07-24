using Microsoft.EntityFrameworkCore;
using SIGET.Models;

namespace SIGET.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Colaboradores> colaboradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colaboradores>().HasData(
                new Colaboradores
                {
                    Id = 1,
                    Nombre = "Liam",
                    Direccion = "Mi direccion",
                    Telefono = "809-899-8828",
                    Correo = "correo@gmail.com",
                    Area = "Tecnologia",
                    ImageUrl = ""
                }
                );
        }
    }
}
