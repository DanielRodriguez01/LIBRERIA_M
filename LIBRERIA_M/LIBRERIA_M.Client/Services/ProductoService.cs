using System.Net.Http.Json;
using LIBRERIA_M.Client.Models;

namespace LIBRERIA_M.Client.Services;

public class ProductoService
{
    private readonly HttpClient _http;

    public ProductoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ProductoDto>> ObtenerProductos()
    {
        var productos =
            await _http.GetFromJsonAsync<List<ProductoDto>>
            ("api/producto");

        return productos ?? new List<ProductoDto>();
    }

    public async Task<ProductoDto?> ObtenerProductoPorId(int id)
    {
        return await _http.GetFromJsonAsync<ProductoDto>(
            $"api/producto/{id}");
    }

    public async Task CrearProducto(ProductoDto producto)
    {
        var response = await _http.PostAsJsonAsync(
            "api/producto",
            producto);

        response.EnsureSuccessStatusCode();
    }

    public async Task ActualizarProducto(ProductoDto producto)
    {
        await _http.PutAsJsonAsync(
            "api/producto",
            producto);
    }

    public async Task DesactivarProducto(int idProducto)
    {
        await _http.DeleteAsync(
            $"api/producto/{idProducto}");
    }

    public async Task ReactivarProducto(int idProducto)
    {
        await _http.PutAsync(
            $"api/producto/reactivar/{idProducto}",
            null);
    }
}