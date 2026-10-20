using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LIBRERIA_M.Data;
using LIBRERIA_M.Models.Panel_de_Control;

namespace LIBRERIA_M.Repositories.Panel_de_Control
{
    public class PanelControlRepository : IPanelControlRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public PanelControlRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<PanelControlResumenDto> ObtenerResumen(DateTime? fechaReferencia)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@FechaReferencia", fechaReferencia);

            var resultado = await connection.QueryFirstOrDefaultAsync<PanelControlResumenDto>(
                "dbo.sp_PanelControlResumen",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return resultado ?? new PanelControlResumenDto();
        }

        public async Task<IEnumerable<VentaPorDiaDto>> ObtenerVentasPorDia(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@FechaDesde", fechaDesde);
            parameters.Add("@FechaHasta", fechaHasta);

            return await connection.QueryAsync<VentaPorDiaDto>(
                "dbo.sp_EstadisticasVentasPorDia",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<ProductoMasVendidoDto>> ObtenerProductosMasVendidos(int top)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Top", top);

            return await connection.QueryAsync<ProductoMasVendidoDto>(
                "dbo.sp_PanelControlProductosMasVendidos",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<PedidoPendientePanelDto>> ObtenerPedidosPendientes(int cantidad)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Cantidad", cantidad);

            return await connection.QueryAsync<PedidoPendientePanelDto>(
                "dbo.sp_PanelControlPedidosPendientes",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}