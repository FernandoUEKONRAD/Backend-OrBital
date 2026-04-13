namespace Orbital.API.DTOs;

// Lo que manda el usuario para cambiar estado
public class CambiarEstadoDto
{
    public string NuevoEstado { get; set; } = "";
}

// Lo que devuelve la API
public class EstadoPlanetaResponseDto
{
    public int PlanetaId { get; set; }
    public string NombrePlaneta { get; set; } = "";
    public string EstadoActual { get; set; } = "";
    public List<string> SiguientesEstadosPermitidos { get; set; } = new();
}