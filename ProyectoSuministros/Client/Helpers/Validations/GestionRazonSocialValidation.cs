using System;
using FluentValidation;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class GestionRazonSocialValidation : AbstractValidator<RazonSocial>
	{
		public GestionRazonSocialValidation()
		{
            RuleFor(x => x.Nombre).NotEmpty().WithName("Nombre de la Razon Social");
        }
	}
}

