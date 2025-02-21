using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProyectoRentACar.Models
{
    [Table("GAMA")]
    public class Gama
    {
        [Key]
        [Column("ID")]
        public int IdGama { get; set; }

        [Column("NIVEL")]
        public string Nivel { get; set; }
    }
}
