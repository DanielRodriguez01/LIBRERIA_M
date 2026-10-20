using LIBRERIA_M.Client.Pages;
using LIBRERIA_M.Client.Services;
using LIBRERIA_M.Components;
using LIBRERIA_M.Data;
using LIBRERIA_M.Repositories.Cliente;
using LIBRERIA_M.Repositories.Compra;
using LIBRERIA_M.Repositories.Panel_de_Control;
using LIBRERIA_M.Repositories.Pedido;
using LIBRERIA_M.Repositories.Producto;
using LIBRERIA_M.Repositories.Proveedor;
using LIBRERIA_M.Repositories.Ventas;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<SqlConnectionFactory>();

// Registro de Repositorios (Servidor / API)
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<ICompraRepository, CompraRepository>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPanelControlRepository, PanelControlRepository>();

// Modificado para que tome automáticamente localhost en tu PC y itempurl en la nube
builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<Microsoft.AspNetCore.Components.NavigationManager>();
    return new HttpClient
    {
        BaseAddress = new Uri(navigationManager.BaseUri)
    };
});

// Servicios para Blazor Client y prerenderizado
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<CompraService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<VentaService>();
builder.Services.AddScoped<PanelControlService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(LIBRERIA_M.Client._Imports).Assembly);

// Agregado para resolver el enrutamiento en servidores remotos como IIS
app.MapFallbackToFile("index.html");

app.Run();