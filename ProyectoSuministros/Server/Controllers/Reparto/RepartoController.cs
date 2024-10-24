﻿using System;
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
    public class RepartoController : Controller
    {
        private readonly ApplicationDbContext context;

        public RepartoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Creación de clientes
        [HttpPost("save")]
        public async Task<ActionResult> PostCliente([FromBody] Reparto reparto)
        {
            try
            {
                if (reparto is null)
                {
                    return BadRequest();
                }

                //Si el destino viene en ceros del front lo agregamos como nuevo sino lo actualizamos
                if (reparto.ID == 0)
                {
                    context.Add(reparto);
                    await context.SaveChangesAsync();
                }
                else
                {
                    context.Update(reparto);
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
        public async Task<ActionResult> GetList([FromQuery] ParametrosBusquedaCatalogo repartos)
        {
            try
            {
                var reparto = context.Reparto.Where(x => x.Activo == true).AsQueryable();

                if (!string.IsNullOrEmpty(repartos.nombreReparto))
                    reparto = reparto.Where(x => x.Nombre != null && !string.IsNullOrEmpty(x.Nombre) && x.Nombre.ToLower().Contains(repartos.nombreReparto.ToLower()));

                return Ok(reparto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("listado")]
        public async Task<ActionResult> GetLista()
        {
            try
            {
                var reparto = context.Reparto
                    .Where(x => x.Activo == true)
                    .ToList();
                return Ok(reparto);
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

                var reparto = context.Reparto.Where(x => x.ID == Id).FirstOrDefault();
                if (reparto == null)
                {
                    return NotFound();
                }

                reparto.Activo = status;

                context.Update(reparto);

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

