using LIBRERIA_M.Models;

namespace LIBRERIA_M.Repositories
{
    public interface IClienteRepository
    {
        // 1. Listados y Filtros (Figma - Listado de Clientes)
        Task<IEnumerable<ClienteDto>> ListarClientesAsync();
        Task<IEnumerable<ClienteDto>> ListarClientesAlDiaAsync();
        Task<IEnumerable<ClienteDto>> ListarClientesConDeudaAsync();
        Task<IEnumerable<ClienteDto>> BuscarClientesAsync(string? textoBusqueda);
        Task<ClienteDto?> ObtenerPorIdAsync(int idCliente);

        // 2. Operaciones de Escritura y Estado
        Task CrearClienteAsync(ClienteDto cliente);
        Task ActualizarClienteAsync(ClienteDto cliente);
        Task DesactivarClienteAsync(int idCliente);
        Task ReactivarClienteAsync(int idCliente);

        // 3. Resumen e Historiales (Figma - Detalle de Cliente)
        Task<ClienteResumenDto?> ObtenerResumenClienteAsync(int idCliente);
        Task<IEnumerable<ClienteHistorialCompraDto>> ObtenerHistorialComprasAsync(int idCliente);
        Task<IEnumerable<ClienteHistorialPagoDto>> ObtenerHistorialPagosAsync(int idCliente);
    }
}