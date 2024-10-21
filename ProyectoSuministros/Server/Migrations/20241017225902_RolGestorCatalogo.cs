using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoSuministros.Server.Migrations
{
    /// <inheritdoc />
    public partial class RolGestorCatalogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles(Id, Name, NormalizedName)
                                Values('21ea7c4c-5272-471c-bbea-bbb2823c1c51','Gestor Catalogo','GESTOR CATALOGO')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles WHERE Id = '21ea7c4c-5272-471c-bbea-bbb2823c1c51'");
        }
    }
}
