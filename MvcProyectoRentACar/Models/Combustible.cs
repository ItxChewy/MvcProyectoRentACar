using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProyectoRentACar.Models
{
    [Table("COMBUSTIBLE")]
    public class Combustible
    {
        [Key]
        [Column("ID")]
        public int IdCombustible { get; set; }

        [Column("TIPO")]
        public string Tipo { get; set; }
    }
}
