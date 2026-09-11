using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LIBRERIA_M.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<CompraService>();
builder.Services.AddScoped<ProveedorService>(); // <-- REGISTRO DE PROVEEDOR SERVICE

await builder.Build().RunAsync();