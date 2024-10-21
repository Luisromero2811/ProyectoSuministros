using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoSuministros.Server.Migrations
{
    /// <inheritdoc />
    public partial class RolAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles(Id, Name, NormalizedName)
                                Values('fd330377-fd06-4c6b-9446-aaba6da303e3','admin','ADMIN')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles WHERE Id = 'fd330377-fd06-4c6b-9446-aaba6da303e3'");
        }
    }
}
