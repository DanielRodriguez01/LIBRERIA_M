using LIBRERIA_M.Models;
using LIBRERIA_M.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LIBRERIA_M.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var productos = await _productoRepository.ListarProductos();

            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var producto = await _productoRepository.ObtenerProductoPorId(id);

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoDto producto)
        {
            await _productoRepository.CrearProducto(producto);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] ProductoDto producto)
        {
            await _productoRepository.ActualizarProducto(producto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            await _productoRepository.DesactivarProducto(id);

            return Ok();
        }

        [HttpPut("reactivar/{id}")]
        public async Task<IActionResult> Reactivar(int id)
        {
            await _productoRepository.ReactivarProducto(id);

            return Ok();
        }
    }
}
