using System;
using ProyectoSuministros.Shared.Modelos;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoSuministros.Shared.DTOs
{
	public class UsuarioInfo
	{
        public string Id { get; set; } = string.Empty;
        public int UserCod { get; set; }
        //Nombre de persona
        public string Nombre { get; set; } = string.Empty;
        //Nombre de usuario en sistema 
        public string UserName { get; set; } = string.Empty;
        //Roles asignados
        public List<string> Roles { get; set; } = new List<string>();
        //Contraseña
        public string Password { get; set; } = string.Empty;
        //Estado
        public bool Activo { get; set; } = true;
        public bool ShowPassword { get; set; } = false;
        [NotMapped]
        public bool passwordView { get; set; } = true;
        [NotMapped]
        public bool ShowUsersActions { get; set; } = false;
    }
}

