using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SIGET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablaComponentesFisicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "componentesfisicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PuntoReabastecimiento = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_componentesfisicos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "componentesfisicos",
                columns: new[] { "Id", "Cantidad", "Descripcion", "ImageUrl", "Nombre", "PuntoReabastecimiento" },
                values: new object[,]
                {
                    { 1, 20, "Este es un Disco Duro SSD", "", "Disco Duro SSD", 5 },
                    { 2, 50, "Este es otro producto", "", "Otro equipo", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "componentesfisicos");

            migrationBuilder.CreateTable(
                name: "inventario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaRenovacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PuntoReabastecimiento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventario", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "inventario",
                columns: new[] { "Id", "Cantidad", "Descripcion", "FechaExpiracion", "FechaRenovacion", "ImageUrl", "Nombre", "PuntoReabastecimiento" },
                values: new object[,]
                {
                    { 1, 20, "Este es un Disco Duro SSD", null, null, "", "Disco Duro SSD", 5 },
                    { 2, 50, "Este es otro producto", null, null, "", "Otro equipo", 5 }
                });
        }
    }
}
