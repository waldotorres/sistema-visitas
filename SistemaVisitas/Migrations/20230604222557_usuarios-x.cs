using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVisitas.Migrations
{
    /// <inheritdoc />
    public partial class usuariosx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodUsuario",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Visitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdTipoVisita = table.Column<int>(type: "int", nullable: false),
                    IdCita = table.Column<int>(type: "int", nullable: true),
                    IdLocal = table.Column<int>(type: "int", nullable: true),
                    FechHoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechHoraFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    EstadoVisita = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioAtencion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioAnulacion = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    fechaAnulacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitas_AspNetUsers_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitas_AspNetUsers_IdUsuarioAnulacion",
                        column: x => x.IdUsuarioAnulacion,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Visitas_AspNetUsers_IdUsuarioAtencion",
                        column: x => x.IdUsuarioAtencion,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Visitas_Citas_IdCita",
                        column: x => x.IdCita,
                        principalTable: "Citas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Visitas_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitas_Locales_IdLocal",
                        column: x => x.IdLocal,
                        principalTable: "Locales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Visitas_TiposVisita_IdTipoVisita",
                        column: x => x.IdTipoVisita,
                        principalTable: "TiposVisita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdCita",
                table: "Visitas",
                column: "IdCita");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdCliente",
                table: "Visitas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdLocal",
                table: "Visitas",
                column: "IdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdTipoVisita",
                table: "Visitas",
                column: "IdTipoVisita");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdUsuario",
                table: "Visitas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdUsuarioAnulacion",
                table: "Visitas",
                column: "IdUsuarioAnulacion");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdUsuarioAtencion",
                table: "Visitas",
                column: "IdUsuarioAtencion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visitas");

            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CodUsuario",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "AspNetUsers");
        }
    }
}
