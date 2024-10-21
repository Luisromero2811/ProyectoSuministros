using System;
namespace ProyectoSuministros.Shared.DTOs
{
	public class ParametrosBusquedaCatalogo
	{
		//Destino
		public string nombreDestino { get; set; } = string.Empty!;
		public string numeroMGC { get; set; } = string.Empty!;
		public string NomMexS { get; set; } = string.Empty!;
		public string PermisoCRE { get; set; } = string.Empty!;
        public int codcte { get; set; } = 0;
		public int IDDestino { get; set; } = 0;

        //Grupos
        public string nombreGrupo { get; set; } = string.Empty!;

		//Clientes
		public string nombreCliente { get; set; } = string.Empty!;

		//Razon Social
		public string nombrerazonSocial { get; set; } = string.Empty!;

		//Cluster
		public string nombreCluster { get; set; } = string.Empty!;
		public int IDCluster { get; set; } = 0;

		//Ejecutivo
		public string nombreEjecutivo { get; set; } = string.Empty!;
		public int IDEjecutivo { get; set; } = 0;

		//Franquicia
		public string nombreFranquicia { get; set; } = string.Empty!;
		public int IDFranquicia { get; set; } = 0;

		//Reparto
		public string nombreReparto { get; set; } = string.Empty!;
		public int IDReparto { get; set; } = 0;

		//TAD
		public string nombreTad { get; set; } = string.Empty!;
		public int IDTad { get; set; } = 0;

		//Zona
		public string nombreZona { get; set; } = string.Empty!;
		public int IDZona { get; set; } = 0;
	}
}

