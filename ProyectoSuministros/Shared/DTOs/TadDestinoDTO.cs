using System;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Shared.DTOs
{
	public class TadDestinoDTO
	{
        //Relación entre razon social y destinos
        public TAD? tad { get; set; }
        public CodDenDTO? destino { get; set; }
    }
}

