namespace Orbital.API.Models
{
    public class Galaxia
    {
        public int Id_Galaxia { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;
    }
}
