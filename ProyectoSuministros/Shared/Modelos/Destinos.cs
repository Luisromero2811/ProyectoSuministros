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

		public bool Activo { get; set; } = true;

		[MaxLength(4)]
		public int? IDFac { get; set; } = null!;

		public int? ID_Rs { get; set; } = null!;

        public int? ID_Cluster { get; set; } = null!;

        public int? ID_Ejecutivo { get; set; } = null!;

        public int? ID_Franquicia { get; set; } = null!;

        public int? ID_Reparto { get; set; } = null!;

        public int? ID_Tad { get; set; } = null!;

        public int? ID_Zona { get; set; } = null!;

        [NotMapped, EpplusIgnore]
		public RazonSocial? RazonSocial { get; set; } = null!;

		[NotMapped, EpplusIgnore]
		public Cluster? Cluster { get; set; } = null!;

		[NotMapped, EpplusIgnore]
		public Ejecutivo? Ejecutivo { get; set; } = null!;

		[NotMapped, EpplusIgnore]
		public Franquicia? Franquicia { get; set; } = null!;

		[NotMapped, EpplusIgnore]
		public Reparto? Reparto { get; set; } = null!;

		[NotMapped, EpplusIgnore]
		public TAD? TAD { get; set; } = null!;

		[NotMapped, EpplusIgnore]
		public Zona? Zona { get; set; } = null!;
	}
}

