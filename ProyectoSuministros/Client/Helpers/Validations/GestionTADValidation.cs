using System;
using FluentValidation;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class GestionTADValidation : AbstractValidator<TAD>
	{
		public GestionTADValidation()
		{
            RuleFor(x => x.Nombre).NotEmpty().WithName("Nombre de la TAD");
        }
	}
}

