using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ValoresDefaultServicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "componentesfisicos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Cantidad", "Descripcion", "Nombre", "PuntoReabastecimiento" },
                values: new object[] { 0, "", "Ninguna", 0 });

            migrationBuilder.UpdateData(
                table: "licencias",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Ninguna");

            migrationBuilder.InsertData(
                table: "servicios",
                columns: new[] { "Id", "ComponentesFisicosId", "Descripcion", "ImageUrl", "LicenciasId", "Nombre", "Precio" },
                values: new object[] { 1, 2, "Descripscion", "", 1, "Nuevo Servicio", 100 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "componentesfisicos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Cantidad", "Descripcion", "Nombre", "PuntoReabastecimiento" },
                values: new object[] { 20, "Este es un Disco Duro SSD", "Disco Duro SSD", 5 });

            migrationBuilder.UpdateData(
                table: "licencias",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Liam");
        }
    }
}
