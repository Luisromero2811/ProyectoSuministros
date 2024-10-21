using System;
using FluentValidation;
using ProyectoSuministros.Shared.DTOs;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class GroupClientValidation : AbstractValidator<GrupoClienteDTO>
	{
		public GroupClientValidation()
		{
            RuleFor(x => x.Grupo).NotEmpty().WithName("Grupo Empresarial");
            RuleFor(x => x.Cliente).NotEmpty().WithName("Cliente");
        }
	}
}

