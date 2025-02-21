using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcProyectoRentACar.Models
{
    [Table("VISTA_COCHES")]  
    public class VistaCoche
    {
        [Key]  
        [Column("ID")]  
        public int IdCoche { get; set; }

        [Column("MARCA")]  
        public string Marca { get; set; }

        [Column("MODELO")]  
        public string Modelo { get; set; }

        [Column("IMAGEN")]  
        public string Imagen { get; set; }

        [Column("ASIENTOS")]  
        public int Asientos { get; set; }

        [Column("MARCHA")]  
        public string Marcha { get; set; }

        [Column("GAMA")]  
        public string Gama { get; set; }

        [Column("ESTADO")]  
        public string Estado { get; set; }

        [Column("KILOMETRAJE")]  
        public int Kilometraje { get; set; }

        [Column("PUERTAS")] 
        public int Puertas { get; set; }

        [Column("COMBUSTIBLE")] 
        public string Combustible { get; set; }

        [Column("PRECIOKILOMETROS")] 
        public decimal PrecioKilometros { get; set; }

        [Column("PRECIOILIMITADO")] 
        public decimal PrecioIlimitado { get; set; }
    }
}
