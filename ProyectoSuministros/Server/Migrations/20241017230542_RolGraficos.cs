using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoSuministros.Server.Migrations
{
    /// <inheritdoc />
    public partial class RolGraficos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles(Id, Name, NormalizedName)
                                Values('4085d6f0-9e4e-48c9-84fa-55f9e571c3f0','Lectura Graficas','LECTURA GRAFICAS')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles WHERE Id = '4085d6f0-9e4e-48c9-84fa-55f9e571c3f0'");
        }
    }
}
