using System.Net.Http.Json;
using LIBRERIA_M.Client.Models.Estadisticas;

namespace LIBRERIA_M.Client.Services
{
    public class EstadisticasService
    {
        private readonly HttpClient _http;

        public EstadisticasService(HttpClient http)
        {
            _http = http;
        }

        private string ConstruirQueryString(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var queryParams = new List<string>();

            if (fechaDesde.HasValue)
                queryParams.Add($"fechaDesde={fechaDesde.Value:yyyy-MM-dd}");

            if (fechaHasta.HasValue)
                queryParams.Add($"fechaHasta={fechaHasta.Value:yyyy-MM-dd}");

            return queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : string.Empty;
        }

        public async Task<EstadisticasResumenDto> ObtenerResumen(DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            var queryString = ConstruirQueryString(fechaDesde, fechaHasta);
            var resultado = await _http.GetFromJsonAsync<EstadisticasResumenDto>($"api/estadisticas/resumen{queryString}");
            return resultado ?? new EstadisticasResumenDto();
        }

        public async Task<List<EstadisticasVentasPorDiaDto>> ObtenerVentasPorDia(DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            var queryString = ConstruirQueryString(fechaDesde, fechaHasta);
            var resultado = await _http.GetFromJsonAsync<List<EstadisticasVentasPorDiaDto>>($"api/estadisticas/ventas-por-dia{queryString}");
            return resultado ?? new List<EstadisticasVentasPorDiaDto>();
        }

        public async Task<List<EstadisticasProductoMasVendidoDto>> ObtenerProductosMasVendidos(DateTime? fechaDesde = null, DateTime? fechaHasta = null, int top = 10)
        {
            var queryParams = new List<string>();

            if (fechaDesde.HasValue)
                queryParams.Add($"fechaDesde={fechaDesde.Value:yyyy-MM-dd}");

            if (fechaHasta.HasValue)
                queryParams.Add($"fechaHasta={fechaHasta.Value:yyyy-MM-dd}");

            queryParams.Add($"top={top}");

            var queryString = "?" + string.Join("&", queryParams);
            var resultado = await _http.GetFromJsonAsync<List<EstadisticasProductoMasVendidoDto>>($"api/estadisticas/productos-mas-vendidos{queryString}");
            return resultado ?? new List<EstadisticasProductoMasVendidoDto>();
        }

        public async Task<EstadisticasEstadoFinancieroDto> ObtenerEstadoFinanciero(DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            var queryString = ConstruirQueryString(fechaDesde, fechaHasta);
            var resultado = await _http.GetFromJsonAsync<EstadisticasEstadoFinancieroDto>($"api/estadisticas/estado-financiero{queryString}");
            return resultado ?? new EstadisticasEstadoFinancieroDto();
        }

        public async Task<EstadisticasResumenComprasDto> ObtenerResumenCompras(DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            var queryString = ConstruirQueryString(fechaDesde, fechaHasta);
            var resultado = await _http.GetFromJsonAsync<EstadisticasResumenComprasDto>($"api/estadisticas/resumen-compras{queryString}");
            return resultado ?? new EstadisticasResumenComprasDto();
        }
    }
}