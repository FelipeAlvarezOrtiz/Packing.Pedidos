using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Packing.Pedidos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    NombreBusqueda = table.Column<string>(type: "nvarchar(750)", maxLength: 750, nullable: false),
                    FormatoId = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    PresentacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Formatos_FormatoId",
                        column: x => x.FormatoId,
                        principalTable: "Formatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_Presentaciones_PresentacionId",
                        column: x => x.PresentacionId,
                        principalTable: "Presentaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_FormatoId",
                table: "Productos",
                column: "FormatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_GrupoId",
                table: "Productos",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_PresentacionId",
                table: "Productos",
                column: "PresentacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
