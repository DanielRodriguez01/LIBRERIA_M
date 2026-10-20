using System.Data;
using Dapper;
using LIBRERIA_M.Data;
using LIBRERIA_M.Models.Ventas;

namespace LIBRERIA_M.Repositories.Ventas
{
    public class VentaRepository : IVentaRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public VentaRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> InsertarVenta(VentaCreacionDto venta)
        {
            using var connection = _connectionFactory.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("IdCliente", venta.IdCliente);
            parameters.Add("IdUsuario", venta.IdUsuario);
            parameters.Add("Observaciones", venta.Observaciones);
            parameters.Add("IdPedido", venta.IdPedido);
            parameters.Add("MontoInicialPagado", venta.MontoInicialPagado);
            parameters.Add("TipoPagoInicial", venta.TipoPagoInicial ?? "EFECTIVO");
            parameters.Add("IdVentaGenerada", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "sp_InsertarVenta",
                parameters,
                commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("IdVentaGenerada");
        }

        public async Task InsertarDetalleVenta(int idVenta, DetalleVentaCreacionDto detalle)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_InsertarDetalleVenta",
                new
                {
                    IdVenta = idVenta,
                    detalle.IdProducto,
                    detalle.Cantidad,
                    detalle.DescuentoPorcentaje
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task RecalcularTotalesVenta(int idVenta)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_RecalcularTotalesVenta",
                new
                {
                    IdVenta = idVenta
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<RespuestaPagoVentaDto?> RegistrarPagoVenta(PagoVentaCreacionDto pago)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<RespuestaPagoVentaDto>(
                "sp_RegistrarPagoVenta",
                new
                {
                    pago.IdCliente,
                    pago.Monto,
                    pago.TipoPago,
                    pago.Observaciones,
                    pago.IdUsuario,
                    pago.IdVenta
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PagoVentaListadoDto>> ListarPagosVenta(int idVenta)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<PagoVentaListadoDto>(
                "sp_ListarPagosVenta",
                new
                {
                    IdVenta = idVenta
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<VentaListadoDto>> ListarVentas(string? buscar, DateTime? fechaInicio, DateTime? fechaFin, string? estado)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<VentaListadoDto>(
                "sp_ListarVentas",
                new
                {
                    Buscar = buscar,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    Estado = estado
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<VentaDetalleCompletoDto?> ObtenerVentaPorId(int idVenta)
        {
            using var connection = _connectionFactory.CreateConnection();

            var venta = await connection.QueryFirstOrDefaultAsync<VentaDetalleCompletoDto>(
                "sp_ObtenerVentaPorId",
                new
                {
                    IdVenta = idVenta
                },
                commandType: CommandType.StoredProcedure);

            if (venta != null)
            {
                var detalles = await connection.QueryAsync<DetalleVentaItemDto>(
                    "sp_ListarDetalleVenta",
                    new
                    {
                        IdVenta = idVenta
                    },
                    commandType: CommandType.StoredProcedure);

                venta.Detalles = detalles.ToList();
            }

            return venta;
        }

        public async Task<IEnumerable<DetalleVentaItemDto>> ListarDetalleVenta(int idVenta)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<DetalleVentaItemDto>(
                "sp_ListarDetalleVenta",
                new
                {
                    IdVenta = idVenta
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<NumeroFacturaDto> GenerarNumeroFacturaVenta()
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<NumeroFacturaDto>(
                "sp_GenerarNumeroFacturaVenta",
                commandType: CommandType.StoredProcedure) ?? new NumeroFacturaDto();
        }
    }
}