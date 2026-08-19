using LIBRERIA_M.Models;
using LIBRERIA_M.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LIBRERIA_M.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        // ==========================================
        // 1. LISTADOS Y FILTROS
        // ==========================================

        
        /// Obtiene todos los clientes activos.
        
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var clientes = await _clienteRepository.ListarClientesAsync();
            return Ok(clientes);
        }

        
        /// Obtiene los clientes que no poseen deudas.
        
        [HttpGet("al-dia")]
        public async Task<IActionResult> ListarAlDia()
        {
            var clientes = await _clienteRepository.ListarClientesAlDiaAsync();
            return Ok(clientes);
        }

        
        /// Obtiene los clientes con deudas pendientes.
        
        [HttpGet("con-deuda")]
        public async Task<IActionResult> ListarConDeuda()
        {
            var clientes = await _clienteRepository.ListarClientesConDeudaAsync();
            return Ok(clientes);
        }

        
        /// Búsqueda dinámica de clientes por Nombre, Apellido, DNI o Teléfono.
        
        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] string? texto)
        {
            var clientes = await _clienteRepository.BuscarClientesAsync(texto);
            return Ok(clientes);
        }

        
        /// Obtiene un cliente por su ID.
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var cliente = await _clienteRepository.ObtenerPorIdAsync(id);
            if (cliente == null)
            {
                return NotFound(new { mensaje = "Cliente no encontrado." });
            }
            return Ok(cliente);
        }

        // ==========================================
        // 2. ESCRITURA Y ESTADO 
        // ==========================================

        
        /// Registra un nuevo cliente.
        
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ClienteDto cliente)
        {
            try
            {
                await _clienteRepository.CrearClienteAsync(cliente);
                return Ok(new { mensaje = "Cliente creado exitosamente." });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        
        /// Actualiza los datos de un cliente existente.
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ClienteDto cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest(new { mensaje = "El ID del cliente no coincide con la URL." });
            }

            try
            {
                await _clienteRepository.ActualizarClienteAsync(cliente);
                return Ok(new { mensaje = "Cliente actualizado exitosamente." });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        
        /// Desactiva (borrado lógico) a un cliente.
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            try
            {
                await _clienteRepository.DesactivarClienteAsync(id);
                return Ok(new { mensaje = "Cliente desactivado correctamente." });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        
        /// Reactiva a un cliente previamente desactivado.
        
        [HttpPatch("{id:int}/reactivar")]
        public async Task<IActionResult> Reactivar(int id)
        {
            try
            {
                await _clienteRepository.ReactivarClienteAsync(id);
                return Ok(new { mensaje = "Cliente reactivado correctamente." });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // ==========================================
        // 3. RESUMEN E HISTORIALES
        // ==========================================

        
        /// Obtiene el resumen financiero acumulado del cliente (Cards superiores).
       
        [HttpGet("{id:int}/resumen")]
        public async Task<IActionResult> ObtenerResumen(int id)
        {
            var resumen = await _clienteRepository.ObtenerResumenClienteAsync(id);
            if (resumen == null)
            {
                return NotFound(new { mensaje = "Resumen de cliente no encontrado." });
            }
            return Ok(resumen);
        }

       
        /// Obtiene el historial de compras/ventas asociadas al cliente.
        
        [HttpGet("{id:int}/compras")]
        public async Task<IActionResult> ObtenerHistorialCompras(int id)
        {
            var compras = await _clienteRepository.ObtenerHistorialComprasAsync(id);
            return Ok(compras);
        }

        
        /// Obtiene el historial de abonos y pagos realizados por el cliente.
        
        [HttpGet("{id:int}/pagos")]
        public async Task<IActionResult> ObtenerHistorialPagos(int id)
        {
            var pagos = await _clienteRepository.ObtenerHistorialPagosAsync(id);
            return Ok(pagos);
        }
    }
}