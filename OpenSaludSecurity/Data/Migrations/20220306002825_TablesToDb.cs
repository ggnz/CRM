using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSaludSecurity.Data.Migrations
{
    public partial class TablesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clinica",
                columns: table => new
                {
                    IdClinica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreClinica = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IdRepresentante = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinica", x => x.IdClinica);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Ap1Usuario = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Ap2Usuario = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    CorreoUsuario = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    TelefonoUsuario = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    DireccionUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FecNacUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    IdMedico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreMedico = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Ap1Medico = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Ap2Medico = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Categoria = table.Column<int>(type: "int", nullable: false),
                    ClinicaIdClinica = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.IdMedico);
                    table.ForeignKey(
                        name: "FK_Medico_Clinica_ClinicaIdClinica",
                        column: x => x.ClinicaIdClinica,
                        principalTable: "Clinica",
                        principalColumn: "IdClinica",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calificacion",
                columns: table => new
                {
                    IdCalificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comentario = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Estrellas = table.Column<int>(type: "int", nullable: false),
                    ClinicaIdClinica = table.Column<int>(type: "int", nullable: true),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificacion", x => x.IdCalificacion);
                    table.ForeignKey(
                        name: "FK_Calificacion_Clinica_ClinicaIdClinica",
                        column: x => x.ClinicaIdClinica,
                        principalTable: "Clinica",
                        principalColumn: "IdClinica",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calificacion_Usuario_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cita",
                columns: table => new
                {
                    IdCita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescripcionCita = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    FechaCita = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    ClinicaIdClinica = table.Column<int>(type: "int", nullable: true),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: true),
                    MedicoIdMedico = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cita", x => x.IdCita);
                    table.ForeignKey(
                        name: "FK_Cita_Clinica_ClinicaIdClinica",
                        column: x => x.ClinicaIdClinica,
                        principalTable: "Clinica",
                        principalColumn: "IdClinica",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cita_Medico_MedicoIdMedico",
                        column: x => x.MedicoIdMedico,
                        principalTable: "Medico",
                        principalColumn: "IdMedico",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cita_Usuario_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_ClinicaIdClinica",
                table: "Calificacion",
                column: "ClinicaIdClinica");

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_UsuarioIdUsuario",
                table: "Calificacion",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_ClinicaIdClinica",
                table: "Cita",
                column: "ClinicaIdClinica");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_MedicoIdMedico",
                table: "Cita",
                column: "MedicoIdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_UsuarioIdUsuario",
                table: "Cita",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Medico_ClinicaIdClinica",
                table: "Medico",
                column: "ClinicaIdClinica");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificacion");

            migrationBuilder.DropTable(
                name: "Cita");

            migrationBuilder.DropTable(
                name: "Medico");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Clinica");
        }
    }
}
