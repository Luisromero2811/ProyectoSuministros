using System;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Shared.DTOs
{
	public class RazonSocialDestinoDTO
	{
        //Relación entre razon social y destinos
        public RazonSocial? razonSocial { get; set; }
        public CodDenDTO? destino { get; set; }
    }
}

