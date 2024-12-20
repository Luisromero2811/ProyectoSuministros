﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OfficeOpenXml.Attributes;

namespace ProyectoSuministros.Shared.Modelos
{
	public class Cliente
	{
		[Key]
		public int? ID { get; set; }

		[MaxLength(40), DisplayName("Nombre del Cliente")]
		public string? Nombre { get; set; }

		[NotMapped, EpplusIgnore]
		public Grupo? Grupo { get; set; } = null!;

		public bool? Activo { get; set; } = true;

		public int? IDGrupo { get; set; }
	}
}

