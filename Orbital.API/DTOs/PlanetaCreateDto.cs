using System.Text.Json.Serialization;

public class PlanetaCreateDto
{
    public string Nombre { get; set; }

    [JsonPropertyName("sistema_estelar")]
    public string? Sistema_Estelar { get; set; }

    public string? Galaxia { get; set; }

    [JsonPropertyName("nivel_tecnologico")]
    public int Nivel_Tecnologico { get; set; }

    public string? Atmosfera { get; set; }

    public long? Poblacion { get; set; }

    [JsonPropertyName("nivel_vida_nativa")]
    public string Nivel_Vida_Nativa { get; set; }

    [JsonPropertyName("id_estado")]
    public int Id_Estado { get; set; }

    [JsonPropertyName("id_propietario")]
    public int? Id_Propietario { get; set; }

    [JsonPropertyName("fecha_descubrimiento")]
    public DateTime Fecha_Descubrimiento { get; set; }

    public string? Coordenadas { get; set; }
    public string? Descripcion { get; set; }
    public bool Activo { get; set; }
}