using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SIGET.Models;

namespace SIGET.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Colaboradores> colaboradores { get; set; }

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
        }
    }
}
