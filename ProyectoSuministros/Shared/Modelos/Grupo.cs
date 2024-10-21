using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OfficeOpenXml.Attributes;

namespace ProyectoSuministros.Shared.Modelos
{
	public class Grupo
	{
		[Key]
		public int? ID { get; set; }

		[MaxLength(40)]
		public string? Nombre { get; set; }

		public bool? Activo { get; set; } = true;

        [NotMapped, EpplusIgnore]
        public Cliente? Cliente { get; set; } = null!;

    }
}

