using System.Net.Http.Json;
using LIBRERIA_M.Client.Models.Compra;
using LIBRERIA_M.Client.Models.Proveedor;

namespace LIBRERIA_M.Client.Services
{
    public class CompraService
    {
        private readonly HttpClient _http;

        public CompraService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CompraListadoDto>> ListarCompras(string? busqueda = null, int? idProveedor = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null, string? estado = null)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(busqueda))
                queryParams.Add($"busqueda={Uri.EscapeDataString(busqueda)}");

            if (idProveedor.HasValue)
                queryParams.Add($"idProveedor={idProveedor.Value}");

            if (fechaDesde.HasValue)
                queryParams.Add($"fechaDesde={fechaDesde.Value:yyyy-MM-dd}");

            if (fechaHasta.HasValue)
                queryParams.Add($"fechaHasta={fechaHasta.Value:yyyy-MM-dd}");

            if (!string.IsNullOrWhiteSpace(estado))
                queryParams.Add($"estado={Uri.EscapeDataString(estado)}");

            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : string.Empty;

            var resultado = await _http.GetFromJsonAsync<List<CompraListadoDto>>($"api/compras{queryString}");
            return resultado ?? new List<CompraListadoDto>();
        }

        public async Task<CompraDetalleCompletoDto?> ObtenerCompraPorId(int id)
        {
            return await _http.GetFromJsonAsync<CompraDetalleCompletoDto>($"api/compras/{id}");
        }

        public async Task<bool> CrearCompra(CrearCompraDto compraDto)
        {
            var respuesta = await _http.PostAsJsonAsync("api/compras", compraDto);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> RegistrarPago(RegistrarPagoProveedorDto pagoDto)
        {
            var respuesta = await _http.PostAsJsonAsync("api/compras/pagos", pagoDto);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<List<ProveedorSelectorDto>> ObtenerProveedoresSelector()
        {
            var resultado = await _http.GetFromJsonAsync<List<ProveedorSelectorDto>>("api/compras/proveedores");
            return resultado ?? new List<ProveedorSelectorDto>();
        }
    }
}