using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSaludSecurity.Data.Migrations
{
    public partial class CalificacionNuevasColumnas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificacion_Clinica_ClinicaIdClinica",
                table: "Calificacion");

            migrationBuilder.DropIndex(
                name: "IX_Calificacion_ClinicaIdClinica",
                table: "Calificacion");

            migrationBuilder.DropColumn(
                name: "ClinicaIdClinica",
                table: "Calificacion");

            migrationBuilder.AddColumn<int>(
                name: "ClinicaRefId",
                table: "Calificacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IdUsuario",
                table: "Calificacion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_ClinicaRefId",
                table: "Calificacion",
                column: "ClinicaRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacion_Clinica_ClinicaRefId",
                table: "Calificacion",
                column: "ClinicaRefId",
                principalTable: "Clinica",
                principalColumn: "IdClinica",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificacion_Clinica_ClinicaRefId",
                table: "Calificacion");

            migrationBuilder.DropIndex(
                name: "IX_Calificacion_ClinicaRefId",
                table: "Calificacion");

            migrationBuilder.DropColumn(
                name: "ClinicaRefId",
                table: "Calificacion");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Calificacion");

            migrationBuilder.AddColumn<int>(
                name: "ClinicaIdClinica",
                table: "Calificacion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_ClinicaIdClinica",
                table: "Calificacion",
                column: "ClinicaIdClinica");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacion_Clinica_ClinicaIdClinica",
                table: "Calificacion",
                column: "ClinicaIdClinica",
                principalTable: "Clinica",
                principalColumn: "IdClinica",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
