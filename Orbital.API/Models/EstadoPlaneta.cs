using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models;

[Table("estado_planeta")]
public class EstadoPlaneta
{
    [Key]
    [Column("id_estado")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("nombre_estado")]
    public string NombreEstado { get; set; } = "";

    [Column("descripcion")]
    public string? Descripcion { get; set; }
}