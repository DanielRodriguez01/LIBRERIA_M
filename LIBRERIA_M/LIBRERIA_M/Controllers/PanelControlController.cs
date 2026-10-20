using System;
using System.Threading.Tasks;
using LIBRERIA_M.Repositories.Panel_de_Control;
using Microsoft.AspNetCore.Mvc;

namespace LIBRERIA_M.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PanelControlController : ControllerBase
    {
        private readonly IPanelControlRepository _panelControlRepository;

        public PanelControlController(IPanelControlRepository panelControlRepository)
        {
            _panelControlRepository = panelControlRepository;
        }

        [HttpGet("resumen")]
        public async Task<IActionResult> ObtenerResumen([FromQuery] DateTime? fechaReferencia)
        {
            var resumen = await _panelControlRepository.ObtenerResumen(fechaReferencia);
            return Ok(resumen);
        }

        [HttpGet("ventas-por-dia")]
        public async Task<IActionResult> ObtenerVentasPorDia([FromQuery] DateTime? fechaDesde, [FromQuery] DateTime? fechaHasta)
        {
            var ventas = await _panelControlRepository.ObtenerVentasPorDia(fechaDesde, fechaHasta);
            return Ok(ventas);
        }

        [HttpGet("productos-mas-vendidos")]
        public async Task<IActionResult> ObtenerProductosMasVendidos([FromQuery] int top = 4)
        {
            var productos = await _panelControlRepository.ObtenerProductosMasVendidos(top);
            return Ok(productos);
        }

        [HttpGet("pedidos-pendientes")]
        public async Task<IActionResult> ObtenerPedidosPendientes([FromQuery] int cantidad = 10)
        {
            var pedidos = await _panelControlRepository.ObtenerPedidosPendientes(cantidad);
            return Ok(pedidos);
        }
    }
}