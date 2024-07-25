using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarColaboradoresId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "colaboradores",
                keyColumn: "Id",
                keyValue: 1,
                column: "ColaboradoresId",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "colaboradores",
                keyColumn: "Id",
                keyValue: 1,
                column: "ColaboradoresId",
                value: 0);
        }
    }
}
