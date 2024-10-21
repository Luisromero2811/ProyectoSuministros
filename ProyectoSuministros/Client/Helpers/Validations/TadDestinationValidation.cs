using System;
using FluentValidation;
using ProyectoSuministros.Shared.DTOs;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class TadDestinationValidation : AbstractValidator<TadDestinoDTO>
	{
		public TadDestinationValidation() 
		{
            RuleFor(x => x.tad).NotEmpty().WithName("TAD");
            RuleFor(x => x.destino).NotEmpty().WithName("Destino");
        }
	}
}

