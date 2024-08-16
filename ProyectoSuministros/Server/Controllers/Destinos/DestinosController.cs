using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoSuministros.Shared.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ProyectoSuministros.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("listado")]
        public async Task<ActionResult> AllList()
        {
            try
            {
                var destinos = context.Destinos.ToList();
                return Ok(destinos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message); 
            }
        }


        [HttpGet("listados")]
        public async Task<ActionResult> GetAll()
        {
            try
            {

                var destinos = await context.Destinos
                    .Select(x => new CodDenDTO { Cod = x.IDGob, Den = x.Estacion! })
                    .OrderBy(x => x.Den)
                    .ToListAsync();
                return Ok(destinos);
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
                var destinos = context.Destinos.Where(x => x.CodRs == destino.codcte && x.Activo == true)
                    .OrderBy(x => x.Estacion)
                    .AsQueryable();

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

        [HttpPost("asignar/{cod:int}")]
        public async Task<ActionResult> RelationRazonSocial([FromBody] CodDenDTO codden, [FromRoute] int cod)
        {
            try
            {
                var destino = await context.Destinos.FirstOrDefaultAsync(x => x.IDGob == codden.Cod);

                if (destino == null)
                {
                    return NotFound();
                }

                destino.CodRs = cod;
                context.Update(destino);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("asignarCluster/{cod:int}")]
        public async Task<ActionResult> RelationCluster([FromBody] CodDenDTO codden, [FromRoute] int cod)
        {
            try
            {
                var destino = await context.Destinos.FirstOrDefaultAsync(x => x.IDGob == codden.Cod);

                if (destino == null)
                {
                    return NotFound();
                }

                destino.IDCluster = cod;
                context.Update(destino);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("asignarEjecutivos/{cod:int}")]
        public async Task<ActionResult> RelationEjecutivos([FromBody] CodDenDTO codden, [FromRoute] int cod)
        {
            try
            {
                var destino = await context.Destinos.FirstOrDefaultAsync(x => x.IDGob == codden.Cod);

                if (destino == null)
                {
                    return NotFound();
                }

                destino.IDEjecutivo = cod;
                context.Update(destino);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("asignarFranquicias/{cod:int}")]
        public async Task<ActionResult> RelationFranquicias([FromBody] CodDenDTO codden, [FromRoute] int cod)
        {
            try
            {
                var destino = await context.Destinos.FirstOrDefaultAsync(x => x.IDGob == codden.Cod);

                if (destino == null)
                {
                    return NotFound();
                }

                destino.IDFranquicia = cod;
                context.Update(destino);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("asignarReparto/{cod:int}")]
        public async Task<ActionResult> RelationReparto([FromBody] CodDenDTO codden, [FromRoute] int cod)
        {
            try
            {
                var destino = await context.Destinos.FirstOrDefaultAsync(x => x.IDGob == codden.Cod);

                if (destino == null)
                {
                    return NotFound();
                }

                destino.IDReparto = cod;
                context.Update(destino);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("asignarTAD/{cod:int}")]
        public async Task<ActionResult> RelationTAD([FromBody] CodDenDTO codden, [FromRoute] int cod)
        {
            try
            {
                var destino = await context.Destinos.FirstOrDefaultAsync(x => x.IDGob == codden.Cod);

                if (destino == null)
                {
                    return NotFound();
                }

                destino.IDTad = cod;
                context.Update(destino);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("asignarZona/{cod:int}")]
        public async Task<ActionResult> RelationZona([FromBody] CodDenDTO codden, [FromRoute] int cod)
        {
            try
            {
                var destino = await context.Destinos.FirstOrDefaultAsync(x => x.IDGob == codden.Cod);

                if (destino == null)
                {
                    return NotFound();
                }

                destino.IDZona = cod;
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

