using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGET.DataAccess.Migrations
{
    public partial class AgregarComputadorIdAColaborador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComputadoresId",
                table: "colaboradores",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "colaboradores",
                keyColumn: "Id",
                keyValue: 1,
                column: "ComputadoresId",
                value: 2);

            migrationBuilder.InsertData(
                table: "colaboradores",
                columns: new[] { "Id", "Area", "ComputadoresId", "Correo", "Direccion", "ImageUrl", "Nombre", "Telefono" },
                values: new object[] { 2, "Tecnologia", 3, "Juan@gmail.com", "Juan direccion", "", "Juan", "809-899-8828" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "colaboradores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "ComputadoresId",
                table: "colaboradores");
        }
    }
}
