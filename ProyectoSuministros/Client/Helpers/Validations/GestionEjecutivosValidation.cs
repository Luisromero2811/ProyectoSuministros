using System;
using FluentValidation;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class GestionEjecutivosValidation : AbstractValidator<Ejecutivo>
	{
		public GestionEjecutivosValidation()
		{
            RuleFor(x => x.Nombre).NotEmpty().WithName("Nombre del Ejecutivo");
        }
	}
}

