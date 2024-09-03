using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AgregarIpEnTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "colaboradores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "colaboradores",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ip",
                value: "1.1.1");

            migrationBuilder.InsertData(
                table: "colaboradores",
                columns: new[] { "Id", "Area", "Correo", "Direccion", "ImageUrl", "Ip", "Nombre", "Telefono" },
                values: new object[] { 6, "Tecnologia", "Josue@gmail.com", "Direccion de Josue", "", "3.2.6", "Josue", "829-365-7824" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "colaboradores",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "colaboradores",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ip",
                value: "10.2.2");

            migrationBuilder.InsertData(
                table: "colaboradores",
                columns: new[] { "Id", "Area", "Correo", "Direccion", "ImageUrl", "Ip", "Nombre", "Telefono" },
                values: new object[] { 3, "Recursos Humanos", "Rodrigo@gmail.com", "Direccion de Rodrigo", "", "10.1.1", "Rodrigo", "829-584-9008" });
        }
    }
}
