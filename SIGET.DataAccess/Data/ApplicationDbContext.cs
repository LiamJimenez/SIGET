using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SIGET.Models;
using SIGET.Models.ViewModel;

namespace SIGET.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Colaboradores> colaboradores { get; set; }
        public DbSet<ComponentesFisicos> componentesfisicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Colaboradores>().HasData(
                new Colaboradores
                {
                    Id = 1,
                    Nombre = "Liam",
                    Direccion = "Mi direccion",
                    Telefono = "809-899-8828",
                    Correo = "correo@gmail.com",
                    Area = "Tecnologia",
                    ImageUrl = "",
                }
                );
            modelBuilder.Entity<ComponentesFisicos>().HasData(
                new ComponentesFisicos
                {
                    Id = 1,
                    Nombre = "Disco Duro SSD",
                    Cantidad = 20,
                    PuntoReabastecimiento = 5,
                    ImageUrl = "",
                    Descripcion = "Este es un Disco Duro SSD",
                },
                new ComponentesFisicos
                {
                    Id = 2,
                    Nombre = "Otro equipo",
                    Cantidad = 50,
                    PuntoReabastecimiento = 5,
                    ImageUrl = "",
                    Descripcion = "Este es otro producto",
                }
                );

        }
    }
}