using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OfficeOpenXml.Attributes;

namespace ProyectoSuministros.Shared.Modelos
{
	public class Destinos
	{
		[Key]
		public int ID { get; set; }

		public int IDGob { get; set; }

		[MaxLength(40)]
		public string NumMGC { get; set; } = string.Empty;

		[MaxLength(128)]
		public string? Estacion { get; set; }

		[MaxLength(128)]
		public string NomMS { get; set; } = string.Empty;

		[MaxLength(13)]
		public string? RFC { get; set; }

		[MaxLength(80)]
		public string CRE { get; set; } = string.Empty;

		[MaxLength(20)]
		public string NEstacion { get; set; } = string.Empty;

		public bool? Activo { get; set; } = true;

		//Relaciones 
		public int? CodRs { get; set; } = 0;

		[NotMapped]
		public RazonSocial? RazonSocial { get; set; } = null!;

		public int? IDCluster { get; set; } = 0;

		[NotMapped]
		public Cluster? Cluster { get; set; } = null!;

		public int? IDEjecutivo { get; set; } = 0;

		[NotMapped]
		public Ejecutivo? Ejecutivo { get; set; } = null!;

		public int? IDFranquicia { get; set; } = 0;

		[NotMapped]
		public Franquicia? Franquicia { get; set; } = null!;

		public int? IDReparto { get; set; } = 0;

		[NotMapped]
		public Reparto? Reparto { get; set; } = null!;

		public int? IDTad { get; set; } = 0;

		[NotMapped]
		public TAD? TAD { get; set; } = null!;

		public int? IDZona { get; set; } = 0;

		[NotMapped]
		public Zona? Zona { get; set; } = null!;
	}
}

