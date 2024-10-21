using System;
using FluentValidation;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class GestionClusterValidation : AbstractValidator<Cluster>
	{
		public GestionClusterValidation()
		{
            RuleFor(x => x.Nombre).NotEmpty().WithName("Nombre del Cluster");
        }
	}
}

