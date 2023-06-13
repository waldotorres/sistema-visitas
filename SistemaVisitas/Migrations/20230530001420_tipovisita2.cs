using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVisitas.Migrations
{
    /// <inheritdoc />
    public partial class tipovisita2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoCita",
                table: "Citas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoCita",
                table: "Citas");
        }
    }
}
