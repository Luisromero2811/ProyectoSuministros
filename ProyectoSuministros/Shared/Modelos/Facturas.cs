using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OfficeOpenXml.Attributes;

namespace ProyectoSuministros.Shared.Modelos
{
	public class Factura
	{
		[Key]
		public int ID { get; set; }

		[MaxLength(10)]
		public string TipDOC { get; set; } = string.Empty;

		[MaxLength(180)]
		public string NRem { get; set; } = string.Empty;

		[MaxLength(190)]
		public string NFac { get; set; } = string.Empty;

		public int IDProd { get; set; } = 0;

		public DateTime Fch_Ven { get; set; } = DateTime.Now;

		[MaxLength(40)]
		public string Destino { get; set; } = string.Empty;

		[MaxLength(20)]
		public string Vehiculo { get; set; } = string.Empty;

		public double VolNat { get; set; }

		public double Temperatura { get; set; }

		public double VolFac { get; set; }

		[MaxLength(10)]
		public string Unidades { get; set; } = string.Empty;

		public double Importe { get; set; }

		[MaxLength(20)]
		public string Moneda { get; set; } = string.Empty;

		public int IDDestino { get; set; }

		public int? Codest { get; set; } = 0;

		[NotMapped, EpplusIgnore]
		public Destinos Destinos { get; set; } = null!;

		[NotMapped, EpplusIgnore]
		public Producto Producto { get; set; } = null!;

		[NotMapped, EpplusIgnore]
		public Estado Estado { get; set; } = null!;
	}
}

