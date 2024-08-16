using System;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Shared.DTOs
{
	public class RepartoDestinoDTO
	{
        //Relación entre reparto y destinos
        public Reparto? reparto { get; set; }
        public CodDenDTO? destino { get; set; }
    }
}

