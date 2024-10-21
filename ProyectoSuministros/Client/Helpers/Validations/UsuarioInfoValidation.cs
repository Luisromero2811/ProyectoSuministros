﻿using System;
using FluentValidation;
using ProyectoSuministros.Shared.DTOs;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class UsuarioInfoValidation : AbstractValidator<UsuarioInfo>
	{
		public UsuarioInfoValidation()
		{
            RuleFor(x => x.Nombre).NotEmpty().WithName("Nombre");
            RuleFor(x => x.UserName).NotEmpty().WithName("Nombre de Usuario");
            RuleFor(x => x.Password).NotEmpty().WithName("Contraseña");
            RuleFor(x => x.Roles).NotEmpty().WithName("Rol");
        }
	}
}

