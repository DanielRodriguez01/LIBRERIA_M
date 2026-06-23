using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using LIBRERIA_M.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7228/")
    });

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ProductoService>();

await builder.Build().RunAsync();