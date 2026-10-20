using Microsoft.AspNetCore.Mvc;
using LIBRERIA_M.Models.Ventas;
using LIBRERIA_M.Repositories.Ventas;

namespace LIBRERIA_M.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaRepository _ventaRepository;

        public VentasController(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarVentas(
            [FromQuery] string? buscar,
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin,
            [FromQuery] string? estado)
        {
            try
            {
                var ventas = await _ventaRepository.ListarVentas(buscar, fechaInicio, fechaFin, estado);
                return Ok(ventas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error al consultar las ventas.", detalle = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerVentaPorId(int id)
        {
            try
            {
                var venta = await _ventaRepository.ObtenerVentaPorId(id);

                if (venta == null)
                    return NotFound(new { mensaje = $"No se encontró la venta con ID {id}." });

                return Ok(venta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error al obtener la venta.", detalle = ex.Message });
            }
        }

        [HttpGet("{id:int}/pagos")]
        public async Task<IActionResult> ListarPagosVenta(int id)
        {
            try
            {
                var pagos = await _ventaRepository.ListarPagosVenta(id);
                return Ok(pagos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error al obtener los pagos de la venta.", detalle = ex.Message });
            }
        }

        [HttpGet("siguiente-numero")]
        public async Task<IActionResult> GenerarNumeroFactura()
        {
            try
            {
                var numeroFactura = await _ventaRepository.GenerarNumeroFacturaVenta();
                return Ok(numeroFactura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error al generar el correlativo de factura.", detalle = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearVenta([FromBody] VentaCreacionDto ventaDto)
        {
            if (ventaDto == null || ventaDto.Detalles == null || !ventaDto.Detalles.Any())
                return BadRequest(new { mensaje = "La venta debe contener al menos un detalle de producto." });

            try
            {
                // 1. Insertar Cabecera
                int idVentaGenerado = await _ventaRepository.InsertarVenta(ventaDto);

                if (idVentaGenerado <= 0)
                    return BadRequest(new { mensaje = "No se pudo generar la venta." });

                // 2. Insertar cada renglón/producto del detalle
                foreach (var detalle in ventaDto.Detalles)
                {
                    await _ventaRepository.InsertarDetalleVenta(idVentaGenerado, detalle);
                }

                // 3. Recalcular Totales y Estado de la venta
                await _ventaRepository.RecalcularTotalesVenta(idVentaGenerado);

                // 4. Retornar la venta completa creada
                var ventaCreada = await _ventaRepository.ObtenerVentaPorId(idVentaGenerado);
                return CreatedAtAction(nameof(ObtenerVentaPorId), new { id = idVentaGenerado }, ventaCreada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("pago")]
        public async Task<IActionResult> RegistrarPago([FromBody] PagoVentaCreacionDto pagoDto)
        {
            if (pagoDto == null || pagoDto.Monto <= 0)
                return BadRequest(new { mensaje = "El monto del pago debe ser mayor a cero." });

            try
            {
                // Si el IdCliente no vino desde el frontend, obtenemos la venta para extraer su IdCliente
                if (!pagoDto.IdCliente.HasValue || pagoDto.IdCliente.Value <= 0)
                {
                    var ventaExistente = await _ventaRepository.ObtenerVentaPorId(pagoDto.IdVenta);
                    if (ventaExistente != null)
                    {
                        pagoDto.IdCliente = ventaExistente.IdCliente;
                    }
                }

                var resultado = await _ventaRepository.RegistrarPagoVenta(pagoDto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}