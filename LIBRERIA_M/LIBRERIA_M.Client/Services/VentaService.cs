using LIBRERIA_M.Client.Models.Ventas;
using System.Net.Http.Json;
using System.Web;

namespace LIBRERIA_M.Client.Services
{
    public class VentaService
    {
        private readonly HttpClient _http;

        public VentaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<VentaListadoDto>> ListarVentas(string? buscar = null, DateTime? fechaInicio = null, DateTime? fechaFin = null, string? estado = null)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);

            if (!string.IsNullOrWhiteSpace(buscar))
                query["buscar"] = buscar;

            if (fechaInicio.HasValue)
                query["fechaInicio"] = fechaInicio.Value.ToString("yyyy-MM-dd");

            if (fechaFin.HasValue)
                query["fechaFin"] = fechaFin.Value.ToString("yyyy-MM-dd");

            if (!string.IsNullOrWhiteSpace(estado))
                query["estado"] = estado;

            string url = "api/Ventas";
            if (query.Count > 0)
                url += "?" + query.ToString();

            return await _http.GetFromJsonAsync<List<VentaListadoDto>>(url) ?? new List<VentaListadoDto>();
        }

        public async Task<VentaDetalleCompletoDto?> ObtenerVentaPorId(int idVenta)
        {
            return await _http.GetFromJsonAsync<VentaDetalleCompletoDto>($"api/Ventas/{idVenta}");
        }

        public async Task<List<PagoVentaListadoDto>> ObtenerPagosVenta(int idVenta)
        {
            return await _http.GetFromJsonAsync<List<PagoVentaListadoDto>>($"api/Ventas/{idVenta}/pagos") ?? new List<PagoVentaListadoDto>();
        }

        public async Task<NumeroFacturaDto?> GenerarNumeroFactura()
        {
            return await _http.GetFromJsonAsync<NumeroFacturaDto>("api/Ventas/siguiente-numero");
        }

        public async Task<VentaDetalleCompletoDto?> CrearVenta(VentaCreacionDto ventaDto)
        {
            var response = await _http.PostAsJsonAsync("api/Ventas", ventaDto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VentaDetalleCompletoDto>();
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }

        public async Task<RespuestaPagoVentaDto?> RegistrarPago(PagoVentaCreacionDto pagoDto)
        {
            var response = await _http.PostAsJsonAsync("api/Ventas/pago", pagoDto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RespuestaPagoVentaDto>();
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }
}