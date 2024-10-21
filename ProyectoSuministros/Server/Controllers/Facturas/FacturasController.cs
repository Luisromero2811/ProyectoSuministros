using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ProyectoSuministros.Shared.DTOs;
using ProyectoSuministros.Shared.Modelos;
using Microsoft.EntityFrameworkCore;
using ProyectoSuministros.Server.Helpers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoSuministros.Server.Controllers.Facturas
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public FacturasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("file")]
        public async Task<ActionResult> Subir_Facturas_Masivas([FromForm] IEnumerable<IFormFile> files)
        {
            if (files is null) { throw new ArgumentNullException(nameof(files)); }

            try
            {
                var MaxAllowedFiles = 10;
                var MaxAllowedSize = 1024 * 1024 * 15;
                var FilesProcesed = 0;

                List<UploadResult> uploadResults = new();

                //Posibles listados de las facturas
                List<Factura> facturas = new();
                List<Factura> facturas_editadas = new();

                foreach (var file in files)
                {
                    var uploadResult = new UploadResult();

                    var unthrustFileName = file.FileName;
                    var thrustFileName = WebUtility.HtmlDecode(unthrustFileName);
                    uploadResult.FileName = thrustFileName;

                    if (FilesProcesed < MaxAllowedFiles)
                    {
                        if (file.Length == 0)
                        {
                            uploadResult.ErrorCode = 2;
                            uploadResult.ErrorMessage = $"(Error: {uploadResult.ErrorCode}), {uploadResult.FileName} : Archivo vacio";
                            return BadRequest(uploadResult.ErrorMessage);
                        }
                        else if (file.Length > MaxAllowedSize)
                        {
                            uploadResult.ErrorCode = 3;
                            uploadResult.ErrorMessage = $"(Error: {uploadResult.ErrorCode}) {uploadResult.FileName} : {file.Length / 1000000} Mb es mayor a la capacidad permitida ({Math.Round((double)(MaxAllowedSize / 1000000))}) Mb";
                            return BadRequest(uploadResult.ErrorMessage);
                        }
                        else
                        {
                            using var stream = new MemoryStream();
                            await file.CopyToAsync(stream);

                            ExcelPackage.LicenseContext = LicenseContext.Commercial;
                            ExcelPackage package = new();

                            package.Load(stream);

                            if (package.Workbook.Worksheets.Count > 0)
                            {
                                using ExcelWorksheet ws = package.Workbook.Worksheets.First();

                                for (int r = 2; r < (ws.Dimension.End.Row + 1); r++)
                                {
                                    var rows = ws.Cells[r, 1, r, 13].ToList();
                                    if (rows.Count > 0)
                                    {
                                        //Validaciones para mostrar que las columnas no deben de estar vacías.

                                        //Tipo de documento
                                        if (ws.Cells[r, 1].Value is null) { return BadRequest($"El tipo de documento no puede estar vacio. (fila: {r}, columna: 1)"); }
                                        var tipo_documento = ws.Cells[r, 1].Value.ToString();
                                        if (string.IsNullOrEmpty(tipo_documento) || string.IsNullOrWhiteSpace(tipo_documento)) { return BadRequest($"El tipo de documento no puede estar vacio. (fila: {r}, columna: 1)"); }

                                        //Número de remisión
                                        if (ws.Cells[r, 2].Value is null) { return BadRequest($"El número de remisión no puede estar vacio. (fila: {r}, columna: 2)"); }
                                        var numero_remision = ws.Cells[r, 2].Value.ToString();
                                        if (string.IsNullOrEmpty(numero_remision) || string.IsNullOrWhiteSpace(numero_remision)) { return BadRequest($"El número de remisión no puede estar vacio. (fila: {r}, columna: 2)"); }

                                        //Número de factura
                                        if (ws.Cells[r, 3].Value is null) { return BadRequest($"El número de factura no puede estar vacio. (fila: {r}, columna: 3)"); }
                                        var numero_factura = ws.Cells[r, 3].Value.ToString();
                                        if (string.IsNullOrEmpty(numero_factura) || string.IsNullOrWhiteSpace(numero_factura)) { return BadRequest($"El número de factura no puede estar vacio. (fila: {r}, columna: 3)"); }

                                        //Producto
                                        if (ws.Cells[r, 4].Value is null) { return BadRequest($"El producto no puede estar vacio. (fila: {r}, columna: 4)"); }
                                        var producto = ws.Cells[r, 4].Value.ToString();
                                        if (string.IsNullOrEmpty(producto) || string.IsNullOrWhiteSpace(producto)) { return BadRequest($"El producto no puede estar vacio. (fila: {r}, columna: 4)"); }

                                        var prd = context.Producto.FirstOrDefault(x => !string.IsNullOrEmpty(x.Nombre) && x.Nombre.Equals(producto) && x.Activo == true);
                                        if (prd is null) { return BadRequest($"No sé encontro el producto registrado {prd?.Nombre}. (fila: {r}, columna: 4)"); }

                                        //Fecha de vencimiento
                                        DateTime fecha_vencimiento = DateTime.Today;
                                        if (ws.Cells[r, 5].Value is null) { return BadRequest($"La fecha de vencimiento no puede estar vacia (fila: {r}, columna: 5)"); }
                                        var fecha = ws.Cells[r, 5].Value.ToString();
                                        if (string.IsNullOrEmpty(fecha) || string.IsNullOrWhiteSpace(fecha)) { return BadRequest($"La fecha no puede estar vacia. (fila: {r}, columna: 5)"); }

                                        if (DateTime.TryParse(fecha, out DateTime fch))
                                            fecha_vencimiento = fch;
                                        else
                                            return BadRequest($"La fecha no tiene un formato valido. (fila: {r}, columna: 5)");

                                        //Destino
                                        if (ws.Cells[r, 6].Value is null) { return BadRequest($"El destino no puede estar vacio. (fila: {r}, columna: 6)"); }
                                        var destino = ws.Cells[r, 6].Value.ToString();
                                        if (string.IsNullOrEmpty(destino) || string.IsNullOrWhiteSpace(destino)) { return BadRequest($"El destino no puede estar vacio. (fila: {r}, columna: 6)"); }

                                        var destinoIdentificador = destino.Split('-')[0];

                                        //Vehiculo
                                        if (ws.Cells[r, 7].Value is null) { return BadRequest($"El vehículo no puede estar vacio. (fila: {r}, columna: 7)"); }
                                        var vehiculo = ws.Cells[r, 7].Value.ToString();
                                        if (string.IsNullOrEmpty(vehiculo) || string.IsNullOrWhiteSpace(vehiculo)) { return BadRequest($"El vehiculo no puede estar vacio. (fila: {r}, columna: 7)"); }

                                        //Volumen natural
                                        double vol_nat = 0;

                                        if (ws.Cells[r, 8].Value is null) { return BadRequest($"El volumen natural no puede estar vacio. (fila: {r}, columna: 8)"); }
                                        var volumen_natural = ws.Cells[r, 8].Value.ToString();
                                        if (string.IsNullOrEmpty(volumen_natural) || string.IsNullOrWhiteSpace(volumen_natural)) { return BadRequest($"El volumen natural no puede estar vacio. (fila: {r}, columna: 8)"); }

                                        if (double.TryParse(volumen_natural, out double vol))
                                            vol_nat = vol;
                                        else
                                            return BadRequest($"No se pudo convertir el volumen natural a un dato valido. (fila: {r}, columna: 8)");

                                        //Temperatura
                                        double temp = 0;

                                        if (ws.Cells[r, 9].Value is null) { return BadRequest($"La temperatura no puede estar vacia. (fila: {r}, columna: 9)"); }
                                        var temperatura = ws.Cells[r, 9].Value.ToString();
                                        if (string.IsNullOrEmpty(tipo_documento) || string.IsNullOrWhiteSpace(tipo_documento)) { return BadRequest($"La temperatura no puede estar vacia. (fila: {r}, columna: 9)"); }

                                        if (double.TryParse(temperatura, out double tmp))
                                            temp = tmp;
                                        else
                                            return BadRequest($"No se pudo convertir la temperatura a un dato valido. (fila {r}, columna: 9)");

                                        //Volumen facturado
                                        double vol_fac = 0;

                                        if (ws.Cells[r, 10].Value is null) { return BadRequest($"El volumen facturado no puede estar vacio. (fila: {r}, columna: 10)"); }
                                        var volumen_facturado = ws.Cells[r, 10].Value.ToString();
                                        if (string.IsNullOrEmpty(volumen_facturado) || string.IsNullOrWhiteSpace(volumen_facturado)) { return BadRequest($"El volumen facturado no puede estar vacio. (fila: {r}, columna: 10)"); }

                                        if (double.TryParse(volumen_facturado, out double volfac))
                                            vol_fac = volfac;
                                        else
                                            return BadRequest($"No se pudo convertir el volumen facturado a un dato valido. (fila: {r}, columna: 10)");

                                        //Unidades
                                        if (ws.Cells[r, 11].Value is null) { return BadRequest($"Las unidades no pueden estar vacias. (fila: {r}, columna: 11)"); }
                                        var unidades = ws.Cells[r, 11].Value.ToString();
                                        if (string.IsNullOrEmpty(unidades) || string.IsNullOrWhiteSpace(unidades)) { return BadRequest($"Las unidades no pueden estar vacias. (fila: {r}, columna: 11)"); }

                                        //Importe
                                        double impor = 0;

                                        if (ws.Cells[r, 12].Value is null) { return BadRequest($"El importe no puede estar vacio. (fila: {r}, columna: 12)"); }
                                        var importe = ws.Cells[r, 12].Value.ToString();
                                        if (string.IsNullOrEmpty(importe) || string.IsNullOrWhiteSpace(importe)) { return BadRequest($"El importe no puede estar vacio. (fila: {r}, columna: 12)"); }

                                        if (double.TryParse(importe, out double imp))
                                            impor = imp;
                                        else
                                            return BadRequest($"No se pudo convertir el importe a un dato factuado. (fila {r}, columna: 12)");

                                        //Moneda
                                        if (ws.Cells[r, 13].Value is null) { return BadRequest($"La moneda no puede estar vacia. (fila: {r}, columna: 13)"); }
                                        var moneda = ws.Cells[r, 13].Value.ToString();
                                        if (string.IsNullOrEmpty(moneda) || string.IsNullOrWhiteSpace(moneda)) { return BadRequest($"La moneda no puede estar vacia. (fila: {r}, columna: 13)"); }

                                        //Instancia a clase factura para dar valor a las propiedades y poder guardarlas
                                        var factura = new Factura()
                                        {
                                            TipDOC = tipo_documento,
                                            NRem = numero_remision,
                                            NFac = numero_factura,
                                            IDProd = prd.ID,
                                            Fch_Ven = fecha_vencimiento,
                                            Destino = destino,
                                            Vehiculo = vehiculo,
                                            VolNat = vol_nat,
                                            Temperatura = temp,
                                            VolFac = vol_fac,
                                            Unidades = unidades,
                                            Importe = impor,
                                            Moneda = moneda,
                                            IDDestino = Convert.ToInt32(destinoIdentificador),
                                            Codest = 1
                                        };

                                        //Practica para ver si se editan o se guardan por primera vez los datos.
                                        //var f = context.Facturas.IgnoreAutoIncludes().FirstOrDefault();

                                        //if (f is not null)
                                        //{

                                        //}
                                        //else
                                        facturas.Add(factura);

                                    }

                                }
                                context.AddRange(facturas);

                                await context.SaveChangesAsync();
                            }
                        }
                    }
                    FilesProcesed++;

                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("filtro")]
        public async Task<ActionResult> GetFacturasFiltro([FromQuery] ParametrosBusquedaFacturasDTO parametros)
        {
            try
            {
                var facturas = context.Facturas.Include(x => x.Producto)
                    .Where(x => x.Codest == 1)
                    .AsQueryable();

                //Filtros
                if (!string.IsNullOrEmpty(parametros.nremision))
                    facturas = facturas.Where(x => x.NRem != null && !string.IsNullOrEmpty(x.NRem) && x.NRem.ToLower().Contains(parametros.nremision.ToLower()));
                if (!string.IsNullOrEmpty(parametros.nfactura))
                    facturas = facturas.Where(x => x.NFac != null && !string.IsNullOrEmpty(x.NFac) && x.NFac.ToLower().Contains(parametros.nfactura.ToLower()));
                if (!string.IsNullOrEmpty(parametros.producto))
                    facturas = facturas.Where(x => x.Producto.Nombre != null && !string.IsNullOrEmpty(x.Producto.Nombre) && x.Producto.Nombre.ToLower().Contains(parametros.producto.ToLower()));
                if (!string.IsNullOrEmpty(parametros.destino))
                    facturas = facturas.Where(x => x.Destino != null && !string.IsNullOrEmpty(x.Destino) && x.Destino.ToLower().Contains(parametros.destino.ToLower()));

                await HttpContext.InsertarParametrosPaginacion(facturas, parametros.tamanopagina, parametros.pagina);

                if (HttpContext.Response.Headers.ContainsKey("pagina"))
                {
                    var pagina = HttpContext.Response.Headers["pagina"];
                    if (pagina != parametros.pagina && !string.IsNullOrEmpty(pagina))
                    {
                        parametros.pagina = int.Parse(pagina!);
                    }
                }

                facturas = facturas.Skip((parametros.pagina - 1) * parametros.tamanopagina).Take(parametros.tamanopagina);

                return Ok(facturas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("cancel/{ID:int}")]
        public async Task<ActionResult> CancelFactura([FromRoute] int ID)
        {
            try
            {
                Factura? factura = context.Facturas.FirstOrDefault(x => x.ID == ID);

                if (factura is null)
                {
                    return NotFound(factura);
                }

                factura.Codest = 2;

                context.Update(factura);

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

