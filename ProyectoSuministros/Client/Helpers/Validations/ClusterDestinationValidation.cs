using System;
using FluentValidation;
using ProyectoSuministros.Shared.DTOs;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class ClusterDestinationValidation : AbstractValidator<ClusterDestinoDTO>
	{
		public ClusterDestinationValidation()
		{
            RuleFor(x => x.cluster).NotEmpty().WithName("Cluster");
            RuleFor(x => x.destino).NotEmpty().WithName("Destino");
        }
	}
}

