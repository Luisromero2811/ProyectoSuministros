using System;
using FluentValidation;
using ProyectoSuministros.Shared.DTOs;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class ZoneDestinationValidation : AbstractValidator<ZonaDestinoDTO>
	{
		public ZoneDestinationValidation()
		{
            RuleFor(x => x.zona).NotEmpty().WithName("Zona");
            RuleFor(x => x.destino).NotEmpty().WithName("Destino");
        }
	}
}

