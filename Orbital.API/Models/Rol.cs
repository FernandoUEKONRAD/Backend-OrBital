namespace Orbital.API.Models
{
    public class Rol
    {
        public int Id_Rol { get; set; }

        public string Nombre_Rol { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public bool Activo { get; set; }
    }
}