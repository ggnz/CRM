using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSaludSecurity.Data.Migrations
{
    public partial class ArreglandoNombreColumnasCita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Medico_MedicoIdMedico",
                table: "Cita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_MedicoIdMedico",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "MedicoIdMedico",
                table: "Cita");

            migrationBuilder.RenameColumn(
                name: "IdMedico",
                table: "Cita",
                newName: "MedicoRefId");

            migrationBuilder.RenameColumn(
                name: "IdClinica",
                table: "Cita",
                newName: "ClinicaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_ClinicaRefId",
                table: "Cita",
                column: "ClinicaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_MedicoRefId",
                table: "Cita",
                column: "MedicoRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Clinica_ClinicaRefId",
                table: "Cita",
                column: "ClinicaRefId",
                principalTable: "Clinica",
                principalColumn: "IdClinica",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Medico_MedicoRefId",
                table: "Cita",
                column: "MedicoRefId",
                principalTable: "Medico",
                principalColumn: "IdMedico",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Clinica_ClinicaRefId",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Medico_MedicoRefId",
                table: "Cita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_ClinicaRefId",
                table: "Cita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_MedicoRefId",
                table: "Cita");

            migrationBuilder.RenameColumn(
                name: "MedicoRefId",
                table: "Cita",
                newName: "IdMedico");

            migrationBuilder.RenameColumn(
                name: "ClinicaRefId",
                table: "Cita",
                newName: "IdClinica");

            migrationBuilder.AddColumn<int>(
                name: "ClinicaIdClinica",
                table: "Cita",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicoIdMedico",
                table: "Cita",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cita_ClinicaIdClinica",
                table: "Cita",
                column: "ClinicaIdClinica");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_MedicoIdMedico",
                table: "Cita",
                column: "MedicoIdMedico");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Clinica_ClinicaIdClinica",
                table: "Cita",
                column: "ClinicaIdClinica",
                principalTable: "Clinica",
                principalColumn: "IdClinica",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Medico_MedicoIdMedico",
                table: "Cita",
                column: "MedicoIdMedico",
                principalTable: "Medico",
                principalColumn: "IdMedico",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
