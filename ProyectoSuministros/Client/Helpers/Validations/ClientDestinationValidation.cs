using System;
using FluentValidation;
using ProyectoSuministros.Shared.DTOs;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class ClientDestinationValidation : AbstractValidator<RazonSocialDestinoDTO>
	{
		public ClientDestinationValidation()
		{
            RuleFor(x => x.razonSocial).NotEmpty().WithName("Razon Social");
            RuleFor(x => x.destino).NotEmpty().WithName("Destino");
        }
	}
}

