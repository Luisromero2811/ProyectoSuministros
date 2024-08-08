using System;
using FluentValidation;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class GestionRepartoValidation : AbstractValidator<Reparto>
	{
		public GestionRepartoValidation()
		{
            RuleFor(x => x.Nombre).NotEmpty().WithName("Nombre del reparto");
        }
	}
}

