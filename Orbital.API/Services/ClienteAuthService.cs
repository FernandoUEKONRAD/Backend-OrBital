using Orbital.API.Data;
using Orbital.API.DTOs;
using Orbital.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Orbital.API.Services
{
    public class ClienteAuthService : IClienteAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ClienteAuthService> _logger;

        public ClienteAuthService(
            AppDbContext context,
            IConfiguration configuration,
            ILogger<ClienteAuthService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ClienteLoginResponseDto?> Login(ClienteLoginDto dto)
        {
            var correo = dto.Correo.Trim().ToLower();
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Correo == correo && c.Activo);

            if (cliente == null) return null;

            if (cliente.Contrasena_Hash != HashPassword(dto.Password)) return null;

            return new ClienteLoginResponseDto
            {
                Token = GenerarToken(cliente),
                Id_Cliente = cliente.Id_Cliente,
                Nombre = cliente.Nombre
            };
        }

        public async Task<ClienteResponseDto> Registrar(ClienteRegistroDto dto)
        {
            var correo = dto.Correo.Trim().ToLower();

            var existe = await _context.Clientes.AnyAsync(c => c.Correo == correo);
            if (existe)
                throw new InvalidOperationException("Ya existe un cliente registrado con ese correo");

            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Tipo_Cliente = dto.Tipo_Cliente,
                Id_Galaxia_Origen = dto.Id_Galaxia_Origen,
                Correo = correo,
                Contrasena_Hash = HashPassword(dto.Password),
                Credito_Disponible = 0,
                Nivel_Confianza = "Nuevo",
                Fecha_Registro = DateTime.Now,
                Activo = true
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            // Cargar galaxia si aplica
            if (cliente.Id_Galaxia_Origen.HasValue)
                await _context.Entry(cliente).Reference(c => c.GalaxiaOrigen).LoadAsync();

            _logger.LogInformation("Nuevo cliente registrado: {Correo}", correo);

            return new ClienteResponseDto
            {
                Id_Cliente = cliente.Id_Cliente,
                Nombre = cliente.Nombre,
                Tipo_Cliente = cliente.Tipo_Cliente,
                Galaxia_Origen = cliente.GalaxiaOrigen?.Nombre,
                Correo = cliente.Correo,
                Credito_Disponible = cliente.Credito_Disponible,
                Nivel_Confianza = cliente.Nivel_Confianza,
                Fecha_Registro = cliente.Fecha_Registro,
                Activo = cliente.Activo
            };
        }

        private string GenerarToken(Cliente cliente)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Claims separados de los usuarios internos: usa "Id_Cliente" y "tipo"="cliente"
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, cliente.Id_Cliente.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, cliente.Correo),
                new Claim("Id_Cliente", cliente.Id_Cliente.ToString()),
                new Claim("tipo", "cliente")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
