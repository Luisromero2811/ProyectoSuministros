using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoSuministros.Shared.DTOs;
using ProyectoSuministros.Shared.Modelos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoSuministros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonaController : Controller
    {
        private readonly ApplicationDbContext context;

        public ZonaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Creación de clientes
        [HttpPost("save")]
        public async Task<ActionResult> PostCliente([FromBody] Zona zona)
        {
            try
            {
                if (zona is null)
                {
                    return BadRequest();
                }

                //Si el destino viene en ceros del front lo agregamos como nuevo sino lo actualizamos
                if (zona.ID == 0)
                {
                    context.Add(zona);
                    await context.SaveChangesAsync();
                }
                else
                {
                    context.Update(zona);
                    await context.SaveChangesAsync();
                }

                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("list")]
        public async Task<ActionResult> GetList([FromQuery] ParametrosBusquedaCatalogo zonas)
        {
            try
            {
                var zona = context.Zona.Where(x => x.Activo == true).AsQueryable();

                if (!string.IsNullOrEmpty(zonas.nombreZona))
                    zona = zona.Where(x => x.Nombre != null && !string.IsNullOrEmpty(x.Nombre) && x.Nombre.ToLower().Contains(zonas.nombreZona.ToLower()));

                return Ok(zona);
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

                var zona = context.Zona.Where(x => x.ID == Id).FirstOrDefault();
                if (zona == null)
                {
                    return NotFound();
                }

                zona.Activo = status;

                context.Update(zona);

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

