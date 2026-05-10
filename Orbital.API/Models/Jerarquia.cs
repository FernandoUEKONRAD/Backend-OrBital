using System.ComponentModel.DataAnnotations;

namespace Orbital.API.Models
{
    public class Jerarquia
    {
        [Key]
        public int Id_Jerarquia { get; set; }

        public string Nombre_Jerarquia { get; set; } = string.Empty;

        public int Nivel_Poder_Minimo { get; set; }

        public int Nivel_Poder_Maximo { get; set; }

        public string Descripcion { get; set; } = string.Empty;
    }
}