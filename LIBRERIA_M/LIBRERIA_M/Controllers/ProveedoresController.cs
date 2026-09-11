using Microsoft.AspNetCore.Mvc;
using LIBRERIA_M.Models.Proveedor;
using LIBRERIA_M.Repositories.Proveedor;

namespace LIBRERIA_M.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ProveedoresController(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        // GET: api/proveedores?soloActivos=true
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDto>>> ListarProveedores([FromQuery] bool soloActivos = true)
        {
            var proveedores = await _proveedorRepository.ListarProveedores(soloActivos);
            return Ok(proveedores);
        }

        // GET: api/proveedores/buscar?termino=abc&soloActivos=true
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ProveedorDto>>> BuscarProveedores([FromQuery] string? termino, [FromQuery] bool soloActivos = true)
        {
            var proveedores = await _proveedorRepository.BuscarProveedores(termino, soloActivos);
            return Ok(proveedores);
        }

        // GET: api/proveedores/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProveedorDto>> ObtenerProveedorPorId(int id)
        {
            var proveedor = await _proveedorRepository.ObtenerProveedorPorId(id);
            if (proveedor == null)
            {
                return NotFound(new { mensaje = $"No se encontró el proveedor con ID {id}." });
            }
            return Ok(proveedor);
        }

        // POST: api/proveedores
        [HttpPost]
        public async Task<ActionResult<int>> CrearProveedor([FromBody] CrearProveedorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevoId = await _proveedorRepository.CrearProveedor(dto);
            return CreatedAtAction(nameof(ObtenerProveedorPorId), new { id = nuevoId }, nuevoId);
        }

        // PUT: api/proveedores/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> ActualizarProveedor(int id, [FromBody] ActualizarProveedorDto dto)
        {
            if (id != dto.IdProveedor)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el objeto enviado." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exito = await _proveedorRepository.ActualizarProveedor(dto);
            if (!exito)
            {
                return NotFound(new { mensaje = "No se pudo actualizar el proveedor." });
            }

            return NoContent();
        }

        // PATCH: api/proveedores/5/desactivar
        [HttpPatch("{id:int}/desactivar")]
        public async Task<IActionResult> DesactivarProveedor(int id)
        {
            var exito = await _proveedorRepository.DesactivarProveedor(id);
            if (!exito)
            {
                return NotFound(new { mensaje = "No se pudo desactivar el proveedor." });
            }

            return NoContent();
        }

        // PATCH: api/proveedores/5/activar
        [HttpPatch("{id:int}/activar")]
        public async Task<IActionResult> ActivarProveedor(int id)
        {
            var exito = await _proveedorRepository.ActivarProveedor(id);
            if (!exito)
            {
                return NotFound(new { mensaje = "No se pudo activar el proveedor." });
            }

            return NoContent();
        }

        // GET: api/proveedores/5/resumen
        [HttpGet("{id:int}/resumen")]
        public async Task<ActionResult<ProveedorResumenDto>> ObtenerResumenProveedor(int id)
        {
            var resumen = await _proveedorRepository.ObtenerResumenProveedor(id);
            if (resumen == null)
            {
                return NotFound(new { mensaje = $"No se encontró información del proveedor con ID {id}." });
            }
            return Ok(resumen);
        }

        // GET: api/proveedores/5/compras
        [HttpGet("{id:int}/compras")]
        public async Task<ActionResult<IEnumerable<ProveedorHistorialCompraDto>>> ObtenerHistorialComprasProveedor(int id)
        {
            var compras = await _proveedorRepository.ObtenerHistorialComprasProveedor(id);
            return Ok(compras);
        }

        // GET: api/proveedores/5/pagos
        [HttpGet("{id:int}/pagos")]
        public async Task<ActionResult<IEnumerable<ProveedorHistorialPagoDto>>> ObtenerHistorialPagosProveedor(int id)
        {
            var pagos = await _proveedorRepository.ObtenerHistorialPagosProveedor(id);
            return Ok(pagos);
        }

        // POST: api/proveedores/pagos
        [HttpPost("pagos")]
        public async Task<IActionResult> RegistrarPagoProveedor([FromBody] RegistrarPagoProveedorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exito = await _proveedorRepository.RegistrarPagoProveedor(dto);
            if (!exito)
            {
                return BadRequest(new { mensaje = "No se pudo registrar el pago al proveedor." });
            }

            return Ok(new { mensaje = "Pago registrado correctamente." });
        }
    }
}