using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Orbital.API.Data;
using Orbital.API.Repositories;
using Orbital.API.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Conexion con DB (MySQL - Pomelo)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// set base cofiguracion JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

// Conexion con frontend (CORS)
var origenesPermitidos = builder.Configuration
    .GetValue<string>("OrigenesPermitidos")?
    .Split(",") ?? new string[] { "*" };

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],

        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

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
builder.Services.AddScoped<IValoracionService, ValoracionService>();


// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingresla el Token JWT Aqui Autenticar las peticiones",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("EmperadorOnly", policy =>
    policy.RequireClaim("Id_Rol", RolesIds.Emperador))
    .AddPolicy("ComandanteOnly", policy =>
        policy.RequireClaim("Id_Rol", RolesIds.Comandante))
    .AddPolicy("AnalistaOnly", policy =>
        policy.RequireClaim("Id_Rol", RolesIds.Analista))
    .AddPolicy("DesarrolladorOnly", policy =>
        policy.RequireClaim("Id_Rol", RolesIds.Desarrollador))
    .AddPolicy("EspecialistaOnly", policy =>
        policy.RequireClaim("Id_Rol", RolesIds.Especialista))
    .AddPolicy("GestorOnly", policy =>
        policy.RequireClaim("Id_Rol", RolesIds.Gestor))
    .AddPolicy("GuerreroConquistaOnly", policy =>
        policy.RequireClaim("Id_Rol", RolesIds.GuerreroConquista))
    .AddPolicy("SistemaScouterOnly", policy =>
        policy.RequireClaim("Id_Rol", RolesIds.SistemaScouter));

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


// "mapeo" de los roles para las politicas de los tokens
 
public static class RolesIds
{
    public const string Emperador = "1";
    public const string Comandante = "2";
    public const string Analista = "3";
    public const string Desarrollador = "4";
    public const string Especialista = "5";
    public const string Gestor = "6";
    public const string GuerreroConquista = "7";
    public const string SistemaScouter = "8";
}