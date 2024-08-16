using System;
using FluentValidation;
using ProyectoSuministros.Shared.DTOs;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class EjecutiveDestinationValidation : AbstractValidator<EjecutivoDestinoDTO>
	{
		public EjecutiveDestinationValidation()
		{
            RuleFor(x => x.ejecutivo).NotEmpty().WithName("Ejecutivo");
            RuleFor(x => x.destino).NotEmpty().WithName("Destino");
        }
	}
}

