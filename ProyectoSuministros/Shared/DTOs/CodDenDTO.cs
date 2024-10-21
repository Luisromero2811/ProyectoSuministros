using System;
using OfficeOpenXml.Attributes;
using ProyectoSuministros.Shared.Modelos;
using System.ComponentModel;

namespace ProyectoSuministros.Shared.DTOs
{
	public class CodDenDTO
	{
        public int Cod { get; set; } = 0;
        public string Den { get; set; } = string.Empty;
    }
}

