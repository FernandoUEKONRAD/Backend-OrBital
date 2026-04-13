namespace Orbital.API.Services;

public class PlanetaEstadoService
{
    // Flujo válido entre estados — exactamente los de tu BD
    private static readonly Dictionary<string, List<string>> _cambiosPermitidos = new()
    {
        { "Disponible",     new() { "En Exploracion", "Restringido" } },
        { "En Exploracion", new() { "En Mision", "Disponible", "Restringido" } },
        { "En Mision",      new() { "Conquistado", "Disponible" } },
        { "Conquistado",    new() { "En Venta", "Restringido" } },
        { "En Venta",       new() { "Vendido", "Conquistado" } },
        { "Vendido",        new() { } },
        { "Restringido",    new() { "Disponible" } }
    };

    public bool CambioEsValido(string estadoActual, string estadoNuevo)
    {
        if (!_cambiosPermitidos.ContainsKey(estadoActual))
            return false;

        return _cambiosPermitidos[estadoActual].Contains(estadoNuevo);
    }

    public List<string> ObtenerSiguientesEstados(string estadoActual)
    {
        if (!_cambiosPermitidos.ContainsKey(estadoActual))
            return new List<string>();

        return _cambiosPermitidos[estadoActual];
    }

    public List<string> ObtenerTodosLosEstados()
    {
        return _cambiosPermitidos.Keys.ToList();
    }
}