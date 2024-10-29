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
                    .Select(x => new CodDenDTO { Cod = x.IDGob, Den = x.NomMS! })
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

                if (!string.IsNullOrEmpty(destino.numeroMGC))
                    destinos = destinos.Where(x => x.NumMGC != null && !string.IsNullOrEmpty(x.NumMGC) && x.NumMGC.ToLower().Contains(destino.numeroMGC.ToLower()));

                if (!string.IsNullOrEmpty(destino.NomMexS))
                    destinos = destinos.Where(x => x.NomMS != null && !string.IsNullOrEmpty(x.NomMS) && x.NomMS.ToLower().Contains(destino.NomMexS.ToLower()));

                if (!string.IsNullOrEmpty(destino.PermisoCRE))
                    destinos = destinos.Where(x => x.CRE != null && !string.IsNullOrEmpty(x.CRE) && x.CRE.ToLower().Contains(destino.PermisoCRE.ToLower()));

                return Ok(destinos);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("listaCluster")]
        public async Task<ActionResult> GetListDestinoCluster([FromQuery] ParametrosBusquedaCatalogo destino)
        {
            try
            {
                var destinos = context.Cluster
                    .Where(x => x.Activo == true)
                    .OrderBy(x => x.Nombre)
                    .AsQueryable();

                if (destino.IDDestino != 0)
                {
                    destinos = destinos.Where(x => x.Destinos.Any(y => y.ID == destino.IDDestino));
                }
                else if (destino.IDCluster != 0)
                {
                    destinos = destinos.Where(x => x.ID == destino.IDCluster);
                }
                else
                {
                    return BadRequest("El destino no cuenta con un Cluster asignado");
                }

                return Ok(destinos);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //listaEjecutivo
        [HttpGet("listaEjecutivo")]
        public async Task<ActionResult> GetListEjecutivoCluster([FromQuery] ParametrosBusquedaCatalogo ejecutivo)
        {
            try
            {
                var ejecutivos = context.Ejecutivo
                    .Where(x => x.Activo == true)
                    .OrderBy(x => x.Nombre)
                    .AsQueryable();

                if (ejecutivo.IDDestino != 0)
                {
                    ejecutivos = ejecutivos.Where(x => x.Destinos.Any(y => y.ID == ejecutivo.IDDestino));
                }
                else if (ejecutivo.IDEjecutivo != 0)
                {
                    ejecutivos = ejecutivos.Where(x => x.ID == ejecutivo.IDEjecutivo);
                }
                else
                {
                    return BadRequest("El destino no cuenta con un Ejecutivo asignado");
                }

                return Ok(ejecutivos);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("listaFranquicia")]
        public async Task<ActionResult> GetListFranquiciaCluster([FromQuery] ParametrosBusquedaCatalogo franquicia)
        {
            try
            {
                var franquicias = context.Franquicia
                    .Where(x => x.Activo == true)
                    .OrderBy(x => x.Nombre)
                    .AsQueryable();

                if (franquicia.IDDestino != 0)
                {
                    franquicias = franquicias.Where(x => x.Destinos.Any(y => y.ID == franquicia.IDDestino));
                }
                else if (franquicia.IDFranquicia != 0)
                {
                    franquicias = franquicias.Where(x => x.ID == franquicia.IDFranquicia);
                }
                else
                {
                    return BadRequest("El destino no cuenta con una Franquicia asignada");
                }

                return Ok(franquicias);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("listaReparto")]
        public async Task<ActionResult> GetListRepartoCluster([FromQuery] ParametrosBusquedaCatalogo reparto)
        {
            try
            {
                var repartos = context.Reparto
                    .Where(x => x.Activo == true)
                    .OrderBy(x => x.Nombre)
                    .AsQueryable();

                if (reparto.IDDestino != 0)
                {
                    repartos = repartos.Where(x => x.Destinos.Any(y => y.ID == reparto.IDDestino));
                }
                else if (reparto.IDReparto != 0)
                {
                    repartos = repartos.Where(x => x.ID == reparto.IDReparto);
                }
                else
                {
                    return BadRequest("El destino no cuenta con un Reparto asignado");
                }

                return Ok(repartos);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("listaTAD")]
        public async Task<ActionResult> GetListTADCluster([FromQuery] ParametrosBusquedaCatalogo tad)
        {
            try
            {
                var tADs = context.TAD
                    .Where(x => x.Activo == true)
                    .OrderBy(x => x.Nombre)
                    .AsQueryable();

                if (tad.IDDestino != 0)
                {
                    tADs = tADs.Where(x => x.Destinos.Any(y => y.ID == tad.IDDestino));
                }
                else if (tad.IDTad != 0)
                {
                    tADs = tADs.Where(x => x.IDGob == tad.IDTad);
                }
                else
                {
                    return BadRequest("El destino no cuenta con una TAD asignada");
                }

                return Ok(tADs);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("listaZona")]
        public async Task<ActionResult> GetListZonaCluster([FromQuery] ParametrosBusquedaCatalogo tad)
        {
            try
            {
                var tADs = context.Zona
                    .Where(x => x.Activo == true)
                    .OrderBy(x => x.Nombre)
                    .AsQueryable();

                if (tad.IDDestino != 0)
                {
                    tADs = tADs.Where(x => x.Destinos.Any(y => y.ID == tad.IDDestino));
                }
                else if (tad.IDZona != 0)
                {
                    tADs = tADs.Where(x => x.ID == tad.IDZona);
                }
                else
                {
                    return BadRequest("El destino no cuenta con una Zona asignada");
                }

                return Ok(tADs);

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

        [HttpGet("filtraractivos")]
        public ActionResult Obtener_Grupos_Activos([FromQuery] Destinos destinos)
        {
            try
            {
                var destinosQuery = context.Destinos
                    .Where(x => x.Activo == true)
                    .IgnoreAutoIncludes()
                    .AsQueryable();

                if (!string.IsNullOrEmpty(destinos.NomMS))
                    destinosQuery = destinosQuery.Where(x => x.NomMS.ToLower().Contains(destinos.NomMS.ToLower())
                    && x.Activo == true
                    );

                return Ok(destinosQuery);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

