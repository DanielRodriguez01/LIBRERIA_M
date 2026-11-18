using System.Data;
using Dapper;
using LIBRERIA_M.Data;
using LIBRERIA_M.Models.Estadisticas;

namespace LIBRERIA_M.Repositories.Estadisticas
{
    public class EstadisticasRepository : IEstadisticasRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public EstadisticasRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<EstadisticasResumenDto> ObtenerResumenEstadisticas(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@FechaDesde", fechaDesde);
            parameters.Add("@FechaHasta", fechaHasta);

            var resultado = await connection.QueryFirstOrDefaultAsync<EstadisticasResumenDto>(
                "dbo.sp_EstadisticasResumen",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return resultado ?? new EstadisticasResumenDto();
        }

        public async Task<IEnumerable<EstadisticasVentasPorDiaDto>> ObtenerVentasPorDia(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@FechaDesde", fechaDesde);
            parameters.Add("@FechaHasta", fechaHasta);

            return await connection.QueryAsync<EstadisticasVentasPorDiaDto>(
                "dbo.sp_EstadisticasVentasPorDia",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<EstadisticasProductoMasVendidoDto>> ObtenerProductosMasVendidos(DateTime? fechaDesde, DateTime? fechaHasta, int top = 10)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@FechaDesde", fechaDesde);
            parameters.Add("@FechaHasta", fechaHasta);
            parameters.Add("@Top", top);

            return await connection.QueryAsync<EstadisticasProductoMasVendidoDto>(
                "dbo.sp_EstadisticasProductosMasVendidos",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<EstadisticasEstadoFinancieroDto> ObtenerEstadoFinanciero(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@FechaDesde", fechaDesde);
            parameters.Add("@FechaHasta", fechaHasta);

            var resultado = await connection.QueryFirstOrDefaultAsync<EstadisticasEstadoFinancieroDto>(
                "dbo.sp_EstadisticasEstadoFinanciero",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return resultado ?? new EstadisticasEstadoFinancieroDto();
        }

        public async Task<EstadisticasResumenComprasDto> ObtenerResumenCompras(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@FechaDesde", fechaDesde);
            parameters.Add("@FechaHasta", fechaHasta);

            var resultado = await connection.QueryFirstOrDefaultAsync<EstadisticasResumenComprasDto>(
                "dbo.sp_EstadisticasResumenCompras",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return resultado ?? new EstadisticasResumenComprasDto();
        }
    }
}