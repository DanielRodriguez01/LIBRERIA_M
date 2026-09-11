using LIBRERIA_M.Client.Models.Proveedor;
using System.Net.Http.Json;

namespace LIBRERIA_M.Client.Services
{
    public class ProveedorService
    {
        private readonly HttpClient _http;

        public ProveedorService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProveedorDto>> ListarProveedores(bool soloActivos = true)
        {
            var proveedores = await _http.GetFromJsonAsync<List<ProveedorDto>>($"api/proveedores?soloActivos={soloActivos}");
            return proveedores ?? new List<ProveedorDto>();
        }

        public async Task<List<ProveedorDto>> BuscarProveedores(string? termino, bool soloActivos = true)
        {
            var proveedores = await _http.GetFromJsonAsync<List<ProveedorDto>>($"api/proveedores/buscar?termino={Uri.EscapeDataString(termino ?? "")}&soloActivos={soloActivos}");
            return proveedores ?? new List<ProveedorDto>();
        }

        public async Task<ProveedorDto?> ObtenerProveedorPorId(int id)
        {
            return await _http.GetFromJsonAsync<ProveedorDto>($"api/proveedores/{id}");
        }

        public async Task<int> CrearProveedor(CrearProveedorDto proveedor)
        {
            var response = await _http.PostAsJsonAsync("api/proveedores", proveedor);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task ActualizarProveedor(ActualizarProveedorDto proveedor)
        {
            var response = await _http.PutAsJsonAsync($"api/proveedores/{proveedor.IdProveedor}", proveedor);
            response.EnsureSuccessStatusCode();
        }

        public async Task DesactivarProveedor(int idProveedor)
        {
            var response = await _http.PatchAsync($"api/proveedores/{idProveedor}/desactivar", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task ActivarProveedor(int idProveedor)
        {
            var response = await _http.PatchAsync($"api/proveedores/{idProveedor}/activar", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<ProveedorResumenDto?> ObtenerResumenProveedor(int idProveedor)
        {
            return await _http.GetFromJsonAsync<ProveedorResumenDto>($"api/proveedores/{idProveedor}/resumen");
        }

        public async Task<List<ProveedorHistorialCompraDto>> ObtenerHistorialComprasProveedor(int idProveedor)
        {
            var compras = await _http.GetFromJsonAsync<List<ProveedorHistorialCompraDto>>($"api/proveedores/{idProveedor}/compras");
            return compras ?? new List<ProveedorHistorialCompraDto>();
        }

        public async Task<List<ProveedorHistorialPagoDto>> ObtenerHistorialPagosProveedor(int idProveedor)
        {
            var pagos = await _http.GetFromJsonAsync<List<ProveedorHistorialPagoDto>>($"api/proveedores/{idProveedor}/pagos");
            return pagos ?? new List<ProveedorHistorialPagoDto>();
        }

        public async Task<bool> RegistrarPagoProveedor(RegistrarPagoProveedorDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/proveedores/pagos", dto);
            return response.IsSuccessStatusCode;
        }
    }
}