using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProyectoRentACar.Models
{
    [Table("ESTADO")]
    public class Estado
    {
        [Key]
        [Column("ID")]
        public int IdEstado { get; set; }

        [Column("ESTADO")]
        public string EstadoDescripcion { get; set; }
    }
}
