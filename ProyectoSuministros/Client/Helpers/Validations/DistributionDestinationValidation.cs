using System;
using FluentValidation;
using ProyectoSuministros.Shared.DTOs;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class DistributionDestinationValidation : AbstractValidator<RepartoDestinoDTO>
	{
		public DistributionDestinationValidation()
		{
            RuleFor(x => x.reparto).NotEmpty().WithName("Reparto");
            RuleFor(x => x.destino).NotEmpty().WithName("Destino");
        }
	}
}

