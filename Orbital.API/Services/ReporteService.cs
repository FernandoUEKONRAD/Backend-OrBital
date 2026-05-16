using Orbital.API.Data;
using Orbital.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Orbital.API.Services
{
    public class ReporteService : IReporteService
    {
        private readonly AppDbContext _context;

        public ReporteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ReporteVentasDto> ReporteVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            // Traer todas las transacciones completadas en el rango
            var transacciones = await _context.Transacciones
                .Where(t =>
                    t.Estado_Transaccion == "Completada" &&
                    t.Fecha_Transaccion >= fechaInicio &&
                    t.Fecha_Transaccion <= fechaFin)
                .Join(
                    _context.MercadoPlanetas,
                    t => t.Id_Publicacion,
                    m => m.Id_Publicacion,
                    (t, m) => new { Transaccion = t, Publicacion = m }
                )
                .Join(
                    _context.PlanetaValoraciones,
                    x => x.Publicacion.Id_Valoracion,
                    v => v.Id_Valoracion,
                    (x, v) => new
                    {
                        x.Transaccion.Precio_Final,
                        x.Publicacion.Id_Planeta,
                        NombrePlaneta = (string?)null,
                        v.Clase_Planeta
                    }
                )
                .ToListAsync();

            if (!transacciones.Any())
            {
                return new ReporteVentasDto
                {
                    Fecha_Inicio = fechaInicio,
                    Fecha_Fin = fechaFin,
                    Total_Planetas_Vendidos = 0,
                    Total_Ingresos = 0,
                    Ventas_Por_Clase = new()
                };
            }

            // Obtener nombres de planetas en query separada
            var idPlanetas = transacciones.Select(t => t.Id_Planeta).Distinct().ToList();
            var planetas = await _context.Planetas
                .Where(p => idPlanetas.Contains(p.Id_Planeta))
                .ToDictionaryAsync(p => p.Id_Planeta, p => p.Nombre);

            var masCaroRaw = transacciones.MaxBy(t => t.Precio_Final);
            var planetaMasCaro = masCaroRaw != null && planetas.TryGetValue(masCaroRaw.Id_Planeta, out var nombre)
                ? nombre : null;

            var ventasPorClase = transacciones
                .GroupBy(t => t.Clase_Planeta)
                .Select(g => new VentasPorClaseDto
                {
                    Clase = g.Key,
                    Cantidad = g.Count(),
                    Total_Ingresos = g.Sum(t => t.Precio_Final)
                })
                .OrderBy(v => v.Clase)
                .ToList();

            return new ReporteVentasDto
            {
                Fecha_Inicio = fechaInicio,
                Fecha_Fin = fechaFin,
                Total_Planetas_Vendidos = transacciones.Count,
                Total_Ingresos = transacciones.Sum(t => t.Precio_Final),
                Planeta_Mas_Caro = planetaMasCaro,
                Precio_Mas_Alto = masCaroRaw?.Precio_Final,
                Ventas_Por_Clase = ventasPorClase
            };
        }
    }
}
