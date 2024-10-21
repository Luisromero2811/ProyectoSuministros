using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoSuministros.Shared.DTOs;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoSuministros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : Controller
    {
        private readonly ApplicationDbContext context;

        public ComprasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        private record Compras_Grafica(string Mes, double? Volumen);
        [HttpGet("MensualClienteSimsa")]
        public ActionResult Datos_ComprasMensualesClientesSIMSA()
        {
            try
            {
                //Hacemos conexion al contexto de Facturas y seleccionamos el volumen facturado (vendido) y mes (extraído de la fecha de vencimiento) 
                var facturas = context.Facturas
                    .Select(x => new { x.VolFac, Mes = x.Fch_Ven.ToString("MMMM").ToUpper() })
                    .ToList();
                //Obtenemos la lista de meses distintos
                var meses = facturas.DistinctBy(x => x.Mes).Select(x => x.Mes);

                List<Compras_Grafica> mes_total = new();

                foreach (var mes in meses)
                {
                    // Sumar los volúmenes por mes
                    var total = facturas.Where(x => x.Mes == mes).Sum(x => x.VolFac);
                    mes_total.Add(new(mes, total));
                }

                return Ok(mes_total);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private record Compras_GraficaMensuales(string Mes, double? VolumenCliente, double? VolumenSimsa);
        [HttpGet("ComprasMensuales2024")]
        public ActionResult Datos_ComprasMensuales2024()
        {
            try
            {
                //Año que nos interesa filtrar
                int year = 2024;

                //Obtenemos del contexto facturas los volúmenes facturados del año 2024 separando por tipo de cliente ("Cliente" y "Simsa")
                var facturas = context.Facturas
                    .Where(x => x.Fch_Ven.Year == year)
                    .Include(x => x.Destinos)
                    .ThenInclude(x => x.RazonSocial)
                    .ThenInclude(x => x.Grupo)
                    .Select(x => new
                    {
                        x.VolFac,
                        Mes = x.Fch_Ven.ToString("MMMM").ToUpper(),
                        ClienteNombre = x.Destinos.RazonSocial.Grupo.Nombre
                    })
                    .ToList();

                //Obtenemos la lista de meses distintos
                var meses = facturas.DistinctBy(x => x.Mes).Select(x => x.Mes).ToList();

                List<Compras_GraficaMensuales> comprasMensuales = new();

                foreach (var mes in meses)
                {
                    //Sumar los volúmenes por mes para clientes "Cliente"
                    var totalClientes = facturas
                        .Where(x => x.Mes == mes && x.ClienteNombre != "SIMSA")
                        .Sum(x => x.VolFac);

                    var totalSimsa = facturas
                        .Where(x => x.Mes == mes && x.ClienteNombre == "SIMSA")
                        .Sum(x => x.VolFac);

                    comprasMensuales.Add(new Compras_GraficaMensuales(mes, totalClientes, totalSimsa));
                }


                return Ok(comprasMensuales);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private record Compras_GraficaClientes(string Mes, double? Volumen);
        [HttpGet("ComprasMensualesClientes2023-2024")]
        public ActionResult Datos_ComprasMensualesClientes2023_2024()
        {
            try
            {
                var facturas = context.Facturas
                    .Include(x => x.Destinos)
                    .ThenInclude(x => x.RazonSocial)
                    .ThenInclude(x => x.Grupo)
                    .Where(x => (x.Fch_Ven.Year == 2023 || x.Fch_Ven.Year == 2024) && x.Destinos.RazonSocial.Grupo.Nombre != "SIMSA")
                    .Select(x => new
                    {
                        x.VolFac,
                        Mes = x.Fch_Ven.ToString("MMMM").ToUpper(),
                        Año = x.Fch_Ven.Year
                    })
                    .ToList();

                //Obtener la lista de meses distintos considerando también el año
                var meses = facturas
                    .DistinctBy(x => new { x.Mes, x.Año })
                    .Select(x => new { x.Mes, x.Año })
                    .OrderBy(x => x.Año)
                    .ThenBy(x => x.Mes);
                List<Compras_GraficaClientes> mes_total = new();

                foreach (var mes in meses)
                {
                    //Sumar los volúmenes por mes y año
                    var total = facturas.Where(x => x.Mes == mes.Mes && x.Año == mes.Año).Sum(x => x.VolFac);
                    mes_total.Add(new($"{mes.Mes} {mes.Año}", total));
                }

                return Ok(mes_total);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private record Volumen_Grupo(string Grupo, double? Volumen);
        [HttpGet("volumenFacturadoGrupo")]
        public IActionResult GetVolumenFacturadoGrupo([FromQuery] int month, [FromQuery] int year)
        {
            try
            {
                var facturas = context.Facturas
                    .Include(x => x.Destinos)
                    .ThenInclude(x => x.RazonSocial)
                    .ThenInclude(x => x.Grupo)
                    .Where(x => x.Fch_Ven.Month == month && x.Fch_Ven.Year == year)
                    .GroupBy(x => new
                    {
                        GrupoId = x.Destinos.RazonSocial.Grupo.ID,
                        GrupoNombre = x.Destinos.RazonSocial.Grupo.Nombre
                    })
                    .Select(x => new
                    {
                        Grupo = x.Key.GrupoNombre,
                        VolumenTotal = x.Sum(x => x.VolFac)
                    })
                    .OrderByDescending(x => x.VolumenTotal)
                    .Take(10)
                    .ToList();

                var mes_total = facturas
                    .Select(x => new Volumen_Grupo(x.Grupo, x.VolumenTotal))
                    .ToList();

                return Ok(mes_total);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private record Comportamiento_Diario(DateTime Fecha, double? VolumenCliente, double? VolumenSimsa);
        [HttpGet("ComportamientoDiario")]
        public IActionResult GetComportamientoDiario()
        {
            try
            {
                //Obtenemos el mes actual
                DateTime fechaActual = DateTime.Now;
                int mesActual = fechaActual.Month;
                int yearActual = fechaActual.Year;

                //Obtenemos del contexto las facturas filtradas por el mes y año actual
                var facturas = context.Facturas
                    .Where(x => x.Fch_Ven.Month == mesActual && x.Fch_Ven.Year == yearActual)
                    .Include(x => x.Destinos)
                    .ThenInclude(x => x.RazonSocial)
                    .ThenInclude(x => x.Grupo)
                    .Select(x => new
                    {
                        x.VolFac,
                        Fecha = x.Fch_Ven,
                        ClienteNombre = x.Destinos.RazonSocial.Grupo.Nombre
                    })
                    .ToList();

                //Obtenemos todas las fechas del mes actual
                var diasDelMes = Enumerable.Range(1, DateTime.DaysInMonth(yearActual, mesActual))
                    .Select(day => new DateTime(yearActual, mesActual, day))
                    .ToList();

                //Lista donde se agregarán los datos del comportamiento diario
                List<Comportamiento_Diario> comportamientoDiario = new();

                foreach (var dia in diasDelMes)
                {
                    //Sumamos los volumenes de compras para clientes que no son SIMSA
                    var totalClientes = facturas
                        .Where(x => x.Fecha.Date == dia && x.ClienteNombre != "SIMSA")
                        .Sum(x => x.VolFac);
                    //Sumamos los volumenes de compra para clientes que son SIMSA
                    var totalSIMSA = facturas
                        .Where(x => x.Fecha.Date == dia && x.ClienteNombre == "SIMSA")
                        .Sum(x => x.VolFac);

                    //Agregamos el resultado para ese dia
                    comportamientoDiario.Add(new Comportamiento_Diario(dia, totalClientes, totalSIMSA));
                }

                return Ok(comportamientoDiario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ComportamientoDiarioSeleccionado")]
        public IActionResult GetComportamientoDiarioSeleccionado([FromQuery]int month, [FromQuery] int year)
        {
            try
            {
                //Obtenemos del contexto las facturas filtradas por el mes y año seleccionados
                var facturas = context.Facturas
                    .Where(x => x.Fch_Ven.Month == month && x.Fch_Ven.Year == year)
                    .Include(x => x.Destinos)
                    .ThenInclude(x => x.RazonSocial)
                    .ThenInclude(x => x.Grupo)
                    .Select(x => new
                    {
                        x.VolFac,
                        Fecha = x.Fch_Ven,
                        ClienteNombre = x.Destinos.RazonSocial.Grupo.Nombre
                    })
                    .ToList();

                //Obtenemos todas las fechas del mes seleccionado
                var diasDelMes = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                    .Select(day => new DateTime(year, month, day))
                    .ToList();

                //Lista donde se agregarán los datos del comportamiento diario
                List<Comportamiento_Diario> comportamientoDiario = new();

                foreach (var dia in diasDelMes)
                {
                    //Sumamos los volumenes de compras para clientes que no son SIMSA
                    var totalClientes = facturas
                        .Where(x => x.Fecha.Date == dia && x.ClienteNombre != "SIMSA")
                        .Sum(x => x.VolFac);
                    //Sumamos los volumenes de compra para clientes que son SIMSA
                    var totalSIMSA = facturas
                        .Where(x => x.Fecha.Date == dia && x.ClienteNombre == "SIMSA")
                        .Sum(x => x.VolFac);

                    //Agregamos el resultado para ese dia
                    comportamientoDiario.Add(new Comportamiento_Diario(dia, totalClientes, totalSIMSA));
                }

                return Ok(comportamientoDiario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        private record VolumenCliente_Destino(string Destino, double? Volumen);
        [HttpGet("VolumenDestinoCliente")]
        public IActionResult GetVolumenDestinoCLIENTE([FromQuery] int month, [FromQuery] int year)
        {
            try
            {
                var facturas = context.Facturas
                    .Include(x => x.Destinos)
                    .ThenInclude(x => x.RazonSocial)
                    .ThenInclude(x => x.Grupo)
                    .Where(x => x.Fch_Ven.Month == month && x.Fch_Ven.Year == year && x.Destinos.RazonSocial.Grupo.Nombre != "SIMSA")
                    .GroupBy(x => x.Destinos.Estacion)
                    .Select(x => new
                    {
                        Destino = x.Key,
                        VolumenTotal = x.Sum(x => x.VolFac)
                    })
                    .OrderByDescending(x => x.VolumenTotal)
                    .Take(10)
                    .ToList();

                //Mapeamos la estructura de VolumenCliente_Destino
                var topDestinos = facturas
                    .Select(x => new VolumenCliente_Destino(x.Destino, x.VolumenTotal))
                    .ToList();

                return Ok(topDestinos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private record VolumenSIMSA_Destino(string Destino, double? Volumen);
        [HttpGet("VolumenDestinoSIMSA")]
        public IActionResult GetVolumenDestinoSIMSA([FromQuery] int month, [FromQuery] int year)
        {
            try
            {
                var facturas = context.Facturas
                 .Include(x => x.Destinos)
                 .ThenInclude(x => x.RazonSocial)
                 .ThenInclude(x => x.Grupo)
                 .Where(x => x.Fch_Ven.Month == month && x.Fch_Ven.Year == year && x.Destinos.RazonSocial.Grupo.Nombre == "SIMSA")
                 .GroupBy(x => x.Destinos.Estacion)
                 .Select(x => new
                 {
                     Destino = x.Key,
                     VolumenTotal = x.Sum(x => x.VolFac)
                 })
                 .OrderByDescending(x => x.VolumenTotal)
                 .Take(10)
                 .ToList();

                //Mapeamos la estructura de VolumenCliente_Destino
                var topDestinos = facturas
                    .Select(x => new VolumenCliente_Destino(x.Destino, x.VolumenTotal))
                    .ToList();

                return Ok(topDestinos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private record HistorialCompras(string Producto, string Mes, double? Volumen);
        [HttpGet("HistorialCompras")]
        public IActionResult GetHistorialCompras()
        {
            try
            {
                //Obtenemos del contexto Facturas los volúmenes facturados del año 2024 y 2023 de los clientes "CLIENTE" para seleccionar los productos que vendieron por mes.
                var facturas = context.Facturas
                    .Include(x => x.Destinos)
                    .ThenInclude(x => x.RazonSocial)
                    .ThenInclude(x => x.Cliente)
                    .Include(x => x.Producto)
                    .Where(x => (x.Fch_Ven.Year == 2023 || x.Fch_Ven.Year == 2024)
                     && x.Destinos.RazonSocial.Grupo.Nombre != "SIMSA")
                    .Select(x => new
                    {
                        x.VolFac,
                        Producto = x.Producto.Nombre,
                        Mes = x.Fch_Ven.ToString("MMMM").ToUpper(),
                        Año = x.Fch_Ven.Year
                    }).ToList();

                //Agrupar por Producto, Mes y Año y así sumar los volumenes
                var comprasPorProducto = facturas
                    .GroupBy(x => new { x.Producto, x.Mes, x.Año })
                    .Select(x => new HistorialCompras(
                        x.Key.Producto,
                        $"{x.Key.Mes} {x.Key.Año}",
                        x.Sum(x => x.VolFac)
                    ))
                    .OrderBy(x => x.Mes)
                    .ThenBy(x => x.Producto)
                    .ToList();

                return Ok(comprasPorProducto);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

