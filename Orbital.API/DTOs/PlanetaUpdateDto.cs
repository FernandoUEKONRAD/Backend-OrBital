namespace Orbital.API.DTOs
{
    public class PlanetaUpdateDto
    {
        public string? Nombre { get; set; }
        public string? Sistema_Estelar { get; set; }
        public string? Galaxia { get; set; }

        public NivelTecnologico? Nivel_Tecnologico { get; set; }

        public string? Atmosfera { get; set; }
        public long? Poblacion { get; set; }

        public string? Nivel_Vida_Nativa { get; set; }

        public int? Id_Estado { get; set; }
        public int? Id_Propietario { get; set; }

        public DateTime? Fecha_Descubrimiento { get; set; }

        public string? Coordenadas { get; set; }
        public string? Descripcion { get; set; }

        public bool? Activo { get; set; }
    }
}
