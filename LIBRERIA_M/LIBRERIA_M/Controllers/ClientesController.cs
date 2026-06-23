using LIBRERIA_M.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var clientes = await _clienteRepository.ListarClientesAsync();

            return Ok(clientes);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Models.ClienteDto cliente)
        {
            await _clienteRepository.CrearClienteAsync(cliente);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar(
        [FromBody] Models.ClienteDto cliente)
        {
            await _clienteRepository.ActualizarClienteAsync(cliente);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            await _clienteRepository.DesactivarClienteAsync(id);

            return Ok();
        }
    }
}
