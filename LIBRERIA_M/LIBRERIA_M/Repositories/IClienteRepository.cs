using LIBRERIA_M.Models;

namespace LIBRERIA_M.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteDto>> ListarClientesAsync();

        Task CrearClienteAsync(ClienteDto cliente);

        Task ActualizarClienteAsync(ClienteDto cliente);

        Task DesactivarClienteAsync(int idCliente);
    }
}