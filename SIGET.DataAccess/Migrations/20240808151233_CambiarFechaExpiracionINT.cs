using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CambiarFechaExpiracionINT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FechaExpiracion",
                table: "inventario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "inventario",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaExpiracion",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaExpiracion",
                table: "inventario",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "inventario",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaExpiracion",
                value: null);
        }
    }
}
