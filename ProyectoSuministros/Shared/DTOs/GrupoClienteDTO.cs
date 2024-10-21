using System;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Shared.DTOs
{
	public class GrupoClienteDTO
	{
		//Relación entre Grupo empresarial y Cliente
		public Grupo? Grupo { get; set; }
		public CodDenDTO? Cliente { get; set; }
	}
}

