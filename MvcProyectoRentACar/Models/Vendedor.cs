using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcProyectoRentACar.Models
{
    [Table("VENDEDOR")]
    public class Vendedor
    {
        [Key]
        [Column("ID")]
        public int IdVendedor { get; set; }

        // Relación con Usuario (uno a uno)
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [Column("NOMBREEMPRESA")]
        public string NombreEmpresa { get; set; }

        [Column("DIRECCION")]
        public string Direccion { get; set; }

        [Column("TELEFONO")]
        public string Telefono { get; set; }

        // Propiedad de navegación
        public virtual Usuario Usuario { get; set; }
    }
}
