using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProyectoRentACar.Models
{
    [Table("ESTADORESERVA")]
    public class EstadoReserva
    {
        [Key]
        [Column("ID")]
        public int IdEstadoReserva { get; set; }

        [Column("ESTADO")]
        public string EstadoDescripcion { get; set; }
    }
}
