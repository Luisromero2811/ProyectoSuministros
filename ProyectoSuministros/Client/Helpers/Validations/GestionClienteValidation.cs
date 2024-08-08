using System;
using FluentValidation;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class GestionClienteValidation : AbstractValidator<Cliente>
	{
		public GestionClienteValidation()
		{
            RuleFor(x => x.Nombre).NotEmpty().WithName("Nombre del cliente");
        }
	}
}

