using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OfficeOpenXml.Attributes;

namespace ProyectoSuministros.Shared.Modelos
{
	public class Ejecutivo
	{
		[Key]
		public int ID { get; set; }

		[MaxLength(220)]
		public string Nombre { get; set; }

		public bool Activo { get; set; } = true;

        public List<Destinos> Destinos { get; set; } = new List<Destinos>();

    }
}

