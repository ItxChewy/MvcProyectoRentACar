using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProyectoRentACar.Models
{
    [Table("ROL")]
    public class Rol
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("ROL")]
        public string Tipo { get; set; }
    }
}
