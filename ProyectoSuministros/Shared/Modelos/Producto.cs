using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSuministros.Shared.Modelos
{
	public class Producto
	{
        [Key]
        public int ID { get; set; }

        public string? Nombre { get; set; } 
    }
}

