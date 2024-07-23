﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSuministros.Shared.Modelos
{
	public class Reparto
	{
        [Key]
		public int ID { get; set; }

		[MaxLength(40)]
		public string Nombre { get; set; }

		public int IDDes { get; set; }
	}
}
