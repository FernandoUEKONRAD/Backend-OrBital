using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Conexion con DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//  Conexion confrontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

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