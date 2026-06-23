using System.Net.Http.Json;
using LIBRERIA_M.Client.Models;

namespace LIBRERIA_M.Client.Services;

public class ClienteService
{
    private readonly HttpClient _http;

    public ClienteService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ClienteDto>> ObtenerClientes()
    {
        var clientes =
            await _http.GetFromJsonAsync<List<ClienteDto>>
            ("api/clientes");

        return clientes ?? new List<ClienteDto>();
    }

    public async Task CrearCliente(ClienteDto cliente)
    {
        await _http.PostAsJsonAsync(
            "api/clientes",
            cliente);
    }

    public async Task ActualizarCliente(ClienteDto cliente)
    {
        await _http.PutAsJsonAsync(
            "api/clientes",
            cliente);
    }

    public async Task DesactivarCliente(int idCliente)
    {
        await _http.DeleteAsync($"api/clientes/{idCliente}");
    }
}