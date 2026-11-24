using EcoTransform.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registrar los servicios en el contenedor de dependencias.

builder.Services.AddControllers();

// Documentación OpenAPI de la API (más información en https://aka.ms/aspnet/openapi).
builder.Services.AddOpenApi();

// Registrar el contexto de base de datos usando la cadena de conexión "EcoTransformDb" del appsettings.json.
builder.Services.AddDbContext<EcoTransformDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EcoTransformDb")));

var app = builder.Build();

// Configurar el pipeline de peticiones HTTP.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();