namespace Orbital.API.Models
{
    public class PlanetaEstado
    {
        public int Id_Estado { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public bool Activo { get; set; }

        public ICollection<Planeta>? Planetas { get; set; }
    }
}