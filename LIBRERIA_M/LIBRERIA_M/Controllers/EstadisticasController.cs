using LIBRERIA_M.Repositories.Estadisticas;
using Microsoft.AspNetCore.Mvc;

namespace LIBRERIA_M.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadisticasController : ControllerBase
    {
        private readonly IEstadisticasRepository _estadisticasRepository;

        public EstadisticasController(IEstadisticasRepository estadisticasRepository)
        {
            _estadisticasRepository = estadisticasRepository;
        }

        [HttpGet("resumen")]
        public async Task<IActionResult> ObtenerResumen([FromQuery] DateTime? fechaDesde, [FromQuery] DateTime? fechaHasta)
        {
            try
            {
                var resultado = await _estadisticasRepository.ObtenerResumenEstadisticas(fechaDesde, fechaHasta);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ventas-por-dia")]
        public async Task<IActionResult> ObtenerVentasPorDia([FromQuery] DateTime? fechaDesde, [FromQuery] DateTime? fechaHasta)
        {
            try
            {
                var resultado = await _estadisticasRepository.ObtenerVentasPorDia(fechaDesde, fechaHasta);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("productos-mas-vendidos")]
        public async Task<IActionResult> ObtenerProductosMasVendidos([FromQuery] DateTime? fechaDesde, [FromQuery] DateTime? fechaHasta, [FromQuery] int top = 10)
        {
            try
            {
                var resultado = await _estadisticasRepository.ObtenerProductosMasVendidos(fechaDesde, fechaHasta, top);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("estado-financiero")]
        public async Task<IActionResult> ObtenerEstadoFinanciero([FromQuery] DateTime? fechaDesde, [FromQuery] DateTime? fechaHasta)
        {
            try
            {
                var resultado = await _estadisticasRepository.ObtenerEstadoFinanciero(fechaDesde, fechaHasta);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("resumen-compras")]
        public async Task<IActionResult> ObtenerResumenCompras([FromQuery] DateTime? fechaDesde, [FromQuery] DateTime? fechaHasta)
        {
            try
            {
                var resultado = await _estadisticasRepository.ObtenerResumenCompras(fechaDesde, fechaHasta);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}