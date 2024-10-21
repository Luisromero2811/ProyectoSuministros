using System;
using OfficeOpenXml.Attributes;
using ProyectoSuministros.Shared.Modelos;

namespace ProyectoSuministros.Shared.DTOs
{
	public class ParametrosBusquedaFacturasDTO
	{
        //public string producto { get; set; } = string.Empty;
        //public string destino { get; set; } = string.Empty;
        //public string cliente { get; set; } = string.Empty;
        //public string zona { get; set; } = string.Empty;
        public int pagina { get; set; } = 1;
        public int tamanopagina { get; set; } = 40;
        public DateTime DateInicio { get; set; } = DateTime.Today.Date;
        public DateTime DateFin { get; set; } = DateTime.Now;
        public string nremision { get; set; } = string.Empty;
        public string nfactura { get; set; } = string.Empty;
        public string producto { get; set; } = string.Empty;
        public string destino { get; set; } = string.Empty;
    }
}

