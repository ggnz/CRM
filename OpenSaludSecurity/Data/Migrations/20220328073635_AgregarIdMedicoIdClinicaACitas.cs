using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSaludSecurity.Data.Migrations
{
    public partial class AgregarIdMedicoIdClinicaACitas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdClinica",
                table: "Cita",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdMedico",
                table: "Cita",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdClinica",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "IdMedico",
                table: "Cita");
        }
    }
}
