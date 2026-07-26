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

        public async Task<IEnumerable<ClienteDto>> ListarClientesAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            var clientes = await connection.QueryAsync<ClienteDto>(
                "sp_ListarClientes",
                commandType: System.Data.CommandType.StoredProcedure);

            return clientes;
        }

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
                commandType: System.Data.CommandType.StoredProcedure);
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
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task DesactivarClienteAsync(int idCliente)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_DesactivarCliente",
                new
                {
                    IdCliente = idCliente
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}