using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVisitas.Migrations
{
    /// <inheritdoc />
    public partial class visitacorreccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Visitas",
                newName: "Observacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Observacion",
                table: "Visitas",
                newName: "Descripcion");
        }
    }
}
