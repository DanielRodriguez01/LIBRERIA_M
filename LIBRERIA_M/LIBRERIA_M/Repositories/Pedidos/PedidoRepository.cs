using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LIBRERIA_M.Data;

namespace LIBRERIA_M.Repositories.Pedido
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public PedidoRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // ==========================================
        // 1. LISTADOS Y CONSULTAS
        // ==========================================

        public async Task<IEnumerable<LIBRERIA_M.Models.Pedidos.PedidoListadoDto>> ListarPedidos(string? estado = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null, string? buscar = null)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<LIBRERIA_M.Models.Pedidos.PedidoListadoDto>(
                "sp_ListarPedidos",
                new
                {
                    Estado = estado,
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta,
                    Buscar = buscar
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<LIBRERIA_M.Models.Pedidos.PedidoDTO>> ListarPedidosPendientesPanel(int cantidad = 10)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<LIBRERIA_M.Models.Pedidos.PedidoDTO>(
                "sp_PanelControlPedidosPendientes",
                new { Cantidad = cantidad },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<LIBRERIA_M.Models.Pedidos.PedidoDTO?> ObtenerPorId(int idPedido)
        {
            using var connection = _connectionFactory.CreateConnection();

            var pedido = await connection.QueryFirstOrDefaultAsync<LIBRERIA_M.Models.Pedidos.PedidoDTO>(
                "sp_ObtenerPedidoPorId",
                new { IdPedido = idPedido },
                commandType: CommandType.StoredProcedure);

            if (pedido != null)
            {
                var detalle = await ListarDetallePedido(idPedido);
                pedido.Detalle = detalle.AsList();
            }

            return pedido;
        }

        public async Task<IEnumerable<LIBRERIA_M.Models.Pedidos.DetallePedidoDTO>> ListarDetallePedido(int idPedido)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<LIBRERIA_M.Models.Pedidos.DetallePedidoDTO>(
                "sp_ListarDetallePedido",
                new { IdPedido = idPedido },
                commandType: CommandType.StoredProcedure);
        }

        // ==========================================
        // 2. ESCRITURA Y EDICIÓN
        // ==========================================

        private class ResultadoInsertarPedido
        {
            public int IdPedidoGenerado { get; set; }
        }

        public async Task<int> InsertarPedido(LIBRERIA_M.Models.Pedidos.CrearPedidoDTO dto)
        {
            using var connection = _connectionFactory.CreateConnection();

            var resultado = await connection.QueryFirstOrDefaultAsync<ResultadoInsertarPedido>(
                "sp_InsertarPedido",
                new
                {
                    dto.IdCliente,
                    dto.IdUsuario,
                    dto.FechaEntrega,
                    dto.Observaciones,
                    dto.OrigenPedido
                },
                commandType: CommandType.StoredProcedure);

            if (resultado != null && resultado.IdPedidoGenerado > 0)
            {
                int idPedido = resultado.IdPedidoGenerado;

                if (dto.Detalles != null && dto.Detalles.Count > 0)
                {
                    foreach (var det in dto.Detalles)
                    {
                        det.IdPedido = idPedido;
                        await InsertarDetallePedido(det);
                    }
                }

                return idPedido;
            }

            throw new InvalidOperationException("No se pudo obtener el ID del pedido generado.");
        }

        public async Task InsertarDetallePedido(LIBRERIA_M.Models.Pedidos.AgregarDetallePedidoDTO dto)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_InsertarDetallePedido",
                new
                {
                    dto.IdPedido,
                    dto.IdProducto,
                    dto.Cantidad
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task ActualizarPedido(LIBRERIA_M.Models.Pedidos.ActualizarPedidoDTO dto)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_ActualizarPedido",
                new
                {
                    dto.IdPedido,
                    dto.FechaEntrega,
                    dto.Observaciones,
                    dto.OrigenPedido
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task EliminarDetallePedido(int idDetallePedido)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_EliminarDetallePedido",
                new { IdDetallePedido = idDetallePedido },
                commandType: CommandType.StoredProcedure);
        }

        // ==========================================
        // 3. CAMBIOS DE ESTADO DE FLUJO
        // ==========================================

        public async Task TerminarPedido(int idPedido)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_TerminarPedido",
                new { IdPedido = idPedido },
                commandType: CommandType.StoredProcedure);
        }

        public async Task EntregarPedido(int idPedido)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_EntregarPedido",
                new { IdPedido = idPedido },
                commandType: CommandType.StoredProcedure);
        }

        public async Task CancelarPedido(int idPedido)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_CancelarPedido",
                new { IdPedido = idPedido },
                commandType: CommandType.StoredProcedure);
        }
    }
}