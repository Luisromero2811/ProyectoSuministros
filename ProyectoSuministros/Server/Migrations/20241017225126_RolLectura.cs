using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoSuministros.Server.Migrations
{
    /// <inheritdoc />
    public partial class RolLectura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles(Id, Name, NormalizedName)
                                Values('60dd83de-4187-462e-bb8a-efa8df01e1f3','Gestor Usuarios','GESTOR USUARIOS')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles(Id, Name, NormalizedName)
                                Values('60dd83de-4187-462e-bb8a-efa8df01e1f3','Gestor Usuarios','GESTOR USUARIOS')");
        }
    }
}
