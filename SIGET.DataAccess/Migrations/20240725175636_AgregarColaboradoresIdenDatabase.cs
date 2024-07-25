using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AgregarColaboradoresIdenDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColaboradoresId",
                table: "colaboradores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "colaboradores",
                keyColumn: "Id",
                keyValue: 1,
                column: "ColaboradoresId",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColaboradoresId",
                table: "colaboradores");
        }
    }
}
