using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVisitas.Migrations
{
    /// <inheritdoc />
    public partial class clientemodificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Ciudades_CiudadId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_CiudadId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CiudadId",
                table: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "CiudadRegion",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CiudadRegion",
                table: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "CiudadId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CiudadId",
                table: "Clientes",
                column: "CiudadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Ciudades_CiudadId",
                table: "Clientes",
                column: "CiudadId",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
