using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LIBRERIA_M.Client.Models.Panel_de_Control;

namespace LIBRERIA_M.Client.Services
{
    public class PanelControlService
    {
        private readonly HttpClient _http;

        public PanelControlService(HttpClient http)
        {
            _http = http;
        }

        public async Task<PanelControlResumenDto> ObtenerResumen(DateTime? fechaReferencia = null)
        {
            var queryParams = new List<string>();

            if (fechaReferencia.HasValue)
                queryParams.Add($"fechaReferencia={fechaReferencia.Value:yyyy-MM-dd}");

            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : string.Empty;

            var resultado = await _http.GetFromJsonAsync<PanelControlResumenDto>($"api/panelcontrol/resumen{queryString}");
            return resultado ?? new PanelControlResumenDto();
        }

        public async Task<List<VentaPorDiaDto>> ObtenerVentasPorDia(DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            var queryParams = new List<string>();

            if (fechaDesde.HasValue)
                queryParams.Add($"fechaDesde={fechaDesde.Value:yyyy-MM-dd}");

            if (fechaHasta.HasValue)
                queryParams.Add($"fechaHasta={fechaHasta.Value:yyyy-MM-dd}");

            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : string.Empty;

            var resultado = await _http.GetFromJsonAsync<List<VentaPorDiaDto>>($"api/panelcontrol/ventas-por-dia{queryString}");
            return resultado ?? new List<VentaPorDiaDto>();
        }

        public async Task<List<ProductoMasVendidoDto>> ObtenerProductosMasVendidos(int top = 4)
        {
            var resultado = await _http.GetFromJsonAsync<List<ProductoMasVendidoDto>>($"api/panelcontrol/productos-mas-vendidos?top={top}");
            return resultado ?? new List<ProductoMasVendidoDto>();
        }

        public async Task<List<PedidoPendientePanelDto>> ObtenerPedidosPendientes(int cantidad = 10)
        {
            var resultado = await _http.GetFromJsonAsync<List<PedidoPendientePanelDto>>($"api/panelcontrol/pedidos-pendientes?cantidad={cantidad}");
            return resultado ?? new List<PedidoPendientePanelDto>();
        }
    }
}