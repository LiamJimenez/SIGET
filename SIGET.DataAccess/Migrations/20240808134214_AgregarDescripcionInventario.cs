using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDescripcionInventario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventario",
                table: "Inventario");

            migrationBuilder.RenameTable(
                name: "Inventario",
                newName: "inventario");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "inventario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventario",
                table: "inventario",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "inventario",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descripcion",
                value: "Este es un Disco Duro SSD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_inventario",
                table: "inventario");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "inventario");

            migrationBuilder.RenameTable(
                name: "inventario",
                newName: "Inventario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventario",
                table: "Inventario",
                column: "Id");
        }
    }
}
