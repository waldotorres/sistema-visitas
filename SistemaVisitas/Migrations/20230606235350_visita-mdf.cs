using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVisitas.Migrations
{
    /// <inheritdoc />
    public partial class visitamdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_AspNetUsers_IdUsuarioAnulacion",
                table: "Visitas");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Locales_IdLocal",
                table: "Visitas");

            migrationBuilder.RenameColumn(
                name: "IdUsuarioAnulacion",
                table: "Visitas",
                newName: "IdUsuarioModificacion");

            migrationBuilder.RenameIndex(
                name: "IX_Visitas_IdUsuarioAnulacion",
                table: "Visitas",
                newName: "IX_Visitas_IdUsuarioModificacion");

            migrationBuilder.AlterColumn<int>(
                name: "IdLocal",
                table: "Visitas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_AspNetUsers_IdUsuarioModificacion",
                table: "Visitas",
                column: "IdUsuarioModificacion",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Locales_IdLocal",
                table: "Visitas",
                column: "IdLocal",
                principalTable: "Locales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_AspNetUsers_IdUsuarioModificacion",
                table: "Visitas");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Locales_IdLocal",
                table: "Visitas");

            migrationBuilder.RenameColumn(
                name: "IdUsuarioModificacion",
                table: "Visitas",
                newName: "IdUsuarioAnulacion");

            migrationBuilder.RenameIndex(
                name: "IX_Visitas_IdUsuarioModificacion",
                table: "Visitas",
                newName: "IX_Visitas_IdUsuarioAnulacion");

            migrationBuilder.AlterColumn<int>(
                name: "IdLocal",
                table: "Visitas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_AspNetUsers_IdUsuarioAnulacion",
                table: "Visitas",
                column: "IdUsuarioAnulacion",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Locales_IdLocal",
                table: "Visitas",
                column: "IdLocal",
                principalTable: "Locales",
                principalColumn: "Id");
        }
    }
}
