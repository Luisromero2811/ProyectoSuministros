using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoSuministros.Shared.Modelos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoSuministros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinosController : Controller
    {
        private readonly ApplicationDbContext context;

        public DestinosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Creación de destinos
        [HttpPost("crear")]
        public async Task<ActionResult> PostDestino([FromBody] Destinos destinos)
        {
            if (destinos is null)
            {
                return BadRequest();
            }

            //Si el destino viene en ceros del front lo agregamos como nuevo sino lo actualizamos
            if (destinos.ID == 0)
            {
                context.Add(destinos);
                await context.SaveChangesAsync();
            }
            else
            {
                context.Update(destinos);
                await context.SaveChangesAsync();
            }

            return Ok();

        }

    }
}

