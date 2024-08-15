using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AgregarImageUrlEnTablaLicencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "licencias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "licencias",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "licencias");
        }
    }
}
