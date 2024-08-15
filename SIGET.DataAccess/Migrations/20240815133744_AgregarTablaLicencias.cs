using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablaLicencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "licencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRenovacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_licencias", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "licencias",
                columns: new[] { "Id", "FechaExpiracion", "FechaRenovacion", "Nombre" },
                values: new object[] { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Liam" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "licencias");
        }
    }
}
