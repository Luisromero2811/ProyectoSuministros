using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoSuministros.Shared.Modelos
{
	public class Facturas
	{
		[Key]
		public int ID { get; set; }

		[MaxLength(10)]
		public string TipDOC { get; set; }

		[MaxLength(180)]
		public string NRem { get; set; }

		[MaxLength(190)]
		public string NFac { get; set; }

		public int IDProd { get; set; } = 0;

		public DateTime Fch_Ven { get; set; } = DateTime.Now;

		[MaxLength(40)]
		public string Destino { get; set; }

		[MaxLength(20)]
		public string Vehiculo { get; set; }

		public float VolNat { get; set; }

		public float Temperatura { get; set; }

		public float VolFac { get; set; }

		[MaxLength(10)]
		public string Unidades { get; set; }

		public float Importe { get; set; }

		[MaxLength(20)]
		public string Moneda { get; set; }

		public int IDDestino { get; set; }
	}
}

