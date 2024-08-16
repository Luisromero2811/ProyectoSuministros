using System;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Shared.DTOs
{
	public class FranquiciaDestinoDTO
	{
        //Relación entre razon social y destinos
        public Franquicia? franquicia { get; set; }
        public CodDenDTO? destino { get; set; }
    }
}

