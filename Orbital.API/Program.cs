using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.Repositories;
using Orbital.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Conexion con DB (MySQL - Pomelo)
// Asegúrate de instalar el paquete Pomelo.EntityFrameworkCore.MySql:
// dotnet add Orbital.API package Pomelo.EntityFrameworkCore.MySql
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//  Conexion confrontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Inyección de dependencias - Repositorios
builder.Services.AddScoped<IPlanetasRepository, PlanetasRepository>();

// Inyección de dependencias - Servicios
builder.Services.AddScoped<IPlanetasService, PlanetasService>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseCors("AllowAll");

app.UseAuthorization(); 

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

//Ejecutar la aplicacion en  http://localhost:5186/swagger/index.html para conectar con frontend y probar los endpoints de la API
app.Run();