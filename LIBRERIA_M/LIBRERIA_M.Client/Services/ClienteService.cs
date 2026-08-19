using System.Net.Http.Json;
using System.Text.Json;
using LIBRERIA_M.Client.Models;

namespace LIBRERIA_M.Client.Services
{
    public class ClienteService
    {
        private readonly HttpClient _http;

        public ClienteService(HttpClient http)
        {
            _http = http;
        }

        // ==========================================
        // 1. LISTADOS Y FILTROS (Figma: Listado)
        // ==========================================

        public async Task<List<ClienteDto>> ObtenerClientesAsync()
        {
            var clientes = await _http.GetFromJsonAsync<List<ClienteDto>>("api/clientes");
            return clientes ?? new List<ClienteDto>();
        }

        public async Task<List<ClienteDto>> ObtenerClientesAlDiaAsync()
        {
            var clientes = await _http.GetFromJsonAsync<List<ClienteDto>>("api/clientes/al-dia");
            return clientes ?? new List<ClienteDto>();
        }

        public async Task<List<ClienteDto>> ObtenerClientesConDeudaAsync()
        {
            var clientes = await _http.GetFromJsonAsync<List<ClienteDto>>("api/clientes/con-deuda");
            return clientes ?? new List<ClienteDto>();
        }

        public async Task<List<ClienteDto>> BuscarClientesAsync(string? texto)
        {
            var clientes = await _http.GetFromJsonAsync<List<ClienteDto>>($"api/clientes/buscar?texto={Uri.EscapeDataString(texto ?? string.Empty)}");
            return clientes ?? new List<ClienteDto>();
        }

        public async Task<ClienteDto?> ObtenerPorIdAsync(int idCliente)
        {
            return await _http.GetFromJsonAsync<ClienteDto>($"api/clientes/{idCliente}");
        }

        // ==========================================
        // 2. OPERACIONES DE ESCRITURA Y ESTADO
        // ==========================================

        public async Task CrearClienteAsync(ClienteDto cliente)
        {
            var response = await _http.PostAsJsonAsync("api/clientes", cliente);
            await ProcesarRespuestaError(response);
        }

        public async Task ActualizarClienteAsync(ClienteDto cliente)
        {
            var response = await _http.PutAsJsonAsync($"api/clientes/{cliente.IdCliente}", cliente);
            await ProcesarRespuestaError(response);
        }

        public async Task DesactivarClienteAsync(int idCliente)
        {
            var response = await _http.DeleteAsync($"api/clientes/{idCliente}");
            await ProcesarRespuestaError(response);
        }

        public async Task ReactivarClienteAsync(int idCliente)
        {
            var response = await _http.PatchAsync($"api/clientes/{idCliente}/reactivar", null);
            await ProcesarRespuestaError(response);
        }

        // ==========================================
        // 3. RESUMEN E HISTORIALES (Figma: Detalles)
        // ==========================================

        public async Task<ClienteResumenDto?> ObtenerResumenClienteAsync(int idCliente)
        {
            return await _http.GetFromJsonAsync<ClienteResumenDto>($"api/clientes/{idCliente}/resumen");
        }

        public async Task<List<ClienteHistorialCompraDto>> ObtenerHistorialComprasAsync(int idCliente)
        {
            var compras = await _http.GetFromJsonAsync<List<ClienteHistorialCompraDto>>($"api/clientes/{idCliente}/compras");
            return compras ?? new List<ClienteHistorialCompraDto>();
        }

        public async Task<List<ClienteHistorialPagoDto>> ObtenerHistorialPagosAsync(int idCliente)
        {
            var pagos = await _http.GetFromJsonAsync<List<ClienteHistorialPagoDto>>($"api/clientes/{idCliente}/pagos");
            return pagos ?? new List<ClienteHistorialPagoDto>();
        }

        // ==========================================
        // MANEJO DE ERRORES DE LA API (THROW SQL)
        // ==========================================

        private async Task ProcesarRespuestaError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                try
                {
                    using var doc = JsonDocument.Parse(content);
                    if (doc.RootElement.TryGetProperty("mensaje", out var mensajeProp))
                    {
                        throw new Exception(mensajeProp.GetString());
                    }
                }
                catch (JsonException)
                {
                    // Si no es un JSON formateado, continuamos
                }

                throw new Exception(!string.IsNullOrWhiteSpace(content) ? content : "Ocurrió un error inesperado al procesar la solicitud.");
            }
        }
    }
}