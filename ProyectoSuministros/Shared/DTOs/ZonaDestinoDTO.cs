using System;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Shared.DTOs
{
	public class ZonaDestinoDTO
	{
        //Relación entre razon social y destinos
        public Zona? zona { get; set; }
        public CodDenDTO? destino { get; set; }
    }
}

