namespace Orbital.API.DTOs
{
    public class JerarquiaDto
    {
        public int Id_Jerarquia { get; set; }

        public string Nombre_Jerarquia { get; set; }

        public int Nivel_Poder_Minimo { get; set; }

        public int? Nivel_Poder_Maximo { get; set; }

        public string Descripcion { get; set; }
    }
}