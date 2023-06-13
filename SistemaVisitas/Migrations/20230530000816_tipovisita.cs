using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVisitas.Migrations
{
    /// <inheritdoc />
    public partial class tipovisita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_TiposCita_TipoVisitaId",
                table: "Citas");

            migrationBuilder.DropTable(
                name: "TiposCita");

            migrationBuilder.CreateTable(
                name: "TiposVisita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposVisita", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_TiposVisita_TipoVisitaId",
                table: "Citas",
                column: "TipoVisitaId",
                principalTable: "TiposVisita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_TiposVisita_TipoVisitaId",
                table: "Citas");

            migrationBuilder.DropTable(
                name: "TiposVisita");

            migrationBuilder.CreateTable(
                name: "TiposCita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposCita", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_TiposCita_TipoVisitaId",
                table: "Citas",
                column: "TipoVisitaId",
                principalTable: "TiposCita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
