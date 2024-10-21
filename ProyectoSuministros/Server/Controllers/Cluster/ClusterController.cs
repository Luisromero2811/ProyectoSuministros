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
    public class ClusterController : Controller
    {
        private readonly ApplicationDbContext context;

        public ClusterController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Creación de clientes
        [HttpPost("save")]
        public async Task<ActionResult> PostCliente([FromBody] Cluster cluster)
        {
            try
            {
                if (cluster is null)
                {
                    return BadRequest();
                }

                //Si el destino viene en ceros del front lo agregamos como nuevo sino lo actualizamos
                if (cluster.ID == 0)
                {
                    context.Add(cluster);
                    await context.SaveChangesAsync();
                }
                else
                {
                    context.Update(cluster);
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
        public async Task<ActionResult> GetList([FromQuery] ParametrosBusquedaCatalogo cluster)
        {
            try
            {
                var clusters = context.Cluster.Where(x => x.Activo == true).AsQueryable();

                if (!string.IsNullOrEmpty(cluster.nombreCluster))
                    clusters = clusters.Where(x => x.Nombre != null && !string.IsNullOrEmpty(x.Nombre) && x.Nombre.ToLower().Contains(cluster.nombreCluster.ToLower()));

                return Ok(clusters);
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

                var cluster = context.Cluster.Where(x => x.ID == Id).FirstOrDefault();
                if (cluster == null)
                {
                    return NotFound();
                }

                cluster.Activo = status;

                context.Update(cluster);

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

