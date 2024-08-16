using System;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Shared.DTOs
{
	public class ClusterDestinoDTO
	{
        //Relación entre razon social y destinos
        public Cluster? cluster { get; set; }
        public CodDenDTO? destino { get; set; }
    }
}

