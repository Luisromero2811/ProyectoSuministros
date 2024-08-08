using System;
using FluentValidation;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class GestionFranquiciasValidation : AbstractValidator<Franquicia>
	{
		public GestionFranquiciasValidation()
		{
            RuleFor(x => x.Nombre).NotEmpty().WithName("Nombre de la franquicia");
        }
	}
}

