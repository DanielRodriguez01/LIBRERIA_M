using LIBRERIA_M.Client.Pages;
using LIBRERIA_M.Client.Services;
using LIBRERIA_M.Components;
using LIBRERIA_M.Data;
using LIBRERIA_M.Repositories.Cliente;
using LIBRERIA_M.Repositories.Compra;
using LIBRERIA_M.Repositories.Estadisticas;
using LIBRERIA_M.Repositories.Panel_de_Control;
using LIBRERIA_M.Repositories.Pedido;
using LIBRERIA_M.Repositories.Producto;
using LIBRERIA_M.Repositories.Proveedor;
using LIBRERIA_M.Repositories.Ventas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

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
builder.Services.AddScoped<IEstadisticasRepository, EstadisticasRepository>();

// ============================================================
// LOGIN - Paso 2: autenticación (JWT) y autorización
// ============================================================

// Los valores se leen de appsettings (sección "Jwt"). Cada entorno tiene su propia clave.
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// Si falta la clave o sigue siendo la de ejemplo, la aplicación no arranca y avisa por qué.
// HMAC-SHA256 necesita una clave de al menos 32 caracteres (256 bits).
if (string.IsNullOrWhiteSpace(jwtKey) || jwtKey.Length < 32 || jwtKey.StartsWith("REEMPLAZAR"))
{
    throw new InvalidOperationException(
        "Falta configurar Jwt:Key en appsettings. Debe ser una clave aleatoria de al menos 32 caracteres.");
}

if (string.IsNullOrWhiteSpace(jwtIssuer) || string.IsNullOrWhiteSpace(jwtAudience))
{
    throw new InvalidOperationException(
        "Falta configurar Jwt:Issuer y Jwt:Audience en appsettings.");
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Qué revisa el servidor en CADA petición que trae un token:
            ValidateIssuer = true,                 // quién lo emitió
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,               // para quién fue emitido
            ValidAudience = jwtAudience,
            ValidateLifetime = true,               // que no esté vencido
            ValidateIssuerSigningKey = true,       // que la firma sea auténtica (no alterado)
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

            // Margen de tolerancia por diferencias de reloj (por defecto serían 5 minutos).
            ClockSkew = TimeSpan.FromSeconds(30),

            // Qué claims se usan como "rol" y como "nombre" del usuario autenticado.
            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.Name
        };
    });

builder.Services.AddAuthorization();

// Se registran en el Paso 3, cuando existan estas clases (si se descomentan antes, no compila):
// builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
// builder.Services.AddScoped<JwtService>();
// builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

// ============================================================

// Modificado para que tome automáticamente localhost en tu PC y ltempurl en la nube
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
builder.Services.AddScoped<EstadisticasService>();

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

// El orden importa: primero se identifica a la persona (Authentication)
// y recién después se decide qué puede hacer (Authorization).
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(LIBRERIA_M.Client._Imports).Assembly);

// Agregado para resolver el enrutamiento en servidores remotos como IIS
app.MapFallbackToFile("index.html");

app.Run();
