using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LIBRERIA_M.Client.Models.Pedidos;

namespace LIBRERIA_M.Client.Services
{
    public class PedidoService
    {
        private readonly HttpClient _http;

        public PedidoService(HttpClient http)
        {
            _http = http;
        }

        // ==========================================
        // 1. CONSULTAS Y LISTADOS
        // ==========================================

        public async Task<List<PedidoListadoDto>> ListarPedidos(string? estado = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null, string? buscar = null)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(estado))
                queryParams.Add($"estado={Uri.EscapeDataString(estado)}");

            if (fechaDesde.HasValue)
                queryParams.Add($"fechaDesde={fechaDesde.Value:yyyy-MM-dd}");

            if (fechaHasta.HasValue)
                queryParams.Add($"fechaHasta={fechaHasta.Value:yyyy-MM-dd}");

            if (!string.IsNullOrWhiteSpace(buscar))
                queryParams.Add($"buscar={Uri.EscapeDataString(buscar)}");

            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : string.Empty;

            var resultado = await _http.GetFromJsonAsync<List<PedidoListadoDto>>($"api/pedidos{queryString}");
            return resultado ?? new List<PedidoListadoDto>();
        }

        public async Task<List<PedidoListadoDto>> ListarPedidosPendientesPanel(int cantidad = 10)
        {
            var resultado = await _http.GetFromJsonAsync<List<PedidoListadoDto>>($"api/pedidos/pendientes-panel?cantidad={cantidad}");
            return resultado ?? new List<PedidoListadoDto>();
        }

        public async Task<PedidoDTO?> ObtenerPorId(int id)
        {
            return await _http.GetFromJsonAsync<PedidoDTO>($"api/pedidos/{id}");
        }

        public async Task<List<DetallePedidoDTO>> ListarDetallePedido(int idPedido)
        {
            var resultado = await _http.GetFromJsonAsync<List<DetallePedidoDTO>>($"api/pedidos/{idPedido}/detalle");
            return resultado ?? new List<DetallePedidoDTO>();
        }

        // ==========================================
        // 2. ESCRITURA Y EDICIÓN
        // ==========================================

        public async Task<HttpResponseMessage> InsertarPedido(CrearPedidoDTO dto)
        {
            return await _http.PostAsJsonAsync("api/pedidos", dto);
        }

        public async Task<bool> InsertarDetallePedido(AgregarDetallePedidoDTO dto)
        {
            var respuesta = await _http.PostAsJsonAsync("api/pedidos/detalle", dto);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarPedido(ActualizarPedidoDTO dto)
        {
            var respuesta = await _http.PutAsJsonAsync("api/pedidos", dto);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarDetallePedido(int idDetallePedido)
        {
            var respuesta = await _http.DeleteAsync($"api/pedidos/detalle/{idDetallePedido}");
            return respuesta.IsSuccessStatusCode;
        }

        // ==========================================
        // 3. CAMBIOS DE ESTADO DE FLUJO
        // ==========================================

        public async Task<bool> TerminarPedido(int id)
        {
            var respuesta = await _http.PutAsync($"api/pedidos/{id}/terminar", null);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> EntregarPedido(int id)
        {
            var respuesta = await _http.PutAsync($"api/pedidos/{id}/entregar", null);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> CancelarPedido(int id)
        {
            var respuesta = await _http.PutAsync($"api/pedidos/{id}/cancelar", null);
            return respuesta.IsSuccessStatusCode;
        }
    }
}
