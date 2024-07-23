using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSuministros.Shared.Modelos
{
	public class Ejecutivo
	{
		[Key]
		public int ID { get; set; }

		[MaxLength(220)]
		public string Nombre { get; set; }

		public int IDDes { get; set; }


	}
}

