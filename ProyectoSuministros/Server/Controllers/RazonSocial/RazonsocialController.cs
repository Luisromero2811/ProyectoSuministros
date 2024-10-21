using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoSuministros.Shared.DTOs;
using ProyectoSuministros.Shared.Modelos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoSuministros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RazonsocialController : Controller
    {
        private readonly ApplicationDbContext context;

        public RazonsocialController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Creación de razones sociales
        [HttpPost("save")]
        public async Task<ActionResult> PostCliente([FromBody] RazonSocial razonSocial)
        {
            try
            {
                if (razonSocial is null)
                {
                    return BadRequest();
                }

                //Si el destino viene en ceros del front lo agregamos como nuevo sino lo actualizamos
                if (razonSocial.ID == 0)
                {
                    context.Add(razonSocial);
                    await context.SaveChangesAsync();
                }
                else
                {
                    context.Update(razonSocial);
                    await context.SaveChangesAsync();
                }

                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //Listado generico
        [HttpGet("lista")]
        public async Task<ActionResult> ListAll()
        {
            try
            {
                var razonessociales = await context.Razonsocial
                    .ToListAsync();
                return Ok(razonessociales);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        //Listado del cual podemos filtrar
        [HttpGet("list")]
        public async Task<ActionResult> GetList([FromQuery] ParametrosBusquedaCatalogo razonSocial)
        {
            try
            {
                var razonesSociales = context.Razonsocial.Where(x => x.Activo == true).AsQueryable();

                if (!string.IsNullOrEmpty(razonSocial.nombrerazonSocial))
                    razonesSociales = razonesSociales.Where(x => x.Nombre != null && !string.IsNullOrEmpty(x.Nombre) && x.Nombre.ToLower().Contains(razonSocial.nombrerazonSocial.ToLower()));

                return Ok(razonesSociales);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //Activación y desactivación de estados
        [HttpPut("{ID:int}")]
        public async Task<ActionResult> ChangeStatus([FromRoute] int Id, [FromBody] bool status)
        {
            try
            {
                if (Id == 0)
                    return BadRequest();

                var razonSocial = context.Razonsocial.Where(x => x.ID == Id).FirstOrDefault();
                if (razonSocial == null)
                {
                    return NotFound();
                }

                razonSocial.Activo = status;

                context.Update(razonSocial);

                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("filtraractivos")]
        public ActionResult Obtener_Grupos_Activos([FromQuery] RazonSocial razonSocial)
        {
            try
            {
                var razones = context.Razonsocial
                    .Where(x => x.Activo == true)
                    .IgnoreAutoIncludes()
                    .AsQueryable();

                if (!string.IsNullOrEmpty(razonSocial.Nombre))
                    razones = razones.Where(x => x.Nombre.ToLower().Contains(razonSocial.Nombre.ToLower())
                    && x.Activo == true
                    );

                return Ok(razones);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

