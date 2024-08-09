using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoSuministros.Shared.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ProyectoSuministros.Shared.DTOs;

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
            try
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("lista")]
        public async Task<ActionResult> GetList([FromQuery] ParametrosBusquedaCatalogo destino)
        {
            try
            {
                var destinos = context.Destinos.Where(x => x.Activo == true).AsQueryable();

                if (!string.IsNullOrEmpty(destino.nombreDestino))
                    destinos = destinos.Where(x => x.Estacion != null && !string.IsNullOrEmpty(x.Estacion) && x.Estacion.ToLower().Contains(destino.nombreDestino.ToLower()));

                return Ok(destinos);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{ID:int}")]
        public async Task<ActionResult> ChangeStatus([FromRoute] int Id, [FromBody] bool status)
        {
            try
            {
                if (Id == 0)
                    return BadRequest();

                var destino = context.Destinos.Where(x => x.ID == Id).FirstOrDefault();
                if (destino == null)
                {
                    return NotFound();
                }

                destino.Activo = status;

                context.Update(destino);

                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

