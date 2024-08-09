using System;
namespace ProyectoSuministros.Shared.DTOs
{
	public class ParametrosBusquedaCatalogo
	{
		//Destino
		public string nombreDestino { get; set; } = string.Empty!;

		//Grupos
		public string nombreGrupo { get; set; } = string.Empty!;

		//Razon Social
		public string nombrerazonSocial { get; set; } = string.Empty!;

		//Cluster
		public string nombreCluster { get; set; } = string.Empty!;

		//Ejecutivo
		public string nombreEjecutivo { get; set; } = string.Empty!;

		//Franquicia
		public string nombreFranquicia { get; set; } = string.Empty!;

		//Reparto
		public string nombreReparto { get; set; } = string.Empty!;

		//TAD
		public string nombreTad { get; set; } = string.Empty!;

		//Zona
		public string nombreZona { get; set; } = string.Empty!;
	}
}

