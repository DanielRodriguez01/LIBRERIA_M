using LIBRERIA_M.Models.Estadisticas;

namespace LIBRERIA_M.Repositories.Estadisticas
{
    public interface IEstadisticasRepository
    {
        Task<EstadisticasResumenDto> ObtenerResumenEstadisticas(DateTime? fechaDesde, DateTime? fechaHasta);

        Task<IEnumerable<EstadisticasVentasPorDiaDto>> ObtenerVentasPorDia(DateTime? fechaDesde, DateTime? fechaHasta);

        Task<IEnumerable<EstadisticasProductoMasVendidoDto>> ObtenerProductosMasVendidos(DateTime? fechaDesde, DateTime? fechaHasta, int top = 10);

        Task<EstadisticasEstadoFinancieroDto> ObtenerEstadoFinanciero(DateTime? fechaDesde, DateTime? fechaHasta);

        Task<EstadisticasResumenComprasDto> ObtenerResumenCompras(DateTime? fechaDesde, DateTime? fechaHasta);
    }
}