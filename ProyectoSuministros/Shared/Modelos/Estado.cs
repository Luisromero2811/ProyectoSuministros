using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSuministros.Shared.Modelos
{
	public class Estado
	{
        [Key]
        public int ID { get; set; }

        [MaxLength(128)]
        public string Nombre { get; set; } = string.Empty!;
    }
}

