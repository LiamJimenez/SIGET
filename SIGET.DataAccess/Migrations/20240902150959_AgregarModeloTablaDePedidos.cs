using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGET.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AgregarModeloTablaDePedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColaboradoresId = table.Column<int>(type: "int", nullable: true),
                    ServiciosId = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pedidos_colaboradores_ColaboradoresId",
                        column: x => x.ColaboradoresId,
                        principalTable: "colaboradores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_pedidos_servicios_ServiciosId",
                        column: x => x.ServiciosId,
                        principalTable: "servicios",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "pedidos",
                columns: new[] { "Id", "Cantidad", "ColaboradoresId", "ServiciosId" },
                values: new object[] { 1, 10, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_ColaboradoresId",
                table: "pedidos",
                column: "ColaboradoresId");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_ServiciosId",
                table: "pedidos",
                column: "ServiciosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pedidos");
        }
    }
}
