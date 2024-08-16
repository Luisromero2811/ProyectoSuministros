using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoSuministros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : Controller
    {
        private readonly ApplicationDbContext context;

        public ComprasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        private record Compras_Grafica(double? Volumen);

        [HttpGet("MensualClienteSimsa")]
        public ActionResult Datos_ComprasMensualesClientesSIMSA()
        {
            try
            {
                var facturas_compras = context.Facturas
                    .Where(x => x.Fch_Ven.Year == DateTime.Now.Year && x.Codest == 1)
                    .Select(x => new { x.VolFac })
                    .ToList();

                List<Compras_Grafica> Compras_Totales = new();

                foreach (var facturas in facturas_compras)
                {
                    var total = facturas_compras.Sum(x =>x.VolFac);
                    Compras_Totales.Add(new(total));
                }

                return Ok(Compras_Totales);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

