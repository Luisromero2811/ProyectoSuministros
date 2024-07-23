using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSuministros.Shared.Modelos
{
	public class Destinos
	{
		[Key]
		public int ID { get; set; }

		public int IDGob { get; set; }

		[MaxLength(40)]
		public string NumMGC { get; set; }

		[MaxLength(128)]
		public string? Estacion { get; set; }

		[MaxLength(128)]
		public string NomMS { get; set; }

		[MaxLength(13)]
		public string? RFC { get; set; }

		[MaxLength(80)]
		public string CRE { get; set; }

		[MaxLength(4)]
		public int NEstacion { get; set; } = 0;

		public bool Activo { get; set; }

		[MaxLength(4)]
		public int IDFac { get; set; } = 0;
	}
}

