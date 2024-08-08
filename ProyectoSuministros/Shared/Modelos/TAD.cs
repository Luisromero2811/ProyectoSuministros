using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSuministros.Shared.Modelos
{
	public class TAD
	{
		[Key]
		public int ID { get; set; }

		public int? IDGob { get; set; }

		[MaxLength(40)]
		public string? Nombre { get; set; }

		public int IDDes { get; set; }

		public bool Activo { get; set; } = true;

	}
}

