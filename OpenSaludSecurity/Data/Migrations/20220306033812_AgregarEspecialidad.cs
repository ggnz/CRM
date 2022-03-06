using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSaludSecurity.Data.Migrations
{
    public partial class AgregarEspecialidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Categoria",
                table: "Medico",
                newName: "Especialidad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Especialidad",
                table: "Medico",
                newName: "Categoria");
        }
    }
}
