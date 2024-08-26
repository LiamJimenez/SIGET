using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CorrecionDeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_servicios_componentesfisicos_ComponentesFisicosId",
                table: "servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_servicios_licencias_LicenciasId",
                table: "servicios");

            migrationBuilder.AlterColumn<int>(
                name: "LicenciasId",
                table: "servicios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentesFisicosId",
                table: "servicios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "componentesfisicos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Descripcion", "Nombre" },
                values: new object[] { "Descrpcion", "Disco SSD" });

            migrationBuilder.UpdateData(
                table: "licencias",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Office 360");

            migrationBuilder.UpdateData(
                table: "servicios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ComponentesFisicosId", "LicenciasId" },
                values: new object[] { 1, null });

            migrationBuilder.AddForeignKey(
                name: "FK_servicios_componentesfisicos_ComponentesFisicosId",
                table: "servicios",
                column: "ComponentesFisicosId",
                principalTable: "componentesfisicos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_servicios_licencias_LicenciasId",
                table: "servicios",
                column: "LicenciasId",
                principalTable: "licencias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_servicios_componentesfisicos_ComponentesFisicosId",
                table: "servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_servicios_licencias_LicenciasId",
                table: "servicios");

            migrationBuilder.AlterColumn<int>(
                name: "LicenciasId",
                table: "servicios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ComponentesFisicosId",
                table: "servicios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "componentesfisicos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Descripcion", "Nombre" },
                values: new object[] { "", "Ninguna" });

            migrationBuilder.UpdateData(
                table: "licencias",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Ninguna");

            migrationBuilder.UpdateData(
                table: "servicios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ComponentesFisicosId", "LicenciasId" },
                values: new object[] { 2, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_servicios_componentesfisicos_ComponentesFisicosId",
                table: "servicios",
                column: "ComponentesFisicosId",
                principalTable: "componentesfisicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_servicios_licencias_LicenciasId",
                table: "servicios",
                column: "LicenciasId",
                principalTable: "licencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
