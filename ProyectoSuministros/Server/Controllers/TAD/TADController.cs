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
    public class TADController : Controller
    {
        private readonly ApplicationDbContext context;

        public TADController(ApplicationDbContext context)
        {
            this.context = context;
        }


        //Creación de clientes
        [HttpPost("save")]
        public async Task<ActionResult> PostCliente([FromBody] TAD tad)
        {
            try
            {
                if (tad is null)
                {
                    return BadRequest();
                }

                //Si el destino viene en ceros del front lo agregamos como nuevo sino lo actualizamos
                if (tad.ID == 0)
                {
                    context.Add(tad);
                    await context.SaveChangesAsync();
                }
                else
                {
                    context.Update(tad);
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
        public async Task<ActionResult> GetList([FromQuery] ParametrosBusquedaCatalogo tads)
        {
            try
            {
                var tad = context.TAD.Where(x => x.Activo == true).AsQueryable();

                if (!string.IsNullOrEmpty(tads.nombreTad))
                    tad = tad.Where(x => x.Nombre != null && !string.IsNullOrEmpty(x.Nombre) && x.Nombre.ToLower().Contains(tads.nombreTad.ToLower()));

                return Ok(tad);
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

                var tad = context.TAD.Where(x => x.ID == Id).FirstOrDefault();
                if (tad == null)
                {
                    return NotFound();
                }

                tad.Activo = status;

                context.Update(tad);

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

