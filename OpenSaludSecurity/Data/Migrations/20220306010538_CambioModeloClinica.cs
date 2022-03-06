using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSaludSecurity.Data.Migrations
{
    public partial class CambioModeloClinica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreClinica",
                table: "Clinica",
                newName: "Nombre");

            migrationBuilder.AlterColumn<string>(
                name: "IdRepresentante",
                table: "Clinica",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Clinica",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clinica",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Clinica",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Clinica");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clinica");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Clinica");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Clinica",
                newName: "NombreClinica");

            migrationBuilder.AlterColumn<int>(
                name: "IdRepresentante",
                table: "Clinica",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
