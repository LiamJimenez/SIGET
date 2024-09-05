using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SIGET.DataAccess.Migrations
{
    public partial class AgregarTablaComputador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "computadores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_computadores", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "computadores",
                columns: new[] { "Id", "Nombre", "Ip" },
                values: new object[,]
                {
                    { 1, "LMJP003", "1.1.1" },
                    { 2, "BBB111", "123.44.14.20" },
                    { 3, "KKK444", "682.60.14.28" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "computadores");
        }
    }
}
