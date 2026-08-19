using System.Data;
using Dapper;
using LIBRERIA_M.Data;
using LIBRERIA_M.Models;

namespace LIBRERIA_M.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public ClienteRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // ==========================================
        // 1. LISTADOS Y FILTROS (Figma: Listado)
        // ==========================================

        public async Task<IEnumerable<ClienteDto>> ListarClientesAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<ClienteDto>(
                "sp_ListarClientes",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ClienteDto>> ListarClientesAlDiaAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<ClienteDto>(
                "sp_ListarClientesAlDia",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ClienteDto>> ListarClientesConDeudaAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<ClienteDto>(
                "sp_ListarClientesConDeuda",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ClienteDto>> BuscarClientesAsync(string? textoBusqueda)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<ClienteDto>(
                "sp_BuscarClientes",
                new { TextoBusqueda = textoBusqueda },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<ClienteDto?> ObtenerPorIdAsync(int idCliente)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
                SELECT IdCliente, Nombre, Apellido, DNI, Telefono, Direccion, Email, TipoCliente, PorcentajeDescuento, Estado, FechaAlta 
                FROM Cliente WITH (NOLOCK) 
                WHERE IdCliente = @IdCliente;";

            return await connection.QueryFirstOrDefaultAsync<ClienteDto>(
                sql,
                new { IdCliente = idCliente });
        }

        // ==========================================
        // 2. ESCRITURA Y ESTADO (Figma: Formulario)
        // ==========================================

        public async Task CrearClienteAsync(ClienteDto cliente)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_InsertarCliente",
                new
                {
                    cliente.Nombre,
                    cliente.Apellido,
                    cliente.DNI,
                    cliente.Telefono,
                    cliente.Direccion,
                    cliente.Email,
                    cliente.TipoCliente,
                    cliente.PorcentajeDescuento,
                    cliente.Estado
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task ActualizarClienteAsync(ClienteDto cliente)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_ActualizarCliente",
                new
                {
                    cliente.IdCliente,
                    cliente.Nombre,
                    cliente.Apellido,
                    cliente.DNI,
                    cliente.Telefono,
                    cliente.Direccion,
                    cliente.Email,
                    cliente.TipoCliente,
                    cliente.PorcentajeDescuento,
                    cliente.Estado
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DesactivarClienteAsync(int idCliente)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_DesactivarCliente",
                new { IdCliente = idCliente },
                commandType: CommandType.StoredProcedure);
        }

        public async Task ReactivarClienteAsync(int idCliente)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_ReactivarCliente",
                new { IdCliente = idCliente },
                commandType: CommandType.StoredProcedure);
        }

        // ==========================================
        // 3. RESUMEN E HISTORIALES (Figma: Detalles)
        // ==========================================

        public async Task<ClienteResumenDto?> ObtenerResumenClienteAsync(int idCliente)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<ClienteResumenDto>(
                "sp_ResumenCliente",
                new { IdCliente = idCliente },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ClienteHistorialCompraDto>> ObtenerHistorialComprasAsync(int idCliente)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<ClienteHistorialCompraDto>(
                "sp_HistorialComprasCliente",
                new { IdCliente = idCliente },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ClienteHistorialPagoDto>> ObtenerHistorialPagosAsync(int idCliente)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<ClienteHistorialPagoDto>(
                "sp_HistorialPagosCliente",
                new { IdCliente = idCliente },
                commandType: CommandType.StoredProcedure);
        }
    }
}