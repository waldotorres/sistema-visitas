using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVisitas.Migrations
{
    /// <inheritdoc />
    public partial class visitafechas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdUsuarioAnulacion",
                table: "Visitas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaModificacion",
                table: "Visitas",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsuarioAnulacion",
                table: "Visitas");

            migrationBuilder.DropColumn(
                name: "fechaModificacion",
                table: "Visitas");
        }
    }
}
