using System;
using FluentValidation;
using ProyectoSuministros.Shared.DTOs;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class FranchiseDestinationValidation : AbstractValidator<FranquiciaDestinoDTO>
	{
		public FranchiseDestinationValidation()
		{
            RuleFor(x => x.franquicia).NotEmpty().WithName("Franquicia");
            RuleFor(x => x.destino).NotEmpty().WithName("Destino");
        }
	}
}

