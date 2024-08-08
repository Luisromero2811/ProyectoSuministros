using System;
using FluentValidation;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class GestionGrupoValidation : AbstractValidator<Grupo>
	{
		public GestionGrupoValidation()
		{
            RuleFor(x => x.Nombre).NotEmpty().WithName("Nombre del grupo");
        }
	}
}

