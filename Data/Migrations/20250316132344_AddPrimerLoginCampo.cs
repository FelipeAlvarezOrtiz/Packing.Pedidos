using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Packing.Pedidos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimerLoginCampo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PrimerLogin",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimerLogin",
                table: "AspNetUsers");
        }
    }
}
