namespace Orbital.API.Models
{
    public class TipoAtmosfera
    {
        public int Id_Atm { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;
    }
}
