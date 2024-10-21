using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoSuministros.Shared.Modelos
{
	public class Usuario
	{
        [JsonProperty("cod"), Key]
        public int Cod { get; set; }
        [JsonProperty("den"), MaxLength(64)]
        public string? Den { get; set; } = string.Empty;
        [JsonProperty("usu"), MaxLength(16)]
        public string? Usu { get; set; } = string.Empty;
        [JsonProperty("cve"), MaxLength(16)]
        public string? Cve { get; set; } = string.Empty;
        [JsonProperty("activo")]
        public bool Activo { get; set; } = true;
    }
}

