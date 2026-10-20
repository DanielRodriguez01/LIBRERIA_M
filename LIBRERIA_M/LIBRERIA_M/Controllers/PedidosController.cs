using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LIBRERIA_M.Models.Pedidos;
using LIBRERIA_M.Repositories.Pedido;
using Microsoft.AspNetCore.Mvc;

namespace LIBRERIA_M.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidosController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        // ==========================================
        // 1. LISTADOS Y CONSULTAS
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> ListarPedidos(
            [FromQuery] string? estado,
            [FromQuery] DateTime? fechaDesde,
            [FromQuery] DateTime? fechaHasta,
            [FromQuery] string? buscar)
        {
            var pedidos = await _pedidoRepository.ListarPedidos(estado, fechaDesde, fechaHasta, buscar);
            return Ok(pedidos);
        }

        [HttpGet("pendientes-panel")]
        public async Task<IActionResult> ListarPedidosPendientesPanel([FromQuery] int cantidad = 10)
        {
            var pedidos = await _pedidoRepository.ListarPedidosPendientesPanel(cantidad);
            return Ok(pedidos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var pedido = await _pedidoRepository.ObtenerPorId(id);
            if (pedido == null)
                return NotFound("Pedido no encontrado.");

            return Ok(pedido);
        }

        [HttpGet("{id:int}/detalle")]
        public async Task<IActionResult> ListarDetallePedido(int id)
        {
            var detalle = await _pedidoRepository.ListarDetallePedido(id);
            return Ok(detalle);
        }

        // ==========================================
        // 2. ESCRITURA Y EDICIÓN
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> InsertarPedido([FromBody] CrearPedidoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var idGenerado = await _pedidoRepository.InsertarPedido(dto);
                return Ok(new { IdPedido = idGenerado, Mensaje = "Pedido creado exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("detalle")]
        public async Task<IActionResult> InsertarDetallePedido([FromBody] AgregarDetallePedidoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _pedidoRepository.InsertarDetallePedido(dto);
                return Ok(new { Mensaje = "Producto agregado al detalle correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarPedido([FromBody] ActualizarPedidoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _pedidoRepository.ActualizarPedido(dto);
                return Ok(new { Mensaje = "Pedido actualizado exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("detalle/{idDetallePedido:int}")]
        public async Task<IActionResult> EliminarDetallePedido(int idDetallePedido)
        {
            try
            {
                await _pedidoRepository.EliminarDetallePedido(idDetallePedido);
                return Ok(new { Mensaje = "Detalle eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ==========================================
        // 3. CAMBIOS DE ESTADO DE FLUJO
        // ==========================================

        [HttpPut("{id:int}/terminar")]
        public async Task<IActionResult> TerminarPedido(int id)
        {
            try
            {
                await _pedidoRepository.TerminarPedido(id);
                return Ok(new { Mensaje = "Pedido marcado como Finalizado/Terminado." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}/entregar")]
        public async Task<IActionResult> EntregarPedido(int id)
        {
            try
            {
                await _pedidoRepository.EntregarPedido(id);
                return Ok(new { Mensaje = "Pedido marcado como Entregado exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}/cancelar")]
        public async Task<IActionResult> CancelarPedido(int id)
        {
            try
            {
                await _pedidoRepository.CancelarPedido(id);
                return Ok(new { Mensaje = "Pedido cancelado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}