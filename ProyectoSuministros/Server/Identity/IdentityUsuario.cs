using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Server.Identity
{
	public class IdentityUsuario:IdentityUser
	{
        public int UserCod { get; set; }

        [NotMapped]
        public Usuario Usuario { get; set; } = null!;
    }
}

