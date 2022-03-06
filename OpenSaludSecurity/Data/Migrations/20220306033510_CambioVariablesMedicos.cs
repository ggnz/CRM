using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSaludSecurity.Data.Migrations
{
    public partial class CambioVariablesMedicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreMedico",
                table: "Medico",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Ap2Medico",
                table: "Medico",
                newName: "Apellido2");

            migrationBuilder.RenameColumn(
                name: "Ap1Medico",
                table: "Medico",
                newName: "Apellido1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Medico",
                newName: "NombreMedico");

            migrationBuilder.RenameColumn(
                name: "Apellido2",
                table: "Medico",
                newName: "Ap2Medico");

            migrationBuilder.RenameColumn(
                name: "Apellido1",
                table: "Medico",
                newName: "Ap1Medico");
        }
    }
}
