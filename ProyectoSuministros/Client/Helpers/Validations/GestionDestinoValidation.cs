using System;
using FluentValidation;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Client.Helpers.Validations
{
	public class GestionDestinoValidation : AbstractValidator<Destinos>
	{
		public GestionDestinoValidation()
		{
			RuleFor(x => x.IDGob).NotEmpty().WithName("ID Suministro");
			RuleFor(x => x.NumMGC).NotEmpty().WithName("Número MGC");
			RuleFor(x => x.Estacion).NotEmpty().WithName("Nombre de la estación");
			RuleFor(x => x.NomMS).NotEmpty().WithName("Nombre México S");
			RuleFor(x => x.RFC).NotEmpty().WithName("RFC");
			RuleFor(x => x.CRE).NotEmpty().WithName("Permiso CRE");
			RuleFor(x => x.NEstacion).NotEmpty().WithName("Número estación");
		}
	}
}

