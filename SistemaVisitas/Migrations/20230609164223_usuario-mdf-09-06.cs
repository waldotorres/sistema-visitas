using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVisitas.Migrations
{
    /// <inheritdoc />
    public partial class usuariomdf0906 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoUsuario",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoUsuario",
                table: "AspNetUsers");
        }
    }
}
