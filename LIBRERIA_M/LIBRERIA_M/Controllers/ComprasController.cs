using LIBRERIA_M.Models.Compra;
using LIBRERIA_M.Models.Proveedor;
using LIBRERIA_M.Repositories.Compra;
using Microsoft.AspNetCore.Mvc;

namespace LIBRERIA_M.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraRepository _compraRepository;

        public ComprasController(ICompraRepository compraRepository)
        {
            _compraRepository = compraRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarCompras(
            [FromQuery] string? busqueda,
            [FromQuery] int? idProveedor,
            [FromQuery] DateTime? fechaDesde,
            [FromQuery] DateTime? fechaHasta,
            [FromQuery] string? estado)
        {
            var result = await _compraRepository.ListarCompras(busqueda, idProveedor, fechaDesde, fechaHasta, estado);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerCompraPorId(int id)
        {
            var compra = await _compraRepository.ObtenerCompraPorId(id);
            if (compra == null) return NotFound("Compra no encontrada.");

            return Ok(compra);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCompra([FromBody] CrearCompraDto dto)
        {
            if (dto == null || dto.Detalles == null || !dto.Detalles.Any())
                return BadRequest("La compra debe incluir al menos un producto.");

            try
            {
                var idGenerado = await _compraRepository.CrearCompra(dto);
                return Ok(new { IdCompra = idGenerado, Mensaje = "Compra registrada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("pagos")]
        public async Task<IActionResult> RegistrarPago([FromBody] RegistrarPagoProveedorDto dto)
        {
            if (dto == null || dto.Monto <= 0)
                return BadRequest("El monto del pago debe ser mayor a cero.");

            try
            {
                var exito = await _compraRepository.RegistrarPago(dto);
                if (!exito) return BadRequest("No se pudo registrar el pago.");

                return Ok(new { Mensaje = "Pago registrado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("proveedores")]
        public async Task<IActionResult> ObtenerProveedoresSelector()
        {
            var proveedores = await _compraRepository.ObtenerProveedoresSelector();
            return Ok(proveedores);
        }
    }
}