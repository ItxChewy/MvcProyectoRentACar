using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProyectoRentACar.Models
{
    [Table("COCHE")]
    public class Coche
    {
        [Key]
        [Column("ID")]
        public int IdCoche { get; set; }

        [Column("MARCA")]
        public string Marca { get; set; }

        [Column("MODELO")]
        public string Modelo { get; set; }

        [Column("MATRICULA")]
        public string Matricula { get; set; }

        [Column("IMAGEN")]
        public string Imagen { get; set; }

        [Column("ASIENTOS")]
        public int Asientos { get; set; }

        [ForeignKey("Marchas")]
        [Column("IDMARCHAS")]
        public int IdMarchas { get; set; }

        [ForeignKey("Gama")]
        [Column("IDGAMA")]
        public int IdGama { get; set; }

        [ForeignKey("Estado")]
        [Column("IDESTADO")]
        public int IdEstadoCoche { get; set; }

        [Column("KILOMETRAJE")]
        public int Kilometraje { get; set; }

        [Column("PUERTAS")]
        public int Puertas { get; set; }

        [ForeignKey("Combustible")]
        [Column("IDCOMBUSTIBLE")]
        public int IdCombustible { get; set; }

        [ForeignKey("Vendedor")]
        [Column("IDVENDEDOR")]
        public int IdVendedor { get; set; }

        [Column("PRECIOKILOMETROS")]
        public decimal PrecioKilometros { get; set; }

        [Column("PRECIOILIMITADO")]
        public decimal PrecioIlimitado { get; set; }
    }
}
