using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcProyectoRentACar.Models
{
    [Table("COMPRADOR")]
    public class Comprador
    {
        [Key]
        [Column("ID")]
        public int IdComprador { get; set; }

        // Relación con Usuario (uno a uno)
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("APELLIDOS")]
        public string Apellidos { get; set; }


        [Column("DNI")]
        public string Dni { get; set; }

        [Column("CARNET")]
        public string Carnet { get; set; }

        [Column("TELEFONO")]
        public string Telefono { get; set; }

        [Column("FECHANACIMIENTO")]
        public DateTime FechaNacimiento { get; set; }

        // Propiedad de navegación
        public virtual Usuario Usuario { get; set; }
    }
}
