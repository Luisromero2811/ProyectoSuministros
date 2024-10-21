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
    public class EjecutivoController : Controller
    {
        private readonly ApplicationDbContext context;

        public EjecutivoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Creación de clientes
        [HttpPost("save")]
        public async Task<ActionResult> PostCliente([FromBody] Ejecutivo ejecutivo)
        {
            try
            {
                if (ejecutivo is null)
                {
                    return BadRequest();
                }

                //Si el destino viene en ceros del front lo agregamos como nuevo sino lo actualizamos
                if (ejecutivo.ID == 0)
                {
                    context.Add(ejecutivo);
                    await context.SaveChangesAsync();
                }
                else
                {
                    context.Update(ejecutivo);
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
        public async Task<ActionResult> GetList([FromQuery] ParametrosBusquedaCatalogo ejecutivo)
        {
            try
            {
                var ejecutivos = context.Ejecutivo.Where(x => x.Activo == true).AsQueryable();

                if (!string.IsNullOrEmpty(ejecutivo.nombreEjecutivo))
                    ejecutivos = ejecutivos.Where(x => x.Nombre != null && !string.IsNullOrEmpty(x.Nombre) && x.Nombre.ToLower().Contains(ejecutivo.nombreEjecutivo.ToLower()));

                return Ok(ejecutivos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("listado")]
        public async Task<ActionResult> GetEjecutives()
        {
            try
            {
                var listado = context.Ejecutivo
                    .Where(x => x.Activo == true)
                    .ToList();

                return Ok(listado);
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

                var ejecutivo = context.Ejecutivo.Where(x => x.ID == Id).FirstOrDefault();
                if (ejecutivo == null)
                {
                    return NotFound();
                }

                ejecutivo.Activo = status;

                context.Update(ejecutivo);

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

