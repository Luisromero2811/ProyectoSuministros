using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoSuministros.Server.Migrations
{
    /// <inheritdoc />
    public partial class RolFacturas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles(Id, Name, NormalizedName)
                                Values('3fcc214f-caa5-428e-89be-b6450f7ee30c','Gestor Facturas','GESTOR FACTURAS')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles WHERE Id = '3fcc214f-caa5-428e-89be-b6450f7ee30c'");
        }
    }
}
