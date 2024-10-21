using System;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Shared.DTOs
{
	public class EjecutivoDestinoDTO
	{
        //Relación entre razon social y destinos
        public Ejecutivo? ejecutivo { get; set; }
        public CodDenDTO? destino { get; set; }
    }
}

