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
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext context;

        public ClienteController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Creación de clientes
        [HttpPost("save")]
        public async Task<ActionResult> PostCliente([FromBody] Cliente clientes)
        {
            try
            {
                if (clientes is null)
                {
                    return BadRequest();
                }

                //Si el destino viene en ceros del front lo agregamos como nuevo sino lo actualizamos
                if (clientes.ID == 0)
                {
                    context.Add(clientes);
                    await context.SaveChangesAsync();
                }
                else
                {
                    context.Update(clientes);
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
        public async Task<ActionResult> ListAll()
        {
            try
            {
                var clientes = context.Cliente.ToList();
                return Ok(clientes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("list")]
        public async Task<ActionResult> GetList([FromQuery] ParametrosBusquedaCatalogo cliente)
        {
            try
            {
                var clientes = context.Cliente.Where(x => x.Activo == true).AsQueryable();

                if (!string.IsNullOrEmpty(cliente.nombreCliente))
                    clientes = clientes.Where(x => x.Nombre != null && !string.IsNullOrEmpty(x.Nombre) && x.Nombre.ToLower().Contains(cliente.nombreCliente.ToLower()));

                return Ok(clientes);
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

                var clientes = context.Cliente.Where(x => x.ID == Id).FirstOrDefault();
                if (clientes == null)
                {
                    return NotFound();
                }

                clientes.Activo = status;

                context.Update(clientes);

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

