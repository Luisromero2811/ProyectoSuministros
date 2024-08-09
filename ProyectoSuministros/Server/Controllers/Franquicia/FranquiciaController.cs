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
    public class FranquiciaController : Controller
    {
        private readonly ApplicationDbContext context;

        public FranquiciaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Creación de clientes
        [HttpPost("save")]
        public async Task<ActionResult> PostCliente([FromBody] Franquicia franquicia)
        {
            try
            {
                if (franquicia is null)
                {
                    return BadRequest();
                }

                //Si el destino viene en ceros del front lo agregamos como nuevo sino lo actualizamos
                if (franquicia.ID == 0)
                {
                    context.Add(franquicia);
                    await context.SaveChangesAsync();
                }
                else
                {
                    context.Update(franquicia);
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
        public async Task<ActionResult> GetList([FromQuery] ParametrosBusquedaCatalogo franquicias)
        {
            try
            {
                var franquicia = context.Franquicia.Where(x => x.Activo == true).AsQueryable();

                if (!string.IsNullOrEmpty(franquicias.nombreFranquicia))
                    franquicia = franquicia.Where(x => x.Nombre != null && !string.IsNullOrEmpty(x.Nombre) && x.Nombre.ToLower().Contains(franquicias.nombreFranquicia.ToLower()));

                return Ok(franquicia);
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

                var franquicia = context.Franquicia.Where(x => x.ID == Id).FirstOrDefault();
                if (franquicia == null)
                {
                    return NotFound();
                }

                franquicia.Activo = status;

                context.Update(franquicia);

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

