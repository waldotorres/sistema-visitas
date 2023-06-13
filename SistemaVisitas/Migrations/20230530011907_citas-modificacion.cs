using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVisitas.Migrations
{
    /// <inheritdoc />
    public partial class citasmodificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaCita",
                table: "Citas",
                newName: "FechaHoraInicioCita");

            migrationBuilder.AddColumn<int>(
                name: "EstadoLocal",
                table: "Locales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaHoraFinCita",
                table: "Citas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoLocal",
                table: "Locales");

            migrationBuilder.DropColumn(
                name: "FechaHoraFinCita",
                table: "Citas");

            migrationBuilder.RenameColumn(
                name: "FechaHoraInicioCita",
                table: "Citas",
                newName: "FechaCita");
        }
    }
}
