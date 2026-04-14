using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.Repositories;
using Orbital.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Conexion con DB (MySQL - Pomelo)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// Conexion con frontend (CORS)
var origenesPermitidos = builder.Configuration
    .GetValue<string>("OrigenesPermitidos")?
    .Split(",") ?? new string[] { "*" };

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(origenesPermitidos)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Inyección de dependencias - Repositorios
builder.Services.AddScoped<IPlanetasRepository, PlanetasRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPlanetaEstadoRepository, PlanetaEstadoRepository>();

// Inyección de dependencias - Servicios
builder.Services.AddScoped<IPlanetasService, PlanetasService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<PlanetaEstadoService>();


// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cors
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();