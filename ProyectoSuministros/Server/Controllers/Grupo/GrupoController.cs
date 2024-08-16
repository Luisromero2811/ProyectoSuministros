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
    public class GrupoController : Controller
    {
        private readonly ApplicationDbContext context;

        public GrupoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Creación de Grupos
        [HttpPost("save")]
        public async Task<ActionResult> PostDestino([FromBody] Grupo grupos)
        {
            try
            {
                if (grupos is null)
                {
                    return BadRequest();
                }

                //Si el destino viene en ceros del front lo agregamos como nuevo sino lo actualizamos
                if (grupos.ID == 0)
                {
                    context.Add(grupos);
                    await context.SaveChangesAsync();
                }
                else
                {
                    context.Update(grupos);
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
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var grupos = context.Grupo.ToList();
                return Ok(grupos);
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("list")]
        public async Task<ActionResult> GetList([FromQuery] ParametrosBusquedaCatalogo grupo)
        {
            try
            {
                var grupos = context.Grupo.Where(x => x.Activo == true).AsQueryable();

                if (!string.IsNullOrEmpty(grupo.nombreGrupo))
                    grupos = grupos.Where(x => x.Nombre != null && !string.IsNullOrEmpty(x.Nombre) && x.Nombre.ToLower().Contains(grupo.nombreGrupo.ToLower()));

                return Ok(grupos);
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

                var grupo = context.Grupo.Where(x => x.ID == Id).FirstOrDefault();
                if (grupo == null)
                {
                    return NotFound();
                }

                grupo.Activo = status;

                context.Update(grupo);

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

