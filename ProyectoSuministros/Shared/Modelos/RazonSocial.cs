using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OfficeOpenXml.Attributes;

namespace ProyectoSuministros.Shared.Modelos
{
	public class RazonSocial
	{
		[Key]
		public int ID { get; set; }

		[MaxLength(128)]
		public string Nombre { get; set; } = string.Empty;

		public int? IDCte { get; set; }

		public bool? Activo { get; set; } = true;

		[NotMapped, EpplusIgnore]
		public Cliente? Cliente { get; set; } = null!;

		[NotMapped, EpplusIgnore]
		public Destinos? Destinos { get; set; } = null!;
	}
}

