using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablaInventario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PuntoReabastecimiento = table.Column<int>(type: "int", nullable: false),
                    FechaExpiracion = table.Column<int>(type: "int", nullable: false),
                    FechaRenovacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Inventario",
                columns: new[] { "Id", "Cantidad", "FechaExpiracion", "FechaRenovacion", "ImageUrl", "Nombre", "PuntoReabastecimiento" },
                values: new object[] { 1, 20, 0, null, "", "Disco Duro SSD", 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventario");
        }
    }
}
