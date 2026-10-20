using LIBRERIA_M.Models.Clientes;

namespace LIBRERIA_M.Repositories.Cliente
{
    public interface IClienteRepository
    {
        // 1. Listados y Filtros
        Task<IEnumerable<ClienteDto>> ListarClientesAsync();
        Task<IEnumerable<ClienteDto>> ListarClientesAlDiaAsync();
        Task<IEnumerable<ClienteDto>> ListarClientesConDeudaAsync();
        Task<IEnumerable<ClienteDto>> BuscarClientesAsync(string? textoBusqueda);
        Task<ClienteDto?> ObtenerPorIdAsync(int idCliente);

        // 2. Escritura y Estado
        Task CrearClienteAsync(ClienteDto cliente);
        Task ActualizarClienteAsync(ClienteDto cliente);
        Task DesactivarClienteAsync(int idCliente);
        Task ReactivarClienteAsync(int idCliente);

        // 3. Resumen, Historiales y Pagos
        Task<ClienteResumenDto?> ObtenerResumenClienteAsync(int idCliente);
        Task<IEnumerable<ClienteHistorialCompraDto>> ObtenerHistorialComprasAsync(int idCliente);
        Task<IEnumerable<ClienteHistorialPagoDto>> ObtenerHistorialPagosAsync(int idCliente);
        Task<(bool Exito, string Mensaje)> RegistrarPagoAsync(RegistrarPagoClienteDto pagoDto);
    }
}