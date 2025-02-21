using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProyectoRentACar.Models
{
    [Table("MARCHAS")]
    public class Marchas
    {
        [Key]
        [Column("ID")]
        public int IdMarchas { get; set; }

        [Column("TIPO")]
        public string Tipo { get; set; }
    }
}
