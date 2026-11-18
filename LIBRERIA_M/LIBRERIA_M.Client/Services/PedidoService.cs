using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
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

        public async Task<List<HistorialPedidoDTO>> ListarHistorialPedido(int idPedido)
        {
            var resultado = await _http.GetFromJsonAsync<List<HistorialPedidoDTO>>($"api/pedidos/{idPedido}/historial");
            return resultado ?? new List<HistorialPedidoDTO>();
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

        // Agrega un producto a un pedido Pendiente o Terminado.
        // Si falla, lanza una excepcion con el mensaje real del servidor
        // (por ejemplo "No existe stock suficiente del producto para agregarlo al pedido.").
        public async Task AgregarProductoAPedido(AgregarDetallePedidoDTO dto)
        {
            var respuesta = await _http.PostAsJsonAsync("api/pedidos/detalle", dto);

            if (!respuesta.IsSuccessStatusCode)
            {
                throw new Exception(await LeerMensajeError(respuesta));
            }
        }

        public async Task<bool> ActualizarPedido(ActualizarPedidoDTO dto)
        {
            var respuesta = await _http.PutAsJsonAsync("api/pedidos", dto);
            return respuesta.IsSuccessStatusCode;
        }

        // Cambia la modalidad de entrega (Retira en local / Enviar pedido) de un pedido
        // Pendiente o Terminado. Si falla, lanza una excepcion con el mensaje real del servidor
        // (por ejemplo "Para enviar el pedido debe escribir la dirección de entrega.").
        public async Task ActualizarEntregaPedido(int id, ActualizarEntregaPedidoDTO dto)
        {
            var respuesta = await _http.PutAsJsonAsync($"api/pedidos/{id}/entrega", dto);

            if (!respuesta.IsSuccessStatusCode)
            {
                throw new Exception(await LeerMensajeError(respuesta));
            }
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

        // Entrega el pedido, genera la venta y registra el pago.
        // Si falla, lanza una excepcion con el mensaje real del servidor
        // (por ejemplo "El monto a pagar supera el total de la venta").
        public async Task<ResultadoEntregaPedidoDTO?> EntregarPedido(int id, EntregarPedidoDTO dto)
        {
            var respuesta = await _http.PutAsJsonAsync($"api/pedidos/{id}/entregar", dto);

            if (!respuesta.IsSuccessStatusCode)
            {
                throw new Exception(await LeerMensajeError(respuesta));
            }

            return await respuesta.Content.ReadFromJsonAsync<ResultadoEntregaPedidoDTO>();
        }

        public async Task<bool> CancelarPedido(int id)
        {
            var respuesta = await _http.PutAsync($"api/pedidos/{id}/cancelar", null);
            return respuesta.IsSuccessStatusCode;
        }

        // ==========================================
        // MANEJO DE ERRORES DE LA API
        // ==========================================

        private static async Task<string> LeerMensajeError(HttpResponseMessage respuesta)
        {
            var contenido = await respuesta.Content.ReadAsStringAsync();

            try
            {
                using var doc = JsonDocument.Parse(contenido);
                var raiz = doc.RootElement;

                // Texto plano enviado como JSON: "mensaje"
                if (raiz.ValueKind == JsonValueKind.String)
                {
                    var textoPlano = raiz.GetString();
                    if (!string.IsNullOrWhiteSpace(textoPlano))
                        return textoPlano;
                }
                // Objeto con la propiedad del mensaje
                else if (raiz.ValueKind == JsonValueKind.Object)
                {
                    foreach (var nombre in new[] { "mensaje", "message", "detail" })
                    {
                        if (raiz.TryGetProperty(nombre, out var valor) && valor.ValueKind == JsonValueKind.String)
                        {
                            var texto = valor.GetString();
                            if (!string.IsNullOrWhiteSpace(texto))
                                return texto;
                        }
                    }
                }
            }
            catch (JsonException)
            {
                // Si no es un JSON, se usa el texto tal cual
            }

            return !string.IsNullOrWhiteSpace(contenido)
                ? contenido
                : "Ocurrió un error inesperado al procesar la solicitud.";
        }
    }
}
