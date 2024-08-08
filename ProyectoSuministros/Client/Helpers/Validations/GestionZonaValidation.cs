
using System;
using FluentValidation;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class GestionZonaValidation : AbstractValidator<Zona>
	{
		public GestionZonaValidation()
		{
            RuleFor(x => x.Nombre).NotEmpty().WithName("Nombre de la zona");
        }
	}
}

